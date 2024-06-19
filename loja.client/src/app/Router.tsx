import { Suspense } from 'react';
import { createBrowserRouter, Outlet, Link } from 'react-router-dom';

import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Button from '@mui/material/Button';

import HomeIcon from '@mui/icons-material/Home';
import LocalShippingIcon from '@mui/icons-material/LocalShipping';

import PageLoading from './components/page-loading';
import ErrorPage from './main/error';
import HomePage from './main/home';
import OrdersPage from './main/orders';
import OrdersDetail from './main/orders/detail';

const navItems = [
  {
    title: 'Início',
    path: 'home',
    icon: <HomeIcon />,
  },
  {
    title: 'Pedidos',
    path: 'orders',
    icon: <LocalShippingIcon />,
  },
];

function Root() {
  return (
    <>
      <AppBar component="nav" color="primary" sx={{ zIndex: (theme) => theme.zIndex.drawer + 2 }}>
        <Toolbar>
          <Box sx={{ marginLeft: 'auto', display: 'flex', gap: '5px' }}>
            {navItems.map((item, i) => (
              <Button
                key={i}
                component={Link}
                to={item.path}
                startIcon={item.icon}
                variant="contained"
              >
                {item.title}
              </Button>
            ))}
          </Box>
        </Toolbar>
      </AppBar>
      <Box
        sx={{
          height: '80vh',
          width: '90vw',
        }}
      >
        <Suspense fallback={<PageLoading />}>
          <Outlet />
        </Suspense>
      </Box>
    </>
  );
}

const Router = createBrowserRouter([
  {
    path: '/',
    element: <Root />,
    errorElement: <ErrorPage />,
    children: [
      {
        path: 'home',
        element: <HomePage />,
      },
      {
        path: 'orders',
        element: <OrdersPage />,
      },
      {
        path: 'orders/:id/detail',
        element: <OrdersDetail />,
        loader: ({ params }) => fetch(`order/${params.id}`),
      },
      {
        path: 'orders/create',
        element: <OrdersDetail />,
      },
    ],
  },
]);

export default Router;
