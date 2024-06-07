import React from 'react';
import { StyleSheet, Text, View } from 'react-native';
import AuthPageWrapper from '../../_components/_wrappers/AuthPageWrapper';
import LinkButton from '../../_components/_inputs/LinkButton';
import { NavigationTypeEnum } from '../../_lib/enums/NavigationTypeEnum';
import { useI18n } from '../../_hooks/useI18n';

const WelcomeScreen: React.FC = () => {
  const { getResource } = useI18n();
  return (
    <AuthPageWrapper>
      <View style={styles.container}>
        <LinkButton to={NavigationTypeEnum.Login} label={getResource('common:start')} />
      </View>
    </AuthPageWrapper>
  );
};

const styles = StyleSheet.create({
  container: {
    height: '100%',
    width: '100%',
    display: 'flex',
    justifyContent: 'flex-end',
    padding: 8,
    paddingBottom: 32,
  },
});
export default WelcomeScreen;
