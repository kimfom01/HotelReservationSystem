import { Base } from "../utils/Base";
import { useState } from "react";
import {
  AvailableRoomsDropDown,
  Room,
  RoomType,
} from "./AvailableRoomsDropDown";
import { HotelsDropDown } from "./HotelsDropDown";
import Datepicker, { DateValueType } from "react-tailwindcss-datepicker";
import { DropDownList } from "../utils/DropDownList";

interface ReservationProps {
  checkIn: Date;
  checkOut: Date;
  specialRequests: string;
  roomPreferences: string;
  numberOfGuests: number;
  guestProfile: GuestProfileProps;
  roomId: string;
  hotelId: string;
}

interface GuestProfileProps {
  firstName: string;
  lastName: string;
  contactEmail: string;
  sex: string;
  age: number;
  adult: boolean;
}

export const Home = () => {
  const [hotelId, setHotelId] = useState<string>();
  const [room, setRoom] = useState<Room>();
  const [roomType, setRoomType] = useState<RoomType>();
  const [dateValue, setDateValue] = useState<DateValueType>({
    startDate: new Date(),
    endDate: null,
  });

  const handleValueChange = (newValue: DateValueType) => {
    console.log("newValue:", newValue);
    setDateValue(newValue);
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
          <div className="row-span-1">
            <Datepicker value={dateValue} onChange={handleValueChange} />
          </div>
          <div className="border rounded-lg border-slate-500 dark:border-white row-span-11 p-4 w-full h-full">
            <form onSubmit={() => {}}>
              <h3 className="text-2xl font-bold text-center mb-8">
                Reservation Form
              </h3>
              <div className="grid gap-4">
                <div className="grid lg:grid-cols-2">
                  <label htmlFor="firstName">First Name: </label>
                  <input
                    className=" bg-white dark:bg-slate-500 text-slate-500 dark:text-white p-1 rounded required:border required:border-red-500 valid:border valid:border-green-500 in-valid:border invalid:border-red-500"
                    id="firstName"
                    type="text"
                    name="firstName"
                    required
                    min={1}
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
                  />
                </div>
                <div className="grid lg:grid-cols-2">
                  <label htmlFor="sex">Sex: </label>
                  <DropDownList id="sex" name="sex" required>
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
                  />
                </div>
                <div className="grid lg:grid-cols-2">
                  <label htmlFor="specialRequests">Special Requests: </label>
                  <textarea
                    className=" bg-white dark:bg-slate-500 text-slate-500 dark:text-white p-1 rounded required:border required:border-red-500 valid:border valid:border-green-500 in-valid:border invalid:border-red-500"
                    id="specialRequests"
                    name="specialRequests"
                    required
                  ></textarea>
                </div>
                <div className="grid lg:grid-cols-2">
                  <label htmlFor="roomPreferences">Room Preferences: </label>
                  <textarea
                    className=" bg-white dark:bg-slate-500 text-slate-500 dark:text-white p-1 rounded required:border required:border-red-500 valid:border valid:border-green-500 in-valid:border invalid:border-red-500"
                    id="roomPreferences"
                    name="roomPreferences"
                    required
                  ></textarea>
                </div>{" "}
                <div className="grid lg:grid-cols-2">
                  <label htmlFor="numberOfGuests">Number of Guests: </label>{" "}
                  <input
                    className=" bg-white out-of-range:border-red-500 required:border required:border-red-500 out-of-range:border in-range:border-green-500 in-range:border dark:bg-slate-500 text-slate-500 dark:text-white p-1 rounded"
                    id="numberOfGuests"
                    type="number"
                    name="numberOfGuests"
                    min={1}
                    max={10} // should be capacity of currently selected room
                    required
                  />
                </div>
                <div>
                  <input
                    id="roomId"
                    type="text"
                    name="roomId"
                    hidden
                    required
                  />
                  <input
                    id="hotelId"
                    type="text"
                    name="hotelId"
                    hidden
                    required
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
