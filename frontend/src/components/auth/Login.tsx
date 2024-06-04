import { useState } from "react";
import { Button } from "../common/Button";
import { InputField } from "../common/InputField";
import { useMutation } from "@tanstack/react-query";
import { VITE_ADMIN_URL } from "../utils/ApiUtil";
import { useNavigate } from "react-router-dom";
import useSignIn from "react-auth-kit/hooks/useSignIn";

interface LoginForm {
  email: string;
  password: string;
}

interface LoginResponse {
  token: string;
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

    const res = await fetch(`${VITE_ADMIN_URL}/api/employee/login`, {
      method: "post",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(loginForm),
    });

    const data: LoginResponse = await res.json();

    return data;
  };

  const { mutateAsync } = useMutation({
    mutationFn: handleLogin,
    onSuccess: (data) => {
      const success = signInUser({
        auth: {
          token: data.token,
          type: "Bearer",
        },
        userState: { email: loginForm.email },
      });

      if (success) {
        console.log("signing in");
        navigate("/admin");
      } else {
        console.log("unsuccessful sign in attempt");
      }
    },
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
          {/* TODO: Add error message  */}
          <Button content="Login" />
        </div>
      </form>
    </div>
  );
};
