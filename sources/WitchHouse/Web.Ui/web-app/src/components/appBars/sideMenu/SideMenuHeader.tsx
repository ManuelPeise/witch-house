import { Grid, Typography } from '@mui/material';
import { Box } from '@mui/system';
import React from 'react';
import ArrowBackIosIcon from '@mui/icons-material/ArrowBackIos';
import { SideMenuHeaderModel } from './sideMenu';

interface IProps {
  model: SideMenuHeaderModel;
  width: number;
  handleResetMenuSelection: () => void;
  onClose: (cb?: () => void) => void;
}

const SideMenuHeader: React.FC<IProps> = (props) => {
  const { model, width, onClose, handleResetMenuSelection } = props;

  const handleClose = React.useCallback(() => {
    onClose(handleResetMenuSelection);
  }, [onClose, handleResetMenuSelection]);

  return (
    <Box sx={{ width: width, height: 64, background: '#000' }} role="presentation">
      <Grid container>
        <Grid
          item
          xs={12}
          style={{ display: 'flex', justifyContent: 'space-between', alignContent: 'space-between', padding: 20 }}
        >
          <Typography variant="h5" style={{ color: '#fff', paddingLeft: '1rem' }}>
            {model.titleResourceKey}
          </Typography>
          <ArrowBackIosIcon
            style={{ paddingLeft: 10, height: 30, width: 30, color: '#ffffff' }}
            onClick={handleClose}
          />
        </Grid>
      </Grid>
    </Box>
  );
};

export default SideMenuHeader;
