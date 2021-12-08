import { Grid, Container } from "@mui/material";
import React from "react";
import CategoryBar from "../components/CategoryBar";
import { Context } from "./index";

const SubcategoryPage = () => {
  const { items } = useContext(Context);

  useEffect(() => {
    fetchCategories().then((data) => items.setCategories(data));
    fetchSubcategories().then((data) => items.setSubcategories(data));
    fetchItems(null, null, 1, 2).then((data) => {
      items.setItems(data.rows);
      items.setTotalCount(data.count);
    });
  }, []);

  useEffect(() => {
    fetchItems(
      items.selectedCategory.id,
      items.selectedSubcategory.id,
      items.page,
      2
    ).then((data) => {
      items.setItems(data.rows);
      items.setTotalCount(data.count);
    });
  }, [items.page, items.selectedCategory, items.selectedSubcategory]);

  return (
    <>
      <Grid container>
        <Grid item xs={12}>
          <CategoryBar />
        </Grid>
        <Container>SUBCATEGORY PAGE</Container>
      </Grid>
    </>
  );
};

export default SubcategoryPage;
