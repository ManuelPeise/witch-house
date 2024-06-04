import React from 'react';
import { Grid, List, ListItem, Typography } from '@mui/material';
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

export const ModuleSettingsForm: React.FC<IProps> = (props) => {
  const { requestModel, disabled } = props;
  const { getResource } = useI18n();
  const { data, post } = useApi<ModuleConfiguration>();

  const loadModules = React.useCallback(async () => {
    await post(endpoints.administration.loadModuleConfiguration, requestModel);
  }, [requestModel, post]);

  const getLabel = React.useCallback(
    (type: ModuleTypeEnum) => {
      switch (type) {
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
    <Grid container padding={5} justifyContent="center">
      <Grid item xs={12} sx={{ padding: '1rem' }}>
        <Typography sx={{ paddingLeft: '1.5rem' }} variant="h6">
          {getResource('administration:labelModuleAdministration')}
        </Typography>
      </Grid>
      <Grid item xs={12}>
        <List>
          {data?.modules.map((module, index) => {
            return (
              <ListItem divider>
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
      </Grid>
    </Grid>
  );
};

export default ModuleSettingsForm;
