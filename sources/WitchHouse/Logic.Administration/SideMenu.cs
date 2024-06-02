using Data.Shared.Enums;
using Data.Shared.Models.Export.SideMenu;

namespace Logic.Administration
{
    public class SideMenu
    {
        public SideMenuExportModel GetSideMenu()
        {
            return new SideMenuExportModel
            {
                HeaderData = new SideMenuHeaderData
                {
                    TitleResourceKey = "Title",
                    SubTitleResourceKey = "SubTitle"
                },
                MenuNodes = new List<MenuNode>
                {
                    new MenuNode
                    {
                        Id = 1,
                        ResourceKey = "menuItemAdministration",
                        UserRoles = AdminRoles,
                        SubMenuNodes = new List<SubMenuNode>
                        {
                            new SubMenuNode
                            {
                                ResourceKey = "menuItemFamilyUserAdministration",
                                UserRoles = AdminRoles,
                                TargetPath = "administration/family"
                            },
                            //new SubMenuNode
                            //{
                            //    ResourceKey = "menuItemUserAdministration",
                            //    UserRoles = AdminRole,
                            //    TargetPath = "administration/users"
                            //},
                            new SubMenuNode
                            {
                                ResourceKey = "menuItemLog",
                                UserRoles = AdminRoles,
                                TargetPath = "administration/log"
                            },
                        }
                    }
                }
            };
        }

        private readonly List<UserRoleEnum> AdminRoles = new List<UserRoleEnum> { UserRoleEnum.Admin, UserRoleEnum.LocalAdmin };
        private readonly List<UserRoleEnum> AdminRole = new List<UserRoleEnum> { UserRoleEnum.Admin };
        private readonly List<UserRoleEnum> LocalAdminRole = new List<UserRoleEnum> { UserRoleEnum.LocalAdmin };
        private readonly List<UserRoleEnum> UserRole = new List<UserRoleEnum> { UserRoleEnum.User };

    }
}
