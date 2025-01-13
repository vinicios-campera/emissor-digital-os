import { Container } from "@mui/material";
import {
  HeaderBreadcrumbs,
  Page,
  useSettings,
} from "@vinicios-campera/kernel-react";
import { DASHBOARD } from "../../../routes/paths";
import ProductsTable from "../../../sections/dashboard/product/ProductsTable";

export default function Products() {
  const { themeStretch } = useSettings();

  return (
    <Page title="Produtos">
      <Container maxWidth={themeStretch ? false : "lg"}>
        <HeaderBreadcrumbs
          heading="Produtos"
          links={[{ name: "Meus produtos", href: DASHBOARD.products }]}
        />
        <ProductsTable />
      </Container>
    </Page>
  );
}
