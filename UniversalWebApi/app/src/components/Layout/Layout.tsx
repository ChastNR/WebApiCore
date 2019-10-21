import React from "react";
import styled from "styled-components";
import SideBar from "./SideBar";
import { Footer } from "./Footer";
import { Header } from "./Header";

const StyledContainer = styled.div`
  width: 100vw;
`;

export const Layout: React.FC = (props: any) => {
  return (
    <StyledContainer>
      <SideBar />
      <Header />
      {props.children}
      <Footer />
    </StyledContainer>
  );
};
