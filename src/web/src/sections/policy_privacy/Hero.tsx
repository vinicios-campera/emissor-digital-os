import { Box, Container, Typography } from "@mui/material";
import { styled } from "@mui/material/styles";
import {
  MotionContainer,
  TextAnimate,
  varFade,
} from "@vinicios-campera/kernel-react";
import { m } from "framer-motion";

const RootStyle = styled("div")(({ theme }) => ({
  backgroundSize: "cover",
  backgroundPosition: "center",
  backgroundImage:
    "url(/assets/overlay.svg), url(/images/policy_privacy.jpg)",
  padding: theme.spacing(10, 0),
  [theme.breakpoints.up("md")]: {
    height: 560,
    padding: 0,
  },
}));

const ContentStyle = styled("div")(({ theme }) => ({
  textAlign: "center",
  [theme.breakpoints.up("md")]: {
    textAlign: "left",
    position: "absolute",
    bottom: theme.spacing(10),
  },
}));

export default function Hero() {
  return (
    <RootStyle>
      <Container
        component={MotionContainer}
        sx={{ position: "relative", height: "100%" }}
      >
        <ContentStyle>
          <TextAnimate
            text="Política "
            sx={{ color: "primary.main" }}
            variants={varFade().inRight}
          />
          <br />
          <Box sx={{ display: "inline-flex", color: "common.white" }}>
            <TextAnimate text="de" sx={{ mr: 2 }} />
            <TextAnimate text="privacidade" />
          </Box>

          <m.div variants={varFade().inRight}>
            <Typography
              variant="h4"
              sx={{
                mt: 5,
                color: "common.white",
                fontWeight: "fontWeightMedium",
              }}
            >
              Emissor Digital de Ordem de Serviço (O.S.)
            </Typography>
          </m.div>
        </ContentStyle>
      </Container>
    </RootStyle>
  );
}
