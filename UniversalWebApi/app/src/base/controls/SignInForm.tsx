import React from "react";
import { signIn, SignInContract } from "../../api/api";
import { inject, observer } from "mobx-react";
import { IAppStore } from "../../stores/AppStore";

const signInForm: React.FC<IAppStore> = inject("appStore")(
  observer(props => {
    const userStore = props.UserStore;

    const handleSubmit = async (event: any) => {
      if (!event.target.checkValidity()) {
        event.target.reportValidity();
      }

      let contract: SignInContract = {
        login: event.target.login.value,
        password: event.target.password.value
      };

      let response = await signIn(contract);

      localStorage.setItem("token", response.token);
      userStore.user.id = response.userId;
    };

    return (
      <div>
        <form onSubmit={handleSubmit}>
          <div>
            <label>Email or phone number:</label>
            <input type="text" name="login" required />
          </div>
          <div>
            <label>Password:</label>
            <input type="text" name="password" required />
          </div>
          <div>
            <button type="submit">Sign in</button>
          </div>
        </form>
      </div>
    );
  })
);

export default signInForm;
