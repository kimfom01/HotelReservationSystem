import { useQueryClient, useMutation } from "@tanstack/react-query";
import { useEffect, useState } from "react";
import useAuthHeader from "react-auth-kit/hooks/useAuthHeader";
import { Button } from "../common/Button";
import { HotelsDropDown } from "../common/HotelsDropDown";
import { InputField } from "../common/InputField";
import { VITE_ADMIN_URL } from "../utils/ApiUtil";
import { RoomTypesDropDown } from "../common/RoomTypesDropDown";

interface RoomForm {
  roomNumber: string;
  hotelId: string;
  roomTypeId: string;
}

export const CreateRoomForm = () => {
  const [roomForm, setRoomForm] = useState<RoomForm>({
    roomNumber: "",
    hotelId: "",
    roomTypeId: "",
  });
  const [hotelId, setHotelId] = useState<string>();
  const [roomTypeId, setRoomTypeId] = useState<string>();
  const authHeader = useAuthHeader();
  const queryClient = useQueryClient();

  const handleCreateRoom = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const requestHeaders: HeadersInit = new Headers();
    requestHeaders.set("Content-Type", "application/json");
    requestHeaders.set("Authorization", `${authHeader}`);

    const payload: RoomForm = {
      ...roomForm,
      hotelId: hotelId!,
      roomTypeId: roomTypeId!,
    };

    const res = await fetch(`${VITE_ADMIN_URL}/api/room`, {
      method: "post",
      headers: requestHeaders,
      body: JSON.stringify(payload),
    });

    const data: RoomForm = await res.json();

    return data;
  };

  const { mutateAsync } = useMutation({
    mutationFn: handleCreateRoom,
    onSuccess: () => {
      setRoomForm({
        roomNumber: "",
        hotelId: "",
        roomTypeId: "",
      });
      queryClient.invalidateQueries({
        queryKey: ["roomTypes", "rooms"],
      });
    },
  });

  useEffect(() => {
    queryClient.invalidateQueries({ queryKey: ["roomTypes"] });
  }, [hotelId]);

  return (
    <div>
      <form onSubmit={mutateAsync}>
        <div className="dark:bg-slate-800 bg-white rounded-md grid row-span-10 gap-8 md:text-xl p-8">
          <div className="flex justify-center font-bold text-2xl">
            <h1 className="text-3xl">Create Room Form</h1>
          </div>
          <HotelsDropDown setHotelId={setHotelId} />
          {hotelId && (
            <RoomTypesDropDown
              setRoomTypeId={setRoomTypeId}
              hotelId={hotelId}
            />
          )}
          <div className="flex flex-col gap-2 md:gap-4">
            <label htmlFor="roomNumber">Room Number:</label>
            <InputField
              type="number"
              name="roomNumber"
              id="roomNumber"
              required
              value={roomForm.roomNumber}
              onChange={(e) =>
                setRoomForm({ ...roomForm, roomNumber: e.target.value })
              }
            />
          </div>
          {/* TODO: Add error message  */}
          <Button content="Create Room" />
        </div>
      </form>
    </div>
  );
};
