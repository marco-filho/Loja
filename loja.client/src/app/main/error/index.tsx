import { useRouteError } from 'react-router-dom';

import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';

function ErrorPage() {
  const error = useRouteError() as Error;

  return (
    <Box sx={{ textAlign: 'center' }}>
      <Typography>Opa! Algo deu errado 🤔</Typography>
      <pre>{error.message || JSON.stringify(error)}</pre>
      <Button color="info" onClick={() => (window.location.href = '/')}>
        Clique aqui para recarregar 🛠
      </Button>
    </Box>
  );
}

export default ErrorPage;
