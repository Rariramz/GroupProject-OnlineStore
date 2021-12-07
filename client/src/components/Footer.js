import { Box, Grid, Paper, Typography, Button } from "@mui/material";
import CheckCircleOutlineIcon from "@mui/icons-material/CheckCircleOutline";
import { styled } from "@mui/styles";
import React from "react";

const BackgroundedDiv = styled(Box)(({ theme }) => ({
  height: "10vh",
  backgroundColor: "#f7f8fa",
  padding: theme.spacing(10, 40),
  display: "flex",
  flexDirection: "row",
  justifyContent: "space-around",
  alignItems: "center",
}));

const Footer = () => {
  return (
    <BackgroundedDiv>
      <span>text in</span>
    </BackgroundedDiv>
  );
};

export default Footer;
