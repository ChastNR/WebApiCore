import React from "react";
import { observer, inject } from "mobx-react";
import { App_Store, IAppStoreInject } from "../stores/AppStore";
import { IUser } from "../contracts/IUser";

const gql = (props: IAppStoreInject) => {
  const userStore = props.appStore.UserStore;

  const renderUser = (user: IUser) => (
    <div key={user.id}>
      <div>{user.id}</div>
      <div>{user.name}</div>
      <div>{user.email}</div>
      <div>{user.phoneNumber}</div>
      <br />
    </div>
  );

  const listOfUsers = () => {
    if (userStore.users.length) {
      return (
        <div>
          {userStore.users.map(user => (
            <div>{renderUser(user)}</div>
          ))}
        </div>
      );
    } else {
      return <button onClick={userStore.getUsers}>Get users</button>;
    }
  };

  return <div>{listOfUsers()}</div>;
};

export default inject(App_Store)(observer(gql));
