import React, { useEffect, useState, useRef } from "react";
import {
  Grid,
  Typography,
  ButtonGroup,
  Button,
  Skeleton,
  Link,
} from "@mui/material";

import fetchWrapper, { get, getFile, post } from "../utils/fetchWrapper";

const CartItem = (props) => {
  const widthRef = useRef(null);
  const [height, setHeight] = useState(0);
  const [image, setImage] = useState(null);
  const [info, setInfo] = useState(null);
  const [count, setCount] = useState(props.count);
  const [total, setTotal] = useState();

  useEffect(() => {
    setHeight(widthRef.current.clientWidth);
    get(`api/Items/GetItem?id=${props.id}`, setInfo);
    getFile(`api/Items/GetImage?id=${props.id}`, setImage);
  }, []);

  const handleCountInc = () => {
    props.onCountInc({
      ItemID: props.id,
      Count: 1,
    });
    setCount((prevState) => prevState + 1);
  };
  const handleCountDec = () => {
    props.onCountDec({
      ItemID: props.id,
      Count: -1,
    });
    setCount((prevState) => (prevState > 1 ? prevState - 1 : prevState));
  };
  const handleRemove = () => {
    props.onRemove({
      ItemID: props.id,
    });
  };

  return (
    <>
      <Grid container spacing={1}>
        <Grid item container xs={6}>
          <Grid item ref={widthRef} xs={4}>
            {image ? (
              <Link href={`../item/${props.id}`}>
                <img src={image} style={{ width: "80%" }} />
              </Link>
            ) : (
              <Skeleton width="80%" height={height * 0.8} />
            )}
          </Grid>
          <Grid
            item
            container
            direction="column"
            alignItems="flex-start"
            justifyContent="space-evenly"
            xs={8}
          >
            <Grid item>
              <Typography variant="h2" color="initial" paddingRight={10}>
                {info ? info.name : ""}
              </Typography>
            </Grid>
            <Grid item>
              <Button variant="text" color="primary" onClick={handleRemove}>
                <Typography variant="h3" color="primary">
                  Remove
                </Typography>
              </Button>
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
              ${info ? info.price : ""}
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
                {count || "3"}
              </Typography>
            </Grid>
            <Grid item>
              <ButtonGroup>
                <Button onClick={handleCountDec} size="small">
                  -
                </Button>
                <Button onClick={handleCountInc} size="small">
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
