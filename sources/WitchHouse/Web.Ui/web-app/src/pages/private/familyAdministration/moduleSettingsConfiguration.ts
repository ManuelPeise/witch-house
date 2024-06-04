import { ModuleTypeEnum } from '../../../lib/enums/ModuleTypeEnum';

type ModuleSettingsConfiguration = {
  hasMaxValue: boolean;
  hasMaxWordLength: boolean;
  hasCalculationRuleSettings: boolean;
};

// export const getModuleSettingsConfiguration = (moduleType: ModuleTypeEnum): ModuleSettingsConfiguration => {
//   switch (moduleType) {
//     case ModuleTypeEnum.MathUnits:
//       return {
//         hasMaxWordLength: false,
//         hasMaxValue: true,
//         hasCalculationRuleSettings: true,
//       };
//     case ModuleTypeEnum.GermanUnits:
//       return {
//         hasMaxWordLength: true,
//         hasMaxValue: false,
//         hasCalculationRuleSettings: false,
//       };
//   }
// };
