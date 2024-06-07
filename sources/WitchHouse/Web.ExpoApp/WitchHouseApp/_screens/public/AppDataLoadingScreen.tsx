import React from 'react';
import { StyleSheet } from 'react-native';
import LoginScreen from './LoginScreen';

const AppDataLoadingScreen: React.FC = () => {
  return <LoginScreen />;
};

const styles = StyleSheet.create({
  overlay: {
    height: '100%',
    width: '100%',
    position: 'absolute',
    left: 0,
    top: 0,
    opacity: 0.8,
    backgroundColor: 'black',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
  },
});
export default AppDataLoadingScreen;
