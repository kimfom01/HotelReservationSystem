import { Link } from "react-router-dom";
import { Logout } from "../auth/Logout";

export const Header = () => {
  return (
    <header>
      <div className="flex justify-between items-center">
        <h1 className="text-3xl text-slate-900 dark:text-white font-bold">
          <a href="/">Azure Hotels</a>
        </h1>
        <div className="flex gap-6 md:gap-20">
          <Link
            to={"/admin"}
            className="dark:text-white visited:text-purple-600 hover:underline"
          >
            Admin
          </Link>
          <Logout />
        </div>
      </div>
    </header>
  );
};
