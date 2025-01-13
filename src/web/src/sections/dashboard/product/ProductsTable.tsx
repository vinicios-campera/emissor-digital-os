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
import { Measure, ProductResponse } from "../../../api/order-service-api";
import DialogDelete from "../../../components/DialogDelete";
import useAuth from "../../../hooks/useAuth";
import { useModalState } from "../../../hooks/useModalState";
import useSnackStatus from "../../../hooks/useSnackStatus";
import { CallApi, GetOSApiClient } from "../../../utils/apiHelpers";
import { getFilter } from "../../filterHelper";
import ProductForm from "./ProductForm";

export default function ProductsTable() {
  const { enqueueSnackbar } = useSnackbar();
  const apiRef = useGridApiRef();
  const { user } = useAuth();

  const initialStates = {
    products: new Array<ProductResponse>(),
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

  const [products, setProducts] = useState<Array<ProductResponse>>(
    initialStates.products
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

  const [currentProduct, setCurrentProduct] = useState<ProductResponse | null>(
    null
  );

  const { snackStatus, setSnackStatus, onCloseSnack } = useSnackStatus();

  function getData() {
    return new Promise<boolean>((resolve, reject) => {
      setIsLoading(true);
      let _sort = sort.length === 0 ? initialStates.sort : sort;
      return CallApi(
        GetOSApiClient(user?.accessToken).getProducts(
          undefined,
          undefined,
          getFilter(filter),
          pagination.pageSize.toString(),
          (pagination.pageSize * pagination.page).toString(),
          _sort.map((element) => `${element.field} ${element.sort}`).join()
        )
      )
        .then((data) => {
          setProducts(data);
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
        GetOSApiClient(user?.accessToken).deleteProduct(currentProduct?.id!)
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
    { field: "description", headerName: "Descrição", flex: 1 },
    {
      field: "unitaryValue",
      headerName: "Preço unitário",
      filterable: false,
      type: "number",
      flex: 0.9,
      headerAlign: "left",
      align: "left",
    },
    {
      field: "measure",
      headerName: "Unidade de medida",
      type: "singleSelect",
      valueOptions: (params: GridValueOptionsParams<ProductResponse>) => {
        return [
          Measure.Box,
          Measure.Centimeters,
          Measure.Kilometers,
          Measure.Meters,
          Measure.Unit,
        ];
      },
      flex: 0.8,
    },
    {
      field: "inserted",
      headerName: "Data",
      type: "date",
      filterable: false,
      flex: 0.7,
    },
    {
      field: "actions",
      type: "actions",
      headerName: "Opções",
      cellClassName: "actions",
      getActions: (params: GridRowParams<ProductResponse>) => {
        const { row } = params;
        return [
          <GridActionsCellItem
            icon={<EditIcon />}
            label="Edit"
            className="textPrimary"
            onClick={() => {
              setCurrentProduct(row);
              openModal("manage");
            }}
          />,
          <GridActionsCellItem
            icon={<DeleteIcon />}
            label="Delete"
            onClick={() => {
              setCurrentProduct(row);
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
            setCurrentProduct(null);
            openModal("manage");
          }}
        >
          Novo produto
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
        rows={products}
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
      <ProductForm
        open={modals.manage}
        onClose={() => closeModal("manage")}
        onFinish={() => {
          closeModal("manage");
          getData();
        }}
        product={currentProduct}
      />
      <DialogDelete
        open={modals.delete}
        title="Exclusão"
        description={
          <>
            Deseja realmente excluir o produto{" "}
            <strong>{currentProduct?.description}</strong>?
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
