import useIsAuthenticated from "react-auth-kit/hooks/useIsAuthenticated";
import useSignOut from "react-auth-kit/hooks/useSignOut";
import { Link, useNavigate } from "react-router-dom";

export const Header = () => {
  const isAuthenticated = useIsAuthenticated();
  const signOut = useSignOut();
  const navigate = useNavigate();

  return (
    <header>
      <div className="flex justify-between items-center">
        <h1 className="text-3xl text-slate-900 dark:text-white font-bold">
          <a href="/">Azure Hotels</a>
        </h1>
        {!isAuthenticated && (
          <Link
            to={"/admin"}
            className="dark:text-white visited:text-purple-600 hover:underline"
          >
            Admin
          </Link>
        )}
        {isAuthenticated && (
          <button
            className="dark:text-white visited:text-purple-600 hover:underline"
            onClick={() => {
              signOut();
              navigate("/auth");
            }}
          >
            Logout
          </button>
        )}
      </div>
    </header>
  );
};
