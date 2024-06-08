import React from 'react';
import { StyleSheet, View, Text, Switch } from 'react-native';
import { ColorEnum } from '../../_lib/enums/ColorEnum';

interface IProps {
  checked: boolean;
  label: string;
  onChange: (checked: boolean) => Promise<void>;
}

const SwitchWithLabel: React.FC<IProps> = (props) => {
  const { checked, label, onChange } = props;

  return (
    <View style={styles.container}>
      <Text style={styles.text}>{label}</Text>
      <Switch value={checked} onValueChange={onChange} />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    display: 'flex',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  text: {
    paddingLeft: 20,
    color: ColorEnum.Gray,
    fontSize: 16,
  },
});
export default SwitchWithLabel;
