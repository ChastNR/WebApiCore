import React from "react";
import { Route, Redirect } from "react-router";

const isAuthenticated: boolean = true;

interface IPrivateRouteProps {
  exact?: boolean;
  path: string;
  component: React.ComponentType<any>;
}

export const PrivateRoute = ({ component, ...rest }: IPrivateRouteProps) => (
  <Route {...rest} render={props => (isAuthenticated ? <React.Component {...props} /> : <Redirect to="/signin" />)} />
);
