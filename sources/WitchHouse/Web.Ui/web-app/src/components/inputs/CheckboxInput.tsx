import { Checkbox, FormControlLabel } from '@mui/material';
import React from 'react';
import { CheckboxProps } from '../../lib/types';

interface IProps extends CheckboxProps {}

const CheckboxInput: React.FC<IProps> = (props) => {
  const { label, property, checked, disabled, onChange } = props;

  const handleChange = React.useCallback(
    (event: React.ChangeEvent<HTMLInputElement>, checked: boolean) => {
      onChange(property, event.currentTarget.checked);
    },
    [property, onChange]
  );

  return (
    <FormControlLabel
      label={label}
      sx={{ color: disabled ? 'gray' : '' }}
      labelPlacement="end"
      control={<Checkbox disabled={disabled} checked={checked} onChange={handleChange} />}
    />
  );
};

export default CheckboxInput;
