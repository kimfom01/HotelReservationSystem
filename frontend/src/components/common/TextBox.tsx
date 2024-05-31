interface TextBoxProps {
  id?: string;
  name?: string;
  value?: string | number | readonly string[];
  onChange?: React.ChangeEventHandler<HTMLTextAreaElement>;
}

export const TextBox = ({ id, name, value, onChange }: TextBoxProps) => {
  return (
    <textarea
      className=" bg-white dark:bg-slate-500 text-slate-500 dark:text-white p-1 rounded required:border required:border-red-500 valid:border valid:border-green-500 in-valid:border invalid:border-red-500"
      id={id}
      name={name}
      value={value}
      onChange={onChange}
    ></textarea>
  );
};
