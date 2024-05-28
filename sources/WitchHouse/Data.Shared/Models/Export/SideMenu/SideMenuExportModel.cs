using Data.Shared.Enums;

namespace Data.Shared.Models.Export.SideMenu
{
    public class SideMenuExportModel
    {
        public SideMenuHeaderData HeaderData { get; set; } = new SideMenuHeaderData();
        public List<MenuNode> MenuNodes { get; set; } = new List<MenuNode>();
    }

    public class SideMenuHeaderData
    {
        public string TitleResourceKey { get; set; } = string.Empty;
        public string SubTitleResourceKey { get; set; } = string.Empty;
    }

    public class MenuNode
    {
        public int Id { get; set; }
        public string ResourceKey { get; set; } = string.Empty;
        public List<UserRoleEnum> UserRoles { get; set; } = new List<UserRoleEnum>();
        public List<SubMenuNode> SubMenuNodes { get; set; } = new List<SubMenuNode>();
    }

    public class SubMenuNode
    {
        public string ResourceKey { get; set; } = string.Empty;
        public List<UserRoleEnum> UserRoles { get; set; } = new List<UserRoleEnum>();
        public string TargetPath { get; set; } = string.Empty;
    }
}
