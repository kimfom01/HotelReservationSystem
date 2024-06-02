import { InputField } from "../common/InputField";
import { Button } from "../common/Button";
import { useState } from "react";
import useAuthHeader from "react-auth-kit/hooks/useAuthHeader";
import { VITE_ADMIN_URL } from "../utils/ApiUtil";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { Hotel } from "../../models/Hotel";

interface HotelForm {
  name: string;
  location: string;
}
export const CreateHotelForm = () => {
  const [hotelForm, setHotelForm] = useState<HotelForm>({
    name: "",
    location: "",
  });
  const authHeader = useAuthHeader();
  const queryClient = useQueryClient();

  const handleCreateHotel = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const requestHeaders: HeadersInit = new Headers();
    requestHeaders.set("Content-Type", "application/json");
    requestHeaders.set("Authorization", `${authHeader}`);

    const res = await fetch(`${VITE_ADMIN_URL}/api/hotel`, {
      method: "post",
      headers: requestHeaders,
      body: JSON.stringify(hotelForm),
    });

    const data: Hotel = await res.json();

    return data;
  };

  const { mutateAsync } = useMutation({
    mutationFn: handleCreateHotel,
    onSuccess: () => {
      setHotelForm({
        name: "",
        location: "",
      });
      queryClient.invalidateQueries({
        queryKey: ["hotels"],
      });
    },
  });
  return (
    <div>
      <form onSubmit={mutateAsync}>
        <div className="dark:bg-slate-800 bg-white rounded-md grid row-span-10 gap-8 md:text-xl p-8">
          <div className="flex justify-center font-bold text-2xl">
            <h1 className="text-3xl">Create Hotel Form</h1>
          </div>
          <div className="flex flex-col gap-2 md:gap-4">
            <label htmlFor="name">Hotel Name:</label>
            <InputField
              type="text"
              name="name"
              id="name"
              required
              value={hotelForm.name}
              onChange={(e) =>
                setHotelForm({ ...hotelForm, name: e.target.value })
              }
            />
          </div>
          <div className="flex flex-col gap-2 md:gap-4">
            <label htmlFor="location">Location:</label>
            <InputField
              type="text"
              name="location"
              required
              id="location"
              value={hotelForm.location}
              onChange={(e) =>
                setHotelForm({ ...hotelForm, location: e.target.value })
              }
            />
          </div>
          {/* TODO: Add error message  */}
          <Button content="Create Hotel" />
        </div>
      </form>
    </div>
  );
};
