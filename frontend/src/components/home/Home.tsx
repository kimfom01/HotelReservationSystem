import { Base } from "../utils/Base";
import { useState } from "react";
import { HotelsDropDown } from "../hotel/HotelsDropDown";
import Datepicker, {
  DateType,
  DateValueType,
} from "react-tailwindcss-datepicker";
import { DropDownList } from "../common/DropDownList";
import { VITE_API_URL } from "../utils/ApiUtil";
import { Button } from "../common/Button";
import { InputField } from "../common/InputField";
import { TextBox } from "../common/TextBox";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { RoomTypesDropDown } from "../room/RoomTypesDropDown";
import { RoomDetails } from "../room/RoomDetails";

interface Reservation {
  checkIn?: DateType;
  checkOut?: DateType;
  specialRequests: string;
  roomPreferences: string;
  numberOfGuests: number;
  guestProfile?: GuestProfile;
  roomTypeId: string;
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
  const queryClient = useQueryClient();
  const [hotelId, setHotelId] = useState<string>("");
  const [capacity, setCapacity] = useState<number>(1);
  const [roomTypeId, setRoomTypeId] = useState<string>("");
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
    roomTypeId: "",
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
      roomTypeId: roomTypeId,
      checkIn: dateValue?.startDate,
      checkOut: dateValue?.endDate,
    };

    const res = await fetch(`${VITE_API_URL}/reservation`, {
      method: "post",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(payload),
    });

    if (!res.ok) {
      throw new Error();
    }

    const data: Reservation = await res.json();

    return data;
  };

  const { mutateAsync } = useMutation({
    mutationFn: handleSubmit,
    onSuccess: () => {
      setDateValue({
        startDate: null,
        endDate: null,
      });
      setReservation({
        checkIn: new Date(),
        checkOut: new Date(),
        specialRequests: "",
        roomPreferences: "",
        numberOfGuests: 1,
        roomTypeId: "",
        hotelId: "",
      });

      setGuestProfile({
        firstName: "",
        lastName: "",
        contactEmail: "",
        sex: "",
        age: 0,
        adult: true,
      });

      queryClient.invalidateQueries({
        queryKey: ["rooms", "roomDetails"],
      });
    },
    onError: (error) => {
      console.log(error.message);
    },
  });

  return (
    <Base>
      <div className="grid lg:grid-rows-1 h-full grid-cols-1 lg:grid-cols-2 gap-4">
        <div className="grid grid-cols-1 text-2xl gap-8 w-full lg:w-3/5">
          <div className="row-span-1">
            <HotelsDropDown setHotelId={setHotelId} />
          </div>
          <div className="row-span-1">
            {hotelId && (
              <RoomTypesDropDown
                key={hotelId}
                hotelId={hotelId}
                setRoomTypeId={setRoomTypeId}
              />
            )}
          </div>
          {roomTypeId && (
            <RoomDetails
              key={roomTypeId}
              roomTypeId={roomTypeId}
              hotelId={hotelId}
              setCapacity={setCapacity}
            />
          )}
        </div>
        <div className="grid grid-cols-1 shadow-lg gap-8">
          <div className="dark:bg-slate-800 text-2xl bg-white rounded-lg row-span-11 p-6 w-full h-full">
            <form onSubmit={mutateAsync}>
              <h3 className="text-3xl font-bold text-center mb-8">
                Reservation Form
              </h3>
              <div className="grid gap-8">
                <div className="row-span-1">
                  <Datepicker
                    value={dateValue}
                    showFooter={true}
                    separator={"to"}
                    placeholder={"Select Check in and Check out dates"}
                    minDate={new Date()}
                    onChange={(newValue: DateValueType) => {
                      setDateValue(newValue);
                    }}
                  />
                </div>
                <div className="grid lg:grid-cols-2">
                  <label htmlFor="firstName">First Name:</label>
                  <InputField
                    id="firstName"
                    type="text"
                    name="firstName"
                    required
                    min={1}
                    value={guestProfile.firstName}
                    onChange={(e) =>
                      setGuestProfile({
                        ...guestProfile,
                        firstName: e.target.value,
                      })
                    }
                  />
                </div>
                <div className="grid lg:grid-cols-2">
                  <label htmlFor="lastName">Last Name:</label>
                  <InputField
                    id="lastName"
                    type="text"
                    name="lastName"
                    required
                    min={1}
                    value={guestProfile.lastName}
                    onChange={(e) =>
                      setGuestProfile({
                        ...guestProfile,
                        lastName: e.target.value,
                      })
                    }
                  />
                </div>
                <div className="grid lg:grid-cols-2">
                  <label htmlFor="contactEmail">Contact Email:</label>
                  <InputField
                    id="contactEmail"
                    type="email"
                    name="contactEmail"
                    required
                    value={guestProfile.contactEmail}
                    onChange={(e) =>
                      setGuestProfile({
                        ...guestProfile,
                        contactEmail: e.target.value,
                      })
                    }
                  />
                </div>
                <div className="grid lg:grid-cols-2">
                  <label htmlFor="age">Age:</label>
                  <InputField
                    id="age"
                    type="number"
                    name="age"
                    min={16}
                    max={100}
                    required
                    value={guestProfile.age}
                    onChange={(e) =>
                      setGuestProfile({
                        ...guestProfile,
                        age: parseInt(e.target.value),
                      })
                    }
                  />
                </div>
                <div className="grid lg:grid-cols-2">
                  <label htmlFor="sex">Sex:</label>
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
                    value={guestProfile.sex}
                  >
                    <option value={""}>-- Select Gender --</option>
                    <option value={"male"}>Male</option>
                    <option value={"female"}>Female</option>
                    <option value={"none"}>Rather not say</option>
                  </DropDownList>
                </div>
                <div className="grid lg:grid-cols-2">
                  <label htmlFor="specialRequests">Special Requests:</label>
                  <TextBox
                    id="specialRequests"
                    name="specialRequests"
                    value={reservation.specialRequests}
                    onChange={(e) =>
                      setReservation({
                        ...reservation,
                        specialRequests: e.target.value,
                      })
                    }
                  ></TextBox>
                </div>
                <div className="grid lg:grid-cols-2">
                  <label htmlFor="roomPreferences">Room Preferences:</label>
                  <TextBox
                    id="roomPreferences"
                    name="roomPreferences"
                    value={reservation.roomPreferences}
                    onChange={(e) =>
                      setReservation({
                        ...reservation,
                        roomPreferences: e.target.value,
                      })
                    }
                  ></TextBox>
                </div>
                <div className="grid lg:grid-cols-2">
                  <label htmlFor="numberOfGuests">Number of Guests: </label>
                  <InputField
                    id="numberOfGuests"
                    type="number"
                    name="numberOfGuests"
                    min={1}
                    max={capacity}
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
                <Button content={"Reserve room"} />
              </div>
            </form>
          </div>
        </div>
      </div>
    </Base>
  );
};
