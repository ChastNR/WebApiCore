import React, { useState } from "react";
import styled from "styled-components";
import { IUser } from "../../contracts/IUser";
import { Link } from "react-router-dom";

interface ISideBarHidden {
  sideBarHidden: boolean;
}

interface ISideBarProps {
  user?: IUser;
}

const Wrapper = styled.div`
  position: absolute;
  width: 100%;
  height: 100%;
  overflow: hidden;
  transition: transform 0.35s;
`;

const Menu = styled.div`
  position: absolute;
  top: 0;
  left: 0;
  background: grey;
  width: 200px;
  height: 100%;
  transform: ${(props: ISideBarHidden) => (props.sideBarHidden ? "translate3d(-200px, 0, 0)" : "translate3d(0, 0, 0)")};
  transition: transform 0.35s;
`;

const StyledOpenButton = styled.button`
  display: ${(props: ISideBarHidden) => (props.sideBarHidden ? "block" : "none")};
  font-size: 1.5em;
  color: blue;
  padding: 0.5em;
  border: 1px solid blue;
  border-radius: 0.1em;
`;

export const SideBar: React.FC<ISideBarProps> = props => {
  const [hidden, open] = useState(true);
  const openBar = () => open(hidden => !hidden);

  return (
    <Wrapper>
      <StyledOpenButton onClick={openBar} sideBarHidden={hidden} />
      <Menu sideBarHidden={hidden}>
        <ul style={{ textAlign: "left" }}>
          <li>
            <StyledOpenButton onClick={openBar} sideBarHidden={!hidden} />
          </li>
          <li>
            <Link to="/signin">Sign in</Link>
          </li>
          <li>
            <a href="#">Menu-1</a>
          </li>
          <li>
            <a href="#">Menu-2</a>
          </li>
          <li>
            <a href="#">Menu-6</a>
          </li>
          <li>{props.user && props.user.id}</li>
        </ul>
      </Menu>
    </Wrapper>
  );
};
