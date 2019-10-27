import React from "react";
import { Switch, Route } from "react-router";
import { Layout } from "./components/layout/Layout";
import { signInForm } from "./base/controls/SignInForm";
import { signUpForm } from "./base/controls/SignUpForm";

export const Router: React.FC = () => (
  <Switch>
    <Route exact path="/" component={Layout} />
    <Route path="/about" />
    <Route path="/contacts" />
    <Route path="/signin" component={signInForm} />
    <Route path="/signup" component={signUpForm} />
  </Switch>
);
