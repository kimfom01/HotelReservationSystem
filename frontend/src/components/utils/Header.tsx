import useIsAuthenticated from "react-auth-kit/hooks/useIsAuthenticated";
import { Link } from "react-router-dom";

export const Header = () => {
  const isAuthenticated = useIsAuthenticated();

  return (
    <header>
      <div className="flex justify-between items-center">
        <h1 className="text-3xl text-slate-900 dark:text-white font-bold">
          <a href="/">Azure Hotels</a>
        </h1>
        <Link
          to={"/admin"}
          className="dark:text-white visited:text-purple-600 hover:underline"
        >
          Admin
        </Link>
        {isAuthenticated && (
          <Link
            className="dark:text-white visited:text-purple-600 hover:underline"
            to={"/logout"}
          >
            Logout
          </Link>
        )}
      </div>
    </header>
  );
};
