import useIsAuthenticated from "react-auth-kit/hooks/useIsAuthenticated";
import useSignOut from "react-auth-kit/hooks/useSignOut";
import { useNavigate } from "react-router-dom";

export const Logout = () => {
  const isAuthenticated = useIsAuthenticated();
  const signOut = useSignOut();
  const navigate = useNavigate();

  return (
    <>
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
    </>
  );
};
