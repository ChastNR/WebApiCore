import React, { useState } from "react";
import styled from "styled-components";
import { faBars } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { IUser } from "../../contracts/IUser";

interface ISideBarHidden {
  sideBarHidden: boolean;
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
  transform: ${(props: ISideBarHidden) =>
    props.sideBarHidden ? "translate3d(-200px, 0, 0)" : "translate3d(0, 0, 0)"};
  transition: transform 0.35s;
`;

const StyledOpenButton = styled(FontAwesomeIcon)`
  display: ${(props: ISideBarHidden) =>
    props.sideBarHidden ? "block" : "none"};
  font-size: 1.5em;
  color: blue;
  padding: 0.5em;
  border: 1px solid blue;
  border-radius: 0.1em;
`;

export const SideBar: React.FC<{ user: IUser }> = ({ user }) => {
  const [hidden, open] = useState(true);
  const openBar = () => open(hidden => (hidden ? false : true));

  return (
    <Wrapper>
      <StyledOpenButton
        icon={faBars}
        onClick={openBar}
        sideBarHidden={hidden}
      />
      <Menu sideBarHidden={hidden}>
        <ul style={{ textAlign: "left" }}>
          <li>
            <StyledOpenButton
              icon={faBars}
              onClick={openBar}
              sideBarHidden={!hidden}
            />
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
          <li>{user.id}</li>
          <li>{user.name}</li>
          <li>{user.email}</li>
          <li>{user.phoneNumber}</li>
        </ul>
      </Menu>
    </Wrapper>
  );
};
