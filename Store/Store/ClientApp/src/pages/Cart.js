import React from "react";
import { Box, Grid } from "@mui/material";
import { Typography } from "@mui/material";
import { styled } from "@mui/styles";

const Content = styled(Box)(({ theme }) => ({
  alignSelf: "center",
  padding: theme.spacing(0, 32),
  margin: theme.spacing(18, 0),
}));

const ItemGridContainer = styled(Grid)(({ theme }) => ({
  width: 1200,
  marginTop: theme.spacing(4),
}));

const Cart = () => {
  return (
    <>
      <Content>
        <Grid
          container
          spacing={3}
          justifyContent="center"
          alignItems="center"
          wrap="nowrap"
        >
          <Grid
            item
            container
            spacing={2}
            justifyContent="center"
            alignItems="center"
          >
            <Grid item>
              <Typography variant="h1" color="initial">
                Your cart items
              </Typography>
            </Grid>
          </Grid>
        </Grid>
      </Content>
    </>
  );
};

export default Cart;
