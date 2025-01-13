import { Box, Container, Tab, Tabs } from "@mui/material";
import {
  HeaderBreadcrumbs,
  Iconify,
  Page,
  useSettings,
  useTabs,
} from "@vinicios-campera/kernel-react";
import { capitalCase } from "change-case";
import { DASHBOARD } from "../../../routes/paths";
import AccountGeneral from "../../../sections/dashboard/account/AccountGeneral";

export default function UserAccount() {
  const { themeStretch } = useSettings();

  const { currentTab, onChangeTab } = useTabs("Geral");

  const ACCOUNT_TABS = [
    {
      value: "Geral",
      icon: <Iconify icon={"ic:round-account-box"} width={20} height={20} />,
      component: <AccountGeneral />,
    },
  ];

  return (
    <Page title="Usuário: Configurações da conta">
      <Container maxWidth={themeStretch ? false : "lg"}>
        <HeaderBreadcrumbs
          heading="Conta"
          links={[{ name: "Configurações da conta", href: DASHBOARD.root }]}
        />

        <Tabs
          allowScrollButtonsMobile
          variant="scrollable"
          scrollButtons="auto"
          value={currentTab}
          onChange={onChangeTab}
        >
          {ACCOUNT_TABS.map((tab) => (
            <Tab
              disableRipple
              key={tab.value}
              label={capitalCase(tab.value)}
              icon={tab.icon}
              value={tab.value}
            />
          ))}
        </Tabs>

        <Box sx={{ mb: 5 }} />

        {ACCOUNT_TABS.map((tab) => {
          const isMatched = tab.value === currentTab;
          return isMatched && <Box key={tab.value}>{tab.component}</Box>;
        })}
      </Container>
    </Page>
  );
}
