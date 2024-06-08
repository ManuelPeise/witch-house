import { Ionicons } from '@expo/vector-icons';
import React from 'react';
import { Pressable, View } from 'react-native';

interface IProps {
  icon: 'log-out' | 'home';
  padding: number;
  size: number;
  onPress: () => Promise<void>;
}

const IconButton: React.FC<IProps> = (props) => {
  const { icon, padding, size, onPress } = props;
  return (
    <Pressable style={{ borderRadius: 50, padding: padding, height: size, width: size }} onPress={onPress}>
      <View style={{ padding: padding }}>
        <Ionicons name={icon} size={30} />
      </View>
    </Pressable>
  );
};

export default IconButton;
