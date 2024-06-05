import { Box, IconButton, Menu, MenuItem, Typography } from '@mui/material';
import React from 'react';
import ArrowCircleDownIcon from '@mui/icons-material/ArrowCircleDown';
import { FamilyAdministrationSectionEnum } from '../enums/FamilyAdministrationSectionEnum';
import { useI18n } from '../../../../hooks/useI18n';

interface IProps {
  firstName: string;
  lastName: string;
  index: number;
  onSectionChanged: (section: FamilyAdministrationSectionEnum, index: number) => void;
}

const UserListItemMenu: React.FC<IProps> = (props) => {
  const { firstName, lastName, index, onSectionChanged } = props;
  const [element, setElement] = React.useState<HTMLElement | null>(null);
  const { getResource } = useI18n();
  const handleMenuOpen = React.useCallback((e: React.MouseEvent<HTMLElement>) => {
    setElement(e.currentTarget);
  }, []);

  const handleMenuClose = React.useCallback(() => {
    setElement(null);
  }, []);

  const handleSectionChanged = React.useCallback(
    (section: FamilyAdministrationSectionEnum) => {
      handleMenuClose();
      onSectionChanged(section, index);
    },
    [index, handleMenuClose, onSectionChanged]
  );

  const menuStyleProps: React.CSSProperties = {
    width: 'auto',
    minWidth: '340px',
  };

  return (
    <Box>
      <Box
        sx={{
          display: 'flex',
          justifyContent: 'space-between',
          alignItems: 'center',
          padding: '.2rem',
          paddingLeft: '2rem',
          paddingRight: '2rem',
        }}
      >
        <Typography variant="h5">{`${firstName} ${lastName}`}</Typography>
        <IconButton size="small" onClick={handleMenuOpen}>
          <ArrowCircleDownIcon sx={{ width: 40, height: 40 }} />
        </IconButton>
      </Box>
      <Menu
        anchorEl={element}
        open={Boolean(element)}
        anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
        transformOrigin={{ vertical: 'top', horizontal: 'right' }}
        slotProps={{
          paper: {
            style: menuStyleProps,
          },
        }}
        onClose={handleMenuClose}
      >
        <MenuItem onClick={handleSectionChanged.bind(null, FamilyAdministrationSectionEnum.Details)}>
          {getResource('administration:captionUserDetails')}
        </MenuItem>
        <MenuItem onClick={handleSectionChanged.bind(null, FamilyAdministrationSectionEnum.Modules)}>
          {getResource('administration:captionModuleAdministration')}
        </MenuItem>
        <MenuItem onClick={handleSectionChanged.bind(null, FamilyAdministrationSectionEnum.ModuleSettings)}>
          {getResource('administration:captionSchoolModuleSettings')}
        </MenuItem>
      </Menu>
    </Box>
  );
};

export default UserListItemMenu;
