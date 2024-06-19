export const endpoints = {
  login: 'Login/AccountLogin',
  registerFamily: 'AccountService/RegisterFamily',
  checkUserName: 'AccountService/CheckUserName?userName={name}',
  getProfile: 'ProfileService/GetProfile/{model}',
  updateProfile: 'ProfileService/UpdateProfile',
  administration: {
    log: 'LogService/GetLogMessages',
    delete: 'LogService/DeleteLogMessages',
    getFamilyUsers: 'FamilyAdministrationService/GetFamilyUsers?familyGuid={id}',
    addFamilyMember: 'FamilyAdministrationService/AddFamilyUser',
    updateFamilyMember: 'FamilyAdministrationService/UpdateFamilyMember',
    loadFamilyUserData: 'FamilyAdministrationService/LoadFamilyUserData?accountGuid={guid}',
    loadModuleConfiguration: 'ModuleService/LoadModuleConfiguration',
    updateModule: 'ModuleService/UpdateModuleConfiguration',
    loadModuleSettings: 'ModuleService/LoadSchoolModule?userGuid={id}',
    updateModuleSettings: 'ModuleService/UpdateSchoolSettings',
  },
  account: {
    passwordCheck: 'ProfileService/CheckPassword',
    updatePassword: 'ProfileService/UpdatePassword',
  },
  menu: {
    sideMenu: 'SideMenuService/GetSideMenu',
  },
};
