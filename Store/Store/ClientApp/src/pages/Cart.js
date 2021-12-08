import React from "react";
import { Box, Grid, Link } from "@mui/material";
import { Typography } from "@mui/material";
import { styled } from "@mui/styles";
import CartItem, { CartItemsHeader } from "../components/CartItem";

const Content = styled(Box)(({ theme }) => ({
  alignSelf: "center",
  padding: theme.spacing(0, 32),
  margin: theme.spacing(18, 0),
}));

const Cart = () => {
  return (
    <>
      <Content>
        <Grid
          container
          spacing={10}
          direction="column"
          justifyContent="center"
          alignItems="center"
          wrap="nowrap"
        >
          <Grid item>
            <Typography variant="h1" color="initial">
              Your cart items
            </Typography>
          </Grid>
          <Grid item>
            <Link href="">
              <Typography variant="h2" color="primary">
                Continue shopping
              </Typography>
            </Link>
          </Grid>
          <Grid item style={{ width: "100%" }}>
            <CartItemsHeader />
            <CartItem />
          </Grid>
        </Grid>
      </Content>
    </>
  );
};

export default Cart;
