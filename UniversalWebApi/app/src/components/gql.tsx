import React from "react";
import { observer, inject } from "mobx-react";
import { App_Store, IAppStoreInject } from "../stores/AppStore";
import { IUserStore } from "../stores/UserStore";
import { IUser } from "../contracts/IUser";
import { Redirect } from "react-router";

@inject(App_Store)
@observer
export class Gql extends React.Component<IAppStoreInject> {
  private readonly UserStore: IUserStore = this.props.appStore.UserStore;

  renderUser = (user: IUser) => {
    return (
      <div key={user.id}>
        <div style={{ color: "red" }}>{user.id}</div>
        <div>{user.name}</div>
        <div>{user.email}</div>
        <div>{user.phoneNumber}</div>
        <br />
      </div>
    );
  };

  render() {
    return (
      <div>
        <button onClick={this.UserStore.getUsers}>Get users</button>
        <div>
          {this.UserStore.users && (
            <div>{this.UserStore.users.map(user => this.renderUser(user))}</div>
          )}
        </div>
      </div>
    );
  }
}

const gqlTest2 = (props: IAppStoreInject) => {
  const userStore = props.appStore.UserStore;

  const listOfUsers = () => {
    if (userStore.users.length) {
      return (
        <div>
          {userStore.users.map(user => (
            <div key={user.id}>
              <div>{user.id}</div>
              <div>{user.name}</div>
              <div>{user.email}</div>
              <div>{user.phoneNumber}</div>
              <br />
            </div>
          ))}
        </div>
      );
    } else {
      return <button onClick={userStore.getUsers}>Get users</button>;
    }
  };

  return <div>{listOfUsers()}</div>;
};

export default inject(App_Store)(observer(gqlTest2));
