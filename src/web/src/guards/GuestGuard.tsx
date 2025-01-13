import { LoadingScreen } from "@vinicios-campera/kernel-react";
import { ReactNode } from "react";
import { Navigate } from "react-router-dom";
import { Logo } from "../Logo";
import useAuth from "../hooks/useAuth";
import { DASHBOARD } from "../routes/paths";

type GuestGuardProps = {
  children: ReactNode;
};

export default function GuestGuard({ children }: GuestGuardProps) {
  const { isAuthenticated, isInitialized } = useAuth();

  if (isAuthenticated) {
    return <Navigate to={DASHBOARD.root} />;
  }

  if (!isInitialized) {
    return <LoadingScreen logo={{ element: Logo() }} />;
  }

  return <>{children}</>;
}
