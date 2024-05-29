import { ReactElement, ReactNode } from "react";

interface DropDownListProps {
  onChange?: React.ChangeEventHandler<HTMLSelectElement>;
  children?: ReactNode | ReactElement;
  id?: string;
  name?: string;
  required?: boolean;
}

export const DropDownList = ({
  onChange,
  children,
  id,
  name,
  required,
}: DropDownListProps) => {
  return (
    <select
      id={id}
      name={name}
      onChange={onChange}
      required={required}
      className="bg-gray-50 border border-slate-500 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
    >
      {children}
    </select>
  );
};
