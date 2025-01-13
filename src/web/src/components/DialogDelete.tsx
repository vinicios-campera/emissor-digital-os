import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
} from "@mui/material";
import { ReactNode } from "react";

type DialogDeleteProps = {
  title: string;
  description: string | ReactNode;
  open: boolean;
  onClose(): void;
  onAfirmative(): void;
};

export default function DialogDelete(props: DialogDeleteProps) {
  const { title, description, open, onClose, onAfirmative } = props;
  return (
    <Dialog open={open} keepMounted onClose={onClose}>
      <DialogTitle>{title}</DialogTitle>
      <DialogContent>
        <DialogContentText>{description}</DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button color="inherit" variant="text" onClick={onClose}>
          Cancelar
        </Button>
        <Button color="error" variant="contained" onClick={onAfirmative}>
          Confirmar
        </Button>
      </DialogActions>
    </Dialog>
  );
}
