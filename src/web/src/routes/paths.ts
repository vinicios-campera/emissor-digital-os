function path(root: string, sublink: string) {
  return `${root}${sublink}`;
}

export const AUTHOR = {
  mail: "vinicioscampera@gmail.com",
  instagram: "https://www.instagram.com/vinicioscampera",
  linkedin: "https://www.linkedin.com/in/vinicios-bryam-campera",
};

export const ROOT = "/";

export const LANDING = {
  root: ROOT,
  privacyPolicy: "/privacy-policy",
  page404: "/404",
};

const ROOTS_AUTH = "/auth";
export const AUTH = {
  root: ROOTS_AUTH,
  login: path(ROOTS_AUTH, "/login"),
};

const ROOTS_DASHBOARD = "/dashboard";
export const DASHBOARD = {
  root: ROOTS_DASHBOARD,
  profile: path(ROOTS_DASHBOARD, "/profile"),
  clients: path(ROOTS_DASHBOARD, "/clients"),
  products: path(ROOTS_DASHBOARD, "/products"),
  orders: path(ROOTS_DASHBOARD, "/orders"),
};
