import { Box, Grid, Paper, Typography, Button } from "@mui/material";
import * as material from "@mui/material";
import CheckCircleOutlineIcon from "@mui/icons-material/CheckCircleOutline";
import { styled } from "@mui/styles";
import React from "react";

import logo from "../images/svg/logo.svg";
import cart from "../images/svg/Cart.svg";
import profile from "../images/svg/Profile.svg";
import { Link } from "react-router-dom";

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
      <Link to="">
        <img src={logo} alt="Candleaf" />
      </Link>
      <HeaderDiv width="40vw">
        <Link
          to=""
          style={{
            textDecoration: "none",
          }}
        >
          <Typography variant="body1" color="initial">
            Home
          </Typography>
        </Link>
        <material.Link
          href="mailto:onlinestore953501@gmail.com"
          style={{
            textDecoration: "none",
          }}
          target="_blank"
        >
          <Typography variant="body1" color="initial">
            Contact us
          </Typography>
        </material.Link>
      </HeaderDiv>

      <HeaderDiv width="7vw">
        <Link to="/login">
          <img src={profile} alt="profile" />
        </Link>
        <Link to="cart">
          <img src={cart} alt="cart" paddingRight="20" />
        </Link>
      </HeaderDiv>
    </HeaderDiv>
  );
};

export default Header;
