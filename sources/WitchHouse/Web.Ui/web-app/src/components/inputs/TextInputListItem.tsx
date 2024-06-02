import { ListItem } from '@mui/material';
import React from 'react';
import TextInput from './TextInput';

interface ITextFieldProps {
  property: string;
  disabled?: boolean;
  fullWidth?: boolean;
  isPassword?: boolean;
  label?: string;
  value: string;
  variant?: 'filled' | 'outlined' | 'standard';
  marginBottom?: number;
  marginTop?: number;
  onChange: (key: string, value: string) => void;
}

interface IProps {
  textFieldProps: ITextFieldProps;
  hasDivider?: boolean;
}

const TextInputListItem: React.FC<IProps> = (props) => {
  return (
    <ListItem divider={props.hasDivider}>
      <TextInput {...props.textFieldProps} />
    </ListItem>
  );
};

export default TextInputListItem;
