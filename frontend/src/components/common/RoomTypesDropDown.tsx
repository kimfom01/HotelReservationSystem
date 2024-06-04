import { useQuery } from "@tanstack/react-query";
import { RoomType } from "../home/AvailableRoomsDropDown";
import { VITE_ADMIN_URL } from "../utils/ApiUtil";
import { DropDownList } from "./DropDownList";

interface RoomTypesDropDownProps {
  setRoomTypeId: React.Dispatch<React.SetStateAction<string>>;
  hotelId: string;
}

export const RoomTypesDropDown = ({
  setRoomTypeId,
  hotelId,
}: RoomTypesDropDownProps) => {
  const getRoomTypes = async () => {
    const res = await fetch(
      `${VITE_ADMIN_URL}/api/RoomType?hotelId=${hotelId}`
    );

    const data: RoomType[] = await res.json();

    return data;
  };

  const {
    data: roomTypes,
    isLoading,
    isError,
    error,
  } = useQuery({
    queryFn: getRoomTypes,
    queryKey: ["roomTypes"],
    refetchInterval: 30 * 1000, // every 30 seconds
  });

  if (isLoading) {
    return <>Loading...</>;
  }

  if (isError) {
    return <>{error?.message}</>;
  }

  return (
    <DropDownList
      onChange={(event) => {
        event.preventDefault();
        setRoomTypeId(event.target.value);
      }}
    >
      <option value={""}>-- Select a Room Type --</option>
      {roomTypes?.map((roomType) => {
        return (
          <option key={roomType.id} value={roomType.id}>
            {roomType.type} - Capacity: {roomType.capacity}
          </option>
        );
      })}
    </DropDownList>
  );
};
