import React from 'react';
import { List, ListItem } from '@mui/material';
import { useApi } from '../../../../hooks/useApi';
import { ModuleConfiguration, UserModuleRequestModel } from '../types';
import { endpoints } from '../../../../lib/api/apiConfiguration';
import SwitchWithLabel from '../../../../components/inputs/SwitchWithLabel';
import { useI18n } from '../../../../hooks/useI18n';
import { ModuleTypeEnum } from '../../../../lib/enums/ModuleTypeEnum';

interface IProps {
  requestModel: UserModuleRequestModel;
  disabled?: boolean;
}

export const ModuleAdministration: React.FC<IProps> = (props) => {
  const { requestModel, disabled } = props;
  const { getResource } = useI18n();
  const { data, post } = useApi<ModuleConfiguration>();

  const loadModules = React.useCallback(async () => {
    await post(endpoints.administration.loadModuleConfiguration, requestModel);
  }, [requestModel, post]);

  const getLabel = React.useCallback(
    (type: ModuleTypeEnum) => {
      switch (type) {
        case ModuleTypeEnum.MobileApp:
          return getResource('administration:labelMobileApp');
        case ModuleTypeEnum.SchoolTraining:
          return getResource('administration:labelSchoolTraining');
        case ModuleTypeEnum.SchoolTrainingStatistics:
          return getResource('administration:labelSchoolTrainingStatistics');
      }
    },
    [getResource]
  );

  const onActiveChange = React.useCallback(
    async (key: string, checked: boolean) => {
      const index = parseInt(key);

      const modules = data?.modules.slice() ?? [];

      if (modules.length) {
        modules[index].isActive = checked;

        await post(endpoints.administration.updateModule, modules[index]).then(async () => {
          await loadModules();
        });
      }
    },
    [data, post, loadModules]
  );

  React.useEffect(() => {
    if (requestModel) {
      loadModules();
    }
    // eslint-disable-next-line
  }, []);

  return (
    <List disablePadding>
      {data?.modules.map((module, index) => {
        return (
          <ListItem key={index} divider>
            <SwitchWithLabel
              label={getLabel(module.moduleType)}
              property={index.toString()}
              key={index}
              disabled={disabled ?? true}
              checked={module.isActive}
              onChange={onActiveChange}
            />
          </ListItem>
        );
      })}
    </List>
  );
};

export default ModuleAdministration;
