import { Route, Routes } from "react-router-dom";
import { Home } from "./components/home/Home";
import AuthOutlet from "@auth-kit/react-router/AuthOutlet";
import { Admin } from "./components/admin/Admin";
import { Auth } from "./components/auth/Auth";

export const App = () => {
  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/auth" element={<Auth />} />
      <Route element={<AuthOutlet fallbackPath="/auth" />}>
        <Route path="/admin" element={<Admin />} />
      </Route>
    </Routes>
  );
};
