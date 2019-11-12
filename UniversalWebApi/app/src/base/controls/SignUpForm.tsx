import React from "react";
import { api } from "../../api/api";
import { IAppStoreInject, App_Store } from "../../stores/AppStore";
import history from "../../history";
import { inject, observer } from "mobx-react";
import { Loading } from "./Loading";
import { SignUpContract } from "../../contracts/SignUpContract";

const signUpForm: React.FC<IAppStoreInject> = props => {
  const showLoader = props.appStore.ShowLoader;

  const handleSubmit = async (event: any) => {
    event.preventDefault();
    showLoader.setLoaderState(true);

    if (
      !event.target.checkValidity() ||
      event.target.password.value !== event.target.passwordCompare.value
    ) {
      event.target.reportValidity();
    }

    let contract: SignUpContract = {
      name: event.target.name.value,
      email: event.target.email.value,
      phoneNumber: event.target.phoneNumber.value,
      password: event.target.password.value,
      passwordCompare: event.target.passwordCompare.value
    };

    let result = await api.post<boolean>("/api/auth/signup", contract);

    if (result) {
      showLoader.setLoaderState(false);
      history.push("signin");
    }
  };

  return (
    <div>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Name:</label>
          <input type="text" name="name" required />
        </div>
        <div>
          <label>Email:</label>
          <input type="text" name="email" required />
        </div>
        <div>
          <label>Phone number:</label>
          <input type="text" name="phoneNumber" required />
        </div>
        <div>
          <label>Password:</label>
          <input type="text" name="password" required />
        </div>
        <div>
          <label>Repeat password:</label>
          <input type="text" name="passwordCompare" required />
        </div>
        <div>
          <button type="submit">Sign up</button>
        </div>
      </form>
    </div>
  );
};

export default inject(App_Store)(observer(signUpForm));
