interface InputFieldProps {
  id?: string;
  type?: React.HTMLInputTypeAttribute;
  name?: string;
  required?: boolean;
  min?: string | number;
  max?: string | number;
  value?: string | number | readonly string[];
  size?: string;
  onChange?: React.ChangeEventHandler<HTMLInputElement>;
}

export const InputField = ({
  id,
  type,
  name,
  required,
  min,
  max,
  value,
  size,
  onChange,
}: InputFieldProps) => {
  return (
    <input
      className={`bg-white dark:bg-slate-500 text-slate-500 dark:text-white p-1 rounded-lg required:border-red-500 valid:border-green-500 invalid:border-red-500 out-of-range:border-red-500 in-range:border-green-500 ${size}`}
      id={id}
      type={type}
      name={name}
      max={max}
      required={required}
      min={min}
      value={value}
      onChange={onChange}
    />
  );
};
