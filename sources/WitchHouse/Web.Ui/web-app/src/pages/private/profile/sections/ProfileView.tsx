import React from 'react';
import { DropdownItem, ProfileData } from '../../../../lib/types';
import { Grid } from '@mui/material';
import FormItemWrapper from '../../../../components/wrappers/FormItemWrapper';
import TextInput from '../../../../components/inputs/TextInput';
import { useI18n } from '../../../../hooks/useI18n';
import DateInput from '../../../../components/inputs/DateInput';
import SubmitButton from '../../../../components/buttons/SubmitButton';
import DropdownInput from '../../../../components/inputs/DropdownInput';

interface IProps {
  profile: ProfileData;
  updateProfile: (profile: ProfileData) => Promise<void>;
}

const ProfileView: React.FC<IProps> = (props) => {
  const { profile, updateProfile } = props;
  const { getResource, changeLanguage } = useI18n();

  const [profileData, setProfileData] = React.useState(profile);

  const languageItems = React.useMemo((): DropdownItem[] => {
    return [
      { id: 0, value: 'De', label: getResource('common:labelGerman') },
      { id: 1, value: 'En', label: getResource('common:labelEnglish') },
    ];
  }, [getResource]);

  const handleChanged = React.useCallback(
    (key: string, value: string) => {
      if (profileData) {
        setProfileData({ ...profileData, [key]: value });
      }
    },
    [profileData]
  );

  const handleDateChanged = React.useCallback(
    (key: string, value: string) => {
      if (profileData) {
        setProfileData({ ...profileData, [key]: value });
      }
    },
    [profileData]
  );

  const handleLanguageChanged = React.useCallback(
    (key: string, item: DropdownItem) => {
      const lng = item.value.toLocaleLowerCase();
      if (lng === 'en' || lng === 'de') {
        changeLanguage(lng);
        setProfileData({ ...profileData, [key]: item.value.toLocaleLowerCase() });
      }
    },
    [profileData, changeLanguage]
  );

  const handleCancel = React.useCallback(async () => {
    if (profile) {
      setProfileData(profile);
      if (profile.culture === 'en' || profile.culture === 'de') {
        changeLanguage(profile.culture);
      }
    }
  }, [profile, changeLanguage]);

  const cancelDisabled = React.useMemo(() => {
    return JSON.stringify(profile) === JSON.stringify(profileData);
  }, [profile, profileData]);

  const saveDisabled = React.useMemo(() => {
    return JSON.stringify(profile) === JSON.stringify(profileData);
  }, [profile, profileData]);

  return (
    <Grid container style={{ width: '100%', padding: '2rem' }}>
      <Grid item xs={12}>
        <FormItemWrapper marginVertical={25}>
          <TextInput
            property="userId"
            fullWidth
            disabled
            label={getResource('common:labelUserId')}
            value={profileData.userId}
            onChange={handleChanged}
          />
        </FormItemWrapper>
        <FormItemWrapper marginVertical={25}>
          <TextInput
            property="familyGuid"
            fullWidth
            disabled
            label={getResource('common:labelFamilyId')}
            value={profileData.familyGuid ?? ''}
            onChange={handleChanged}
          />
        </FormItemWrapper>
        <FormItemWrapper marginVertical={25}>
          <DropdownInput
            property="culture"
            label={getResource('common:labelCulture')}
            fullWidth
            selectedValue={
              profileData.culture
                ? languageItems.find((x) => x.value.toLowerCase() === profileData.culture?.toLowerCase())?.id ?? 0
                : 0
            }
            onChange={handleLanguageChanged}
            items={languageItems}
          />
        </FormItemWrapper>
        <FormItemWrapper marginVertical={25}>
          <TextInput
            property="firstName"
            fullWidth
            label={getResource('common:labelFirstName')}
            value={profileData.firstName}
            onChange={handleChanged}
          />
        </FormItemWrapper>
        <FormItemWrapper marginVertical={25}>
          <TextInput
            property="lastName"
            fullWidth
            label={getResource('common:labelLastName')}
            value={profileData.lastName}
            onChange={handleChanged}
          />
        </FormItemWrapper>
        <FormItemWrapper marginVertical={25}>
          <TextInput
            property="userName"
            fullWidth
            disabled
            label={getResource('common:labelUserName')}
            value={profileData.userName}
            onChange={handleChanged}
          />
        </FormItemWrapper>
        <FormItemWrapper marginVertical={25}>
          <DateInput
            property="dateOfBirth"
            label={getResource('common:labelDateOfBirth')}
            fullWidth
            disableFuture={true}
            disablePast={false}
            minDate="01.01.1900"
            dateValue={profileData.dateOfBirth != null ? profileData.dateOfBirth : null}
            onChange={handleDateChanged}
          />
        </FormItemWrapper>
        <FormItemWrapper marginVertical={25}>
          <Grid>
            <Grid item xs={12} style={{ display: 'flex', justifyContent: 'flex-end', padding: 10, gap: 6 }}>
              <SubmitButton
                title={getResource('common:labelCancel')}
                variant="outlined"
                disabled={cancelDisabled}
                onClick={handleCancel}
              />
              <SubmitButton
                title={getResource('common:labelSave')}
                variant="outlined"
                disabled={saveDisabled}
                onClick={updateProfile.bind(null, profileData)}
              />
            </Grid>
          </Grid>
        </FormItemWrapper>
      </Grid>
    </Grid>
  );
};

export default ProfileView;
