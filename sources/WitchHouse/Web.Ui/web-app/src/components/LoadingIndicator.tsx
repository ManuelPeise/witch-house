import { Box, LinearProgress } from '@mui/material';
import React from 'react';

interface IProps {
  isLoading: boolean;
}

const LoadingIndicator: React.FC<IProps> = (props) => {
  const { isLoading } = props;
  return (
    <Box sx={{ width: '100%', height: '1rem' }}>
      {isLoading && <LinearProgress variant="indeterminate" color="secondary" />}
    </Box>
  );
};

export default LoadingIndicator;
