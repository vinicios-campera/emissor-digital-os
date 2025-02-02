import {
  Box,
  Container,
  Link,
  Stack,
  StackProps,
  Tooltip,
  Typography,
} from "@mui/material";
import { styled } from "@mui/material/styles";
import { MotionContainer, varFade } from "@vinicios-campera/kernel-react";
import { m } from "framer-motion";
import { Link as RouterLink } from "react-router-dom";

const RootStyle = styled(m.div)(({ theme }) => ({
  position: "relative",
  backgroundColor: theme.palette.grey[400],
  [theme.breakpoints.up("md")]: {
    top: 0,
    left: 0,
    width: "100%",
    height: "100vh",
    display: "flex",
    position: "fixed",
    alignItems: "center",
  },
}));

const ContentStyle = styled((props: StackProps) => (
  <Stack spacing={5} {...props} />
))(({ theme }) => ({
  zIndex: 10,
  maxWidth: 520,
  margin: "auto",
  textAlign: "center",
  position: "relative",
  paddingTop: theme.spacing(15),
  paddingBottom: theme.spacing(15),
  [theme.breakpoints.up("md")]: {
    margin: "unset",
    textAlign: "left",
  },
}));

const HeroOverlayStyle = styled(m.img)({
  zIndex: 9,
  width: "100%",
  height: "100%",
  objectFit: "cover",
  position: "absolute",
});

const HeroImgStyle = styled(m.img)(({ theme }) => ({
  top: 0,
  right: 0,
  bottom: 0,
  zIndex: 8,
  width: "100%",
  margin: "auto",
  position: "absolute",
  [theme.breakpoints.up("lg")]: {
    right: "8%",
    width: "auto",
    height: "48vh",
  },
}));

export default function HomeHero() {
  return (
    <MotionContainer>
      <RootStyle>
        <HeroOverlayStyle
          alt="overlay"
          src="/assets/overlay.svg"
          variants={varFade().in}
        />

        <HeroImgStyle
          alt="hero"
          src="images/home_hero.png"
          variants={varFade().inUp}
        />

        <Container>
          <ContentStyle>
            <m.div variants={varFade().inRight}>
              <Typography variant="h1" sx={{ color: "common.white" }}>
                Emissor digital <br />
                de
                <Typography
                  component="span"
                  variant="h1"
                  sx={{ color: "primary.main" }}
                >
                  &nbsp;Ordem de Serviço
                </Typography>
              </Typography>
            </m.div>

            <m.div variants={varFade().inRight}>
              <Typography sx={{ color: "common.white" }}>
                Cansado de entregar orçamentos ou recibos de serviço em papel
                para seu cliente?. Com ele, você poderá cadastrar seus clientes,
                produtos/itens e o mais legal, criar recibos digitalmente.{" "}
                <br />
                <br />
                Isso não é incrível?
              </Typography>
            </m.div>

            <Stack spacing={2.5}>
              <m.div variants={varFade().inRight}>
                <Typography variant="overline" sx={{ color: "primary.light" }}>
                  Download .apk
                </Typography>
              </m.div>

              <Stack
                direction="row"
                spacing={1.5}
                justifyContent={{ xs: "center", md: "flex-start" }}
              >
                <Link
                  component={RouterLink}
                  variant="subtitle2"
                  to={`${process.env.REACT_APP_API_URL}/api/v1/helper/app`}
                  target="_blank"
                >
                  <Tooltip title="Android">
                    <m.img
                      variants={varFade().inRight}
                      width={50}
                      height={50}
                      alt="Android"
                      src={`/assets/android.svg`}
                    />
                  </Tooltip>
                </Link>
              </Stack>
            </Stack>
          </ContentStyle>
        </Container>
      </RootStyle>
      <Box sx={{ height: { md: "100vh" } }} />
    </MotionContainer>
  );
}
