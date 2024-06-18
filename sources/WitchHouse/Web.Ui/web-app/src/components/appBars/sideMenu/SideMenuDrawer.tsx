import { Drawer, List } from '@mui/material';
import { Box } from '@mui/system';
import React from 'react';
import { useApi } from '../../../hooks/useApi';
import { endpoints } from '../../../lib/api/apiConfiguration';
import { SideMenu } from './sideMenu';
import SideMenuItem from './SideMenuItem';
import SideMenuHeader from './SideMenuHeader';
import { useAuth } from '../../../lib/AuthContext';
import { UserRoleEnum } from '../../../lib/enums/UserRoleEnum';

const drawerWidth = 380;
interface IProps {
  open: boolean;
  onClose: (cb?: () => void) => void;
}

const SideMenuDrawer: React.FC<IProps> = (props) => {
  const { open, onClose } = props;
  const { loginResult } = useAuth();
  const [expanded, setExpanded] = React.useState<number>(0);

  const menuApi = useApi<SideMenu>();

  React.useEffect(() => {
    const loadSideMenu = async () => {
      await menuApi.get(endpoints.menu.sideMenu);
    };

    loadSideMenu();
    // eslint-disable-next-line
  }, []);

  const handleExpandedChanged = React.useCallback((key: number) => {
    setExpanded(key);
  }, []);

  const handleResetMenuSelection = React.useCallback(() => {
    setExpanded(0);
  }, []);

  const canAccess = React.useCallback(
    (requiredRoles: UserRoleEnum[]) => {
      const matchingRoles = requiredRoles.filter((role) => loginResult?.userData?.userRoles?.includes(role));

      return matchingRoles?.length > 0;
    },
    [loginResult]
  );

  if (!menuApi.data || loginResult == null) {
    return null;
  }

  return (
    <Drawer open={open}>
      <SideMenuHeader
        handleResetMenuSelection={handleResetMenuSelection}
        width={drawerWidth}
        model={menuApi.data?.headerData}
        onClose={onClose}
      />
      <Box sx={{ width: drawerWidth, height: '100%', backgroundColor: '#000' }} role="presentation">
        <List disablePadding>
          {menuApi.data.menuNodes?.map((node, index) => {
            return canAccess(node.userRoles) ? (
              <SideMenuItem
                key={index}
                node={node}
                selectedItem={expanded}
                userRoles={loginResult.userData.userRoles}
                onExpandItem={handleExpandedChanged}
                onClose={onClose}
                handleResetMenuSelection={handleResetMenuSelection}
              />
            ) : null;
          })}
        </List>
      </Box>
    </Drawer>
  );
};

export default SideMenuDrawer;
