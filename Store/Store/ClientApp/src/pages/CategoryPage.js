import React, { useState, useRef } from "react";
import { Box, Typography, Grid, Container, Divider, Chip } from "@mui/material";
import { styled } from "@mui/material/styles";
import PrettyPreviewImage from "../components/PrettyPreviewImage";
import ItemCard from "../components/ItemCard";

const Content = styled(Box)(({ theme }) => ({
  padding: theme.spacing(0, 32),
  margin: theme.spacing(18, 0),
}));

const ItemGridContainer = styled(Grid)(({ theme }) => ({
  width: 1200,
  marginTop: theme.spacing(4),
}));

const CategoryPage = () => {
  const productsRef = useRef(null);
  const [subcategories, setSubcategories] = useState(["2", "2", "2", "2"]);
  const [popularProducts, PopularProducts] = useState(["2", "2", "2", "2"]);

  const renderSubcategory = () => (
    <>
      <Divider textAlign="left">
        <Typography variant="h1" color="textSecondary">
          Second
        </Typography>
      </Divider>
      <ItemGridContainer item container spacing={4}>
        {popularProducts.map((item) => (
          <Grid item xs={3}>
            <ItemCard id={item.id} sx />
          </Grid>
        ))}
      </ItemGridContainer>
    </>
  );

  return (
    <>
      <PrettyPreviewImage />
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
          {subcategories.map((item) => (
            <Grid item>{renderSubcategory(item)}</Grid>
          ))}
        </Grid>
      </Content>
    </>
  );
};

export default CategoryPage;
