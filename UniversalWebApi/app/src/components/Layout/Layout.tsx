import React from "react";
import styled from "styled-components";
import { SideBar } from "./SideBar";
import { inject, observer } from "mobx-react";
import { IAppStore } from "../../stores/AppStore";
import { Footer } from "./Footer";

const StyledContainer = styled.div`
  width: 100vw;
`;

export const Layout: React.FC<{ appStore: IAppStore }> = inject("appStore")(
  observer(({ appStore }) => {
    const user = appStore.UserStore.userState;
    return (
      <StyledContainer>
        <SideBar user={user} />
        <Footer />
      </StyledContainer>
    );
  })
);
