import {
  DashboardLayout,
  DashboardLayoutProps,
  Iconify,
  LandingPageLayout,
  LandingPageLayoutProps,
  LoadingScreen,
  LogoOnlyLayout,
  Page404,
  SettingsDrawerDescriptionsProps,
  ThemeSettings,
  getIcon,
} from "@vinicios-campera/kernel-react";
import { ElementType, Suspense, lazy } from "react";
import { Navigate, useLocation, useRoutes } from "react-router-dom";
import { Logo } from "../Logo";
import { allLangs } from "../contants";
import AuthGuard from "../guards/AuthGuard";
import GuestGuard from "../guards/GuestGuard";
import useAuth from "../hooks/useAuth";
import useLocales from "../locales/useLocales";
import { AUTH, AUTHOR, DASHBOARD, LANDING, ROOT } from "./paths";

const Loadable = (Component: ElementType) => (props: any) => {
  return (
    <Suspense fallback={<LoadingScreen logo={{ element: Logo() }} />}>
      <Component {...props} />
    </Suspense>
  );
};

const HomePage = Loadable(lazy(() => import("../pages/home/Home")));
const PolicyPrivacyPage = Loadable(
  lazy(() => import("../pages/home/PolicyPrivacy"))
);
const LoginPage = Loadable(lazy(() => import("../pages/auth/Login")));
const UserAccountPage = Loadable(
  lazy(() => import("../pages/dashboard/account/UserAccount"))
);
const ClientsPage = Loadable(
  lazy(() => import("../pages/dashboard/client/Clients"))
);
const ProductsPage = Loadable(
  lazy(() => import("../pages/dashboard/product/Products"))
);
const OrdersPage = Loadable(
  lazy(() => import("../pages/dashboard/order/Orders"))
);

export default function Router() {
  const { pathname } = useLocation();
  const { logout, userApi } = useAuth();
  const { translate, onChangeLang, currentLang } = useLocales();

  const landingPageValues: LandingPageLayoutProps = {
    logo: { element: Logo() },
    isHome: pathname === LANDING.root,
    copyRight: {
      description: translate("landing.copyRight.description").toString(),
      path: LANDING.root,
    },
    menuItems: [
      {
        title: translate("landing.menuItems.home").toString(),
        icon: <Iconify icon={"eva:home-fill"} />,
        path: LANDING.root,
      },
      {
        title: translate("landing.menuItems.policyPrivacy").toString(),
        icon: <Iconify icon={"eva:person-fill"} />,
        path: LANDING.privacyPolicy,
      },
    ],
    version: {
      description: "v.1.0.0",
      path: LANDING.root,
      target: "_self",
    },
    mainButton: {
      description: translate("landing.mainButton.description").toString(),
      path: AUTH.login,
    },
    footer: {
      description: translate("landing.footer.description").toString(),
      year: 2024,
      links: [
        {
          headline: translate(
            "landing.footer.links.moreInfo.headline"
          ).toString(),
          children: [
            {
              name: translate(
                "landing.footer.links.moreInfo.contact"
              ).toString(),
              href: `mailto:${AUTHOR.mail}`,
            },
          ],
        },
      ],
      social: {
        initialColor: true,
        simple: true,
        socialLinks: [
          {
            name: "Instagram",
            icon: "ant-design:instagram-filled",
            socialColor: "#E02D69",
            path: AUTHOR.instagram,
          },
          {
            name: "Linkedin",
            icon: "eva:linkedin-fill",
            socialColor: "#007EBB",
            path: AUTHOR.linkedin,
          },
        ],
      },
    },
  };

  const descriptionDrawer: SettingsDrawerDescriptionsProps = {
    contrast: translate("drawer.contrast").toString(),
    direction: translate("drawer.direction").toString(),
    fullscren: {
      textClose: translate("drawer.fullscren.textClose").toString(),
      textOpen: translate("drawer.fullscren.textOpen").toString(),
    },
    layout: translate("drawer.layout").toString(),
    mode: translate("drawer.mode").toString(),
    presets: translate("drawer.presets").toString(),
    stretch: translate("drawer.stretch").toString(),
    title: translate("drawer.title").toString(),
  };

  const dashboardPageValues: DashboardLayoutProps = {
    logo: { element: Logo() },
    account: {
      account: {
        email: userApi?.email!,
        displayName: userApi?.name!,
        photoUrl: userApi?.pictureUrl!,
        roles: userApi?.roles!,
        permissionTitle: translate(
          "dashboard.account.permissionTitle"
        ).toString(),
        to: ROOT,
      },
      menu: [
        {
          label: translate("dashboard.menu.profile.label").toString(),
          linkTo: DASHBOARD.profile,
        },
      ],
      logout: {
        title: translate("dashboard.logout.title").toString(),
        handleLogout: () => logout(),
      },
    },
    descriptionsSearchBar: {
      placeholder: translate(
        "dashboard.descriptionsSearchBar.placeholder"
      ).toString(),
      title: translate("dashboard.descriptionsSearchBar.title").toString(),
      prefixHelp: translate(
        "dashboard.descriptionsSearchBar.prefixHelp"
      ).toString(),
      sulfixHelp: translate(
        "dashboard.descriptionsSearchBar.sulfixHelp"
      ).toString(),
      tip: translate("dashboard.descriptionsSearchBar.tip").toString(),
    },
    header: {
      language: {
        currentLang: currentLang,
        languages: allLangs,
        onChangeLang: (newLang) => onChangeLang(newLang),
      },
      notification: {
        currentLang: currentLang,
        descriptions: {
          title: translate(
            "dashboard.notification.descriptions.title"
          ).toString(),
          subtitle: translate(
            "dashboard.notification.descriptions.subtitle"
          ).toString(),
          newTitle: translate(
            "dashboard.notification.descriptions.newTitle"
          ).toString(),
          beforeThatTitle: translate(
            "dashboard.notification.descriptions.beforeThatTitle"
          ).toString(),
          viewAllTitle: translate(
            "dashboard.notification.descriptions.viewAllTitle"
          ).toString(),
        },
        notifications: userApi?.notifications as {
          id: string;
          title: string;
          description: string;
          icon: string;
          createdAt: Date;
          isUnRead: boolean;
        }[],
        onClickNotification: (id) => console.log(id),
        onViewAllClick: () => console.log("onViewAllClick"),
      },
    },
    help: {
      helpTitle: translate("dashboard.help.helpTitle").toString(),
      helpDescription: translate("dashboard.help.helpDescription").toString(),
      helpButtonTitle: translate("dashboard.help.helpButtonTitle").toString(),
      helpPath: `mailto:${AUTHOR.mail}`,
    },
    navConfig: [
      {
        subheader: "Menu",
        items: [
          {
            role: "default",
            title: translate("dashboard.navConfig.profile.title").toString(),
            path: DASHBOARD.profile,
            icon: getIcon("ic_profile"),
            caption: translate(
              "dashboard.navConfig.profile.caption"
            ).toString(),
          },
          {
            role: "default",
            title: translate("dashboard.navConfig.clients.title").toString(),
            path: DASHBOARD.clients,
            icon: getIcon("ic_users"),
          },
          {
            role: "default",
            title: translate("dashboard.navConfig.products.title").toString(),
            path: DASHBOARD.products,
            icon: getIcon("ic_marcket"),
          },
          {
            role: "default",
            title: translate("dashboard.navConfig.orders.title").toString(),
            path: DASHBOARD.orders,
            icon: getIcon("ic_ticket"),
            caption: translate("dashboard.navConfig.orders.caption").toString(),
          },
        ],
      },
    ],
  };

  const notFoundValues = {
    pageTitle: translate("notFound.pageTitle").toString(),
    title: translate("notFound.title").toString(),
    subTitle: translate("notFound.subTitle").toString(),
    goToHomeTitle: translate("notFound.goToHomeTitle").toString(),
  };

  return useRoutes([
    {
      path: "*",
      element: <LogoOnlyLayout logo={{ element: Logo() }} />,
      children: [
        {
          path: "404",
          element: <Page404 {...notFoundValues} />,
        },
        { path: "*", element: <Navigate to={LANDING.page404} replace /> },
      ],
    },
    {
      path: LANDING.root,
      element: <LandingPageLayout {...landingPageValues} />,
      children: [
        {
          element: <HomePage />,
          index: true,
        },
      ],
    },
    {
      path: LANDING.privacyPolicy,
      element: <LandingPageLayout {...landingPageValues} />,
      children: [
        {
          element: <PolicyPrivacyPage />,
          index: true,
        },
      ],
    },
    {
      path: AUTH.root,
      children: [
        {
          path: AUTH.login,
          element: (
            <GuestGuard>
              <LoginPage />
            </GuestGuard>
          ),
        },
      ],
    },
    {
      path: DASHBOARD.root,
      element: (
        <ThemeSettings
          localization={currentLang.systemValue}
          descriptions={descriptionDrawer}
        >
          <AuthGuard>
            <DashboardLayout {...dashboardPageValues} />
          </AuthGuard>
        </ThemeSettings>
      ),
      children: [
        {
          path: DASHBOARD.profile,
          element: <UserAccountPage />,
          index: true,
        },
        {
          path: DASHBOARD.clients,
          element: <ClientsPage />,
          index: true,
        },
        {
          path: DASHBOARD.products,
          element: <ProductsPage />,
          index: true,
        },
        {
          path: DASHBOARD.orders,
          element: <OrdersPage />,
          index: true,
        },
      ],
    },
    { path: "*", element: <Navigate to={LANDING.page404} replace /> },
  ]);
}
