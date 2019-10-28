import styled from "styled-components";
import React from "react";
import { SideBar } from "./SideBar";
import { Footer } from "./Footer";

const StyledContainer = styled.div`
  width: 100vw;
`;

interface ILayout {
  text?: string;
  children?: any;
}

export const Layout: React.FC<ILayout> = props => {
  const text = props.text;
  return (
    <StyledContainer>
      <SideBar />
      {text}
      {props.children}
      <Footer />
    </StyledContainer>
  );
};
