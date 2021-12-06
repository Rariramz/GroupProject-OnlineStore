import { Box, Grid, Paper, Typography, Button } from "@mui/material";
import CheckCircleOutlineIcon from "@mui/icons-material/CheckCircleOutline";
import { styled } from "@mui/styles";
import React from "react";

import twoCandlesImage from "../images/twoCandles.png";

const BackgroundedDiv = styled(Box)(({ theme }) => ({
  height: "60vh",
  backgroundColor: "#f7f8fa",
  padding: theme.spacing(10, 40),
  display: "flex",
  flexDirection: "row",
  justifyContent: "space-around",
  alignItems: "center",
}));

const NarrowedTypography = styled(Typography)(({ theme }) => ({
  width: 350,
}));

const CandleInfo = (props) => {
  return (
    <BackgroundedDiv>
      <Grid
        item
        container
        spacing={3}
        direction="column"
        justify="center"
        alignItems="flex-start"
        alignContent="center"
        wrap="nowrap"
      >
        <Grid item>
          <NarrowedTypography variant="h1" color="initial">
            Clean and fragrant soy wax
          </NarrowedTypography>
        </Grid>
        <Grid item>
          <NarrowedTypography variant="body2" color="primary">
            Made for your home and for your wellness
          </NarrowedTypography>
        </Grid>
        <Grid item></Grid>
        <Grid item container spacing={2}>
          <Grid item>
            <CheckCircleOutlineIcon />
          </Grid>
          <Grid item>
            <Typography variant="body2" color="initial">
              Eco-sustainable: All recyclable materials, 0% CO2 emissions
            </Typography>
          </Grid>
        </Grid>
        <Grid item container spacing={2}>
          <Grid item>
            <CheckCircleOutlineIcon />
          </Grid>
          <Grid item>
            <Typography variant="body2" color="initial">
              Hyphoallergenic: 100% natural, human friendly ingredients
            </Typography>
          </Grid>
        </Grid>
        <Grid item container spacing={2}>
          <Grid item>
            <CheckCircleOutlineIcon />
          </Grid>
          <Grid item>
            <Typography variant="body2" color="initial">
              Handmade: All candles are craftly made with love.
            </Typography>
          </Grid>
        </Grid>
        <Grid item container spacing={2}>
          <Grid item>
            <CheckCircleOutlineIcon />
          </Grid>
          <Grid item>
            <Typography variant="body2" color="initial">
              Long burning: No more waste. Created for last long.
            </Typography>
          </Grid>
        </Grid>
        <Grid item></Grid>
        <Grid item></Grid>
        <Grid item></Grid>
        <Button onClick={props.onClick} size="large">
          <Typography variant="button">Learn more</Typography>
        </Button>
      </Grid>
      <Paper elevation={12}>
        <img src={twoCandlesImage} />
      </Paper>
    </BackgroundedDiv>
  );
};

export default CandleInfo;
