import React from "react";
import { signIn, SignInContract } from "../../api/api";
import { inject, observer } from "mobx-react";
import { App_Store, IAppStoreInject } from "../../stores/AppStore";
import history from "../../history";
import { Loading } from "./Loading";

const signInForm: React.FC<IAppStoreInject> = props => {
  const userStore = props.appStore.UserStore;
  const showLoader = props.appStore.ShowLoader;

  const handleSubmit = async (event: any) => {
    event.preventDefault();

    showLoader.setLoaderState(true);

    if (!event.target.checkValidity()) {
      event.target.reportValidity();
    }

    let contract: SignInContract = {
      login: event.target.login.value,
      password: event.target.password.value
    };

    let response = await signIn(contract);

    if (response !== undefined) {
      showLoader.setLoaderState(false);
      localStorage.setItem("token", response.token);
      userStore.user.id = response.userId;
      history.push("/");
    } else {
      showLoader.setLoaderState(false);
    }
  };

  return (
    <div>
      {showLoader.showLoader && <Loading />}
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
};

export default inject(App_Store)(observer(signInForm));
