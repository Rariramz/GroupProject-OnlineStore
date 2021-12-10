import React, { useState, useEffect } from "react";
import { Box, Grid, Link, Stack, Divider, Button, Dialog } from "@mui/material";
import { Typography } from "@mui/material";
import { styled } from "@mui/styles";
import CartItem, { CartItemsHeader } from "../components/CartItem";
import { get, post } from "../utils/fetchWrapper";
import AddressDialog from "../components/AddressDialog";
import { Api } from "@mui/icons-material";
import OkDialog from "../components/OkDialog";

const Content = styled(Box)(({ theme }) => ({
  alignSelf: "center",
  padding: theme.spacing(0, 32),
  margin: theme.spacing(18, 0),
}));

const Cart = () => {
  const [cartItems, setCartItems] = useState([]);
  const [total, setTotal] = useState(0);
  const [modalOpen, setModalOpen] = useState(false);
  const [okOpen, setOkOpen] = useState(false);

  const onCountInc = (cartItemModel) => {
    post("api/Cart/ChangeItemCount", console.log, cartItemModel);
    updateInfo();
  };
  const onCountDec = (cartItemModel) => {
    post("api/Cart/ChangeItemCount", console.log, cartItemModel);
    updateInfo();
  };
  const onRemove = (cartItemModel) => {
    post("api/Cart/Remove", console.log, cartItemModel);
    console.log(cartItemModel);
    updateInfo();
  };

  const handleCheckoutConfirm = (address) => {
    get(`api/Orders/MakeOrder?addressData=${address}`, (item) => {
      console.log(item);
      setOkOpen(true);
      get("api/Cart/ClearCart", console.log);
      updateInfo();
    });
    setModalOpen(false);
  };

  useEffect(() => {
    get("api/Cart/GetShoppingDetails", setCartItems);
    get("api/Cart/GetTotal", setTotal);
  });

  const updateInfo = () => {
    get("api/Cart/GetShoppingDetails", setCartItems);
    get("api/Cart/GetTotal", setTotal);
  };

  return (
    <>
      <AddressDialog
        open={modalOpen}
        handleClose={() => setModalOpen(false)}
        onCheckoutConfirm={handleCheckoutConfirm}
      />
      <OkDialog open={okOpen} handleClose={() => setOkOpen(false)} />
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
              {cartItems.length ? (
                cartItems.map((item) => (
                  <CartItem
                    id={item.itemID}
                    count={item.count}
                    onCountInc={onCountInc}
                    onCountDec={onCountDec}
                    onRemove={onRemove}
                    key={item.itemID}
                  />
                ))
              ) : (
                <Box padding="20vh">
                  <Typography variant="h1" color="primary" textAlign="center">
                    Cart is empty
                  </Typography>
                </Box>
              )}
            </Stack>
          </Grid>
          {cartItems.length > 0 && (
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
                  {total ? `$ ${total}` : ""}
                </Typography>
              </Grid>
              <Grid item>
                {cartItems.length > 0 ? (
                  <Button
                    size="medium"
                    onClick={() => {
                      setModalOpen(true);
                    }}
                  >
                    <Typography variant="h2" color="white">
                      Check-Out
                    </Typography>
                  </Button>
                ) : (
                  <Button size="medium" disabled>
                    <Typography variant="h2" color="white">
                      Check-Out
                    </Typography>
                  </Button>
                )}
              </Grid>
            </Grid>
          )}
        </Grid>
      </Content>
    </>
  );
};

export default Cart;
