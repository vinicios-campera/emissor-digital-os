import { styled } from "@mui/material/styles";
import { Page } from "@vinicios-campera/kernel-react";
import Content from "../../sections/policy_privacy/Content";
import Hero from "../../sections/policy_privacy/Hero";

const RootStyle = styled("div")(({ theme }) => ({
  paddingTop: theme.spacing(8),
  [theme.breakpoints.up("md")]: {
    paddingTop: theme.spacing(11),
  },
}));

export default function PolicyPrivacy() {
  return (
    <Page title="O.S. | Politica de Privacidade">
      <RootStyle>
        <Hero />
        <Content />
      </RootStyle>
    </Page>
  );
}
