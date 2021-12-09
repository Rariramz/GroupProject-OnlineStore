import React from "react";
import { Box, Grid, Link, Stack, Divider, Button } from "@mui/material";
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
            <Stack divider={<Divider flexItem />} spacing={2}>
              <CartItemsHeader />
              <CartItem />
              <CartItem />
              <CartItem />
            </Stack>
          </Grid>
          <Grid
            item
            container
            justifyContent="flex-end"
            alignItems="center"
            spacing={10}
          >
            <Grid item>
              <Typography variant="h2" color="initial" textAlign="right">
                Subtotal:
              </Typography>
            </Grid>
            <Grid item>
              <Typography variant="h1" color="initial" textAlign="right">
                $ {19.29}
              </Typography>
            </Grid>
            <Grid item>
              <Button size="large">
                <Typography variant="h1" color="white">
                  Check-Out
                </Typography>
              </Button>
            </Grid>
          </Grid>
        </Grid>
      </Content>
    </>
  );
};

export default Cart;
