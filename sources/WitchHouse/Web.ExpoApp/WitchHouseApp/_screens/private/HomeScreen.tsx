import React from 'react';
import { Text, View } from 'react-native';
import IconButton from '../../_components/_inputs/IconButton';

const HomeScreen: React.FC = () => {
  return (
    <View>
      <Text>Home</Text>
      <IconButton size={50} padding={2} icon="home" onPress={async () => {}} />
    </View>
  );
};

export default HomeScreen;
