import React from "react";
import { signUp, SignUpContract } from "../../api/api";
import { Layout } from "../../components/layout/Layout";

export const signUpForm: React.FC = () => {
  const handleSubmit = async (event: any) => {
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

    let result = await signUp(contract);
    if (result === true) {
      window.location.href = "/signin";
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
