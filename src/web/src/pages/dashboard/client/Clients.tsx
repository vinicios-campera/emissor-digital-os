import { Container } from "@mui/material";
import {
  HeaderBreadcrumbs,
  Page,
  useSettings,
} from "@vinicios-campera/kernel-react";
import { DASHBOARD } from "../../../routes/paths";
import ClientsTable from "../../../sections/dashboard/client/ClientsTable";

export default function Clients() {
  const { themeStretch } = useSettings();

  return (
    <Page title="Clientes">
      <Container maxWidth={themeStretch ? false : "lg"}>
        <HeaderBreadcrumbs
          heading="Clientes"
          links={[{ name: "Meus clientes", href: DASHBOARD.clients }]}
        />
        <ClientsTable />
      </Container>
    </Page>
  );
}
