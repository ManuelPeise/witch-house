import React from 'react';
import { StyleSheet, View } from 'react-native';
import { useAuth } from '../../_hooks/useAuth';

const HomeScreen: React.FC = () => {
  const {} = useAuth();

  return <View style={styles.container}></View>;
};

const styles = StyleSheet.create({
  container: {
    padding: 10,
  },
});
export default HomeScreen;
