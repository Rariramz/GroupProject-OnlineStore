import React, { useState, useEffect } from "react";
import { Box, Grid, Link, Stack, Divider, Button } from "@mui/material";
import { Typography } from "@mui/material";
import { styled } from "@mui/styles";
import CartItem, { CartItemsHeader } from "../components/CartItem";
import { get, post } from "../utils/fetchWrapper";

const Content = styled(Box)(({ theme }) => ({
  alignSelf: "center",
  padding: theme.spacing(0, 32),
  margin: theme.spacing(18, 0),
}));

const Cart = () => {
  const [cartItems, setCartItems] = useState([]);
  const [total, setTotal] = useState(0);

  const onCountInc = (cartItemModel) => {
    post("api/Cart/ChangeItemCount", console.log, cartItemModel);
    getTotal();
    getTotal();
  };
  const onCountDec = (cartItemModel) => {
    post("api/Cart/ChangeItemCount", console.log, cartItemModel);
    getTotal();
    getTotal();
  };
  const onRemove = (cartItemModel) => {
    post("api/Cart/Remove", console.log, cartItemModel);
    console.log(cartItemModel);
    setCartItems((prevState) =>
      prevState.filter((item) => {
        return item.itemID != cartItemModel.ItemID;
      })
    );
    get("api/Cart/GetTotal", setTotal);
  };

  const getTotal = () => {
    get("api/Cart/GetTotal", setTotal);
  };

  useEffect(() => {
    get("api/Cart/GetTotal", setTotal);
    get("api/Cart/GetShoppingDetails", setCartItems);
  }, []);

  useEffect(() => {
    get("api/Cart/GetTotal", setTotal);
  });

  return (
    <>
      <Content>
        <Grid
          container
          spacing={6}
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
              {cartItems.map((item) => (
                <CartItem
                  id={item.itemID}
                  count={item.count}
                  onCountInc={onCountInc}
                  onCountDec={onCountDec}
                  onRemove={onRemove}
                  key={item.itemID}
                />
              ))}
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
              <Button variant="outlined" size="medium" onClick={getTotal}>
                <Typography variant="h2" color="primary">
                  Apply available discounts
                </Typography>
              </Button>
            </Grid>
            <Grid item>
              <Typography variant="h2" color="initial" textAlign="right">
                Subtotal:
              </Typography>
            </Grid>
            <Grid item>
              <Typography variant="h1" color="initial" textAlign="right">
                $ {total ? total : ""}
              </Typography>
            </Grid>
            <Grid item>
              <Button size="medium">
                <Typography variant="h2" color="white">
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
