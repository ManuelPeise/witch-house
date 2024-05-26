import { Grid } from '@mui/material';
import React, { PropsWithChildren } from 'react';

interface IProps extends PropsWithChildren {
  marginHorizontal?: number;
  marginVertical?: number;
}

const FormItemWrapper: React.FC<IProps> = (props) => {
  const { children, marginHorizontal, marginVertical } = props;
  return (
    <Grid
      item
      xs={12}
      style={{
        marginTop: `${marginVertical}px`,
        marginBottom: `${marginVertical}px`,
        marginLeft: `${marginHorizontal}px`,
        marginRight: `${marginHorizontal}px`,
      }}
    >
      {children}
    </Grid>
  );
};

export default FormItemWrapper;
