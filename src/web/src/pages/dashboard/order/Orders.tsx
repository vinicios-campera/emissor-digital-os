import { Container } from "@mui/material";
import {
  HeaderBreadcrumbs,
  Page,
  useSettings,
} from "@vinicios-campera/kernel-react";
import { DASHBOARD } from "../../../routes/paths";
import OrdersTable from "../../../sections/dashboard/order/OrdersTable";

export default function Orders() {
  const { themeStretch } = useSettings();

  return (
    <Page title="Ordens">
      <Container maxWidth={themeStretch ? false : "lg"}>
        <HeaderBreadcrumbs
          heading="Ordens"
          links={[{ name: "Minhas ordens de serviço", href: DASHBOARD.orders }]}
        />
        <OrdersTable />
      </Container>
    </Page>
  );
}
