import { TextField } from '@mui/material';
import React, { memo } from 'react';

interface IProps {
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

const TextInput: React.FC<IProps> = (props) => {
  const { property, disabled, fullWidth, isPassword, label, value, variant, marginBottom, marginTop, onChange } = props;

  const handleChange = React.useCallback(
    (e: React.ChangeEvent<HTMLInputElement>) => {
      onChange(property, e.currentTarget.value as string);
    },
    [property, onChange]
  );

  return (
    <TextField
      sx={{ marginBottom: marginBottom, marginTop: marginTop }}
      autoComplete="off"
      fullWidth={fullWidth}
      disabled={disabled}
      variant={variant}
      value={value}
      type={isPassword ? 'password' : 'text'}
      label={label}
      onChange={handleChange}
    />
  );
};

export default memo(TextInput);
