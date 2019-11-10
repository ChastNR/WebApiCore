import styled from "styled-components";
import React from "react";
import { SideBar } from "./SideBar";
import { Footer } from "./Footer";

const StyledContainer = styled.div`
  width: 100vw;
`;

interface ILayout {
  children?: any;
}

export const Layout: React.FC<ILayout> = props => {
  return (
    <StyledContainer>
      <SideBar />
      {props.children}
      <Footer />
    </StyledContainer>
  );
};
