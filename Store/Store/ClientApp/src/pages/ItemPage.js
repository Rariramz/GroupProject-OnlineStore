import {
  Grid,
  Typography,
  Button,
  ButtonGroup,
  Link,
  Box,
  Paper,
} from "@mui/material";
import { styled } from "@mui/material/styles";
import React, { useRef, useState, useEffect } from "react";

import itemImage from "../images/dummyItemImage.jpg";

const Content = styled(Box)(({ theme }) => ({
  padding: theme.spacing(0, 32),
  margin: theme.spacing(18, 0),
}));

const ItemPage = (props) => {
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
      <Content>
        <Grid
          container
          spacing={5}
          direction="row"
          justify="flex-start"
          alignItems="stretch"
          wrap="nowrap"
        >
          <Grid item xs={6}>
            <img src={itemImage} style={{ width: "100%" }} />
            <Typography
              variant="h2"
              color="initial"
              textAlign="center"
              style={{
                paddingInline: 35,
                paddingBlock: 20,
              }}
            >
              All hand-made with natural soy wax, Candleaf is made for your
              pleasure moments.
            </Typography>
            <Typography
              variant="h2"
              color="primary"
              textAlign="center"
              style={{
                paddingInline: 35,
              }}
            >
              FREE SHIPPING.
            </Typography>
          </Grid>
          <Grid
            container
            spacing={3}
            direction="column"
            justify="center"
            alignItems="flex-start"
            wrap="nowrap"
            xs={7}
            style={{ backgroundColor: "white", padding: 40 }}
          >
            <Grid item>
              <Typography
                variant="h1"
                color="initial"
                style={{ fontWeight: "bold" }}
              >
                Candle Name
              </Typography>
            </Grid>
            <Grid item container spacing={10}>
              <Grid item xs={4}>
                <Typography
                  variant="h1"
                  color="primary"
                  style={{ fontWeight: "bold" }}
                >
                  $ 9.99
                </Typography>
              </Grid>
              <Grid item xs={8}>
                <Typography
                  variant="h1"
                  color="initial"
                  style={{ fontWeight: "bold" }}
                >
                  Category
                </Typography>
              </Grid>
              <Grid
                item
                xs={4}
                container
                justifyContent="flex-start"
                alignItems="center"
                spacing={2}
              >
                <Grid item xs={12} textAlign="left">
                  <Typography variant="h2" color="initial">
                    Quantity:
                  </Typography>
                </Grid>
                <Grid item>
                  <Typography variant="h2" color="initial">
                    12
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
              <Grid item xs={8}>
                <Button size="large">
                  <Typography variant="h2" style={{ fontWeight: "bold" }}>
                    Add to cart
                  </Typography>
                </Button>
              </Grid>
              <Grid item xs={12}>
                <Paper elevation={5} style={{ padding: 30 }}>
                  <Typography
                    variant="h3"
                    color="initial"
                    style={{ fontWeight: "bold" }}
                  >
                    Description:
                  </Typography>
                  <Typography variant="body1" color="textSecondary">
                    Lorem Ipsum is simply dummy text of the printing and
                    typesetting industry. Lorem Ipsum has been the industry's
                    standard dummy text ever since the 1500s, when an unknown
                    printer took a galley of type and scrambled it to make a
                    type specimen book. It has survived not only five centuries,
                    but also the leap into electronic typesetting, remaining
                    essentially unchanged. It was popularised in the 1960s with
                    the release of Letraset sheets containing Lorem Ipsum
                    passages, and more recently with desktop publishing software
                    like Aldus PageMaker including versions of Lorem Ipsum.
                  </Typography>
                </Paper>
              </Grid>
            </Grid>
          </Grid>
        </Grid>
      </Content>
    </>
  );
};

export default ItemPage;
