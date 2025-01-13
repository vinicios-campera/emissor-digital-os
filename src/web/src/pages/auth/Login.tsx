import { Box, Card, Container, Stack, Typography } from "@mui/material";
import { styled } from "@mui/material/styles";
import { Image, Page, useResponsive } from "@vinicios-campera/kernel-react";
import { Logo } from "../../Logo";
import LoginForm from "../../sections/auth/LoginForm";

const RootStyle = styled("div")(({ theme }) => ({
  [theme.breakpoints.up("md")]: {
    display: "flex",
  },
}));

const HeaderStyle = styled("header")(({ theme }) => ({
  top: 0,
  zIndex: 9,
  lineHeight: 0,
  width: "100%",
  display: "flex",
  alignItems: "center",
  position: "absolute",
  padding: theme.spacing(3),
  justifyContent: "space-between",
  [theme.breakpoints.up("md")]: {
    alignItems: "flex-start",
    padding: theme.spacing(7, 5, 0, 7),
  },
}));

const SectionStyle = styled(Card)(({ theme }) => ({
  width: "100%",
  maxWidth: 464,
  display: "flex",
  flexDirection: "column",
  justifyContent: "center",
  margin: theme.spacing(2, 0, 2, 2),
}));

const ContentStyle = styled("div")(({ theme }) => ({
  maxWidth: 480,
  margin: "auto",
  minHeight: "100vh",
  display: "flex",
  justifyContent: "center",
  flexDirection: "column",
  padding: theme.spacing(12, 0),
}));

export default function Login() {
  const mdUp = useResponsive("up", "md");

  return (
    <Page title="Login">
      <RootStyle>
        <HeaderStyle>
          <Box sx={{ width: 40, height: 40 }}>
            <Logo />
          </Box>
        </HeaderStyle>

        {mdUp && (
          <SectionStyle>
            <Typography variant="h3" sx={{ px: 5, mt: 10, mb: 5 }}>
              Olá, Bem vindo
            </Typography>
            <Image
              visibleByDefault
              disabledEffect
              src="/assets/illustrations/illustration_login.png"
              alt="login"
            />
          </SectionStyle>
        )}

        <Container maxWidth="sm">
          <ContentStyle>
            <Stack direction="row" alignItems="center" sx={{ mb: 5 }}>
              <Box sx={{ flexGrow: 1 }}>
                <Typography variant="h4" gutterBottom>
                  Fazer login
                </Typography>
                <Typography sx={{ color: "text.secondary" }}>
                  Digite suas credenciais abaixo
                </Typography>
              </Box>

              <Image
                disabledEffect
                src={"/assets/icons/ic_firebase.png"}
                sx={{ width: 32, height: 32 }}
              />
            </Stack>

            <LoginForm />
          </ContentStyle>
        </Container>
      </RootStyle>
    </Page>
  );
}
