import { useState, useEffect, useMemo, useCallback } from 'react';
import { useNavigate, useLocation, useLoaderData } from 'react-router-dom';
import {
  useForm,
  useFieldArray,
  FormProvider,
  SubmitHandler,
  SubmitErrorHandler,
  FieldErrors,
} from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';
import { pt } from 'yup-locales';

import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Grid from '@mui/material/Grid';
import Backdrop from '@mui/material/Backdrop';
import Button from '@mui/material/Button';
import {
  DataGrid,
  GridRowId,
  GridColDef,
  GridEditInputCell,
  GridActionsCellItem,
  GridRowModesModel,
  GridRowModes,
  GridEventListener,
  GridRowEditStopReasons,
  GridToolbarContainer,
  GridSlots,
} from '@mui/x-data-grid';
import { ptBR } from '@mui/x-data-grid/locales';

import AddIcon from '@mui/icons-material/Add';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import SaveIcon from '@mui/icons-material/Save';
import CancelIcon from '@mui/icons-material/Close';

import { useAppDispatch } from '../../../store';
import { setOrder } from './store/slice';
import OrderActions from './store/actions';
import InputController from '../../../components/input-controller';
import ConfirmationDialog from '../../../components/confirmation-dialog';

import type { Order } from '../../../types/entities';
import { moneyMask } from '../../../utils/masks';

yup.setLocale(pt);

const rules = {
  clientName: {
    min: 2,
    max: 100,
  },
  total: {
    max: 999999999,
  },
  product: {
    min: 2,
    max: 100,
  },
  amount: {
    min: 1,
    max: 999,
  },
  price: {
    min: 0,
    max: 999999,
  },
};

const schema = yup.object({
  id: yup.number().nullable(),
  clientName: yup
    .string()
    .label('Nome do cliente')
    .required()
    .min(rules.clientName.min)
    .max(rules.clientName.max),
  orderedAt: yup.date().label('Feito em').nullable(),
  total: yup.number().label('Preço total').nullable(),
  items: yup
    .array()
    .of(
      yup.object().shape({
        id: yup.number(),
        product: yup
          .string()
          .label('Nome do produto')
          .min(rules.product.min)
          .max(rules.product.max)
          .required(),
        amount: yup
          .number()
          .label('Quantidade')
          .min(rules.amount.min)
          .max(rules.amount.max)
          .required(),
        price: yup
          .number()
          .label('Preço unitário')
          .min(rules.price.min)
          .max(rules.price.max)
          .required(),
        isNew: yup.boolean(),
      })
    )
    .min(1, 'Adicione ao menos um produto')
    .required(),
});

type TestSchema = yup.InferType<typeof schema>;

const defaultValues: TestSchema = {
  id: null,
  clientName: '',
  orderedAt: null,
  total: null,
  items: [],
};

interface AddToolbarProps {
  setRows: (emptyRow: unknown) => void;
  setRowModesModel: (newModel: (oldModel: GridRowModesModel) => GridRowModesModel) => void;
}

const AddToolbar = (props: AddToolbarProps) => {
  const { setRows, setRowModesModel } = props;

  const handleClick = () => {
    const id = Date.now();

    setRows({ id, product: '', amount: 0, price: 0, isNew: true });
    setRowModesModel((oldModel) => ({
      ...oldModel,
      [id]: { mode: GridRowModes.Edit, fieldToFocus: 'product' },
    }));
  };

  return (
    <GridToolbarContainer>
      <Button startIcon={<AddIcon />} onClick={handleClick}>
        Novo produto
      </Button>
    </GridToolbarContainer>
  );
};

function OrdersDetail() {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const order = useLoaderData();
  const { state } = useLocation();
  const { editing, creating } = state;
  const [dialogData, setDialogData] = useState(0);
  const [rowModesModel, setRowModesModel] = useState<GridRowModesModel>({});

  const methods = useForm({
    mode: 'onBlur',
    defaultValues,
    resolver: yupResolver(schema),
  });

  const products = methods.watch('items');
  const { append, update, remove } = useFieldArray({ control: methods.control, name: 'items' });

  const onSave: SubmitHandler<TestSchema> = (data) => {
    data.items.forEach((x) => {
      if (x.isNew) x.id = 0;
    });

    if (creating) dispatch(OrderActions.addOrder(data as Order, () => navigate('/orders')));
    else if (editing) dispatch(OrderActions.editOrder(data as Order, () => navigate('/orders')));
  };

  const verifyErrors: SubmitErrorHandler<TestSchema> = (errors: FieldErrors<TestSchema>) => {
    const fields = Object.values(errors);

    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    const searchRecursively = (field: any) => {
      if (field.message) alert(field.message);
      else {
        const values = Object.values(field);
        values.forEach((v) => searchRecursively(v));
      }
    };

    fields.forEach(searchRecursively);
  };

  useEffect(() => {
    if (!creating) methods.reset(order as TestSchema);

    dispatch(setOrder({ order, editing, creating }));

    return () => {
      dispatch(setOrder(null));
      return;
    };
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    methods.setValue(
      'total',
      products.reduce((prev: number, curr) => prev + curr.amount * curr.price, 0)
    );
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [products]);

  const openDeleteConfirmation = (id: GridRowId) => () => setDialogData(id as number);

  const handleRowEditStop: GridEventListener<'rowEditStop'> = (params, event) => {
    if (params.reason === GridRowEditStopReasons.rowFocusOut) {
      event.defaultMuiPrevented = true;
    }
  };

  const handleEditClick = useCallback(
    (id: GridRowId) => () => {
      setRowModesModel({ ...rowModesModel, [id]: { mode: GridRowModes.Edit } });
    },
    [rowModesModel]
  );

  const handleSaveClick = useCallback(
    (id: GridRowId) => () => {
      setRowModesModel({ ...rowModesModel, [id]: { mode: GridRowModes.View } });
    },
    [rowModesModel]
  );

  const handleDeleteClick = () => {
    remove(products.findIndex((x) => x.id === dialogData));
  };

  const handleCancelClick = useCallback(
    (id: GridRowId) => () => {
      setRowModesModel({
        ...rowModesModel,
        [id]: { mode: GridRowModes.View, ignoreModifications: true },
      });

      const editedRow = products.find((p) => p.id === id);
      if (editedRow!.isNew) {
        remove(products.findIndex((x) => x.id === id));
      }
    },
    [products, remove, rowModesModel]
  );

  const columns: GridColDef[] = useMemo(
    () => [
      {
        field: 'id',
        headerName: 'ID',
        headerAlign: 'center',
        align: 'center',
        renderCell: (params) => (params.row.isNew ? '-' : params.row.id),
      },
      {
        field: 'product',
        headerName: 'Produto',
        flex: 2,
        editable: true,
      },
      {
        field: 'amount',
        headerName: 'Quantidade',
        headerAlign: 'center',
        align: 'center',
        flex: 1,
        editable: true,
        renderEditCell: (params) => (
          <GridEditInputCell
            {...params}
            type="number"
            inputProps={{
              ...rules.amount,
            }}
          />
        ),
      },
      {
        field: 'price',
        headerName: 'Preço (R$)',
        headerAlign: 'center',
        align: 'center',
        flex: 1,
        editable: true,
        renderCell: (params) => moneyMask(params.row.price),
        renderEditCell: (params) => (
          <GridEditInputCell
            {...params}
            type="number"
            inputProps={{
              ...rules.price,
            }}
          />
        ),
      },
      {
        field: 'actions',
        type: 'actions',
        headerName: 'Ações',
        cellClassName: 'actions',
        getActions: ({ id }) => {
          const isInEditMode = rowModesModel[id]?.mode === GridRowModes.Edit;

          if (isInEditMode) {
            return [
              <GridActionsCellItem
                icon={<SaveIcon />}
                label="Save"
                sx={{
                  color: 'primary.main',
                }}
                onClick={handleSaveClick(id)}
              />,
              <GridActionsCellItem
                icon={<CancelIcon />}
                label="Cancel"
                className="textPrimary"
                onClick={handleCancelClick(id)}
                color="inherit"
              />,
            ];
          }

          return [
            <GridActionsCellItem
              icon={<EditIcon />}
              label="Edit"
              className="textPrimary"
              onClick={handleEditClick(id)}
              color="inherit"
            />,
            <GridActionsCellItem
              icon={<DeleteIcon />}
              label="Delete"
              onClick={openDeleteConfirmation(id)}
              color="error"
            />,
          ];
        },
      },
    ],
    [handleCancelClick, handleEditClick, handleSaveClick, rowModesModel]
  );

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  const processRowUpdate = (newRow: any) => {
    const updatedRow = { ...newRow, price: Number(newRow.price).toFixed(2) };

    update(
      products.findIndex((x) => x.id === updatedRow.id),
      updatedRow
    );

    return updatedRow;
  };

  const handleRowModesModelChange = (newRowModesModel: GridRowModesModel) => {
    setRowModesModel(newRowModesModel);
  };

  return (
    <FormProvider {...methods}>
      <Box
        component={'form'}
        onSubmit={methods.handleSubmit(onSave, verifyErrors)}
        sx={{
          display: 'flex',
          flexDirection: 'column',
          gap: '100px',
        }}
      >
        <Backdrop
          invisible
          sx={{ zIndex: (theme) => theme.zIndex.drawer + 1 }}
          open={!editing && !creating}
        />
        <Box sx={{ display: 'flex', justifyContent: 'space-between' }}>
          <Typography variant="h3">
            {(editing && 'Editar') || (creating && 'Novo') || 'Visualizar'} pedido
          </Typography>
          {(creating || editing) && (
            <Box sx={{ alignContent: 'end' }}>
              <Button type="submit" variant="contained" color="success">
                Salvar
              </Button>
            </Box>
          )}
        </Box>
        <Grid container spacing={2}>
          <Grid item xs={12} md={6}>
            <InputController name="clientName" label="Nome do cliente" />
          </Grid>
          <Grid item xs={12} md={3}>
            <InputController
              name="total"
              label="Total"
              readOnly
              masker={(v: string | number) => moneyMask(Math.min(Number(v), rules.total.max))}
            />
          </Grid>
          <Grid item xs={12} md={3}>
            <InputController name="orderedAt" label="Feito em" datePicker disabled />
          </Grid>
          <Grid item xs={12} sx={{ textAlign: '' }}>
            <Typography variant="overline">Produtos</Typography>
          </Grid>
          <Grid item xs={12}>
            <DataGrid
              autoHeight
              rows={products}
              columns={columns}
              editMode="row"
              initialState={{
                pagination: {
                  paginationModel: {
                    pageSize: 10,
                  },
                },
              }}
              pageSizeOptions={[10, 20]}
              localeText={ptBR.components.MuiDataGrid.defaultProps.localeText}
              rowModesModel={rowModesModel}
              onRowModesModelChange={handleRowModesModelChange}
              onRowEditStop={handleRowEditStop}
              processRowUpdate={processRowUpdate}
              disableRowSelectionOnClick
              slots={{
                toolbar: AddToolbar as GridSlots['toolbar'],
              }}
              slotProps={{
                toolbar: {
                  setRows: (emptyRow: never) => append(emptyRow),
                  setRowModesModel,
                },
              }}
            />
            <ConfirmationDialog
              open={dialogData > 0}
              title="Excluir produto"
              message="Tem certeza que deseja excluir este produto?"
              onClose={() => setDialogData(0)}
              onConfirmation={handleDeleteClick}
            />
          </Grid>
        </Grid>
      </Box>
    </FormProvider>
  );
}

export default OrdersDetail;
