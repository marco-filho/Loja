import { useState, useEffect, useCallback, useMemo, memo } from 'react';
import { useNavigate } from 'react-router-dom';

import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import { DataGrid, GridColDef, GridActionsCellItem, GridToolbarContainer } from '@mui/x-data-grid';

import { ptBR } from '@mui/x-data-grid/locales';
import PreviewIcon from '@mui/icons-material/Preview';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import AddIcon from '@mui/icons-material/Add';

import ConfirmationDialog from '../../components/confirmation-dialog';
import { useAppDispatch, useAppSelector } from '../../store';
import actions from './store/actions';

import type { Order } from '../../types/entities';
import { moneyMask } from '../../utils/masks';

function OrdersPage() {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const orders = useAppSelector(({ reducers }) => reducers.orders);
  const [dialogData, setDialogData] = useState(0);

  useEffect(() => {
    dispatch(actions.getAll());
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const previewOrder = useCallback(
    (params: { row: Order }) => () => {
      navigate(`./${params.row.id}/detail`, { relative: 'path', state: { editing: false } });
    },
    [navigate]
  );

  const editOrder = useCallback(
    (params: { row: Order }) => () => {
      navigate(`./${params.row.id}/detail`, { relative: 'path', state: { editing: true } });
    },
    [navigate]
  );

  const openDeleteConfirmation = useCallback(
    (params: { row: Order }) => () => setDialogData(params.row.id),
    []
  );

  const handleDeleteClick = () => {
    dispatch(actions.deleteOrder(dialogData));
  };

  const createOrder = useCallback(() => {
    navigate(`./create`, { relative: 'path', state: { creating: true } });
  }, [navigate]);

  const columns: GridColDef<Order>[] = useMemo(
    () => [
      { field: 'id', headerName: 'ID', headerAlign: 'center', align: 'center' },
      {
        field: 'clientName',
        headerName: 'Cliente',
        flex: 2,
      },
      {
        field: 'orderedAt',
        headerName: 'Feito em',
        headerAlign: 'center',
        align: 'center',
        flex: 1,
        renderCell: (params) => new Date(params.row.orderedAt).toLocaleString(),
      },
      {
        field: 'total',
        headerName: 'Total',
        headerAlign: 'center',
        align: 'center',
        flex: 1,
        renderCell: (params) => moneyMask(params.row.total),
      },
      {
        field: 'actions',
        type: 'actions',
        headerName: 'Ações',
        cellClassName: 'actions',
        flex: 1,
        getActions: (params) => [
          <GridActionsCellItem
            icon={<PreviewIcon />}
            label="Detalhes"
            onClick={previewOrder(params)}
            color="info"
          />,
          <GridActionsCellItem
            icon={<EditIcon />}
            label="Editar"
            onClick={editOrder(params)}
            color="inherit"
          />,
          <GridActionsCellItem
            icon={<DeleteIcon />}
            label="Excluir"
            onClick={openDeleteConfirmation(params)}
            color="error"
          />,
        ],
      },
    ],
    [openDeleteConfirmation, editOrder, previewOrder]
  );

  const AddToolbar = memo(() => {
    return (
      <GridToolbarContainer>
        <Button startIcon={<AddIcon />} onClick={createOrder}>
          Novo pedido
        </Button>
      </GridToolbarContainer>
    );
  });

  return (
    <Box
      sx={{
        display: 'flex',
        flexDirection: 'column',
        gap: '100px',
      }}
    >
      <Box sx={{ display: 'flex', justifyContent: 'start' }}>
        <Typography variant="h3">Pedidos</Typography>
      </Box>
      <Box sx={{ width: '100%' }}>
        <DataGrid
          autoHeight
          rows={orders.list}
          columns={columns}
          initialState={{
            pagination: {
              paginationModel: {
                pageSize: 10,
              },
            },
          }}
          pageSizeOptions={[10, 20]}
          localeText={ptBR.components.MuiDataGrid.defaultProps.localeText}
          slots={{
            toolbar: AddToolbar,
          }}
        />
        <ConfirmationDialog
          open={dialogData > 0}
          title="Excluir pedido"
          message={`Tem certeza que deseja excluir pedido nº ${dialogData}?`}
          onClose={() => setDialogData(0)}
          onConfirmation={handleDeleteClick}
        />
      </Box>
    </Box>
  );
}

export default OrdersPage;
