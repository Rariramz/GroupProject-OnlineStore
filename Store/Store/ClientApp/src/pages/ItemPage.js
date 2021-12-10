import {
  Grid,
  Typography,
  Button,
  ButtonGroup,
  Link,
  Box,
  Paper,
  Skeleton,
} from "@mui/material";
import { styled } from "@mui/material/styles";
import React, { useRef, useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import ErrorBoundary from "../utils/ErrorBoundary";

import { get, getFile, post } from "../utils/fetchWrapper";
import PageNotFound from "./PageNotFound";

const Content = styled(Box)(({ theme }) => ({
  padding: theme.spacing(0, 32),
  margin: theme.spacing(18, 0),
}));

const ItemPage = (props) => {
  const widthRef = useRef(null);
  const { id } = useParams();
  const [height, setHeight] = useState(0);
  const [info, setInfo] = useState(null);
  const [image, setImage] = useState(null);
  const [categoryName, setCategoryName] = useState(null);
  const [count, setCount] = useState(1);

  const [invalid, setInvalid] = useState(false);

  const handleCountInc = () => {
    setCount((prevState) => prevState + 1);
  };
  const handleCountDec = () => {
    setCount((prevState) => (prevState > 1 ? prevState - 1 : prevState));
  };
  const handleAddToCart = () => {
    post("api/Cart/AddItemForUser", console.log, { ItemID: id, Count: count });
  };

  useEffect(() => {
    setHeight(widthRef.current.clientWidth);
    get(`api/Items/GetItem/?id=${id}`, (info) => {
      if (!info.invalid) setInfo(info);
      else {
        setInvalid(true);
      }
    });
    getFile(`api/Items/GetImage/?id=${id}`, setImage);
  }, []);

  const getCategoryName = () => {
    get(`api/Categories/GetCategory?id=${info.categoryID}`, ({ name }) =>
      setCategoryName(name)
    );
  };

  return (
    <>
      {!invalid ? (
        <Content>
          <Grid
            container
            spacing={5}
            direction="row"
            justify="flex-start"
            alignItems="stretch"
            wrap="nowrap"
          >
            <Grid item xs={6} ref={widthRef}>
              {image ? (
                <img src={image} width="100%" />
              ) : (
                <Skeleton variant="rectangular" width="100%" height={height} />
              )}
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
            {info && (
              <Grid
                item
                container
                spacing={10}
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
                    {info ? info.name : ""}
                  </Typography>
                </Grid>
                <Grid item container spacing={20}>
                  <Grid item xs={6}>
                    <Typography
                      variant="h2"
                      color="primary"
                      style={{ fontWeight: "bold" }}
                    >
                      $ {info ? info.price : ""}
                    </Typography>
                  </Grid>
                  <Grid item xs={6}>
                    <Typography
                      variant="h2"
                      color="initial"
                      style={{ fontWeight: "bold" }}
                    >
                      {categoryName
                        ? categoryName
                        : info
                        ? getCategoryName()
                        : ""}
                    </Typography>
                  </Grid>
                  <Grid
                    item
                    xs={12}
                    container
                    justifyContent="center"
                    alignItems="center"
                    spacing={5}
                  >
                    <Grid item xs={4}>
                      <Typography variant="h2" color="initial">
                        Count:
                      </Typography>
                    </Grid>
                    <Grid item xs={4}>
                      <Typography
                        variant="h2"
                        color="initial"
                        textAlign="right"
                      >
                        {count}
                      </Typography>
                    </Grid>
                    <Grid item xs={4}>
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
                  <Grid item xs={12}>
                    {info && (
                      <Paper elevation={5} style={{ padding: 30 }}>
                        <Typography
                          variant="h3"
                          color="initial"
                          style={{ fontWeight: "bold" }}
                          lineHeight={3}
                        >
                          Description:
                        </Typography>
                        <Typography
                          variant="body1"
                          color="textSecondary"
                          lineHeight={1.8}
                        >
                          {info ? info.description : ""}
                        </Typography>
                      </Paper>
                    )}
                  </Grid>
                  <Grid item xs={6}></Grid>
                  <Grid item xs={6}>
                    <Button size="large" onClick={handleAddToCart}>
                      <Typography variant="h2" style={{ fontWeight: "bold" }}>
                        Add to cart
                      </Typography>
                    </Button>
                  </Grid>
                </Grid>
              </Grid>
            )}
          </Grid>
        </Content>
      ) : (
        <PageNotFound />
      )}
    </>
  );
};

export default ItemPage;
