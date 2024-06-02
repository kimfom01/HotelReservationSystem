import { VITE_ADMIN_URL } from "../utils/ApiUtil";
import { RoomType } from "./AvailableRoomsDropDown";
import { useQuery } from "@tanstack/react-query";

interface RoomTypeDetailsRowProp {
  name: string;
  item?: string;
}

const RoomTypeDetailsRow = ({ name, item }: RoomTypeDetailsRowProp) => {
  return (
    <div className="grid grid-cols-2 border-b dark:border-slate-400 last:border-none">
      <div className="self-center">
        <span className="lg:bg-slate-100 lg:dark:bg-slate-900 rounded lg:p-2">
          {name}:
        </span>
      </div>
      {item && <div className="self-center">{item}</div>}
    </div>
  );
};

export interface RoomDetailsProps {
  roomTypeId: string;
  hotelId: string;
  setCapacity: React.Dispatch<React.SetStateAction<number>>;
}

export const RoomDetails = ({
  roomTypeId,
  hotelId,
  setCapacity,
}: RoomDetailsProps) => {
  const getRoomDetails = async () => {
    const res = await fetch(
      `${VITE_ADMIN_URL}/api/RoomType/s?roomTypeId=${roomTypeId}&hotelId=${hotelId}`
    );

    const data: RoomType = await res.json();

    setCapacity(data.capacity);

    return data;
  };

  const {
    data: roomType,
    isLoading,
    isError,
  } = useQuery({
    queryFn: getRoomDetails,
    queryKey: ["roomDetails"],
    refetchInterval: 30 * 1000, // every 30 seconds
  });

  if (isLoading) {
    return <>Loading...</>;
  }

  if (isError) {
    return <>Error getting room details, please try again</>;
  }

  return (
    <div className="dark:bg-slate-800 bg-white rounded-lg p-6 grid row-span-10 gap-4">
      <div className="text-3xl font-bold flex justify-center">Room Details</div>
      <RoomTypeDetailsRow
        item={`${roomType?.description}`}
        name="Description"
      />
      <RoomTypeDetailsRow item={`${roomType?.type}`} name="Type" />
      <RoomTypeDetailsRow item={`${roomType?.capacity}`} name="Capacity" />
      <RoomTypeDetailsRow item={`${roomType?.roomPrice}`} name="Price" />
    </div>
  );
};
