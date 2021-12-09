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
  paddingInline: theme.spacing(20),
  paddingBottom: theme.spacing(10),
  backgroundColor: "#272727",
}));

const Footer = () => {
  return (
    <>
      <Footer1>
        <Grid
          item
          container
          spacing={3}
          direction="row"
          justifyContent="center"
          alignItems="center"
          alignContent="center"
        >
          <Grid item xs={12}>
            <hr width="1200" height="1.5" style={{ marginTop: 60 }} />
          </Grid>
          <Grid item xs={6}>
            <img class="logo" src={logo} alt="Candleaf" />
          </Grid>
          <Grid item xs={6}>
            <Typography variant="body2" color="#FFFFFF" textAlign="right">
              Your natural candle made for your home and for your wellness.
            </Typography>
            <Typography variant="h3" color="#FFFFFF" textAlign="right">
              ©Candleaf All Rights Reserved.
            </Typography>
          </Grid>
        </Grid>
      </Footer1>
    </>
  );
};

export default Footer;
