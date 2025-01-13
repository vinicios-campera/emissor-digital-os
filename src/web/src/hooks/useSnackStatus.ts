import { useState } from "react";

type UseSnackStatusProps = {
  message: string;
  variant: "default" | "error" | "success" | "warning" | "info";
  hasMessage: boolean;
};
export default function useSnackStatus() {
  const initialState: UseSnackStatusProps = {
    hasMessage: false,
    variant: "default",
    message: "",
  };
  const [snackStatus, setSnackStatus] =
    useState<UseSnackStatusProps>(initialState);

  return {
    snackStatus,
    setSnackStatus: (props: Omit<UseSnackStatusProps, "hasMessage">) =>
      setSnackStatus({
        hasMessage: true,
        message: props.message,
        variant: props.variant,
      }),
    onCloseSnack: () => setSnackStatus(initialState),
  };
}
