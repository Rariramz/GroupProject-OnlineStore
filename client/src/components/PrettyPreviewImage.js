import React from "react";
import { styled } from "@mui/system";
import backgroundImage from "../images/homeBackgoundImage.png";
import { Paper, Grid, Button, Typography } from "@mui/material";

const BackgroundedDiv = styled(Paper)(({ theme }) => ({
  width: "100%",
  height: "100vh",
  backgroundImage: `Url(${backgroundImage})`,
  backgroundSize: "cover",
}));

const TransparentPaper = styled(Paper)(({ theme }) => ({
  width: 850,
  height: 400,

  padding: theme.spacing(0, 25),
  boxSizing: "border-box",

  background: "rgba(247, 248, 250, 0.8)",
  backdropFilter: "blur(24px)",
  borderRadius: 10,
}));

const PrettyPreviewImage = (props) => {
  return (
    <BackgroundedDiv>
      <Grid
        container
        spacing={0}
        direction="column"
        alignItems="center"
        justifyContent="center"
        style={{ minHeight: "100vh" }}
      >
        <Grid item>
          <TransparentPaper elevation={0}>
            <Grid
              container
              spacing={13}
              direction="column"
              alignItems="center"
              justifyContent="center"
            >
              <Grid
                item
                spacing={3}
                container
                direction="column"
                alignItems="center"
                justifyContent="center"
              >
                <Grid item>
                  <Typography variant="h1" color="initial">
                    🌱
                  </Typography>
                </Grid>
                <Grid item>
                  <Typography variant="h1" color="initial">
                    The nature candle
                  </Typography>
                </Grid>
                <Grid item>
                  <Typography variant="h3" color="initial" textAlign="center">
                    All handmade with natural soy wax, Candleaf is a companion
                    for all your pleasure moments
                  </Typography>
                </Grid>
              </Grid>
              <Grid item>
                <Button onClick={props.onClick} size="large">
                  <Typography variant="button" textAlign="center">
                    Check our most popular products
                  </Typography>
                </Button>
              </Grid>
            </Grid>
          </TransparentPaper>
        </Grid>
      </Grid>
    </BackgroundedDiv>
  );
};

export default PrettyPreviewImage;
