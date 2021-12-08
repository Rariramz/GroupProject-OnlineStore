import React from "react";
import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";

import itemImage from "../images/dummyItemImage.jpg";
import { Box } from "@mui/system";

const CartItem = (props) => {
  return (
    <>
      <Grid container spacing={1}>
        <Grid item xs={6} style={{ backgroundColor: "grey" }}>
          <Typography variant="h3" color="initial">
            Product
          </Typography>
        </Grid>
        <Grid
          item
          container
          justifyContent="space-between"
          xs={6}
          style={{ backgroundColor: "cyan" }}
        >
          <Grid item xs={2}>
            <Box style={{ width: 10 }}>
              <img src={itemImage} style={{ objectFit: "contain" }} />
            </Box>
          </Grid>
          <Grid item xs={4}>
            <Typography variant="h3" color="initial">
              Quantity
            </Typography>
          </Grid>
          <Grid item xs={4}>
            <Typography variant="h3" color="initial">
              Total
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
