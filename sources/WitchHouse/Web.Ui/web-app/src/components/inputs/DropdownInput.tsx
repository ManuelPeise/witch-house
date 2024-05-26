import { FormControl, InputLabel, MenuItem, Select, SelectChangeEvent } from '@mui/material';
import React from 'react';
import { DropdownItem } from '../../lib/types';

interface IProps {
  property: string;
  fullWidth?: boolean;
  disabled?: boolean;
  label?: string;
  selectedValue: number;
  items: DropdownItem[];
  onChange: (property: string, item: DropdownItem) => void;
}

const DropdownInput: React.FC<IProps> = (props) => {
  const { fullWidth, label, selectedValue, items, disabled, property, onChange } = props;

  const handleChange = React.useCallback(
    (event: SelectChangeEvent<number>, child: React.ReactNode) => {
      const id = event.target.value as number;
      const item = items.find((x) => x.id === id);

      if (item) {
        onChange(property, item);
      }
    },
    [items, property, onChange]
  );

  return (
    <FormControl fullWidth={fullWidth}>
      <InputLabel>{label}</InputLabel>
      <Select value={selectedValue} disabled={disabled} onChange={handleChange} label={label}>
        {items.map((item, index) => {
          return (
            <MenuItem key={index} value={item.id} disabled={selectedValue === item.id}>
              {item.label}
            </MenuItem>
          );
        })}
      </Select>
    </FormControl>
  );
};

export default DropdownInput;
