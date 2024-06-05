import { Grid, Typography } from '@mui/material';
import React, { PropsWithChildren, memo } from 'react';

interface IProps extends PropsWithChildren {
  caption: string;
}

const SectionLayout: React.FC<IProps> = (props) => {
  const { caption, children } = props;
  return (
    <Grid container padding={5} justifyContent="center">
      <Grid item xs={10} padding={5} sx={{ padding: '1rem' }}>
        <Typography sx={{ paddingLeft: '1rem', fontSize: '1.6rem', color: 'lightblue' }}>{caption}</Typography>
      </Grid>
      <Grid item xs={10} padding={0} sx={{ marginTop: '2rem' }}>
        {children}
      </Grid>
    </Grid>
  );
};

export default memo(SectionLayout);
