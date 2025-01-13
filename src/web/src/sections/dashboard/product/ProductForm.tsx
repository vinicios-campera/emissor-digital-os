import { LoadingButton } from "@mui/lab";
import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  Stack,
  TextField,
} from "@mui/material";
import { Form, Formik } from "formik";
import { useSnackbar } from "notistack";
import { useEffect } from "react";
import * as Yup from "yup";
import { Measure, ProductResponse } from "../../../api/order-service-api";
import { NumericFormatCustom } from "../../../components/NumericFormat";
import useAuth from "../../../hooks/useAuth";
import useSnackStatus from "../../../hooks/useSnackStatus";
import { CallApi, GetOSApiClient } from "../../../utils/apiHelpers";

const schema = Yup.object().shape({
  description: Yup.string().required("Descrição é obrigatório").nullable(),
  unitaryValue: Yup.string()
    .required("Valor unitário é obrigatório")
    .nullable(),
});

type ProductFormProps = {
  product: ProductResponse | null;
  open: boolean;
  onClose(): void;
  onFinish(): void;
};

export default function ProductForm(props: ProductFormProps) {
  const { product, open, onClose, onFinish } = props;
  const isEditing = product;
  const { enqueueSnackbar } = useSnackbar();
  const { user } = useAuth();
  const { snackStatus, setSnackStatus, onCloseSnack } = useSnackStatus();

  function onSubmit(values: any) {
    return new Promise<boolean>((resolve, reject) => {
      return isEditing
        ? CallApi(GetOSApiClient(user?.accessToken).updateProduct(values))
            .then((data) => {
              setSnackStatus({ message: "Atualizado", variant: "success" });
              onFinish();
              resolve(data);
            })
            .catch((err) => {
              setSnackStatus({ message: err, variant: "error" });
              resolve(false);
            })
        : CallApi(GetOSApiClient(user?.accessToken).addProduct(values))
            .then((data) => {
              setSnackStatus({ message: "Adicionado", variant: "success" });
              onFinish();
              resolve(data);
            })
            .catch((err) => {
              setSnackStatus({ message: err, variant: "error" });
              resolve(false);
            });
    });
  }

  const defaultValues = {
    id: product?.id,
    description: product?.description,
    measure: product?.measure,
    unitaryValue: product?.unitaryValue,
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
    <Dialog open={open} onClose={onClose} fullWidth>
      <Formik
        enableReinitialize
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
            <DialogTitle>
              {isEditing ? "Editar produto" : "Novo produto"}
            </DialogTitle>
            <DialogContent>
              <Stack spacing={1} sx={{ mt: 1 }}>
                <FormControl fullWidth>
                  <TextField
                    type="text"
                    label="Descrição"
                    size="small"
                    {...getFieldProps("description")}
                    error={Boolean(touched.description && errors.description)}
                    helperText={touched.description && errors.description}
                    inputProps={{ style: { textTransform: "capitalize" } }}
                  />
                </FormControl>
                <FormControl fullWidth>
                  <InputLabel>Unidade de medida</InputLabel>
                  <Select
                    size="small"
                    value={values.measure}
                    defaultValue={Measure.Unit}
                    label="Unidade de medida"
                    onChange={(e) => {
                      setFieldValue("measure", e.target.value);
                    }}
                  >
                    <MenuItem value={Measure.Box}>Caixa</MenuItem>
                    <MenuItem value={Measure.Centimeters}>CM</MenuItem>
                    <MenuItem value={Measure.Kilometers}>KM</MenuItem>
                    <MenuItem value={Measure.Meters}>Metros</MenuItem>
                    <MenuItem value={Measure.Unit}>UN</MenuItem>
                  </Select>
                </FormControl>
                <FormControl fullWidth>
                  <TextField
                    size="small"
                    label="Valor unitário"
                    {...getFieldProps("unitaryValue")}
                    error={Boolean(touched.unitaryValue && errors.unitaryValue)}
                    helperText={touched.unitaryValue && errors.unitaryValue}
                    InputProps={{
                      inputComponent: NumericFormatCustom as any,
                    }}
                  />
                </FormControl>
              </Stack>
            </DialogContent>
            <DialogActions>
              <Button variant="text" onClick={onClose}>
                Cancelar
              </Button>
              <LoadingButton
                type="submit"
                variant="contained"
                loading={isSubmitting}
              >
                {isEditing ? "Editar" : "Adicionar"}
              </LoadingButton>
            </DialogActions>
          </Form>
        )}
      </Formik>
    </Dialog>
  );
}
