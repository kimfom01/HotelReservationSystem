import { Base } from "../utils/Base";
import { Login } from "./Login";
import { Register } from "./Register";

export const Auth = () => {
  return (
    <Base>
      <div className="grid h-full grid-cols-1 lg:grid-cols-2 gap-52">
        <Login />
        <Register />
      </div>
    </Base>
  );
};
