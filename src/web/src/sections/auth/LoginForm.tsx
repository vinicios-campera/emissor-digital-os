import { Visibility, VisibilityOff } from "@mui/icons-material";
import { LoadingButton } from "@mui/lab";
import {
  Button,
  Checkbox,
  FormControl,
  FormControlLabel,
  FormHelperText,
  IconButton,
  InputAdornment,
  InputLabel,
  Link,
  OutlinedInput,
  Stack,
  TextField,
} from "@mui/material";
import { Iconify } from "@vinicios-campera/kernel-react";
import { Form, Formik } from "formik";
import { useSnackbar } from "notistack";
import { useEffect, useState } from "react";
import { Link as RouterLink } from "react-router-dom";
import * as Yup from "yup";
import useAuth from "../../hooks/useAuth";
import useSnackStatus from "../../hooks/useSnackStatus";

export default function LoginForm() {
  const { login, loginGoogle } = useAuth();

  const [showPassword, setShowPassword] = useState(false);

  const { snackStatus, setSnackStatus, onCloseSnack } = useSnackStatus();

  const { enqueueSnackbar } = useSnackbar();

  const defaultValues = {
    email: localStorage.getItem("OS_email"),
    password: localStorage.getItem("OS_password"),
    remember:
      localStorage.getItem("OS_email") !== null &&
      localStorage.getItem("OS_password") !== null,
  };

  const schema = Yup.object().shape({
    email: Yup.string()
      .email("Digite um email válido")
      .required("Email obrigatório"),
    password: Yup.string().required("Senha obrigatória"),
  });

  useEffect(() => {
    snackStatus.hasMessage &&
      enqueueSnackbar(snackStatus.message, {
        variant: snackStatus.variant,
        onClose: () => onCloseSnack(),
      });
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [snackStatus]);

  return (
    <Formik
      validationSchema={schema}
      initialValues={defaultValues}
      onSubmit={(values) => {
        if (values.remember) {
          localStorage.setItem("OS_email", values.email!);
          localStorage.setItem("OS_password", values.password!);
        } else {
          localStorage.removeItem("OS_email");
          localStorage.removeItem("OS_password");
        }
        return new Promise<boolean>((resolve, reject) => {
          login(values.email!, values.password!)
            .then(() => resolve(true))
            .catch((err) => {
              setSnackStatus({ message: err.code, variant: "error" });
              resolve(false);
            });
        });
      }}
    >
      {({
        handleSubmit,
        isSubmitting,
        setFieldValue,
        values,
        initialValues,
        getFieldProps,
        touched,
        errors,
      }) => (
        <Form
          autoComplete="off"
          noValidate
          onSubmit={handleSubmit}
          style={{ marginTop: 15 }}
        >
          <Stack spacing={1}>
            <FormControl fullWidth>
              <TextField
                type="text"
                label="Email"
                autoComplete="username"
                {...getFieldProps("email")}
                error={Boolean(touched.email && errors.email)}
                helperText={touched.email && errors.email}
              />
            </FormControl>
            <FormControl fullWidth>
              <InputLabel htmlFor="outlined-adornment-password">
                Senha
              </InputLabel>
              <OutlinedInput
                id="outlined-adornment-password"
                type={showPassword ? "text" : "password"}
                label="Senha"
                autoComplete="current-password"
                {...getFieldProps("password")}
                error={Boolean(touched.password && errors.password)}
                endAdornment={
                  <InputAdornment position="end">
                    <IconButton
                      aria-label="toggle password visibility"
                      onClick={() => setShowPassword(!showPassword)}
                      edge="end"
                    >
                      {showPassword ? <VisibilityOff /> : <Visibility />}
                    </IconButton>
                  </InputAdornment>
                }
              />
              {!!touched.password && (
                <FormHelperText error id="accountId-error">
                  {errors.password}
                </FormHelperText>
              )}
            </FormControl>
          </Stack>
          <Stack
            direction="row"
            alignItems="center"
            justifyContent="space-between"
            sx={{ my: 2 }}
          >
            <FormControlLabel
              control={
                <Checkbox
                  checked={values.remember}
                  onChange={(e, checked) => setFieldValue("remember", checked)}
                />
              }
              label="Lembre-me"
            />
            <Link
              component={RouterLink}
              variant="subtitle2"
              to="/resetPassword"
            >
              Esqueci minha senha?
            </Link>
          </Stack>

          <Stack spacing={1} direction="column">
            <LoadingButton
              fullWidth
              type="submit"
              variant="contained"
              loading={isSubmitting}
            >
              Login
            </LoadingButton>
            <Button
              color="error"
              variant="text"
              startIcon={<Iconify icon="eva:google-fill" />}
              onClick={loginGoogle}
            >
              Entrar com google
            </Button>
          </Stack>
        </Form>
      )}
    </Formik>
  );
}
