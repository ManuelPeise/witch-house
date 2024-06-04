import React from 'react';
import { MenuNode } from './sideMenu';
import { List, ListItem, ListItemButton, Typography } from '@mui/material';
import { useI18n } from '../../../hooks/useI18n';
import { useNavigate } from 'react-router';
import './sideMenu.css';
import { UserRoleEnum } from '../../../lib/enums/UserRoleEnum';

interface IProps {
  node: MenuNode;
  selectedItem: number;
  userRole: UserRoleEnum;
  onExpandItem: (id: number) => void;
  handleResetMenuSelection: () => void;
  onClose: (cb?: () => void) => void;
}

const SideMenuItem: React.FC<IProps> = (props) => {
  const { node, selectedItem, userRole, onExpandItem, onClose, handleResetMenuSelection } = props;
  const { getResource } = useI18n();
  const navigate = useNavigate();

  const handleNavigate = React.useCallback(
    (route: string) => {
      navigate(route, { replace: true });
      onClose(handleResetMenuSelection);
    },
    [navigate, onClose, handleResetMenuSelection]
  );

  return (
    <ListItem disablePadding className="side-menu-item">
      <ListItemButton className="side-menu-item-button" onClick={onExpandItem.bind(null, node.id)}>
        <Typography className="item-text" variant="h5">
          {getResource(`menu:${node.resourceKey}`)}
        </Typography>
      </ListItemButton>
      {selectedItem === node.id && (
        <List className="sub-item-list" disablePadding>
          {node.subMenuNodes.map((sub, index) => {
            return sub.userRoles.includes(userRole) ? (
              <ListItemButton
                className="side-menu-sub-item-button"
                key={index}
                onClick={handleNavigate.bind(null, sub.targetPath)}
              >
                <Typography className="item-text" variant="body1">
                  {getResource(`menu:${sub.resourceKey}`)}
                </Typography>
              </ListItemButton>
            ) : null;
          })}
        </List>
      )}
    </ListItem>
  );
};

export default SideMenuItem;
