import { useState, useCallback } from "react";

export type ModalState = { [key: string]: boolean };

export const useModalState = (initialState: ModalState) => {
  const [modals, setModals] = useState<ModalState>(initialState);

  const openModal = useCallback((modalName: string) => {
    setModals((modals) => ({ ...modals, [modalName]: true }));
  }, []);

  const closeModal = useCallback((modalName: string) => {
    setModals((modals) => ({ ...modals, [modalName]: false }));
  }, []);

  return { modals, openModal, closeModal };
};
