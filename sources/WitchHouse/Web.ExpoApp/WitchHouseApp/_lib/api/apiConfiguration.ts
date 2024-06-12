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
    getTrainingResultStatistics: 'TrainingResultService/GetUnitResultStatistics',
    saveTrainingResult: 'TrainingResultService/SaveUnitResult',
    saveTrainingResults: 'TrainingResultService/SaveUnitResults',
  },
};
