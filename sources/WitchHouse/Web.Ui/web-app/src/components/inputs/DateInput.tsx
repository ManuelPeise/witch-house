import React from 'react';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import dayjs, { Dayjs } from 'dayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';

interface IProps {
  label: string;
  property: string;
  fullWidth?: boolean;
  dateValue: string | null;
  disablePast?: boolean;
  disableFuture?: boolean;
  minDate?: string;
  onChange: (key: string, value: string) => void;
}

const DateInput: React.FC<IProps> = (props) => {
  const { label, fullWidth, property, disablePast, disableFuture, minDate, dateValue, onChange } = props;

  const handleChange = React.useCallback(
    (value: Dayjs | null) => {
      console.log(value);
      if (value != null) {
        onChange(property, value?.format('DD/MM/YYYY') as string);
      }
    },
    [property, onChange]
  );

  const date = dayjs(dateValue);
  return (
    <LocalizationProvider dateAdapter={AdapterDayjs}>
      <DatePicker
        format="DD/MM/YYYY"
        slotProps={{ textField: { fullWidth: fullWidth } }}
        label={label}
        closeOnSelect
        disablePast={disablePast}
        disableFuture={disableFuture}
        minDate={dayjs(minDate)}
        value={date}
        onChange={handleChange}
      />
    </LocalizationProvider>
  );
};

export default DateInput;
