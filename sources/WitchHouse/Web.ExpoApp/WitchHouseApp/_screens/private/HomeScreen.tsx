import React from 'react';
import { StyleSheet, View, Text } from 'react-native';
import { useAuth } from '../../_hooks/useAuth';
import { useI18n } from '../../_hooks/useI18n';

const HomeScreen: React.FC = () => {
  const { loginResult } = useAuth();
  const { getUserDataReducerState } = useAuth();
  const { getResource } = useI18n();

  const userData = getUserDataReducerState();

  return (
    <View style={styles.container}>
      <Text style={styles.text}>{getResource('common:labelGreeting').replace('{user}', userData.firstName)}</Text>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    padding: 10,
    height: '100%',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
  },
  text: {
    textAlign: 'center',
    fontSize: 30,
  },
});
export default HomeScreen;
