import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useState } from "react";
import useAuthHeader from "react-auth-kit/hooks/useAuthHeader";
import { Button } from "../common/Button";
import { InputField } from "../common/InputField";
import { VITE_ADMIN_URL } from "../utils/ApiUtil";
import { TextBox } from "../common/TextBox";
import { HotelsDropDown } from "../common/HotelsDropDown";

interface RoomTypeForm {
  type: string;
  capacity: number;
  description: string;
  roomPrice: number;
  hotelId: string;
}

export const CreateRoomTypeForm = () => {
  const [roomTypeForm, setRoomTypeForm] = useState<RoomTypeForm>({
    type: "",
    capacity: 0,
    description: "",
    roomPrice: 0,
    hotelId: "",
  });
  const [hotelId, setHotelId] = useState<string>("");
  const authHeader = useAuthHeader();
  const queryClient = useQueryClient();

  const handleCreateRoomType = async (
    event: React.FormEvent<HTMLFormElement>
  ) => {
    event.preventDefault();

    const requestHeaders: HeadersInit = new Headers();
    requestHeaders.set("Content-Type", "application/json");
    requestHeaders.set("Authorization", `${authHeader}`);

    const payload: RoomTypeForm = {
      ...roomTypeForm,
      hotelId: hotelId,
    };

    const res = await fetch(`${VITE_ADMIN_URL}/api/roomtype`, {
      method: "post",
      headers: requestHeaders,
      body: JSON.stringify(payload),
    });

    const data: RoomTypeForm = await res.json();

    return data;
  };

  const { mutateAsync } = useMutation({
    mutationFn: handleCreateRoomType,
    onSuccess: () => {
      setRoomTypeForm({
        type: "",
        capacity: 0,
        description: "",
        roomPrice: 0,
        hotelId: "",
      });
      queryClient.invalidateQueries({
        queryKey: ["roomTypes"],
      });
    },
  });
  return (
    <div>
      <form onSubmit={mutateAsync}>
        <div className="dark:bg-slate-800 shadow-lg bg-white rounded-md grid row-span-10 gap-8 md:text-xl p-8">
          <div className="flex justify-center font-bold text-2xl">
            <h1 className="text-3xl">Create Room Type Form</h1>
          </div>
          <HotelsDropDown setHotelId={setHotelId} />
          <div className="flex flex-col gap-2 md:gap-4">
            <label htmlFor="type">Room Type:</label>
            <InputField
              type="text"
              name="type"
              id="type"
              required
              value={roomTypeForm.type}
              onChange={(e) =>
                setRoomTypeForm({ ...roomTypeForm, type: e.target.value })
              }
            />
          </div>
          <div className="flex flex-col gap-2 md:gap-4">
            <label htmlFor="capacity">Capacity:</label>
            <InputField
              type="number"
              name="capacity"
              required
              id="capacity"
              value={roomTypeForm.capacity}
              onChange={(e) =>
                setRoomTypeForm({
                  ...roomTypeForm,
                  capacity: parseInt(e.target.value),
                })
              }
            />
          </div>
          <div className="flex flex-col gap-2 md:gap-4">
            <label htmlFor="description">Description:</label>
            <TextBox
              id="description"
              name="description"
              value={roomTypeForm.description}
              onChange={(e) =>
                setRoomTypeForm({
                  ...roomTypeForm,
                  description: e.target.value,
                })
              }
            ></TextBox>
          </div>

          <div className="flex flex-col gap-2 md:gap-4">
            <label htmlFor="roomPrice">Room Price:</label>
            <InputField
              type="number"
              name="roomPrice"
              required
              id="roomPrice"
              value={roomTypeForm.roomPrice}
              onChange={(e) =>
                setRoomTypeForm({
                  ...roomTypeForm,
                  roomPrice: parseInt(e.target.value),
                })
              }
            />
          </div>
          {/* TODO: Add error message  */}
          <Button content="Create Room Type" />
        </div>
      </form>
    </div>
  );
};
