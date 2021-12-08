import { Box, Grid, Paper, Typography, Button } from "@mui/material";
import CheckCircleOutlineIcon from "@mui/icons-material/CheckCircleOutline";
import { styled } from "@mui/styles";
import React from "react";

import logo from "../images/svg/logo.svg";
import cart from "../images/svg/Cart.svg";
import profile from "../images/svg/Profile.svg";

const HeaderDiv = styled(Box)(({ theme }) => ({
  display: "flex",
  flexDirection: "row",
  justifyContent: "space-around",
  alignItems: "center",
  padding: 10,
}));

const Header = () => {
  return (
    <HeaderDiv>
      <img class="logo" src={logo} alt="Candleaf" />
      <HeaderDiv width="40vw">
        <Typography variant="body1" color="initial" textDecoration="none">
          Home
        </Typography>
        <Typography variant="body1" color="initial" textDecoration="none">
          About
        </Typography>
        <Typography variant="body1" color="initial" textDecoration="none">
          Contact us
        </Typography>
      </HeaderDiv>

      <HeaderDiv width="7vw">
        <img class="profile" src={profile} alt="profile" />
        <img class="cart" src={cart} alt="cart" paddingRight="20" />
      </HeaderDiv>
    </HeaderDiv>
  );
};

export default Header;
