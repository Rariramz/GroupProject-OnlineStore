import React, { useEffect, useState } from "react";
import { Link, Grid, Typography, ButtonGroup, Button } from "@mui/material";

import itemImage from "../images/dummyItemImage.jpg";
import { Box, typography } from "@mui/system";

const CartItem = (props) => {
  const [quantity, setQuantity] = useState(props.quantity || 3);
  const [total, setTotal] = useState();

  useEffect(() => {
    setTotal(quantity * (props.price || 4));
  }, []);

  const handleQuantityInc = () => {
    setQuantity((prevState) => prevState + 1);
    setTotal(quantity * (props.price || 4));
  };
  const handleQuantityDec = () => {
    setQuantity((prevState) => (prevState > 1 ? prevState - 1 : prevState));
    setTotal(quantity * (props.price || 4));
  };
  return (
    <>
      <Grid container spacing={1}>
        <Grid item xs={6}>
          <Grid
            container
            spacing={5}
            direction="row"
            justify="flex-start"
            alignItems="stretch"
            wrap="nowrap"
          >
            <Grid item>
              <img src={itemImage} style={{ width: 250 }} />
            </Grid>
            <Grid
              item
              container
              direction="column"
              alignItems="flex-start"
              justifyContent="space-evenly"
            >
              <Grid item>
                <Typography variant="h2" color="initial">
                  {props.name || "Spiced mint cheto tam"}
                </Typography>
              </Grid>
              <Grid item>
                <Link href="">
                  <Typography variant="h3" color="primary">
                    Remove
                  </Typography>
                </Link>
              </Grid>
            </Grid>
          </Grid>
        </Grid>
        <Grid
          item
          container
          justifyContent="space-between"
          alignItems="center"
          xs={6}
        >
          <Grid item xs={4}>
            <Typography variant="h3" color="initial">
              ${props.price || "9.99"}
            </Typography>
          </Grid>
          <Grid
            item
            container
            direction="row"
            justifyContent="center"
            alignItems="center"
            spacing={1}
            xs={4}
          >
            <Grid item>
              <Typography variant="h3" color="initial">
                {quantity || "3"}
              </Typography>
            </Grid>
            <Grid item>
              <ButtonGroup>
                <Button onClick={handleQuantityDec} size="small">
                  -
                </Button>
                <Button onClick={handleQuantityInc} size="small">
                  +
                </Button>
              </ButtonGroup>
            </Grid>
          </Grid>
          <Grid item xs={4}>
            <Typography variant="h3" color="initial" textAlign="right">
              $ {total}
            </Typography>
          </Grid>
        </Grid>
      </Grid>
    </>
  );
};

export const CartItemsHeader = () => (
  <Grid container spacing={1}>
    <Grid item xs={6}>
      <Typography variant="h3" color="initial">
        Product
      </Typography>
    </Grid>
    <Grid item container justifyContent="space-between" xs={6}>
      <Grid item>
        <Typography variant="h3" color="initial">
          Price
        </Typography>
      </Grid>
      <Grid item>
        <Typography variant="h3" color="initial">
          Quantity
        </Typography>
      </Grid>
      <Grid item>
        <Typography variant="h3" color="initial">
          Total
        </Typography>
      </Grid>
    </Grid>
  </Grid>
);

export default CartItem;
