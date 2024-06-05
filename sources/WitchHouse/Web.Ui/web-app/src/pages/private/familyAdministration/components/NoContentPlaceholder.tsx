import { Grid, Typography } from '@mui/material';
import React from 'react';

interface IProps {
  label: string;
}

const NoContentPlaceholder: React.FC<IProps> = (props) => {
  const { label } = props;
  return (
    <Grid container justifyContent="center" alignContent="center">
      <Grid item sx={{ display: 'flex', justifyItems: 'center', alignItems: 'center' }}>
        <Typography sx={{ color: 'red', fontSize: '1.4rem' }}>{label}</Typography>
      </Grid>
    </Grid>
  );
};

export default NoContentPlaceholder;
