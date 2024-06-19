import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import LinearProgress from '@mui/material/LinearProgress';

function PageLoading() {
  return (
    <Box sx={{ marginLeft: 'auto', marginRight: 'auto' }}>
      <Typography>Carregando...</Typography>
      <LinearProgress />
    </Box>
  );
}

export default PageLoading;
