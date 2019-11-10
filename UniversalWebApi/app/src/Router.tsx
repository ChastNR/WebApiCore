import React from "react";
import { Switch, Route } from "react-router";
import { Layout } from "./components/layout/Layout";
import signInForm from "./base/controls/SignInForm";
import signUpForm from "./base/controls/SignUpForm";
import gqlTest2, { Gql } from "./components/gql";
import { Counter } from "./components/Counter";

export const Router: React.FC = () => {
  return (
    <Switch>
      <Route exact path="/" component={Layout} />
      <Route path="/signin" component={signInForm} />
      <Route path="/signup" component={signUpForm} />
      <Route path="/gql" component={Gql} />
      <Route path="/gqltest" component={gqlTest2} />
      <Route path="/counter" component={Counter} />
    </Switch>
  );
};
