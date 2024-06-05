import { List } from '@mui/material';
import React from 'react';
import { useApi } from '../../../../hooks/useApi';
import { ModuleSettings, UserDataModel } from '../types';
import { endpoints } from '../../../../lib/api/apiConfiguration';
import SchoolModuleSettings from './SchoolModuleSettings';
import NoContentPlaceholder from './NoContentPlaceholder';
import { useI18n } from '../../../../hooks/useI18n';

interface IProps {
  selectedUser: UserDataModel;
  disabled: boolean;
}

const SchoolModuleSettingsAdministration: React.FC<IProps> = (props) => {
  const { selectedUser, disabled } = props;
  const { data, get, post } = useApi<ModuleSettings[]>();
  const { getResource } = useI18n();

  const loadSettings = React.useCallback(async () => {
    await get(endpoints.administration.loadModuleSettings.replace('{id}', selectedUser.userId));
  }, [selectedUser, get]);

  React.useEffect(() => {
    if (selectedUser) {
      loadSettings();
    }
  }, [selectedUser, loadSettings]);

  const handleUpdateSettings = React.useCallback(
    async (settings: ModuleSettings) => {
      await post(endpoints.administration.updateModuleSettings, settings).then(async () => {
        await loadSettings();
      });
    },
    [post, loadSettings]
  );

  if (data == null || !data.length) {
    return <NoContentPlaceholder label={getResource('administration:errorNoActiveModules')} />;
  }

  return (
    <List disablePadding sx={{ padding: '2rem', width: '100%' }}>
      {data?.map((model, index) => {
        return <SchoolModuleSettings key={index} model={model} disabled={disabled} onSave={handleUpdateSettings} />;
      })}
    </List>
  );
};

export default SchoolModuleSettingsAdministration;
