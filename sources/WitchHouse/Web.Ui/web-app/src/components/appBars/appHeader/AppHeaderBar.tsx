import { AppBar, Box, FormLabel, IconButton, Toolbar, Typography } from '@mui/material';
import React from 'react';
import MenuIcon from '@mui/icons-material/Menu';
import AccountCircle from '@mui/icons-material/AccountCircle';
import { useAuth } from '../../../lib/AuthContext';
import UserMenu from './UserMenu';

const AppHeaderBar: React.FC = () => {
  const { loginResult } = useAuth();
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);

  const handleProfileMenuOpen = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleProfileMenuClose = React.useCallback(() => {
    setAnchorEl(null);
  }, []);

  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar position="sticky" style={{ backgroundColor: '#000000' }}>
        <Toolbar>
          <IconButton size="large" edge="start" color="inherit" sx={{ mr: 2 }}>
            <MenuIcon />
          </IconButton>
          <Typography variant="h6" noWrap component="div" sx={{ display: { xs: 'none', sm: 'block' } }}>
            {process.env.REACT_APP_Name}
          </Typography>
          <Box sx={{ flexGrow: 1 }} />
          <Box sx={{ display: { xs: 'none', md: 'flex' } }}>
            <IconButton
              style={{ display: 'flex', alignItems: 'center', gap: 4 }}
              size="large"
              edge="end"
              aria-label="account of current user"
              aria-haspopup="true"
              onClick={handleProfileMenuOpen}
              color="inherit"
            >
              <AccountCircle />
              <FormLabel style={{ color: '#ffffff' }}>{loginResult?.userName}</FormLabel>
            </IconButton>
          </Box>
        </Toolbar>
      </AppBar>
      <UserMenu anchor={anchorEl} open={anchorEl !== null} handleClose={handleProfileMenuClose} />
    </Box>
  );
};

export default AppHeaderBar;
