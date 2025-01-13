import { LoadingButton } from "@mui/lab";
import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  FormControl,
  Stack,
  TextField,
} from "@mui/material";
import { TextFieldMasked } from "@vinicios-campera/kernel-react";
import { Form, Formik } from "formik";
import { useSnackbar } from "notistack";
import { useEffect } from "react";
import * as Yup from "yup";
import { ClientResponse } from "../../../api/order-service-api";
import useAuth from "../../../hooks/useAuth";
import useSnackStatus from "../../../hooks/useSnackStatus";
import { CallApi, GetOSApiClient } from "../../../utils/apiHelpers";

const schema = Yup.object().shape({
  name: Yup.string().required("Nome é obrigatório").nullable(),
  cellphone: Yup.string().required("Celular é obrigatório").nullable(),
});

type ClientFormProps = {
  client: ClientResponse | null;
  open: boolean;
  onClose(): void;
  onFinish(): void;
};

export default function ClientForm(props: ClientFormProps) {
  const { client, open, onClose, onFinish } = props;
  const isEditing = client;
  const { enqueueSnackbar } = useSnackbar();
  const { user } = useAuth();
  const { snackStatus, setSnackStatus, onCloseSnack } = useSnackStatus();

  function onSubmit(values: any) {
    return new Promise<boolean>((resolve, reject) => {
      return isEditing
        ? CallApi(GetOSApiClient(user?.accessToken).updateClient(values))
            .then((data) => {
              setSnackStatus({ message: "Atualizado", variant: "success" });
              onFinish();
              resolve(data);
            })
            .catch((err) => {
              setSnackStatus({ message: err, variant: "error" });
              resolve(false);
            })
        : CallApi(GetOSApiClient(user?.accessToken).addClient(values))
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
    id: client?.id,
    name: client?.name,
    document: client?.document,
    cep: client?.cep,
    cellphone: client?.cellphone,
    state: client?.state,
    city: client?.city,
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
        }) => (
          <Form autoComplete="off" noValidate onSubmit={handleSubmit}>
            <DialogTitle>
              {isEditing ? "Editar cliente" : "Novo cliente"}
            </DialogTitle>
            <DialogContent>
              <Stack spacing={1} sx={{ mt: 1 }}>
                <FormControl fullWidth>
                  <TextField
                    type="text"
                    label="Nome"
                    size="small"
                    {...getFieldProps("name")}
                    error={Boolean(touched.name && errors.name)}
                    helperText={touched.name && errors.name}
                    inputProps={{ style: { textTransform: "capitalize" } }}
                  />
                </FormControl>
                <FormControl fullWidth>
                  <TextFieldMasked
                    id="document"
                    size="small"
                    variant="outlined"
                    label={{ prop: "document", text: "CPF/CNPJ" }}
                    getFieldProps={() => getFieldProps("document")}
                    mask={[
                      { mask: "000.000.000-00" },
                      { mask: "00.000.000/0000-00" },
                    ]}
                    hasError={Boolean(touched.document && errors.document)}
                    error={
                      !!touched.document && errors.document
                        ? errors.document
                        : undefined
                    }
                  />
                </FormControl>
                <FormControl fullWidth>
                  <TextFieldMasked
                    id="cep"
                    size="small"
                    variant="outlined"
                    label={{ prop: "cep", text: "CEP" }}
                    getFieldProps={() => getFieldProps("cep")}
                    mask={[{ mask: "00000-000" }]}
                    hasError={Boolean(touched.cep && errors.cep)}
                    error={!!touched.cep && errors.cep ? errors.cep : undefined}
                  />
                </FormControl>
                <FormControl fullWidth>
                  <TextFieldMasked
                    id="state"
                    size="small"
                    variant="outlined"
                    label={{ prop: "state", text: "Estado" }}
                    getFieldProps={() => getFieldProps("state")}
                    mask={[{ mask: /^[a-zA-Z ]{0,2}$/ }]}
                    hasError={Boolean(touched.state && errors.state)}
                    error={
                      !!touched.state && errors.state ? errors.state : undefined
                    }
                    style={{ textTransform: "uppercase" }}
                  />
                </FormControl>
                <FormControl fullWidth>
                  <TextField
                    InputLabelProps={{ shrink: values.city ? true : false }}
                    type="text"
                    label="Cidade"
                    size="small"
                    {...getFieldProps("city")}
                    error={Boolean(touched.city && errors.city)}
                    helperText={touched.city && errors.city}
                    inputProps={{ style: { textTransform: "capitalize" } }}
                  />
                </FormControl>
                <FormControl fullWidth>
                  <TextFieldMasked
                    id="cellphone"
                    size="small"
                    variant="outlined"
                    label={{ prop: "cellphone", text: "Celular" }}
                    getFieldProps={() => getFieldProps("cellphone")}
                    mask={[
                      { mask: "(00)0000-0000" },
                      { mask: "(00)00000-0000" },
                    ]}
                    hasError={Boolean(touched.cellphone && errors.cellphone)}
                    error={
                      !!touched.cellphone && errors.cellphone
                        ? errors.cellphone
                        : undefined
                    }
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
