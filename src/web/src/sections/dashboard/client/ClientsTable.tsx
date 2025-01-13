import AddIcon from "@mui/icons-material/Add";
import DeleteIcon from "@mui/icons-material/Delete";
import EditIcon from "@mui/icons-material/Edit";
import { Button } from "@mui/material";
import {
  DataGrid,
  GridActionsCellItem,
  GridColDef,
  GridFilterModel,
  GridPaginationModel,
  GridRenderCellParams,
  GridRowParams,
  GridSlots,
  GridSortModel,
  GridToolbarColumnsButton,
  GridToolbarContainer,
  GridToolbarFilterButton,
  GridValueOptionsParams,
  useGridApiRef,
} from "@mui/x-data-grid";
import { ptBR } from "@mui/x-data-grid/locales";
import { useSnackbar } from "notistack";
import { useEffect, useState } from "react";
import { ClientResponse, Person } from "../../../api/order-service-api";
import DialogDelete from "../../../components/DialogDelete";
import useAuth from "../../../hooks/useAuth";
import { useModalState } from "../../../hooks/useModalState";
import useSnackStatus from "../../../hooks/useSnackStatus";
import { CallApi, GetOSApiClient } from "../../../utils/apiHelpers";
import { getFilter } from "../../filterHelper";
import ClientForm from "./ClientForm";

export default function ClientsTable() {
  const { enqueueSnackbar } = useSnackbar();
  const apiRef = useGridApiRef();
  const { user } = useAuth();

  const initialStates = {
    clients: new Array<ClientResponse>(),
    pagination: {
      page: 0,
      pageSize: 10,
    },
    sort: [
      {
        field: "inserted",
        sort: "asc",
      },
    ] as GridSortModel,
  };

  const [clients, setClients] = useState<Array<ClientResponse>>(
    initialStates.clients
  );

  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [pagination, setPagination] = useState<GridPaginationModel>(
    initialStates.pagination
  );

  const [filter, setFilter] = useState<GridFilterModel>({} as GridFilterModel);
  const [sort, setSort] = useState<GridSortModel>(initialStates.sort);

  const { modals, openModal, closeModal } = useModalState({
    manage: false,
    delete: false,
  });

  const [currentClient, setCurrentClient] = useState<ClientResponse | null>(
    null
  );

  const { snackStatus, setSnackStatus, onCloseSnack } = useSnackStatus();

  function getData() {
    return new Promise<boolean>((resolve, reject) => {
      setIsLoading(true);
      let _sort = sort.length === 0 ? initialStates.sort : sort;
      return CallApi(
        GetOSApiClient(user?.accessToken).getClients(
          undefined,
          undefined,
          getFilter(filter),
          pagination.pageSize.toString(),
          (pagination.pageSize * pagination.page).toString(),
          _sort.map((element) => `${element.field} ${element.sort}`).join()
        )
      )
        .then((data) => {
          setClients(data);
          resolve(true);
        })
        .catch((err) => {
          setSnackStatus({ message: err, variant: "error" });
          resolve(false);
        })
        .finally(() => setIsLoading(false));
    });
  }

  function onDelete() {
    return new Promise<boolean>((resolve, reject) => {
      return CallApi(
        GetOSApiClient(user?.accessToken).deleteClient(currentClient?.id!)
      )
        .then((data) => {
          setSnackStatus({ message: "Removido", variant: "success" });
          getData();
          resolve(data);
        })
        .catch((err) => {
          setSnackStatus({ message: err, variant: "error" });
          resolve(false);
        });
    });
  }

  useEffect(() => {
    snackStatus.hasMessage &&
      enqueueSnackbar(snackStatus.message, {
        variant: snackStatus.variant,
        onClose: () => onCloseSnack(),
      });
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [snackStatus]);

  useEffect(() => {
    getData();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [pagination, filter, sort]);

  const columns: GridColDef[] = [
    { field: "name", headerName: "Nome", flex: 1 },
    { field: "document", headerName: "Documento", flex: 0.7 },
    { field: "state", headerName: "UF", flex: 0.2 },
    { field: "city", headerName: "Cidade", flex: 0.9 },
    { field: "cep", headerName: "CEP", filterable: false, flex: 0.5 },
    { field: "cellphone", headerName: "Celular", filterable: false, flex: 0.6 },
    {
      field: "type",
      headerName: "Tipo",
      type: "singleSelect",
      valueOptions: (params: GridValueOptionsParams<ClientResponse>) => {
        return [Person.Legal, Person.Physical, Person.Unknown];
      },
      renderCell: (params: GridRenderCellParams<ClientResponse, any>) => {
        switch (params.row.type) {
          case Person.Physical:
            return "Física";
          case Person.Legal:
            return "Jurídica";
          default:
            return "Desconhecido";
        }
      },
      flex: 0.4,
    },
    {
      field: "inserted",
      headerName: "Data",
      type: "date",
      filterable: false,
      flex: 0.5,
    },
    {
      field: "actions",
      type: "actions",
      headerName: "Opções",
      cellClassName: "actions",
      getActions: (params: GridRowParams<ClientResponse>) => {
        const { row } = params;
        return [
          <GridActionsCellItem
            icon={<EditIcon />}
            label="Edit"
            className="textPrimary"
            onClick={() => {
              setCurrentClient(row);
              openModal("manage");
            }}
          />,
          <GridActionsCellItem
            icon={<DeleteIcon />}
            label="Delete"
            onClick={() => {
              setCurrentClient(row);
              openModal("delete");
            }}
          />,
        ];
      },
      minWidth: 100,
    },
  ];

  function EditToolbar() {
    return (
      <GridToolbarContainer sx={{ padding: 1 }}>
        <GridToolbarFilterButton />
        <GridToolbarColumnsButton />
        <Button
          startIcon={<AddIcon />}
          onClick={() => {
            setCurrentClient(null);
            openModal("manage");
          }}
        >
          Novo cliente
        </Button>
      </GridToolbarContainer>
    );
  }

  return (
    <>
      <DataGrid
        disableRowSelectionOnClick
        localeText={ptBR.components.MuiDataGrid.defaultProps.localeText}
        apiRef={apiRef}
        autoHeight
        density="compact"
        rows={clients}
        columns={columns}
        slots={{
          toolbar: EditToolbar as GridSlots["toolbar"],
        }}
        filterMode="server"
        loading={isLoading}
        pageSizeOptions={[5, 10, 25, 50]}
        paginationModel={pagination}
        initialState={{ pagination: { ...pagination, rowCount: -1 } }}
        paginationMode="server"
        rowCount={-1}
        onPaginationModelChange={setPagination}
        sortModel={sort}
        onFilterModelChange={setFilter}
        onSortModelChange={setSort}
      />
      <ClientForm
        open={modals.manage}
        onClose={() => closeModal("manage")}
        onFinish={() => {
          closeModal("manage");
          getData();
        }}
        client={currentClient}
      />
      <DialogDelete
        open={modals.delete}
        title="Exclusão"
        description={
          <>
            Deseja realmente excluir o cliente{" "}
            <strong>{currentClient?.name}</strong>?
          </>
        }
        onClose={() => closeModal("delete")}
        onAfirmative={() => {
          closeModal("delete");
          onDelete();
        }}
      />
    </>
  );
}
