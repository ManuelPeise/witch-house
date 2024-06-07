import React from 'react';
import { StyleSheet, View } from 'react-native';
import { TextInput } from 'react-native-gesture-handler';
import { ColorEnum } from '../../_lib/enums/ColorEnum';
import { FontSizeEnum } from '../../_lib/enums/FontSizeEnum';
import { Colors } from 'react-native/Libraries/NewAppScreen';

interface IProps {
  value: string;
  placeholder?: string;
  onChange: (value: string) => void;
  validate?: () => void;
  isPassword?: boolean;
  disabled?: boolean;
}

const TextField: React.FC<IProps> = (props) => {
  const { value, disabled, isPassword, placeholder, onChange, validate } = props;

  const [focused, setFocused] = React.useState<boolean>(false);

  const onBlur = React.useCallback(() => {
    validate && validate();
    setFocused(false);
  }, [validate]);

  const onFocus = React.useCallback(() => {
    setFocused(true);
  }, []);

  return (
    <View style={styles.container}>
      <TextInput
        style={[styles.field, focused ? styles.focused : null]}
        editable={!disabled}
        secureTextEntry={isPassword}
        value={value}
        placeholder={placeholder}
        // underlineColorAndroid={focused ? ColorEnum.Blue : ColorEnum.Black}
        onChangeText={onChange}
        onEndEditing={onFocus}
        onBlur={onBlur}
      />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    width: '100%',
    padding: 4,
  },
  field: {
    width: '100%',
    padding: 0,
    margin: 0,
    borderBottomWidth: 2,
    fontSize: FontSizeEnum.md,
  },
  focused: {
    borderBottomColor: ColorEnum.Blue,
  },
});
export default TextField;
