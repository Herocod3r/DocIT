import loadable from "loadable-components";
// import Loading from './Loading';

export const signIn = loadable(() => import("../../signIn"));
export const forgetPassword = loadable(() =>
  import("../../signIn/forgetPassword")
);
export const createAccount = loadable(() => import("../../createAccount"));

// export const Dashboard = loadable(() => import("../../dashboard"));

export const NotFoundPage = loadable(() => import("../../notFound"));
