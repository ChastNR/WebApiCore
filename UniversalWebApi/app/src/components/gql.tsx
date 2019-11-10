import React from "react";
import { observer, inject } from "mobx-react";
import { IAppStore, App_Store, User_Store } from "../stores/AppStore";
import { IUser } from "../contracts/IUser";
import { Layout } from "./layout/Layout";
import { IUserStore } from "../stores/UserStore";

@inject(App_Store)
@observer
export class Gql extends React.Component<IAppStore, {}> {
  private readonly UserStore: IUserStore = this.props[User_Store];

  render() {
    return (
      <div>
        <button onClick={() => this.UserStore.getUsers}>Get users</button>
        <div>
          {this.UserStore.users && (
            <div>
              {this.UserStore.users.map(user => (
                <div key={user.id}>
                  <div>{user.id}</div>
                  <div>{user.name}</div>
                </div>
              ))}
            </div>
          )}
        </div>
      </div>
    );
  }
}

const gqlTest2 = (appStore: IAppStore) => {
  const userStore = appStore[User_Store];

  const listOfUsers = () => {
    if (userStore.users.length) {
      return (
        <div>
          {userStore.users.map(user => (
            <div>
              {user.id}
              {user.name}
              {user.email}
              {user.phoneNumber}
            </div>
          ))}
        </div>
      );
    } else {
      return <button onClick={() => userStore.getUsers}>Get users</button>;
    }
  };

  return <div>{listOfUsers()}</div>;
};

export default inject(App_Store)(observer(gqlTest2));
