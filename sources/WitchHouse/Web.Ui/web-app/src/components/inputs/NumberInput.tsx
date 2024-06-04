import { TextField } from '@mui/material';
import React, { memo } from 'react';

interface IProps {
  property: string;
  disabled?: boolean;
  fullWidth?: boolean;
  isPassword?: boolean;
  label?: string;
  value: number;
  variant?: 'filled' | 'outlined' | 'standard';
  marginBottom?: number;
  marginTop?: number;
  onChange: (key: string, value: string) => void;
}

const regex = /^[0-9\b]+$/;

const NumberInput: React.FC<IProps> = (props) => {
  const { property, disabled, fullWidth, isPassword, label, value, variant, marginBottom, marginTop, onChange } = props;

  const handleChange = React.useCallback(
    (e: React.ChangeEvent<HTMLInputElement>) => {
      const value = e.currentTarget.value as string;

      if (value === '' || regex.test(value)) {
        onChange(property, value);
      }
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
      value={value ?? 0}
      inputProps={{ style: { textAlign: 'end' } }}
      type={isPassword ? 'password' : 'text'}
      label={label}
      onChange={handleChange}
    />
  );
};

export default memo(NumberInput);
