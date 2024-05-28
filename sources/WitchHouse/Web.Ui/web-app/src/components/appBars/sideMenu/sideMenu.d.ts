import { UserRoleEnum } from '../../../lib/enums/UserRoleEnum';

export type SideMenuHeaderModel = {
  titleResourceKey: string;
  subTitleResourceKey: string;
};

export type SubMenuNode = {
  resourceKey: string;
  userRoles: UserRoleEnum[];
  targetPath: string;
};

export type MenuNode = {
  id: number;
  resourceKey: string;
  userRoles: UserRoleEnum[];
  subMenuNodes: SubMenuNode[];
};

export type SideMenu = {
  headerData: SideMenuHeaderModel;
  menuNodes: MenuNode[];
};
