import { Base } from "../utils/Base";
import { CreateHotelForm } from "./CreateHotelForm";
import { CreateRoomForm } from "./CreateRoomForm";
import { CreateRoomTypeForm } from "./CreateRoomTypeForm";

export const Admin = () => {
  return (
    <Base>
      <div className="grid grid-cols-1 lg:grid-cols-3 gap-12">
        <CreateHotelForm />
        <CreateRoomTypeForm />
        <CreateRoomForm />
      </div>
    </Base>
  );
};
