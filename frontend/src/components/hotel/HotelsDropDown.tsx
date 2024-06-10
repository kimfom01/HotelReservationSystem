import { useQuery } from "@tanstack/react-query";
import { VITE_API_URL } from "../utils/ApiUtil";
import { DropDownList } from "../common/DropDownList";
import { Hotel } from "../../models/Hotel";

interface HotelsDropDownProps {
  setHotelId: React.Dispatch<React.SetStateAction<string>>;
}

export const HotelsDropDown = ({ setHotelId }: HotelsDropDownProps) => {
  const getHotels = async () => {
    const res = await fetch(`${VITE_API_URL}/hotel`);

    const data: Hotel[] = await res.json();

    return data;
  };

  const {
    data: hotels,
    isLoading,
    isError,
    error,
  } = useQuery({
    queryFn: getHotels,
    queryKey: ["hotels"],
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
        setHotelId(event.target.value);
      }}
    >
      <option value={""}>-- Select a Hotel --</option>
      {hotels?.map((hotel) => {
        return (
          <option key={hotel.id} value={hotel.id}>
            {hotel.name}
          </option>
        );
      })}
    </DropDownList>
  );
};
