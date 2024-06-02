import { Grid, Paper } from '@mui/material';
import React, { PropsWithChildren } from 'react';

interface IProps extends PropsWithChildren {
  evaluation: number;
  paperHeight: number;
  component: JSX.Element;
}

const VerticalTabbedPage: React.FC<IProps> = (props) => {
  const { children, evaluation, paperHeight, component } = props;

  return (
    <Grid container justifyContent="center" style={{ marginTop: '2rem', gap: 30 }}>
      <Grid item xs={3}>
        <Paper elevation={evaluation} sx={{ height: paperHeight }}>
          {component}
        </Paper>
      </Grid>
      <Grid item xs={8}>
        <Paper elevation={evaluation / 2} sx={{ height: paperHeight }}>
          {children}
        </Paper>
      </Grid>
    </Grid>
  );
};

export default VerticalTabbedPage;
