import { TextField } from '@mui/material';
import React, { memo } from 'react';

interface IProps {
  property: string;
  disabled?: boolean;
  fullWidth?: boolean;
  isPassword?: boolean;
  label?: string;
  value: string;
  onChange: (key: string, value: string) => void;
}

const TextInput: React.FC<IProps> = (props) => {
  const { property, disabled, fullWidth, isPassword, label, value, onChange } = props;

  const handleChange = React.useCallback(
    (e: React.ChangeEvent<HTMLInputElement>) => {
      onChange(property, e.currentTarget.value as string);
    },
    [property, onChange]
  );

  return (
    <TextField
      autoComplete="off"
      fullWidth={fullWidth}
      disabled={disabled}
      value={value}
      type={isPassword ? 'password' : 'text'}
      label={label}
      onChange={handleChange}
    />
  );
};

export default memo(TextInput);
