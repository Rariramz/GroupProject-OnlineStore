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
              <img class="logo" src={logo} alt="Candleaf" style={{ paddingLeft: 115 }} />
              <Typography variant="body3" color="#FFFFFF" style={{ paddingLeft: 115, width: 250 }}>
                Your natural candle made foryour home and for your wellness.
              </Typography>
              <Typography variant="body4" color="#FFFFFF" style={{ paddingLeft: 115, paddingTop: 50, width: 250 }}>
                ©Candleaf All Rights Reserved.
              </Typography>
            </Grid>

            <Grid
              item
              container
              spacing={3}
              direction="row"
              justify="center"
              alignItems="flex-end"
              alignContent="center"
              wrap="nowrap"
            >
              <Grid
                item
                container
                spacing={3}
                direction="Column"
                justify="center"
                alignItems="flex-start"
                alignContent="center"
                wrap="nowrap"
              >
                <Typography variant="body3" color="primary">
                  Discovery
                </Typography>
                <Typography variant="body3" color="#FFFFFF" style={{ paddingTop: 25 }}>
                  New season
                </Typography>
                <Typography variant="body3" color="#FFFFFF" style={{ paddingTop: 20 }}>
                  Most searched
                </Typography>
                <Typography variant="body3" color="#FFFFFF" style={{ paddingTop: 20 }}>
                  Most selled
                </Typography>
              </Grid>

              <Grid
                item
                container
                spacing={3}
                direction="Column"
                justify="center"
                alignItems="flex-start"
                alignContent="center"
                wrap="nowrap"
              >
                <Typography variant="body3" color="primary">
                  About
                </Typography>
                <Typography variant="body3" color="#FFFFFF" style={{ paddingTop: 25 }}>
                  Help
                </Typography>
                <Typography variant="body3" color="#FFFFFF" style={{ paddingTop: 20 }}>
                  Shipping
                </Typography>
                <Typography variant="body3" color="#FFFFFF" style={{ paddingTop: 20 }}>
                  Affiliate
                </Typography>
              </Grid>

              <Grid
                item
                container
                spacing={3}
                direction="Column"
                justify="center"
                alignItems="flex-start"
                alignContent="center"
                wrap="nowrap"
              >
                <Typography variant="body3" color="primary">
                  Info
                </Typography>
                <Typography variant="body3" color="#FFFFFF" style={{ paddingTop: 25 }}>
                  Contact us
                </Typography>
                <Typography variant="body3" color="#FFFFFF" style={{ paddingTop: 20 }}>
                  Privacy Policies
                </Typography>
                <Typography variant="body3" color="#FFFFFF" style={{ paddingTop: 20 }}>
                  Terms & Conditions
                </Typography>
              </Grid>

            </Grid>
          </Grid>
        </Grid>
      </Footer1>
    </>
  );
};

export default Footer;
