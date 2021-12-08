import React from "react";
import {
  Card,
  CardContent,
  CardMedia,
  Grid,
  Typography,
  Skeleton,
} from "@mui/material";
import { styled } from "@mui/material/styles";

import categoryImage from "../images/prazdnicnye.jpg";

const CategoryCardContent = styled(CardContent)(({ theme }) => ({
  paddingBottom: 10,
  "&:last-child": {
    paddingBottom: 10,
  },
}));

const CustomCard = styled(Card)(({ theme }) => ({
  filter: "drop-shadow(0px 4px 24px rgba(10, 10, 10, 0.22))",
}));

const CategoryCard = () => {
  return (
    <CustomCard elevation={0}>
      <CardMedia component="img" title="Category" image={categoryImage} />
      <CategoryCardContent>
        <Typography variant="body2" color="initial" textAlign="left">
          Category Name
        </Typography>
        <Typography variant="body1" color="primary" textAlign="right">
          9.99$
        </Typography>
      </CategoryCardContent>
    </CustomCard>
  );
};

export default CategoryCard;
