import React from "react";
import { signIn, SignInContract } from "../../api/api";
import { Layout } from "../../components/layout/Layout";

export const signInForm: React.FC = () => {
  const handleSubmit = async (event: any) => {
    if (!event.target.checkValidity()) {
      event.target.reportValidity();
    }

    let contract: SignInContract = {
      login: event.target.login.value,
      password: event.target.password.value
    };

    await signIn(contract);
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
};
