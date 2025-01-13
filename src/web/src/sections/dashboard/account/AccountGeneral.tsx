import { LoadingButton } from "@mui/lab";
import {
  Card,
  FormControl,
  FormControlLabel,
  Grid,
  Stack,
  Switch,
  TextField,
  Typography,
} from "@mui/material";
import {
  CustomFile,
  InputUploadAvatar,
  TextFieldMasked,
  fData,
} from "@vinicios-campera/kernel-react";
import { Form, Formik } from "formik";
import { useSnackbar } from "notistack";
import { useEffect, useState } from "react";
import * as Yup from "yup";
import { UserUpdate } from "../../../api/order-service-api";
import useAuth from "../../../hooks/useAuth";
import useSnackStatus from "../../../hooks/useSnackStatus";
import { CallApi, GetOSApiClient } from "../../../utils/apiHelpers";

export default function AccountGeneral() {
  const { enqueueSnackbar } = useSnackbar();
  const { user, userApi } = useAuth();

  const [photo, setPhoto] = useState<CustomFile | string | null>(null);
  const [photoInOrder, setPhotoInOrder] = useState<boolean>(
    userApi?.addPictureInOrder!
  );
  const { snackStatus, setSnackStatus, onCloseSnack } = useSnackStatus();

  const schema = Yup.object().shape({
    name: Yup.string().required("Apelido é obrigatório").nullable(),
    nameFull: Yup.string().required("Nome completo é obrigatório").nullable(),
  });

  const defaultValues: Omit<
    UserUpdate,
    "init" | "toJSON" | "picture" | "addPictureInOrder"
  > = {
    name: userApi?.name,
    nameFull: userApi?.nameFull,
    address: userApi?.address,
    cellphone: userApi?.cellphone,
    city: userApi?.city,
    document: userApi?.document,
    state: userApi?.state,
    telephone: userApi?.telephone,
    id: userApi?.id,
  };

  useEffect(() => {
    userApi?.pictureUrl && setPhoto(userApi?.pictureUrl);
  }, [userApi]);

  useEffect(() => {
    snackStatus.hasMessage &&
      enqueueSnackbar(snackStatus.message, {
        variant: snackStatus.variant,
        onClose: () => onCloseSnack(),
      });
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [snackStatus]);

  function onSubmit(values: Omit<UserUpdate, "init" | "toJSON">) {
    return new Promise<boolean>(function (resolve, reject) {
      const sendRequest = (payload: UserUpdate) =>
        CallApi(GetOSApiClient(user?.accessToken).updateUser(payload))
          .then((data) => {
            setSnackStatus({
              message: "Atualizado",
              variant: "success",
            });
            resolve(data);
          })
          .catch((err) => {
            setSnackStatus({ message: err, variant: "error" });
            resolve(false);
          });

      var payload = UserUpdate.fromJS(values);
      payload.addPictureInOrder = photoInOrder;
      if (photo instanceof File) {
        const reader = new FileReader();
        reader.onloadend = () => {
          let { result } = reader;
          let index = (result as string).indexOf("base64") + 7;
          let data = (result as string).slice(index);
          payload.picture = data;
          return sendRequest(payload);
        };
        reader.readAsDataURL(photo);
      } else {
        return sendRequest(payload);
      }
    });
  }

  return (
    <Grid container spacing={3}>
      <Grid item xs={12} md={4}>
        <Card sx={{ py: 4, px: 3, textAlign: "center" }}>
          <InputUploadAvatar
            file={photo}
            name="photoURL"
            maxSize={3000000}
            onDrop={(acceptedFiles: File[]) => {
              const file = acceptedFiles[0];
              if (file) {
                setPhoto(
                  Object.assign(file, {
                    preview: URL.createObjectURL(file),
                  })
                );
              }
            }}
            error={{ hasError: photo === null, message: "Error" }}
            helperText={
              <Typography
                variant="caption"
                sx={{
                  mt: 2,
                  mx: "auto",
                  display: "block",
                  textAlign: "center",
                  color: "text.secondary",
                }}
              >
                Permitido imagens *.jpeg, *.jpg
                <br /> tamanho máximo {fData(3000000)}
              </Typography>
            }
          />
          <FormControlLabel
            label="Anexar sua foto na O.S.?"
            control={
              <Switch
                size="small"
                checked={photoInOrder}
                onChange={(e) => setPhotoInOrder(e.target.checked)}
              />
            }
          />
        </Card>
      </Grid>

      <Grid item xs={12} md={8}>
        <Card sx={{ p: 3 }}>
          <Formik
            enableReinitialize
            validationSchema={schema}
            initialValues={defaultValues}
            onSubmit={onSubmit}
          >
            {({
              handleSubmit,
              isSubmitting,
              status,
              setStatus,
              getFieldProps,
              touched,
              errors,
              values,
            }) => (
              <Form autoComplete="off" noValidate onSubmit={handleSubmit}>
                <Grid container spacing={2}>
                  <Grid item xs={6} md={6}>
                    <FormControl fullWidth>
                      <TextField
                        InputLabelProps={{ shrink: values.name ? true : false }}
                        type="text"
                        label="Apelido"
                        size="small"
                        {...getFieldProps("name")}
                        error={Boolean(touched.name && errors.name)}
                        helperText={touched.name && errors.name}
                        inputProps={{ style: { textTransform: "capitalize" } }}
                      />
                    </FormControl>
                  </Grid>
                  <Grid item xs={6} md={6}>
                    <FormControl fullWidth>
                      <TextField
                        InputLabelProps={{
                          shrink: values.nameFull ? true : false,
                        }}
                        type="text"
                        label="Nome completo"
                        size="small"
                        {...getFieldProps("nameFull")}
                        error={Boolean(touched.nameFull && errors.nameFull)}
                        helperText={touched.nameFull && errors.nameFull}
                        inputProps={{ style: { textTransform: "capitalize" } }}
                      />
                    </FormControl>
                  </Grid>
                  <Grid item xs={6} md={6}>
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
                  </Grid>
                  <Grid item xs={6} md={6}>
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
                        hasError={Boolean(
                          touched.cellphone && errors.cellphone
                        )}
                        error={
                          !!touched.cellphone && errors.cellphone
                            ? errors.cellphone
                            : undefined
                        }
                      />
                    </FormControl>
                  </Grid>
                  <Grid item xs={6} md={6}>
                    <FormControl fullWidth>
                      <TextFieldMasked
                        id="telephone"
                        size="small"
                        variant="outlined"
                        label={{ prop: "telephone", text: "Telefone fixo" }}
                        getFieldProps={() => getFieldProps("telephone")}
                        mask={[{ mask: "(00)0000-0000" }]}
                        hasError={Boolean(
                          touched.telephone && errors.telephone
                        )}
                        error={
                          !!touched.telephone && errors.telephone
                            ? errors.telephone
                            : undefined
                        }
                      />
                    </FormControl>
                  </Grid>
                  <Grid item xs={6} md={6}>
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
                          !!touched.state && errors.state
                            ? errors.state
                            : undefined
                        }
                        style={{ textTransform: "uppercase" }}
                      />
                    </FormControl>
                  </Grid>
                  <Grid item xs={6} md={6}>
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
                  </Grid>
                  <Grid item xs={6} md={6}>
                    <FormControl fullWidth>
                      <TextField
                        InputLabelProps={{
                          shrink: values.address ? true : false,
                        }}
                        type="text"
                        label="Endereço"
                        size="small"
                        {...getFieldProps("address")}
                        error={Boolean(touched.address && errors.address)}
                        helperText={touched.address && errors.address}
                        inputProps={{ style: { textTransform: "capitalize" } }}
                      />
                    </FormControl>
                  </Grid>
                </Grid>
                <Stack spacing={3} alignItems="flex-end" sx={{ mt: 3 }}>
                  <LoadingButton
                    type="submit"
                    variant="contained"
                    loading={isSubmitting}
                  >
                    Salvar
                  </LoadingButton>
                </Stack>
              </Form>
            )}
          </Formik>
        </Card>
      </Grid>
    </Grid>
  );
}
