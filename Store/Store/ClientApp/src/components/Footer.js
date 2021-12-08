import { Box, Grid, Paper, Typography, Button } from "@mui/material";
import CheckCircleOutlineIcon from "@mui/icons-material/CheckCircleOutline";
import { styled } from "@mui/styles";
import React from "react";

import logo from "../images/footer.png";

const Footer1 = styled(Box)(({ theme }) => ({
  display: "flex",
  flexDirection: "row",
  justifyContent: "space-around",
  alignItems: "center",
  padding: 10,
  backgroundColor: "#272727",
}));

const Footer2 = styled(Box)(({ theme }) => ({
  display: "flex",
  flexDirection: "row",
  justifyContent: "space-around",
  alignItems: "center",
  padding: 10,
}));

const Footer = () => {
  return (
    <>
      <Footer1>
        <Grid
          item
          container
          spacing={3}
          direction="column"
          justify="center"
          alignItems="center"
          alignContent="center"
          wrap="nowrap"
        >
          <hr width="1200" height="1.5" style={{ marginTop: 60 }}></hr>
          <Grid
            item
            container
            direction="row"
            justify="center"
            alignItems="center"
            alignContent="center"
            wrap="nowrap"
          >
            <Grid
              item
              container
              direction="column"
              justify="center"
              alignItems="flex-start"
              alignContent="center"
              wrap="nowrap"
            >
              <img class="logo" src={logo} alt="Candleaf" />
              <Typography variant="body2" color="#FFFFFF">
                Your natural candle made for your home and for your wellness.
              </Typography>
              <Typography variant="h3" color="#FFFFFF">
                ©Candleaf All Rights Reserved.
              </Typography>
            </Grid>
          </Grid>
        </Grid>
      </Footer1>
    </>
  );
};

export default Footer;
