import { Menu, MenuItem } from '@mui/material';
import React from 'react';
import { useI18n } from '../../../hooks/useI18n';
import { useAuth } from '../../../lib/AuthContext';
import { useNavigate } from 'react-router-dom';
import { RouteTypes } from '../../../lib/Router';

interface IProps {
  anchor: null | HTMLElement;
  open: boolean;
  handleClose: () => void;
}

const UserMenu: React.FC<IProps> = (props) => {
  const { anchor, open, handleClose } = props;
  const navigate = useNavigate();

  const { onLogout } = useAuth();
  const { getResource } = useI18n();

  const handleNavigateToProfile = React.useCallback(() => {
    handleClose();
    navigate(RouteTypes.Profile, { replace: true });
  }, [navigate, handleClose]);

  const handleLogout = React.useCallback(() => {
    handleClose();
    onLogout();
    navigate(RouteTypes.Login, { replace: true });
  }, [handleClose, onLogout, navigate]);

  return (
    <Menu
      anchorEl={anchor}
      anchorOrigin={{
        vertical: 'top',
        horizontal: 'right',
      }}
      transformOrigin={{
        vertical: 'top',
        horizontal: 'right',
      }}
      keepMounted
      open={open}
      onClose={handleClose}
    >
      <MenuItem style={{ padding: '.2rem 2rem' }} onClick={handleNavigateToProfile}>
        {getResource('common:labelProfile')}
      </MenuItem>
      <MenuItem style={{ padding: '.2rem 2rem' }} onClick={handleLogout}>
        {getResource('common:labelLogout')}
      </MenuItem>
    </Menu>
  );
};

export default UserMenu;
