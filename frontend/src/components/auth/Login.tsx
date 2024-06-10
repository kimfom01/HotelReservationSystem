import { useState } from "react";
import { Button } from "../common/Button";
import { InputField } from "../common/InputField";
import { useMutation } from "@tanstack/react-query";
import { VITE_ADMIN_URL } from "../utils/ApiUtil";
import { useNavigate } from "react-router-dom";
import useSignIn from "react-auth-kit/hooks/useSignIn";
import axios, { AxiosResponse } from "axios";
import { toast } from "react-toastify";

interface LoginForm {
  email: string;
  password: string;
}

interface LoginResponse {
  token: string;
  expires_in: number;
  refresh_token: string;
  refresh_expires_in: number;
  token_type: string;
  status: number;
}

export const Login = () => {
  const signInUser = useSignIn();
  const navigate = useNavigate();
  const [loginForm, setLoginForm] = useState<LoginForm>({
    email: "",
    password: "",
  });

  const handleLogin = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const res: AxiosResponse = await toast.promise(
      axios.post<LoginForm>(
        `${VITE_ADMIN_URL}/api/employee/login`,
        JSON.stringify(loginForm),
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      ),
      {
        pending: "Logging in...",
        success: "User successfully logged in!",
        error: "Error logging in user",
      }
    );

    const data: LoginResponse = await res.data;

    const success = signInUser({
      auth: {
        token: data.token,
        type: "Bearer",
      },
      userState: { email: loginForm.email },
    });

    if (success) {
      navigate("/admin");
    } else {
      console.log("unsuccessful sign in attempt");
    }

    return data;
  };

  const { mutateAsync } = useMutation({
    mutationFn: handleLogin,
  });

  return (
    <div className="">
      <form onSubmit={mutateAsync}>
        <div className="dark:bg-slate-800 shadow-lg bg-white rounded-md grid row-span-10 gap-8 md:text-xl p-8">
          <div className="flex justify-center font-bold text-2xl">
            <h1 className="text-3xl">Login Form</h1>
          </div>
          <div className="flex flex-col gap-2 md:gap-4">
            <label htmlFor="email">Email:</label>
            <InputField
              type="email"
              name="email"
              id="email"
              required
              value={loginForm.email}
              size="md:text-2xl"
              onChange={(e) =>
                setLoginForm({ ...loginForm, email: e.target.value })
              }
            />
          </div>
          <div className="flex flex-col gap-2 md:gap-4">
            <label htmlFor="password">Password:</label>
            <InputField
              type="password"
              name="password"
              required
              id="password"
              value={loginForm.password}
              size="md:text-2xl"
              onChange={(e) =>
                setLoginForm({ ...loginForm, password: e.target.value })
              }
            />
          </div>
          <Button content="Login" />
        </div>
      </form>
    </div>
  );
};
