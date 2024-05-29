import { Base } from "../utils/Base";
import { useState } from "react";
import {
  AvailableRoomsDropDown,
  Room,
  RoomType,
} from "./AvailableRoomsDropDown";
import { HotelsDropDown } from "./HotelsDropDown";
import Datepicker, {
  DateType,
  DateValueType,
} from "react-tailwindcss-datepicker";
import { DropDownList } from "../utils/DropDownList";
import { VITE_RESERVATION_URL } from "../utils/ApiUtil";

interface Reservation {
  checkIn?: DateType;
  checkOut?: DateType;
  specialRequests: string;
  roomPreferences: string;
  numberOfGuests: number;
  guestProfile?: GuestProfile;
  roomId: string;
  hotelId: string;
}

interface GuestProfile {
  firstName: string;
  lastName: string;
  contactEmail: string;
  sex: string;
  age: number;
  adult: boolean;
}

export const Home = () => {
  const [hotelId, setHotelId] = useState<string>("");
  const [room, setRoom] = useState<Room>({
    roomNumber: "",
    availability: true,
    hotelId: "",
    roomTypeId: "",
    id: "",
  });
  const [roomType, setRoomType] = useState<RoomType>();
  const [dateValue, setDateValue] = useState<DateValueType>({
    startDate: new Date(),
    endDate: null,
  });
  const [reservation, setReservation] = useState<Reservation>({
    checkIn: new Date(),
    checkOut: new Date(),
    specialRequests: "",
    roomPreferences: "",
    numberOfGuests: 1,
    roomId: "",
    hotelId: "",
  });
  const [guestProfile, setGuestProfile] = useState<GuestProfile>({
    firstName: "",
    lastName: "",
    contactEmail: "",
    sex: "",
    age: 0,
    adult: true,
  });

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const payload: Reservation = {
      ...reservation,
      guestProfile: guestProfile,
      hotelId: hotelId,
      roomId: room?.id,
      checkIn: dateValue?.startDate,
      checkOut: dateValue?.endDate,
    };

    const res = await fetch(`${VITE_RESERVATION_URL}/api/Reservation`, {
      method: "post",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(payload),
    });

    const data = await res.json();

    console.table(data);
  };

  return (
    <Base>
      <div className="grid lg:grid-rows-1 h-full grid-cols-1 lg:grid-cols-2 gap-4">
        <div className="grid grid-cols-1 gap-8 w-full lg:w-3/5">
          <div className="row-span-1">
            <HotelsDropDown setHotelId={setHotelId} />
          </div>
          <div className="row-span-1">
            {hotelId && (
              <AvailableRoomsDropDown
                hotelId={hotelId}
                setRoom={setRoom}
                setRoomType={setRoomType}
              />
            )}
          </div>
          <div className="border rounded-lg border-slate-500 dark:border-white p-4 grid row-span-10 gap-4">
            <div className="grid grid-cols-2">
              Room Number: {room && <span>{room.roomNumber}</span>}
            </div>
            <div className="grid grid-cols-2">
              Description:
              {roomType && <span>{roomType.description}</span>}
            </div>
            <div className="grid grid-cols-2">
              Type: {roomType && <span>{roomType.type}</span>}
            </div>
            <div className="grid grid-cols-2">
              Capacity: {roomType && <span>{roomType.capacity}</span>}
            </div>
            <div className="grid grid-cols-2">
              Price: {roomType && <span>{roomType.roomPrice}</span>}
            </div>
          </div>
        </div>
        <div className="grid grid-cols-1 gap-8">
          <div className="border rounded-lg border-slate-500 dark:border-white row-span-11 p-4 w-full h-full">
            <form onSubmit={handleSubmit}>
              <h3 className="text-2xl font-bold text-center mb-8">
                Reservation Form
              </h3>
              <div className="grid gap-4">
                <div className="row-span-1">
                  <Datepicker
                    value={dateValue}
                    showFooter={true}
                    separator={"to"}
                    placeholder={"Select Check in and Checkout Dates"}
                    minDate={new Date()}
                    onChange={(newValue: DateValueType) => {
                      setDateValue(newValue);
                    }}
                  />
                </div>
                <div className="grid lg:grid-cols-2">
                  <label htmlFor="firstName">First Name: </label>
                  <input
                    className=" bg-white dark:bg-slate-500 text-slate-500 dark:text-white p-1 rounded required:border required:border-red-500 valid:border valid:border-green-500 in-valid:border invalid:border-red-500"
                    id="firstName"
                    type="text"
                    name="firstName"
                    required
                    min={1}
                    onChange={(e) =>
                      setGuestProfile({
                        ...guestProfile,
                        firstName: e.target.value,
                      })
                    }
                  />
                </div>
                <div className="grid lg:grid-cols-2">
                  <label htmlFor="lastName">Last Name: </label>
                  <input
                    className=" bg-white dark:bg-slate-500 text-slate-500 dark:text-white p-1 rounded required:border required:border-red-500 valid:border valid:border-green-500 in-valid:border invalid:border-red-500"
                    id="lastName"
                    type="text"
                    name="lastName"
                    required
                    min={1}
                    onChange={(e) =>
                      setGuestProfile({
                        ...guestProfile,
                        lastName: e.target.value,
                      })
                    }
                  />
                </div>
                <div className="grid lg:grid-cols-2">
                  <label htmlFor="contactEmail">Contact Email: </label>
                  <input
                    className=" bg-white dark:bg-slate-500 text-slate-500 dark:text-white p-1 rounded required:border required:border-red-500 valid:border valid:border-green-500 in-valid:border invalid:border-red-500"
                    id="contactEmail"
                    type="email"
                    name="contactEmail"
                    required
                    onChange={(e) =>
                      setGuestProfile({
                        ...guestProfile,
                        contactEmail: e.target.value,
                      })
                    }
                  />
                </div>
                <div className="grid lg:grid-cols-2">
                  <label htmlFor="age">Age: </label>
                  <input
                    className=" bg-white out-of-range:border-red-500 required:border required:border-red-500 out-of-range:border in-range:border-green-500 in-range:border dark:bg-slate-500 text-slate-500 dark:text-white p-1 rounded"
                    id="age"
                    type="number"
                    name="age"
                    min={16}
                    max={100}
                    required
                    onChange={(e) =>
                      setGuestProfile({
                        ...guestProfile,
                        age: parseInt(e.target.value),
                      })
                    }
                  />
                </div>
                <div className="grid lg:grid-cols-2">
                  <label htmlFor="sex">Sex: </label>
                  <DropDownList
                    onChange={(e) =>
                      setGuestProfile({
                        ...guestProfile,
                        sex: e.target.value,
                      })
                    }
                    id="sex"
                    name="sex"
                    required
                  >
                    <option value={""}>-- Select Gender --</option>
                    <option value={"male"}>Male</option>
                    <option value={"female"}>Female</option>
                    <option value={"none"}>Rather not say</option>
                  </DropDownList>
                </div>
                <div className="grid grid-cols-2">
                  <label htmlFor="adult">Adult?: </label>
                  <input
                    className="rounded checked:text-blue-500 checked:outline-none focus:outline-none focus:ring-blue-500"
                    id="adult"
                    type="checkbox"
                    name="adult"
                    onChange={(e) =>
                      setGuestProfile({
                        ...guestProfile,
                        adult: e.target.checked,
                      })
                    }
                  />
                </div>
                <div className="grid lg:grid-cols-2">
                  <label htmlFor="specialRequests">Special Requests: </label>
                  <textarea
                    className=" bg-white dark:bg-slate-500 text-slate-500 dark:text-white p-1 rounded required:border required:border-red-500 valid:border valid:border-green-500 in-valid:border invalid:border-red-500"
                    id="specialRequests"
                    name="specialRequests"
                    onChange={(e) =>
                      setReservation({
                        ...reservation,
                        specialRequests: e.target.value,
                      })
                    }
                  ></textarea>
                </div>
                <div className="grid lg:grid-cols-2">
                  <label htmlFor="roomPreferences">Room Preferences: </label>
                  <textarea
                    className=" bg-white dark:bg-slate-500 text-slate-500 dark:text-white p-1 rounded required:border required:border-red-500 valid:border valid:border-green-500 in-valid:border invalid:border-red-500"
                    id="roomPreferences"
                    name="roomPreferences"
                    onChange={(e) =>
                      setReservation({
                        ...reservation,
                        roomPreferences: e.target.value,
                      })
                    }
                  ></textarea>
                </div>
                <div className="grid lg:grid-cols-2">
                  <label htmlFor="numberOfGuests">Number of Guests: </label>
                  <input
                    className=" bg-white out-of-range:border-red-500 required:border required:border-red-500 out-of-range:border in-range:border-green-500 in-range:border dark:bg-slate-500 text-slate-500 dark:text-white p-1 rounded"
                    id="numberOfGuests"
                    type="number"
                    name="numberOfGuests"
                    min={1}
                    max={roomType?.capacity}
                    value={reservation.numberOfGuests}
                    required
                    onChange={(e) =>
                      setReservation({
                        ...reservation,
                        numberOfGuests: parseInt(e.target.value),
                      })
                    }
                  />
                </div>
                <div className="flex justify-center">
                  <button
                    type="submit"
                    className="bg-blue-700 dark:bg-blue-600 w-fit p-4 rounded-xl focus:ring-blue-300 focus:ring-4 focus:outline-none active:ring-blue-300"
                  >
                    Reserve room
                  </button>
                </div>
              </div>
            </form>
          </div>
        </div>
      </div>
    </Base>
  );
};
