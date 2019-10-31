import React from "react";
import { Switch, Route } from "react-router";
import { Layout } from "./components/layout/Layout";
import { signInForm } from "./base/controls/SignInForm";
import { signUpForm } from "./base/controls/SignUpForm";
import { IAppStoreInject } from "./stores/AppStore";

export const Router: React.FC<IAppStoreInject> = () => {
  return (
    <Switch>
      <Route exact path="/">
        <Layout text="yeah!!!!" />
      </Route>
      <Route path="/signin" component={signInForm} />
      <Route path="/signup" component={signUpForm} />
    </Switch>
  );
};
