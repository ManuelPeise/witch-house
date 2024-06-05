import { ModuleSettingsTypeEnum } from '../../../lib/enums/ModuleSettingsTypeEnum';

type ModuleSettingsConfiguration = {
  hasMinValue: boolean;
  hasMaxValue: boolean;
  hasMaxWordLength: boolean;
  hasCalculationRuleSettings: boolean;
};

export const getModuleSettingsConfiguration = (moduleType: ModuleSettingsTypeEnum): ModuleSettingsConfiguration => {
  switch (moduleType) {
    case ModuleSettingsTypeEnum.MathUnits:
      return {
        hasMaxWordLength: false,
        hasMinValue: true,
        hasMaxValue: true,
        hasCalculationRuleSettings: true,
      };
    case ModuleSettingsTypeEnum.GermanUnits:
      return {
        hasMaxWordLength: true,
        hasMinValue: false,
        hasMaxValue: false,
        hasCalculationRuleSettings: false,
      };
  }
};
