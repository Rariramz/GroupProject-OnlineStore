import { Box, Typography, Grid, Container } from "@mui/material";
import { Link } from "react-router-dom";
import { styled } from "@mui/material/styles";
import React, { useRef, useState, useEffect } from "react";

import ItemCard from "../components/ItemCard";
import PrettyPreviewImage from "../components/PrettyPreviewImage";
import CandleInfo from "../components/CandleInfo";
import CategoryCard from "../components/CategoryCard";
import fetchWrapper, { get, post } from "../utils/fetchWrapper";

const Content = styled(Box)(({ theme }) => ({
  padding: theme.spacing(0, 32),
  margin: theme.spacing(18, 0),
}));

const ItemGridContainer = styled(Grid)(({ theme }) => ({
  width: 1200,
  marginTop: theme.spacing(4),
}));

const Home = () => {
  const productsRef = useRef(null);
  const [categories, setCategories] = useState(["2", "2", "2"]);
  const [popularProducts, setPopularProducts] = useState([]);

  const scrollToProducts = () => {
    productsRef.current.scrollIntoView({ behavior: "smooth" });
  };
  useEffect(() => {
    get("api/Items/GetPopularItems?count=8", setPopularProducts);
  }, []);

  const func = (res) => {
    if (res.success) {
      console.log("HOORAY");
    } else {
      console.log(res.errorCodes);
    }
  };

  const renderProductCategoryCard = () => {
    return (
      <Link to={`category/${0}`} style={{ textDecoration: "none" }}>
        <CategoryCard />
      </Link>
    );
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
                Candles
              </Typography>
            </Grid>
            <Grid item>
              <Typography variant="body1" color="textSecondary">
                You will find any candle your heart could wish for!
              </Typography>
            </Grid>
            <ItemGridContainer item container spacing={4}>
              {categories.map((item) => (
                <Grid item xs={4}>
                  {renderProductCategoryCard(item)}
                </Grid>
              ))}
            </ItemGridContainer>
          </Grid>
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
                Popular products
              </Typography>
            </Grid>
            <Grid item>
              <Typography variant="body1" color="textSecondary">
                Order it for you or for your beloved ones
              </Typography>
            </Grid>
            <ItemGridContainer item container spacing={4} alignItems="stretch">
              {popularProducts.map((item) => (
                <Grid item xs={3}>
                  <ItemCard id={item.id} />
                </Grid>
              ))}
            </ItemGridContainer>
          </Grid>
          <Grid item></Grid>
        </Grid>
      </Content>
      <CandleInfo onClick={scrollToProducts} />
    </>
  );
};

export default Home;
