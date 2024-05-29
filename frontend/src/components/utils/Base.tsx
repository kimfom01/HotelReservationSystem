import { ReactElement, ReactNode } from "react";
import { Header } from "./Header";

interface Props {
  children: ReactNode | ReactElement;
}

export const Base = ({ children }: Props) => {
  return (
    <div className="container mx-auto h-full px-10 py-5 dark:text-white text-slate-900 flex flex-col gap-10">
      <Header />
      <div className="h-full">{children}</div>
    </div>
  );
};
