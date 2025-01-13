import { LoadingButton } from "@mui/lab";
import {
  Autocomplete,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  FormControl,
  Grid,
  Stack,
  TextField,
} from "@mui/material";
import { GridRowsProp } from "@mui/x-data-grid";
import { DatePicker } from "@mui/x-date-pickers";
import { Form, Formik } from "formik";
import { debounce } from "lodash";
import { useSnackbar } from "notistack";
import { useCallback, useEffect, useState } from "react";
import * as Yup from "yup";
import {
  ClientResponse,
  OrderInsert,
  OrderProductInsert,
  ProductResponse,
} from "../../../api/order-service-api";
import { NumericFormatCustom } from "../../../components/NumericFormat";
import useAuth from "../../../hooks/useAuth";
import useSnackStatus from "../../../hooks/useSnackStatus";
import { CallApi, GetOSApiClient } from "../../../utils/apiHelpers";
import ProductsToOrderTable from "./ProductsToOrderTable";
import { openPdfInNewTab } from "../../../utils/pdfHelper";

const schema = Yup.object().shape({
  note: Yup.string().max(100, "Máximo 100 caracteres"),
});

const schemaAddProduct = Yup.object().shape({
  amount: Yup.string().required("Quantidade é obrigatório").nullable(),
  unitaryValue: Yup.string()
    .required("Valor unitário é obrigatório")
    .nullable(),
});

type ProductFormProps = {
  open: boolean;
  onClose(): void;
  onFinish(): void;
};

export default function OrderForm(props: ProductFormProps) {
  const { open, onClose, onFinish } = props;
  const { enqueueSnackbar } = useSnackbar();
  const { user } = useAuth();
  const { snackStatus, setSnackStatus, onCloseSnack } = useSnackStatus();
  const [clientsOptions, setClientsOptions] = useState<
    readonly ClientResponse[]
  >([]);
  const [searchClient, setSearchClient] = useState<string>("");
  const [isOpenClient, setIsOpenClient] = useState<boolean>(false);
  const [clientToAdd, setClientToAdd] = useState<ClientResponse | null>(null);

  const [productsOptions, setProductsOptions] = useState<
    readonly ProductResponse[]
  >([]);
  const [searchProduct, setSearchProduct] = useState<string>("");
  const [isOpenProduct, setIsOpenProduct] = useState<boolean>(false);
  const [isOpenAddProd, setIsOpenAddProd] = useState<boolean>(false);
  const [productToAdd, setProductToAdd] = useState<ProductResponse | null>(
    null
  );

  const [isLoading, setIsLoading] = useState<boolean>(false);

  const [rows, setRows] = useState([] as GridRowsProp);

  function onSubmit(values: any) {
    return new Promise<boolean>((resolve, reject) => {
      values.discount = values.discount === "" ? "0" : values.discount;
      var payload = OrderInsert.fromJS(values);
      payload.clientId = clientToAdd?.id;
      payload.products = rows.flatMap((item) => {
        return OrderProductInsert.fromJS({
          id: item["id"].length > 20 ? item["id"] : null,
          amount: item["amount"],
          description: item["description"],
          measure: item["measure"],
          unitaryValue: item["unitaryValue"],
        });
      });
      return CallApi(GetOSApiClient(user?.accessToken).addOrder(payload))
        .then((data) => {
          setSnackStatus({ message: "Adicionado", variant: "success" });
          openPdfInNewTab(data);
          onFinish();
          setRows([]);
          resolve(true);
        })
        .catch((err) => {
          setSnackStatus({ message: err, variant: "error" });
          values.discount = values.discount === "0" ? "" : values.discount;
          resolve(false);
        });
    });
  }

  // eslint-disable-next-line react-hooks/exhaustive-deps
  const getClientsDelayed = useCallback(
    debounce((text, callback) => {
      CallApi(
        GetOSApiClient(user?.accessToken).getClients(
          "Id, Name",
          undefined,
          `contains(toUpper(Name), toUpper('${text}'))`,
          "10",
          "0",
          undefined
        )
      ).then(callback);
    }, 200),
    []
  );
  useEffect(() => {
    setIsLoading(true);
    getClientsDelayed(searchClient, (filteredOptions: any) => {
      setClientsOptions(filteredOptions);
      setIsLoading(false);
    });
  }, [searchClient, getClientsDelayed]);

  // eslint-disable-next-line react-hooks/exhaustive-deps
  const getProductsDelayed = useCallback(
    debounce((text, callback) => {
      CallApi(
        GetOSApiClient(user?.accessToken).getProducts(
          "Id, Description, UnitaryValue, Measure",
          undefined,
          `contains(toUpper(Description), toUpper('${text}'))`,
          "10",
          "0",
          undefined
        )
      ).then(callback);
    }, 200),
    []
  );
  useEffect(() => {
    setIsLoading(true);
    getProductsDelayed(searchProduct, (filteredOptions: any) => {
      setProductsOptions(filteredOptions);
      setIsLoading(false);
    });
  }, [searchProduct, getProductsDelayed]);

  const defaultValues = {
    start: new Date(),
    finish: new Date(),
    note: "",
    discount: "",
  };

  const defaultValuesAddProduct = {
    amount: "",
    unitaryValue: productToAdd?.unitaryValue,
  };

  useEffect(() => {
    snackStatus.hasMessage &&
      enqueueSnackbar(snackStatus.message, {
        variant: snackStatus.variant,
        onClose: () => onCloseSnack(),
      });
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [snackStatus]);

  return (
    <>
      <Dialog
        open={open}
        onClose={() => {
          onClose();
          setRows([]);
        }}
        fullWidth
        maxWidth="lg"
      >
        <Formik
          validationSchema={schema}
          initialValues={defaultValues}
          onSubmit={onSubmit}
        >
          {({
            handleSubmit,
            getFieldProps,
            touched,
            errors,
            isSubmitting,
            values,
            setFieldValue,
          }) => (
            <Form autoComplete="off" noValidate onSubmit={handleSubmit}>
              <DialogTitle>Nova ordem de serviço</DialogTitle>
              <DialogContent>
                <Grid container sx={{ mt: 1 }} spacing={1}>
                  <Grid item md={4}>
                    <FormControl fullWidth>
                      <DatePicker
                        label="Data inicio"
                        slotProps={{
                          textField: {
                            size: "small",
                          },
                        }}
                        value={values.start}
                        onChange={(newValue) =>
                          setFieldValue("start", newValue)
                        }
                      />
                    </FormControl>
                  </Grid>
                  <Grid item md={4}>
                    <FormControl fullWidth>
                      <DatePicker
                        label="Data finalização"
                        slotProps={{
                          textField: {
                            size: "small",
                          },
                        }}
                        value={values.finish}
                        onChange={(newValue) =>
                          setFieldValue("finish", newValue)
                        }
                      />
                    </FormControl>
                  </Grid>
                  <Grid item md={4}>
                    <FormControl fullWidth>
                      <TextField
                        size="small"
                        label="Desconto"
                        {...getFieldProps("discount")}
                        error={Boolean(touched.discount && errors.discount)}
                        helperText={touched.discount && errors.discount}
                        InputProps={{
                          inputComponent: NumericFormatCustom as any,
                        }}
                      />
                    </FormControl>
                  </Grid>
                  <Grid item md={12}>
                    <FormControl fullWidth>
                      <TextField
                        multiline
                        rows={3}
                        label="Observação"
                        size="small"
                        {...getFieldProps("note")}
                        error={Boolean(touched.note && errors.note)}
                        helperText={touched.note && errors.note}
                      />
                    </FormControl>
                  </Grid>
                  <Grid item md={12}>
                    <FormControl fullWidth>
                      <Autocomplete
                        getOptionKey={(opt) => opt.id!}
                        size="small"
                        open={isOpenClient}
                        onOpen={() => setIsOpenClient(true)}
                        onClose={() => setIsOpenClient(false)}
                        onChange={(event, value) => {
                          setClientToAdd(value);
                        }}
                        onInputChange={(event, value) => {
                          setSearchClient(value);
                        }}
                        isOptionEqualToValue={(option, value) => {
                          return option.id === value.id;
                        }}
                        getOptionLabel={(option) => option.name!}
                        options={clientsOptions}
                        renderInput={(params) => (
                          <TextField
                            {...params}
                            label="Cliente"
                            helperText="* A ordem de serviço sem cliente, não ficará salva consulta futura"
                          />
                        )}
                      />
                    </FormControl>
                  </Grid>
                  <Grid item md={12}>
                    <FormControl fullWidth>
                      <Autocomplete
                        getOptionKey={(opt) => opt.id!}
                        size="small"
                        open={isOpenProduct}
                        onOpen={() => setIsOpenProduct(true)}
                        onClose={() => setIsOpenProduct(false)}
                        onChange={(event, value) => {
                          if (rows.some((x) => x.id === value?.id)) {
                            setSnackStatus({
                              message: "Produto selecionado já foi adicionado",
                              variant: "error",
                            });
                          } else {
                            setProductToAdd(value);
                            value && setIsOpenAddProd(true);
                          }
                        }}
                        value={productToAdd}
                        onInputChange={(event, value) => {
                          setSearchProduct(value);
                        }}
                        isOptionEqualToValue={(option, value) => {
                          return option.id === value.id;
                        }}
                        getOptionLabel={(option) => option.description!}
                        options={productsOptions}
                        loading={isLoading}
                        renderInput={(params) => (
                          <TextField {...params} label="Produto" />
                        )}
                      />
                    </FormControl>
                  </Grid>
                  <Grid item md={12}>
                    <ProductsToOrderTable rows={rows} setRows={setRows} />
                  </Grid>
                </Grid>
              </DialogContent>
              <DialogActions>
                <Button
                  variant="text"
                  onClick={() => {
                    onClose();
                    setRows([]);
                  }}
                >
                  Cancelar
                </Button>
                <LoadingButton
                  type="submit"
                  variant="contained"
                  loading={isSubmitting}
                >
                  Adicionar
                </LoadingButton>
              </DialogActions>
            </Form>
          )}
        </Formik>
      </Dialog>
      <Dialog
        open={isOpenAddProd}
        onClose={() => {
          setProductToAdd(null);
          setIsOpenAddProd(false);
        }}
      >
        <Formik
          enableReinitialize
          validationSchema={schemaAddProduct}
          initialValues={defaultValuesAddProduct}
          onSubmit={(values) => {
            setProductToAdd(null);
            setIsOpenAddProd(false);
            setRows((prevArray) => [
              ...prevArray,
              {
                id: productToAdd?.id,
                description: productToAdd?.description,
                unitaryValue: values.unitaryValue,
                amount: values.amount,
                measure: productToAdd?.measure,
                existing: true,
              },
            ]);
          }}
        >
          {({
            handleSubmit,
            getFieldProps,
            touched,
            errors,
            isSubmitting,
            values,
            setFieldValue,
          }) => (
            <Form autoComplete="off" noValidate onSubmit={handleSubmit}>
              <DialogTitle>Adicionar {productToAdd?.description}</DialogTitle>
              <DialogContent>
                <Stack sx={{ mt: 1 }} spacing={1}>
                  <FormControl fullWidth>
                    <TextField
                      size="small"
                      label="Valor unitário"
                      {...getFieldProps("unitaryValue")}
                      error={Boolean(
                        touched.unitaryValue && errors.unitaryValue
                      )}
                      helperText={touched.unitaryValue && errors.unitaryValue}
                      InputProps={{
                        inputComponent: NumericFormatCustom as any,
                      }}
                    />
                  </FormControl>
                  <FormControl fullWidth>
                    <TextField
                      size="small"
                      label="Quantidade"
                      {...getFieldProps("amount")}
                      error={Boolean(touched.amount && errors.amount)}
                      helperText={touched.amount && errors.amount}
                      InputProps={{
                        inputComponent: NumericFormatCustom as any,
                        inputProps: { removePrefix: true },
                      }}
                    />
                  </FormControl>
                </Stack>
              </DialogContent>
              <DialogActions>
                <Button
                  variant="text"
                  onClick={() => {
                    setProductToAdd(null);
                    setIsOpenAddProd(false);
                  }}
                >
                  Cancelar
                </Button>
                <Button type="submit" variant="contained">
                  Adicionar
                </Button>
              </DialogActions>
            </Form>
          )}
        </Formik>
      </Dialog>
    </>
  );
}
