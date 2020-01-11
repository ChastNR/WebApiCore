import React from "react";
import { Switch, Route } from "react-router";
import Layout from "./components/layout/Layout";
import signInForm from "./base/controls/SignInForm";
import signUpForm from "./base/controls/SignUpForm";
import gql from "./components/gql";
import { Counter } from "./components/Counter";
import { PrivateRoute } from "./CustomRoutes";

export const App: React.FC = () => (
  <Switch>
    <Route exact path="/" component={Layout} />
    <Route path="/signin" component={signInForm} />
    <Route path="/signup" component={signUpForm} />
    <Route path="/gql" component={gql} />
    <PrivateRoute path="/counter" component={Counter} />
  </Switch>
);
