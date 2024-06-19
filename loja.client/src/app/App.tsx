import './App.css';
import { Provider } from 'react-redux';
import { RouterProvider } from 'react-router-dom';

import { createTheme, ThemeProvider } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFnsV3';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { ptBR } from 'date-fns/locale';

import { store } from './store';
import Router from './Router';

const theme = createTheme({
  palette: {
    primary: {
      main: '#000',
    },
  },
});

function App() {
  return (
    <>
      <CssBaseline />
      <ThemeProvider theme={theme}>
        <LocalizationProvider adapterLocale={ptBR} dateAdapter={AdapterDateFns}>
          <Provider store={store}>
            <RouterProvider router={Router} />
          </Provider>
        </LocalizationProvider>
      </ThemeProvider>
    </>
  );
}

export default App;
