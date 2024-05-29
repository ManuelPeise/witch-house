export const endpoints = {
  login: 'Login/AccountLogin',
  registerFamily: 'AccountService/RegisterFamily',
  checkUserName: 'AccountService/CheckUserName?userName={name}',
  getProfile: 'ProfileService/GetProfile/{model}',
  updateProfile: 'ProfileService/UpdateProfile',
  administration: {
    log: 'LogService/GetLogMessages',
    delete: 'LogService/DeleteLogMessages',
  },
  account: {
    passwordCheck: 'ProfileService/CheckPassword',
    updatePassword: 'ProfileService/UpdatePassword',
  },
  menu: {
    sideMenu: 'SideMenuService/GetSideMenu',
  },
};
