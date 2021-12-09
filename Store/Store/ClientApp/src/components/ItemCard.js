import {
  Card,
  CardContent,
  CardMedia,
  Grid,
  Skeleton,
  Typography,
} from "@mui/material";
import { Link } from "react-router-dom";
import { styled } from "@mui/material/styles";
import React, { useEffect, useRef, useState } from "react";

import { get, getFile, getFileBase64 } from "../utils/fetchWrapper";

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
  const widthRef = useRef(null);
  const [height, setHeight] = useState(0);
  const [itemInfo, setItemInfo] = useState({});
  const [image, setImage] = useState(null);

  useEffect(() => {
    setHeight(widthRef.current.clientWidth);
    get(`api/Items/GetItem?id=${props.id}`, setItemInfo);
    getFile(`api/Items/GetImage?id=${props.id}`, setImage);
  }, []);
  return (
    <>
      <Link to={`../item/${props.id || 0}`} style={{ textDecoration: "none" }}>
        <CustomCard elevation={0} ref={widthRef}>
          {image ? (
            <CardMedia component="img" alt="No Image" image={image} />
          ) : (
            <Skeleton variant="rectangular" width="100%" height={height} />
          )}
          <ItemCardContent>
            <Typography variant="body2" color="initial" textAlign="left">
              {itemInfo ? itemInfo.name : <Skeleton />}
            </Typography>
            <Typography variant="body1" color="primary" textAlign="right">
              {itemInfo ? itemInfo.price : <Skeleton />}
            </Typography>
          </ItemCardContent>
        </CustomCard>
      </Link>
    </>
  );
};

export default ItemCard;
