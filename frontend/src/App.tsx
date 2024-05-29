import { Route, Routes } from "react-router-dom";
import { Home } from "./components/home/Home";
import AuthOutlet from "@auth-kit/react-router/AuthOutlet";
import { Login } from "./components/auth/Login";
import { Register } from "./components/auth/Register";
import { Admin } from "./components/admin/Admin";

export const App = () => {
  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/login" element={<Login />} />
      <Route path="/register" element={<Register />} />
      <Route element={<AuthOutlet fallbackPath="/login" />}>
        <Route path="/admin" element={<Admin />} />
      </Route>
    </Routes>
  );
};
