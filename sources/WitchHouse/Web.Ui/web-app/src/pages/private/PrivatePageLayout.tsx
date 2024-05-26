import { Grid } from '@mui/material';
import React from 'react';
import { Outlet } from 'react-router-dom';
import AppHeaderBar from '../../components/appBars/appHeader/AppHeaderBar';

const PrivatePagePayout: React.FC = () => {
  return (
    <Grid container>
      <Grid item xs={12}>
        <AppHeaderBar />
      </Grid>
      <Grid item xs={12}>
        <Outlet />
      </Grid>
    </Grid>
  );
};

export default PrivatePagePayout;
