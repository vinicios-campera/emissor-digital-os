import AddIcon from "@mui/icons-material/Add";
import AttachMoneyIcon from "@mui/icons-material/AttachMoney";
import DeleteIcon from "@mui/icons-material/Delete";
import MoneyOffIcon from "@mui/icons-material/MoneyOff";
import PictureAsPdfIcon from "@mui/icons-material/PictureAsPdf";
import ShareIcon from "@mui/icons-material/Share";
import WhatsAppIcon from "@mui/icons-material/WhatsApp";
import { Button } from "@mui/material";
import {
  DataGrid,
  GridActionsCellItem,
  GridColDef,
  GridFilterModel,
  GridPaginationModel,
  GridRenderCellParams,
  GridRowParams,
  GridRowSelectionModel,
  GridSlots,
  GridSortModel,
  GridToolbarColumnsButton,
  GridToolbarContainer,
  GridToolbarFilterButton,
  GridValueOptionsParams,
  useGridApiRef,
} from "@mui/x-data-grid";
import { ptBR } from "@mui/x-data-grid/locales";
import { Label } from "@vinicios-campera/kernel-react";
import { useSnackbar } from "notistack";
import { useEffect, useState } from "react";
import { OrderResponse, OrderState } from "../../../api/order-service-api";
import DialogDelete from "../../../components/DialogDelete";
import useAuth from "../../../hooks/useAuth";
import { useModalState } from "../../../hooks/useModalState";
import useSnackStatus from "../../../hooks/useSnackStatus";
import { CallApi, GetOSApiClient } from "../../../utils/apiHelpers";
import { openPdfInNewTab } from "../../../utils/pdfHelper";
import { removeEspecialCharacters } from "../../../utils/stringHelper";
import { getFilterOrders } from "./filterOrdersHelper";
import OrderForm from "./OrderForm";

export default function OrdersTable() {
  const { enqueueSnackbar } = useSnackbar();
  const apiRef = useGridApiRef();
  const { user, userApi } = useAuth();

  const initialStates = {
    orders: new Array<OrderResponse>(),
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

  const [orders, setOrders] = useState<Array<OrderResponse>>(
    initialStates.orders
  );

  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [pagination, setPagination] = useState<GridPaginationModel>(
    initialStates.pagination
  );

  const [filter, setFilter] = useState<GridFilterModel>({} as GridFilterModel);
  const [sort, setSort] = useState<GridSortModel>(initialStates.sort);
  const [selection, setSelection] = useState<GridRowSelectionModel>([]);

  const { modals, openModal, closeModal } = useModalState({
    add: false,
    delete: false,
  });

  const [currentOrder, setCurrentOrder] = useState<OrderResponse | null>(null);

  const { snackStatus, setSnackStatus, onCloseSnack } = useSnackStatus();

  function getData() {
    return new Promise<boolean>((resolve, reject) => {
      setIsLoading(true);
      let _sort = sort.length === 0 ? initialStates.sort : sort;
      return CallApi(
        GetOSApiClient(user?.accessToken).getOrders(
          undefined,
          undefined,
          getFilterOrders(filter),
          pagination.pageSize.toString(),
          (pagination.pageSize * pagination.page).toString(),
          _sort.map((element) => `${element.field} ${element.sort}`).join()
        )
      )
        .then((data) => {
          setOrders(data);
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
        GetOSApiClient(user?.accessToken).deleteOrder(currentOrder?.id!)
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

  function onChangeState(ids: string[], newState: OrderState) {
    return new Promise<boolean>((resolve, reject) => {
      return CallApi(
        GetOSApiClient(user?.accessToken).updateOrderState(ids, newState)
      )
        .then((data) => {
          setSnackStatus({ message: "Alterado", variant: "success" });
          getData();
          resolve(data);
        })
        .catch((err) => {
          setSnackStatus({ message: err, variant: "error" });
          resolve(false);
        });
    });
  }

  function onGetPdfs(id: string[]) {
    return new Promise<boolean>((resolve, reject) => {
      return CallApi(GetOSApiClient(user?.accessToken).getOrderAsPdf(id))
        .then((data) => {
          openPdfInNewTab(data);
          resolve(true);
        })
        .catch((err) => {
          setSnackStatus({ message: err, variant: "error" });
          resolve(false);
        });
    });
  }

  function onGetPdfAndShareWpp(id: string, phone: string) {
    return new Promise<boolean>((resolve, reject) => {
      return CallApi(GetOSApiClient(user?.accessToken).getOrderDetailById(id))
        .then((data) => {
          var message = userApi?.name
            ? `Emitida por: ${userApi?.name}\n`
            : `Emitida por: ${userApi?.email}\n` +
              `Ordem de serviço: N° ${data.identifier}\n` +
              `Total: R$ ${data.amount?.toLocaleString()}\n` +
              `Acesse o link abaixo para mais detalhes\n` +
              `${data.pdfUrl}`;

          window.open(
            `https://wa.me/55${removeEspecialCharacters(
              phone
            )}?text=${encodeURI(message)}`,
            "_blank"
          );
          resolve(true);
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
    { field: "identifier", headerName: "Código", filterable: false, flex: 0.2 },
    {
      field: "client",
      headerName: "Cliente",
      renderCell(params: GridRenderCellParams<OrderResponse>) {
        return params.row.client?.name;
      },
      flex: 1,
    },
    {
      field: "document",
      headerName: "CPF/CNPJ",
      renderCell(params: GridRenderCellParams<OrderResponse>) {
        return params.row.client?.document;
      },
      flex: 0.6,
    },
    {
      field: "amount",
      headerName: "Valor",
      filterable: false,
      type: "number",
      flex: 0.5,
      headerAlign: "left",
      align: "left",
    },
    {
      field: "discount",
      headerName: "Desconto",
      filterable: false,
      type: "number",
      flex: 0.5,
      headerAlign: "left",
      align: "left",
    },
    {
      field: "inserted",
      headerName: "Data",
      type: "date",
      filterable: false,
      flex: 0.7,
    },
    {
      field: "state",
      headerName: "Status",
      type: "singleSelect",
      valueOptions: (params: GridValueOptionsParams<OrderResponse>) => {
        return [OrderState.None, OrderState.Pay];
      },
      renderCell(params: GridRenderCellParams<OrderResponse>) {
        return params.row.state === OrderState.Pay ? (
          <Label color="success">Paga</Label>
        ) : (
          <Label color="error">Pendente</Label>
        );
      },
      flex: 0.5,
    },
    {
      field: "actions",
      type: "actions",
      headerName: "Opções",
      cellClassName: "actions",
      getActions: (params: GridRowParams<OrderResponse>) => {
        const { row } = params;
        return [
          <GridActionsCellItem
            icon={<PictureAsPdfIcon />}
            label="Pdf"
            title="Obter PDF"
            onClick={() => {
              onGetPdfs([params.row.id!]);
            }}
          />,
          <GridActionsCellItem
            icon={<WhatsAppIcon />}
            label="Whats"
            title="Compartilhar via WhatsApp"
            onClick={() => {
              onGetPdfAndShareWpp(
                params.row.id!,
                params.row.client?.cellphone!
              );
            }}
          />,
          <GridActionsCellItem
            icon={
              row.state === OrderState.Pay ? (
                <MoneyOffIcon />
              ) : (
                <AttachMoneyIcon />
              )
            }
            label="State"
            title={
              row.state === OrderState.Pay
                ? "Alterar para NÃO PAGA"
                : "Alterar para PAGA"
            }
            className="textPrimary"
            onClick={() => {
              onChangeState(
                [row.id!],
                row.state! === OrderState.Pay ? OrderState.None : OrderState.Pay
              );
            }}
          />,
          <GridActionsCellItem
            icon={<DeleteIcon />}
            label="Delete"
            onClick={() => {
              setCurrentOrder(row);
              openModal("delete");
            }}
          />,
        ];
      },
      flex: 0.6,
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
            setCurrentOrder(null);
            openModal("add");
          }}
        >
          Nova O.S.
        </Button>
        <Button
          disabled={selection.length === 0}
          startIcon={<ShareIcon />}
          onClick={() => {
            onGetPdfs(selection.flatMap((item) => item as string));
          }}
        >
          Obter PDF selecionadas
        </Button>
        <Button
          disabled={selection.length === 0}
          startIcon={<AttachMoneyIcon />}
          onClick={() => {
            onChangeState(
              selection.flatMap((item) => item as string),
              OrderState.Pay
            );
          }}
        >
          Alterar para PAGA
        </Button>
        <Button
          disabled={selection.length === 0}
          startIcon={<MoneyOffIcon />}
          onClick={() => {
            onChangeState(
              selection.flatMap((item) => item as string),
              OrderState.None
            );
          }}
        >
          Alterar para NÃO PAGA
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
        rows={orders}
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
        checkboxSelection
        onRowSelectionModelChange={(newRowSelectionModel) => {
          setSelection(newRowSelectionModel);
        }}
        rowSelectionModel={selection}
      />
      <OrderForm
        open={modals.add}
        onClose={() => closeModal("add")}
        onFinish={() => {
          closeModal("add");
          getData();
        }}
      />
      <DialogDelete
        open={modals.delete}
        title="Exclusão"
        description={
          <>
            Deseja realmente excluir a O.S{" "}
            <strong>{currentOrder?.identifier}</strong> do cliente{" "}
            <strong>{currentOrder?.client?.name}</strong>?
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
