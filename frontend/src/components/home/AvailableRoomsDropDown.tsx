import { useQuery } from "@tanstack/react-query";
import { VITE_ADMIN_URL } from "../utils/ApiUtil";
import { DropDownList } from "../utils/DropDownList";

export interface Room {
  roomNumber: string;
  availability: boolean;
  hotelId: string;
  roomTypeId: string;
  id: string;
}

interface AvailableRoomsProps {
  hotelId: string;
  setRoom: React.Dispatch<React.SetStateAction<Room | undefined>>;
  setRoomType: React.Dispatch<React.SetStateAction<RoomType | undefined>>;
}

export interface RoomType {
  type: string;
  capacity: number;
  description: string;
  roomPrice: number;
  hotelId: string;
  id: string;
}

export const AvailableRoomsDropDown = ({
  hotelId,
  setRoom,
  setRoomType,
}: AvailableRoomsProps) => {
  const getAvailableRooms = async (hotelId: string) => {
    const res = await fetch(`${VITE_ADMIN_URL}/api/room/available/${hotelId}`);

    const data: Room[] = await res.json();

    return data;
  };

  const getSelectedRoomDetails = async (
    event: React.ChangeEvent<HTMLSelectElement>
  ) => {
    event.preventDefault();
    // Get selected room
    const roomRes = await fetch(
      `${VITE_ADMIN_URL}/api/Room?hotelId=${hotelId}&roomId=${event.target.value}`
    );
    const room: Room = await roomRes.json();
    setRoom(room);

    // Get room type asssociated with selected room
    const roomTypeRes = await fetch(
      `${VITE_ADMIN_URL}/api/RoomType/s?roomTypeId=${room.roomTypeId}&hotelId=${hotelId}`
    );
    const roomType: RoomType = await roomTypeRes.json();
    setRoomType(roomType);
  };

  const {
    data: rooms,
    isLoading,
    isError,
    error,
  } = useQuery({
    queryFn: () => getAvailableRooms(hotelId),
    queryKey: ["rooms"],
    refetchInterval: 30 * 1000, // every 30 seconds
  });

  if (isLoading) {
    return <>Loading...</>;
  }

  if (isError) {
    return <>{error?.message}</>;
  }

  return (
    <DropDownList onChange={getSelectedRoomDetails}>
      <option value={""}>-- Select a Room --</option>
      {rooms?.map((room) => {
        return (
          <option key={room.id} value={room.id}>
            {room.roomNumber}
          </option>
        );
      })}
    </DropDownList>
  );
};
