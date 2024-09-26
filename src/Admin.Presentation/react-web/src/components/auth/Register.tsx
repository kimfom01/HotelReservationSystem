import { useMutation } from "@tanstack/react-query";
import { useState } from "react";
import { InputField } from "../common/InputField";
import { Button } from "../common/Button";
import { VITE_API_URL } from "../utils/ApiUtil";
import { toast } from "react-toastify";
import axios, { AxiosResponse } from "axios";

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

  const reset = () => {
    setRegisterForm({
      firstName: "",
      lastName: "",
      email: "",
      password: "",
      confirmPassword: "",
    });
  };

  const handleRegister = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const res: AxiosResponse = await toast.promise(
      axios.post<RegisterForm>(
        `${VITE_API_URL}/user/register`,
        JSON.stringify(registerForm),
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      ),
      {
        pending: "Registering...",
        success: "User successfully registered!",
        error: "Error registering new user",
      }
    );

    if (res.status === 204) {
      reset();
    }

    const data = await res.data;

    return data;
  };

  const { mutateAsync } = useMutation({
    mutationFn: handleRegister,
    onSuccess: () => {},
  });

  return (
    <div className="">
      <form onSubmit={mutateAsync}>
        <div className="dark:bg-slate-800 shadow-lg bg-white rounded-md grid row-span-10 gap-8 md:text-xl p-8">
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
          <Button content="Register" />
        </div>
      </form>
    </div>
  );
};
