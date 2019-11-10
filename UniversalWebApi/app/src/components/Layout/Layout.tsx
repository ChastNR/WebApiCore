import styled from "styled-components";
import React from "react";
import { SideBar } from "./SideBar";
import { Footer } from "./Footer";
import { IAppStoreInject, App_Store } from "../../stores/AppStore";
import { inject, observer } from "mobx-react";

const StyledContainer = styled.div`
  width: 100vw;
`;

interface ILayout extends IAppStoreInject {
  children?: any;
}

const Layout: React.FC<ILayout> = props => {
  return (
    <StyledContainer>
      <SideBar user={props.appStore.UserStore.user} />
      {props.children}
      <Footer />
    </StyledContainer>
  );
};

export default inject(App_Store)(observer(Layout));
