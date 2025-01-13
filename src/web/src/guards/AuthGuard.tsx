import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
} from "@mui/material";
import { LoadingScreen } from "@vinicios-campera/kernel-react";
import { ReactNode, useState } from "react";
import { Navigate, useLocation } from "react-router-dom";
import { Logo } from "../Logo";
import useAuth from "../hooks/useAuth";
import Login from "../pages/auth/Login";
import { LANDING } from "../routes/paths";
import { CallApi, GetOSApiClient } from "../utils/apiHelpers";

type AuthGuardProps = {
  children: ReactNode;
};

export default function AuthGuard({ children }: AuthGuardProps) {
  const { isAuthenticated, isInitialized, user, userApi, logout } = useAuth();

  const { pathname } = useLocation();

  const [requestedLocation, setRequestedLocation] = useState<string | null>(
    null
  );

  const [open, setOpen] = useState<boolean>(true);

  if (!isInitialized) {
    return <LoadingScreen logo={{ element: Logo() }} />;
  }

  if (!isAuthenticated) {
    if (pathname !== requestedLocation) {
      setRequestedLocation(pathname);
    }
    return <Login />;
  }

  if (requestedLocation && pathname !== requestedLocation) {
    setRequestedLocation(null);
    return <Navigate to={requestedLocation} />;
  }

  if (isAuthenticated && !userApi?.privacyPolicyAccepted) {
    return (
      <Dialog open={open}>
        <DialogTitle>
          Você ainda não aceitou a politica de privacidade
        </DialogTitle>
        <DialogContent>
          <DialogContentText>
            Para seguir, você precisa aceitar a{" "}
            <a href={LANDING.privacyPolicy} target="_blank" rel="noreferrer">
              politica de privacidade
            </a>{" "}
            do Emissor Digital de Ordem de Serviço.
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button
            color="error"
            onClick={() => {
              setOpen(false);
              logout();
            }}
          >
            Recusar
          </Button>
          <Button
            color="success"
            autoFocus
            onClick={async () => {
              setOpen(false);
              await CallApi(
                GetOSApiClient(user?.accessToken).acceptPrivacyPolicy()
              ).then(() => logout());
            }}
          >
            Aceitar
          </Button>
        </DialogActions>
      </Dialog>
    );
  }

  return <>{children}</>;
}
