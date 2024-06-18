import { IconButton, List, ListItem } from '@mui/material';
import React from 'react';
import VerticalTabbedPage from '../../../components/layout/VerticalTabbedPage';
import { useI18n } from '../../../hooks/useI18n';
import { useAuth } from '../../../lib/AuthContext';
import UserListItemMenu from './components/UserListItemMenu';
import TextInput from '../../../components/inputs/TextInput';
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
import { useApi } from '../../../hooks/useApi';
import { UserDataModel } from './types';
import { endpoints } from '../../../lib/api/apiConfiguration';
import UserDetailsForm from './components/UserDetailsForm';
import { FamilyAdministrationSectionEnum } from './enums/FamilyAdministrationSectionEnum';
import { FamilyMemberModel, FamilyMemberUpdate } from '../../public/Auth/types';
import AddUserForm from './components/AddUserForm';
import ModuleAdministration from './components/ModuleAdministration';
import ModuleSettingsAdministration from './components/SchoolModuleSettingsAdministration';
import SectionLayout from './components/SectionLayout';

interface IProps {
  filterText: string;
  users: UserDataModel[];
  onFilterTextChanged: (key: string, filterText: string) => void;
  onSaveFamilyMember: (userData: UserDataModel) => Promise<void>;
  onUpdateFamilyMember: (userData: UserDataModel) => Promise<void>;
}

const FamilyAdministrationContainer: React.FC = () => {
  const { loginResult } = useAuth();
  const { data, get, post } = useApi<UserDataModel[]>();

  const [filter, setFilter] = React.useState<string>('');

  const getUserData = React.useCallback(async () => {
    if (loginResult != null) {
      get(endpoints.administration.getFamilyUsers.replace('{id}', loginResult?.userData.familyGuid ?? ''));
    }
  }, [loginResult, get]);

  const onSaveFamilyMember = React.useCallback(
    async (userData: UserDataModel) => {
      const model: FamilyMemberModel = {
        familyGuid: userData.familyGuid,
        firstName: userData.firstName,
        lastName: userData.lastName,
        userName: userData.userName,
      };

      await post(endpoints.administration.addFamilyMember, JSON.stringify(model)).then(async () => {
        await getUserData();
      });
    },
    [post, getUserData]
  );

  const onUpdateFamilyMember = React.useCallback(
    async (userData: UserDataModel) => {
      const model: FamilyMemberUpdate = {
        userId: userData.userId,
        isActive: userData.isActive,
        role: userData.role,
      };

      await post(endpoints.administration.updateFamilyMember, JSON.stringify(model)).then(async () => {
        await getUserData();
      });
    },
    [post, getUserData]
  );

  React.useEffect(() => {
    getUserData();
    // eslint-disable-next-line
  }, []);

  const handleFilterChanged = React.useCallback((key: string, filterText: string) => {
    setFilter(filterText);
  }, []);

  if (data == null) {
    return null;
  }

  return (
    <FamilyAdministrationPage
      filterText={filter}
      users={data}
      onFilterTextChanged={handleFilterChanged}
      onSaveFamilyMember={onSaveFamilyMember}
      onUpdateFamilyMember={onUpdateFamilyMember}
    />
  );
};

const FamilyAdministrationPage: React.FC<IProps> = (props) => {
  const { filterText, users, onFilterTextChanged, onSaveFamilyMember, onUpdateFamilyMember } = props;
  const { getResource } = useI18n();
  const { loginResult } = useAuth();
  const [selectedUserId, setSelectedUserId] = React.useState<number>(-1);
  const [sectionType, setSectionType] = React.useState<FamilyAdministrationSectionEnum>(
    FamilyAdministrationSectionEnum.Add
  );

  const handleSectionChanged = React.useCallback((section: FamilyAdministrationSectionEnum, index: number) => {
    setSelectedUserId(index);
    setSectionType(section);
  }, []);

  const selectedUser = React.useMemo(() => {
    if (selectedUserId !== -1) {
      return users[selectedUserId];
    }

    return {} as UserDataModel;
  }, [selectedUserId, users]);

  const filteredUsers = React.useMemo(() => {
    if (filterText === '') {
      return users;
    }

    return users.filter((usr) => usr.userName.toLocaleLowerCase().startsWith(filterText.toLocaleLowerCase())) ?? [];
  }, [users, filterText]);

  if (loginResult == null) {
    return null;
  }

  return (
    <VerticalTabbedPage
      evaluation={10}
      paperHeight={800}
      minWidth={340}
      component={
        <List disablePadding>
          <ListItem
            sx={{
              display: 'flex',
              alignContent: 'space-between',
              gap: 4,
              padding: '1rem',
              paddingLeft: '2rem',
              paddingRight: '2rem',
            }}
          >
            <TextInput
              property="filter"
              fullWidth
              variant="standard"
              label={getResource('common:labelFilter')}
              value={filterText}
              onChange={onFilterTextChanged}
            />
            <IconButton
              sx={{ padding: '.2rem' }}
              onClick={() => handleSectionChanged(FamilyAdministrationSectionEnum.Add, -1)}
            >
              <AddCircleOutlineIcon sx={{ height: 40, width: 40 }} />
            </IconButton>
          </ListItem>
          {filteredUsers.map((user, index) => {
            return (
              <UserListItemMenu
                key={index}
                index={index}
                firstName={user.firstName}
                lastName={user.lastName}
                onSectionChanged={handleSectionChanged}
              />
            );
          })}
        </List>
      }
    >
      {sectionType === FamilyAdministrationSectionEnum.Add && (
        <SectionLayout caption={getResource('administration:captionAddFamilyUser')}>
          <AddUserForm onSave={onSaveFamilyMember} />
        </SectionLayout>
      )}

      {sectionType === FamilyAdministrationSectionEnum.Details && (
        <SectionLayout caption={getResource('administration:captionUserDetails')}>
          <UserDetailsForm
            userData={selectedUser}
            disabled={loginResult.userData.userId === selectedUser.userId}
            onSave={onUpdateFamilyMember}
          />
        </SectionLayout>
      )}
      {sectionType === FamilyAdministrationSectionEnum.Modules && (
        <SectionLayout caption={getResource('administration:captionModuleAdministration')}>
          <ModuleAdministration
            requestModel={{
              userGuid: selectedUser.userId,
              familyGuid: selectedUser.familyGuid,
              roleId: selectedUser.role,
            }}
            disabled={false}
          />
        </SectionLayout>
      )}
      {sectionType === FamilyAdministrationSectionEnum.ModuleSettings && (
        <SectionLayout caption={getResource('administration:captionSchoolModuleSettings')}>
          <ModuleSettingsAdministration
            selectedUser={selectedUser}
            disabled={selectedUser.userId === loginResult.userData.userId}
          />
        </SectionLayout>
      )}
    </VerticalTabbedPage>
  );
};

export default FamilyAdministrationContainer;
