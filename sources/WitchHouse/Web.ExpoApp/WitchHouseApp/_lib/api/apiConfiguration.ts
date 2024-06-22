export const endPoints = {
  health: {
    apiIsAvailable: 'Availability/ApiIsAvailable',
  },
  auth: {
    login: 'Login/MobileAccountLogin',
    initializeMobileLogin: 'AccountService/InitializeMobileLogin',
  },
  sync: {
    loadAppData: 'DataSync/LoadAppData?userId={id}',
    syncAppData: 'DataSync/SyncAppData',
  },
  training: {
    getTrainingResultStatistics: 'TrainingResultService/GetLastUnitResultStatistics/{userId}',
    saveTrainingResult: 'TrainingResultService/SaveUnitResult',
    saveTrainingResults: 'TrainingResultService/SaveUnitResults',
  },
  account: {
    imageUpload: 'ProfileService/UploadProfileImage',
  },
};
