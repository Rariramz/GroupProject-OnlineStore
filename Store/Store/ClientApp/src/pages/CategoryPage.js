import React, { useState, useRef, useEffect } from "react";
import {
  Box,
  Typography,
  Grid,
  Container,
  Divider,
  Chip,
  Skeleton,
} from "@mui/material";
import { styled } from "@mui/material/styles";
import { useParams } from "react-router-dom";

import ItemCard from "../components/ItemCard";
import { get, getFile } from "../utils/fetchWrapper";
import CategoryPreviewImage from "../components/CategoryPreviewImage";
const Content = styled(Box)(({ theme }) => ({
  padding: theme.spacing(0, 32),
  margin: theme.spacing(18, 0),
}));

const ItemGridContainer = styled(Grid)(({ theme }) => ({
  width: 1200,
  marginTop: theme.spacing(4),
}));

const CategoryPage = () => {
  const { id } = useParams();
  const [info, setInfo] = useState();
  const [subcategories, setSubcategories] = useState([]);

  useEffect(() => {
    get(`api/Categories/GetCategory?id=${id}`, getSubcategories);
    get(`api/Categories/GetImage?id=${id}`, console.log);
  }, []);

  const getSubcategories = (info) => {
    setInfo(info);
    console.log(info);
    info.childCategoriesIDs.forEach((id) =>
      get(`api/Categories/GetCategory?id=${id}`, (newCategory) =>
        setSubcategories((prevState) => [...prevState, newCategory])
      )
    );
  };

  const renderSubcategory = (subcategory) => {
    return (
      <>
        <Divider textAlign="left">
          <Typography variant="h1" color="textSecondary">
            {subcategory.name}
          </Typography>
        </Divider>
        <ItemGridContainer item container spacing={4}>
          {subcategory.itemsIDs.map((id) => (
            <Grid item xs={3}>
              <ItemCard id={id} key={id} />
            </Grid>
          ))}
        </ItemGridContainer>
      </>
    );
  };

  return (
    <>
      <Content>
        <Grid
          container
          spacing={18}
          direction="column"
          justify="center"
          alignItems="center"
          alignContent="center"
          marginTop={10}
        >
          {info ? (
            <CategoryPreviewImage id={id} name={info?.name} />
          ) : (
            <Box height="100vh"></Box>
          )}
          {subcategories.map((subcategory) => (
            <Grid item>{renderSubcategory(subcategory)}</Grid>
          ))}
        </Grid>
      </Content>
    </>
  );
};

export default CategoryPage;
