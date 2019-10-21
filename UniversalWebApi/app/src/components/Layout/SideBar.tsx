import React, { useState } from "react";
import styled from "styled-components";
import SLink from "../../base/controls/SLink";

const StyledSideNav = styled.div`
  height: 100%;
  width: ${props => (props.hidden ? "0" : "150px")};
  position: fixed;
  z-index: 1;
  top: 0;
  left: 0;
  border-right: 1px solid black;
  transition: 0.5s;

  display: ${props => (props.hidden ? "none" : "flex")};
  flex-direction: column;
`;

const StyledButton = styled.button``;

const SideBar: React.FC = () => {
  const [hidden, open] = useState(true);
  const openBar = () => open(hidden => (hidden ? false : true));

  return (
    <div>
      <StyledSideNav hidden={hidden} onMouseOut={openBar}>
        <div>
          <SLink to="/main" text="Main" />
        </div>
        <div>
          <SLink to="/main" text="Services" />
        </div>
        <div>
          <SLink to="/main" text="Clients" />
        </div>
        <div>
          <SLink to="/main" text="Contact" />
        </div>
        <div>
          <SLink to="/main" text="About" />
        </div>
      </StyledSideNav>
      <StyledButton onClick={openBar} onMouseOver={openBar} />
    </div>
  );
};

export default SideBar;
