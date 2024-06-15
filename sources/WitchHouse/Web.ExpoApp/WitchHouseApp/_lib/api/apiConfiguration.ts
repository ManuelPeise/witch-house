export const endPoints = {
  health: {
    apiIsAvailable: 'Availability/ApiIsAvailable',
  },
  auth: {
    login: 'Login/MobileAccountLogin',
    initializeMobileLogin: 'AccountService/InitializeMobileLogin',
  },
  sync: {
    syncAppData: 'DataSync/SyncAppData',
  },
  training: {
    getTrainingResultStatistics: 'TrainingResultService/GetLastUnitResultStatistics/{userId}',
    saveTrainingResult: 'TrainingResultService/SaveUnitResult',
    saveTrainingResults: 'TrainingResultService/SaveUnitResults',
  },
};
