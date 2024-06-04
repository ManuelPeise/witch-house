import { Grid } from '@mui/material';
import React from 'react';
import { useApi } from '../../../../hooks/useApi';
import { ModuleSettings, UserDataModel } from '../types';
import { endpoints } from '../../../../lib/api/apiConfiguration';

interface IProps {
  selectedUser: UserDataModel;
}

const ModuleSettingsAdministration: React.FC<IProps> = (props) => {
  const { selectedUser } = props;
  const { data, get } = useApi<ModuleSettings[]>();

  const loadSettings = React.useCallback(async () => {
    await get(endpoints.administration.loadModuleSettings.replace('{id}', selectedUser.userId));
  }, [selectedUser, get]);

  React.useEffect(() => {
    if (selectedUser) {
      loadSettings();
    }
    // eslint-disable-next-line
  }, []);

  return (
    <Grid container>
      {data?.map((d) => {
        return <label>{d.moduleType}</label>;
      })}
    </Grid>
  );
};

export default ModuleSettingsAdministration;
