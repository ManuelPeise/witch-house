import { Grid, Paper } from '@mui/material';
import React, { PropsWithChildren } from 'react';

interface IProps extends PropsWithChildren {
  listComponent: React.ComponentType;
  evaluation: number;
}

const HorizontalTabbedPage: React.FC<IProps> = (props) => {
  const { children, evaluation, listComponent } = props;

  const List = listComponent;

  return (
    <Grid container justifyContent="space-around" style={{ marginTop: '2rem' }}>
      <Grid item xs={3}>
        <Paper elevation={evaluation}>
          <List />
        </Paper>
      </Grid>
      <Grid item xs={8}>
        <Paper elevation={evaluation}>{children}</Paper>
      </Grid>
    </Grid>
  );
};

export default HorizontalTabbedPage;
