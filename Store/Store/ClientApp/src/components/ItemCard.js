import { Card, CardContent, CardMedia, Grid, Typography } from "@mui/material";
import { Link } from "react-router-dom";
import { styled } from "@mui/material/styles";
import React from "react";

import itemImage from "../images/dummyItemImage.jpg";

const ItemCardContent = styled(CardContent)(({ theme }) => ({
  paddingBottom: 10,
  "&:last-child": {
    paddingBottom: 10,
  },
}));

const CustomCard = styled(Card)(({ theme }) => ({
  filter: "drop-shadow(0px 4px 24px rgba(10, 10, 10, 0.22))",
}));

const ItemCard = (props) => {
  return (
    <>
      <Link to={`item/${props.id || 0}`} style={{ textDecoration: "none" }}>
        <CustomCard elevation={0}>
          <CardMedia component="img" title="" image={itemImage} />
          <ItemCardContent>
            <Typography variant="body2" color="initial" textAlign="left">
              Cool candle very good
            </Typography>
            <Typography variant="body1" color="primary" textAlign="right">
              9.99$
            </Typography>
          </ItemCardContent>
        </CustomCard>
      </Link>
    </>
  );
};

export default ItemCard;
