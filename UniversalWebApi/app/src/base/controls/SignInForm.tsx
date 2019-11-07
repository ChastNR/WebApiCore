import React from "react";
import { signIn, SignInContract } from "../../api/api";
import { inject, observer } from "mobx-react";
import { IUserStore } from "../../stores/UserStore";
import { IAppStoreInject } from "../../stores/AppStore";

export const signInForm: React.FC<IAppStoreInject> = inject("appStore")(
  observer(props => {
    const appStore = props!.AppStore;
    //const userStore = appStore.UserStore;

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
      //userStore.user.id = response.userId;
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
