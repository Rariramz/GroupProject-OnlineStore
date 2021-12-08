import { Box, Typography, Grid, Container } from "@mui/material";
import { makeStyles } from "@mui/styles";
import { styled } from "@mui/material/styles";
import React, { useRef } from "react";

import ItemCard from "../components/ItemCard";
import PrettyPreviewImage from "../components/PrettyPreviewImage";
import CandleInfo from "../components/CandleInfo";
import Header from "../components/Header";

const Content = styled(Box)(({ theme }) => ({
  padding: theme.spacing(0, 32),
  margin: theme.spacing(18, 0),
}));

const ProductsGridContainer = styled(Grid)(({ theme }) => ({
  width: 1200,
  marginTop: theme.spacing(4),
}));

const Home = () => {
  const productsRef = useRef(null);

  const scrollToProducts = () => {
    productsRef.current.scrollIntoView({ behavior: "smooth" });
  };

  return (
    <>
      <PrettyPreviewImage onClick={scrollToProducts} />
      <Content ref={productsRef}>
        <Grid
          container
          spacing={18}
          direction="column"
          justify="center"
          alignItems="center"
          alignContent="center"
          wrap="nowrap"
        >
          <Grid
            item
            container
            spacing={1}
            direction="column"
            justify="center"
            alignItems="center"
            alignContent="center"
            wrap="nowrap"
          >
            <Grid item>
              <Typography variant="h1" color="initial">
                Products
              </Typography>
            </Grid>
            <Grid item>
              <Typography variant="body1" color="textSecondary">
                Order it for you or for your beholved ones
              </Typography>
            </Grid>

            <ProductsGridContainer item container spacing={4}>
              <Grid item xs={3}>
                <ItemCard />
              </Grid>
              <Grid item xs={3}>
                <ItemCard />
              </Grid>
              <Grid item xs={3}>
                <ItemCard />
              </Grid>
              <Grid item xs={3}>
                <ItemCard />
              </Grid>
              <Grid item xs={3}>
                <ItemCard />
              </Grid>
              <Grid item xs={3}>
                <ItemCard />
              </Grid>
              <Grid item xs={3}>
                <ItemCard />
              </Grid>
              <Grid item xs={3}>
                <ItemCard />
              </Grid>
            </ProductsGridContainer>
          </Grid>
          <Grid item></Grid>
        </Grid>
      </Content>
      <CandleInfo onClick={scrollToProducts} />
    </>
  );
};

export default Home;
