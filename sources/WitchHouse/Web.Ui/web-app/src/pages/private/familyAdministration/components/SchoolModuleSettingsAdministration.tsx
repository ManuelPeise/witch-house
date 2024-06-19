import { List } from '@mui/material';
import React from 'react';
import { useApi } from '../../../../hooks/useApi';
import { ModuleSettings, SchoolModule, UserDataModel } from '../types';
import { endpoints } from '../../../../lib/api/apiConfiguration';
import SchoolModuleSettings from './SchoolModuleSettings';
import NoContentPlaceholder from './NoContentPlaceholder';
import { useI18n } from '../../../../hooks/useI18n';
import { ResponseMessage } from '../../../../lib/api/types';

interface IProps {
  selectedUser: UserDataModel;
  disabled: boolean;
}

const SchoolModuleSettingsAdministration: React.FC<IProps> = (props) => {
  const { selectedUser, disabled } = props;
  const { data, get, post } = useApi<ResponseMessage<SchoolModule>>();
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

  if (data == null || !data.data == null) {
    return <NoContentPlaceholder label={getResource('administration:errorNoActiveModules')} />;
  }

  console.log(data);
  return (
    <List disablePadding sx={{ padding: '2rem', width: '100%' }}>
      <SchoolModuleSettings
        key={'schoolModuleSettings'}
        module={data.data}
        disabled={disabled}
        onSave={handleUpdateSettings}
      />
      ;
    </List>
  );
};

export default SchoolModuleSettingsAdministration;
