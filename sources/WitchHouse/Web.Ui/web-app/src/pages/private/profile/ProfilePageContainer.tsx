import { Grid, Paper } from '@mui/material';
import React from 'react';
import { useAuth } from '../../../lib/AuthContext';
import { useApi } from '../../../hooks/useApi';
import { ListItemModel, ProfileData } from '../../../lib/types';
import { endpoints } from '../../../lib/api/apiConfiguration';
import LoadingIndicator from '../../../components/LoadingIndicator';
import ListItemMenu from '../../../components/list/ListItemMenu';
import { useI18n } from '../../../hooks/useI18n';
import ProfileView from './sections/ProfileView';
import ChangePasswordView from './sections/ChangePasswordView';

enum ProfileSectionEnum {
  Profile = 0,
  ChangePassword = 1,
}

interface IProps {
  profileData: ProfileData;
  isLoading: boolean;
  selectedItem: number;
  items: ListItemModel[];
  onSelect: (id: number) => void;
  updateProfile: (profile: ProfileData) => Promise<void>;
}

const ProfilePageContainer: React.FC = () => {
  const { loginResult } = useAuth();
  const { getResource } = useI18n();
  const [selectedItem, setSelectedItem] = React.useState<ProfileSectionEnum>(ProfileSectionEnum.Profile);
  const profileDataApi = useApi<ProfileData>();

  const loadProfileData = React.useCallback(async () => {
    if (loginResult != null) {
      await profileDataApi.get(endpoints.getProfile.replace('{model}', loginResult?.userId));
    }
  }, [profileDataApi, loginResult]);

  const updateProfile = React.useCallback(
    async (profile: ProfileData) => {
      await profileDataApi.post(endpoints.updateProfile, JSON.stringify(profile)).then(async () => {
        loadProfileData();
      });
    },
    [profileDataApi, loadProfileData]
  );

  React.useEffect(() => {
    loadProfileData();
    // eslint-disable-next-line
  }, []);

  const listItems = React.useMemo((): ListItemModel[] => {
    return [
      { id: 0, isSelected: selectedItem === ProfileSectionEnum.Profile, value: getResource('common:labelProfileData') },
      {
        id: 1,
        isSelected: selectedItem === ProfileSectionEnum.ChangePassword,
        value: getResource('common:labelChangePassword'),
      },
    ];
  }, [selectedItem, getResource]);

  const onSelectItem = React.useCallback((id: number) => {
    setSelectedItem(id);
  }, []);

  if (profileDataApi.data == null || profileDataApi.isLoading) {
    return null;
  }

  return (
    <ProfilePage
      profileData={profileDataApi.data}
      isLoading={profileDataApi.isLoading}
      selectedItem={selectedItem}
      items={listItems}
      onSelect={onSelectItem}
      updateProfile={updateProfile}
    />
  );
};

const ProfilePage: React.FC<IProps> = (props) => {
  const { profileData, isLoading, selectedItem, items, onSelect, updateProfile } = props;
  return (
    <Grid container style={{ height: '100%' }}>
      <Grid item xs={12}>
        <LoadingIndicator isLoading={isLoading} />
      </Grid>
      <Grid container style={{ display: 'flex', justifyContent: 'center', gap: 24, marginTop: '3rem' }}>
        <Grid item xs={3}>
          <Paper elevation={10} style={{ width: '100%', height: '100%' }}>
            <ListItemMenu selectedItem={selectedItem} onSelect={onSelect} listItems={items} />
          </Paper>
        </Grid>
        <Grid item xs={7}>
          {!isLoading && (
            <Paper elevation={10} style={{ width: '100%', height: '100%' }}>
              {selectedItem === ProfileSectionEnum.Profile && (
                <ProfileView profile={profileData} updateProfile={updateProfile} />
              )}
              {selectedItem === ProfileSectionEnum.ChangePassword && <ChangePasswordView />}
            </Paper>
          )}
        </Grid>
      </Grid>
    </Grid>
  );
};

export default ProfilePageContainer;
