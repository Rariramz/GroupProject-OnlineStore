import React, { useState, useEffect } from "react";
import { styled } from "@mui/system";
import { Box, Paper, Grid, Button, Typography, Skeleton } from "@mui/material";
import { getFile } from "../utils/fetchWrapper";

import backgroundImage from "../images/homeBackgoundImage.png";

const BackgroundedDiv = styled(Box)(({ theme }) => ({
  width: "70%",
  height: "60vh",
  marginLeft: 70,
  borderRadius: 20,
  jusitfySelf: "center",
  backgroundImage: `Url(${backgroundImage})`,
  backgroundSize: "cover",
}));

const CategoryPreviewImage = (props) => {
  const [image, setImage] = useState(null);

  useEffect(() => {
    getFile(`api/Categories/GetInsideImage?id=${props.id}`, setImage);
  }, []);

  return (
    <>
      {image ? (
        <BackgroundedDiv style={{ backgroundImage: `Url(${image})` }}>
          <Grid
            container
            spacing={0}
            direction="row"
            alignItems="center"
            justifyContent="center"
            style={{ height: "100%" }}
          >
            <Grid item>
              <Typography color="white" fontSize={80}>
                {props.name && props.name}
              </Typography>
            </Grid>
          </Grid>
        </BackgroundedDiv>
      ) : (
        <Skeleton
          style={{
            width: "70%",
            height: "80vh",
            marginLeft: 70,
            borderRadius: 20,
            jusitfySelf: "center",
          }}
        />
      )}
    </>
  );
};

export default CategoryPreviewImage;
