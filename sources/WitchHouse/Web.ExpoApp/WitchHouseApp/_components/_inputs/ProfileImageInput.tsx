import React from 'react';
import { Image, Pressable, StyleSheet, View } from 'react-native';
import * as ImagePicker from 'expo-image-picker';
import { Ionicons } from '@expo/vector-icons';
import { ColorEnum } from '../../_lib/enums/ColorEnum';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { AsyncStorageKeyEnum } from '../../_lib/enums/AsyncStorageKeyEnum';

interface IProps {
  size: number;
  variant: 'round' | 'default';
  disabled?: boolean;
  quality?: 0 | 1;
  imageSrc: string;
  onSaveImage: (img: string) => Promise<void>;
}

const base64Prefix = 'data:image/png;base64,';

const ProfileImageInput: React.FC<IProps> = (props) => {
  const { size, variant, disabled, quality, imageSrc, onSaveImage } = props;
  const [image, setImage] = React.useState<string | null>(base64Prefix + imageSrc ?? null);

  const handleImageSelect = React.useCallback(async () => {
    const result = await ImagePicker.launchCameraAsync({
      mediaTypes: ImagePicker.MediaTypeOptions.Images,
      allowsEditing: true,
      aspect: [4, 3],
      quality: quality ?? 0,
      base64: true,
    });

    if (!result.canceled) {
      setImage(`${base64Prefix}${result.assets[0].base64}`);

      await onSaveImage(result.assets[0].base64);
    }
  }, [onSaveImage]);

  return (
    <Pressable
      style={[
        styles.container,
        styles[variant],
        { width: size + 10, height: size + 10, borderRadius: (size + 10) / 2 },
      ]}
      disabled={disabled}
      onPress={handleImageSelect}
    >
      {image && <Image style={[styles.image, styles[variant], { width: size, height: size }]} src={image} />}
      {!image && <Ionicons style={[styles.image, styles[variant]]} name="image-sharp" size={size / 2} />}
    </Pressable>
  );
};

const styles = StyleSheet.create({
  container: {
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: ColorEnum.LightGray,
    padding: 10,
  },
  image: {
    padding: 10,
  },
  round: {
    borderRadius: 50,
  },
  default: {
    borderRadius: 0,
  },
});

export default ProfileImageInput;
