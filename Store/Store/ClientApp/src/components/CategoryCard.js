import React, { useEffect, useState, useRef } from "react";
import {
  Box,
  Card,
  CardContent,
  CardMedia,
  Grid,
  Typography,
  Skeleton,
} from "@mui/material";
import { Link } from "react-router-dom";
import { styled } from "@mui/material/styles";

import { get, getFile } from "../utils/fetchWrapper";

const CategoryCardContent = styled(CardContent)(({ theme }) => ({
  paddingBottom: 10,
  "&:last-child": {
    paddingBottom: 10,
  },
}));

const CustomCard = styled(Card)(({ theme }) => ({
  filter: "drop-shadow(0px 4px 24px rgba(10, 10, 10, 0.22))",
}));

const CategoryCard = (props) => {
  const widthRef = useRef(null);
  const [categoryInfo, setCategoryInfo] = useState(null);
  const [height, setHeight] = useState(0);
  const [image, setImage] = useState(null);

  useEffect(() => {
    setHeight(widthRef.current.clientWidth * 1.5);
    get(
      `https://localhost:44498/api/Categories/GetCategory?id=${props.id}`,
      setCategoryInfo
    );
    getFile(`api/Categories/GetInsideImage?id=${props.id}`, setImage);
  }, []);

  return (
    <Link to={`category/${props.id}`} style={{ textDecoration: "none" }}>
      <CustomCard elevation={0} ref={widthRef}>
        {image ? (
          <CardMedia component="img" image={image} />
        ) : (
          <Skeleton variant="rectangular" width="100%" height={height} />
        )}
        <CategoryCardContent>
          <Typography variant="h1" color="initial" textAlign="center">
            {categoryInfo ? categoryInfo.name : <Skeleton />}
          </Typography>
        </CategoryCardContent>
      </CustomCard>
    </Link>
  );
};

export default CategoryCard;
