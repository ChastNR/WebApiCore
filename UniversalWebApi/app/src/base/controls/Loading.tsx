import React from "react";
import styled from "styled-components";
import loading from "../../img/loading.svg";

interface IStyledLoaderProps {
  backgroundUrl: string;
}

const StyledLoader = styled.div`
  position: fixed;
  left: 0px;
  top: 0px;
  width: 100%;
  height: 100%;
  z-index: 999;
  background: url(${(props: IStyledLoaderProps) => props.backgroundUrl}) 50% 50% no-repeat rgba(249, 249, 249, 0.5);
`;

export const Loading = () => <StyledLoader backgroundUrl={loading} />;
