import { Grid, Paper } from '@mui/material';
import React, { PropsWithChildren } from 'react';

interface IProps extends PropsWithChildren {
  evaluation: number;
  paperHeight: number;
  component: JSX.Element;
  minWidth: number;
}

const VerticalTabbedPage: React.FC<IProps> = (props) => {
  const { children, evaluation, minWidth, paperHeight, component } = props;

  return (
    <Grid container justifyContent="center" style={{ marginTop: '2rem', gap: 30, minWidth: '760px' }}>
      <Grid item xs={12} lg={3} sx={{ height: paperHeight, minWidth: `${minWidth}px` }}>
        <Paper elevation={evaluation} sx={{ height: paperHeight, minWidth: `${minWidth}px` }}>
          {component}
        </Paper>
      </Grid>
      <Grid item xs={12} lg={8}>
        <Paper elevation={evaluation / 2} sx={{ height: paperHeight }}>
          {children}
        </Paper>
      </Grid>
    </Grid>
  );
};

export default VerticalTabbedPage;
