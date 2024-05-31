import { useMutation } from "@tanstack/react-query";
import { useState } from "react";
import { InputField } from "../common/InputField";
import { Button } from "../common/Button";
import { VITE_ADMIN_URL } from "../utils/ApiUtil";

interface RegisterForm {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  confirmPassword: string;
}

export const Register = () => {
  const [registerForm, setRegisterForm] = useState<RegisterForm>({
    firstName: "",
    lastName: "",
    email: "",
    password: "",
    confirmPassword: "",
  });

  const handleRegister = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const res = await fetch(`${VITE_ADMIN_URL}/api/employee/register`, {
      method: "post",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(registerForm),
    });

    const data = await res.json();

    return data;
  };

  const { mutateAsync } = useMutation({
    mutationFn: handleRegister,
    onSuccess: () => {
      setRegisterForm({
        firstName: "",
        lastName: "",
        email: "",
        password: "",
        confirmPassword: "",
      });
    },
  });

  return (
    <div className="">
      <form onSubmit={mutateAsync}>
        <div className="border rounded-md border-slate-500 dark:border-white grid row-span-10 gap-8 md:text-xl p-8">
          <div className="flex justify-center font-bold text-2xl">
            <h1 className="text-3xl">Registration Form</h1>
          </div>
          <div className="flex flex-col gap-2 md:gap-4">
            <label htmlFor="firstName">First Name:</label>
            <InputField
              type="text"
              name="firstName"
              id="firstName"
              required
              value={registerForm.firstName}
              size="md:text-2xl"
              onChange={(e) =>
                setRegisterForm({ ...registerForm, firstName: e.target.value })
              }
            />
          </div>
          <div className="flex flex-col gap-2 md:gap-4">
            <label htmlFor="lastName">Last Name:</label>
            <InputField
              type="text"
              name="lastName"
              id="lastName"
              required
              value={registerForm.lastName}
              size="md:text-2xl"
              onChange={(e) =>
                setRegisterForm({ ...registerForm, lastName: e.target.value })
              }
            />
          </div>
          <div className="flex flex-col gap-2 md:gap-4">
            <label htmlFor="email">Email:</label>
            <InputField
              type="email"
              name="email"
              id="email"
              required
              value={registerForm.email}
              size="md:text-2xl"
              onChange={(e) =>
                setRegisterForm({ ...registerForm, email: e.target.value })
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
              value={registerForm.password}
              size="md:text-2xl"
              onChange={(e) =>
                setRegisterForm({ ...registerForm, password: e.target.value })
              }
            />
          </div>
          <div className="flex flex-col gap-2 md:gap-4">
            <label htmlFor="confirmPassword">Confirm Password:</label>
            <InputField
              type="password"
              name="confirmPassword"
              required
              id="confirmPassword"
              value={registerForm.confirmPassword}
              size="md:text-2xl"
              onChange={(e) =>
                setRegisterForm({
                  ...registerForm,
                  confirmPassword: e.target.value,
                })
              }
            />
          </div>
          {/* TODO: Add error message  */}
          <Button content="Register" />
        </div>
      </form>
    </div>
  );
};
