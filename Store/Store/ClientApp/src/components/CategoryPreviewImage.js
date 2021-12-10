import React, { useState, useEffect } from "react";
import { styled } from "@mui/system";
import { Box, Paper, Grid, Button, Typography } from "@mui/material";
import { getFile } from "../utils/fetchWrapper";

import backgroundImage from "../images/homeBackgoundImage.png";

const BackgroundedDiv = styled(Box)(({ theme }) => ({
  width: "100%",
  height: "50vh",
  borderRadius: 20,
  backgroundImage: `Url(${backgroundImage})`,
  backgroundSize: "cover",
}));

const TransparentPaper = styled(Paper)(({ theme }) => ({
  width: 1000,
  height: 200,

  padding: theme.spacing(0, 25),
  boxSizing: "border-box",

  background: "rgba(247, 248, 250, 0.8)",
  backdropFilter: "blur(0px)",
  borderRadius: 10,
}));

const CategoryPreviewImage = (props) => {
  const [image, setImage] = useState(null);

  useEffect(() => {
    getFile(`api/Categories/GetInsideImage?id=${props.id}`, setImage);
  }, []);

  return (
    <BackgroundedDiv
      style={{ backgroundImage: `Url(${image || backgroundImage})` }}
    >
      <Grid
        container
        spacing={0}
        direction="column"
        alignItems="center"
        justifyContent="center"
      >
        <Grid item>
          {/* <TransparentPaper elevation={0}>
            {/* <Grid
              container
              spacing={13}
              direction="column"
              alignItems="center"
              justifyContent="center"
            > 
            { <Grid
                item
                spacing={3}
                container
                direction="column"
                alignItems="center"
                justifyContent="center"
              ></Grid>
              <Grid item>
                <Typography variant="button" textAlign="center">
                  Check our most popular products
                </Typography>
              </Grid>
            </Grid>}
          </TransparentPaper> */}
        </Grid>
      </Grid>
    </BackgroundedDiv>
  );
};

export default CategoryPreviewImage;
