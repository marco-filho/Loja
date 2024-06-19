import { Controller, useFormContext } from 'react-hook-form';
import TextField from '@mui/material/TextField';
import { DateTimePicker } from '@mui/x-date-pickers/DateTimePicker';

const InputController = (props: {
  name: string;
  label: string;
  type?: string | 'text';
  readOnly?: boolean;
  disabled?: boolean;
  helperText?: string;
  datePicker?: boolean;
  InputProps?: object;
  masker?: (value: string | number) => string | number;
  unmasker?: (maskedValue: string | number) => string | number;
}) => {
  const { control } = useFormContext();

  if (props.datePicker)
    return (
      <Controller
        name={props.name}
        control={control}
        render={({ field: { onChange, onBlur, value, ref }, fieldState }) => (
          <div>
            <DateTimePicker
              value={new Date(value)}
              label={props.label}
              onChange={(v: Date | null | undefined) => {
                if (!Number.isNaN(v?.valueOf())) onChange(v?.toISOString());
                else onChange(null);
              }}
              slots={{
                textField: (params) => (
                  <TextField
                    {...params}
                    onBlur={onBlur}
                    ref={ref}
                    fullWidth
                    variant="outlined"
                    error={!!fieldState.error}
                    helperText={(fieldState.error && fieldState.error.message) || props.helperText}
                  />
                ),
              }}
              disabled={props.disabled}
            />
          </div>
        )}
      />
    );

  return (
    <Controller
      name={props.name}
      control={control}
      render={({ field, fieldState }) => (
        <TextField
          {...field}
          value={props.masker ? props.masker(field.value ?? '') : field.value ?? ''}
          onChange={(e) =>
            props.unmasker ? field.onChange(props.unmasker(e.target.value)) : field.onChange(e)
          }
          required
          type={props.type}
          error={!!fieldState.error}
          helperText={(fieldState.error && fieldState.error.message) || props.helperText}
          label={props.label}
          variant="outlined"
          fullWidth
          disabled={props.disabled}
          InputProps={{ ...props.InputProps, readOnly: props.readOnly }}
        />
      )}
    />
  );
};

export default InputController;
