import React, { useState } from "react";
import { styled } from "@mui/system";
import { Button, Paper, Box, Grid, Typography } from "@mui/material";
import CreateSubcategory from "../components/modals/CreateSubcategory";
import CreateItem from "../components/modals/CreateItem";
import DeleteSubcategory from "../components/modals/DeleteSubcategory";

const Admin = () => {
  const [addSubcategoryVisible, setAddSubcategoryVisible] = useState(false);
  const [deleteSubcategoryVisible, setDeleteSubcategoryVisible] =
    useState(false);
  const [addItemVisible, setAddItemVisible] = useState(false);
  const [deleteItemVisible, setDeleteItemVisible] = useState(false);

  const StyledPaper = styled(Paper)(({ theme }) => ({
    width: 700,
    height: 400,
    padding: theme.spacing(5, 30),
    boxSizing: "border-box",
    background: "#fff", //"rgba(247, 248, 250, 0.8)",
    borderRadius: 6,
  }));
  const ColoredBackground = styled(Box)(({ theme }) => ({
    width: "100%",
    height: "100vh",
    backgroundColor: "rgb(238, 247, 242)",
    backgroundSize: "cover",
  }));

  return (
    <ColoredBackground>
      <Grid
        container
        spacing={0}
        direction="column"
        alignItems="center"
        justifyContent="center"
        style={{ minHeight: "100vh" }}
      >
        <StyledPaper>
          <Grid
            container
            item
            spacing={4}
            direction="column"
            alignItems="center"
            justifyContent="center"
          >
            <Grid item sx={{ marginBottom: 3 }}>
              <Typography variant="h1">Admin panel</Typography>
            </Grid>

            <Grid
              container
              item
              spacing={2}
              direction="column"
              alignItems="center"
              justifyContent="center"
            >
              <Grid item>
                <Button onClick={() => setAddSubcategoryVisible(true)}>
                  Add subcategory
                </Button>
              </Grid>
              <Grid item>
                <Button
                  style={{ background: "hotpink" }}
                  onClick={() => setDeleteSubcategoryVisible(true)}
                >
                  Delete subcategory
                </Button>
              </Grid>
            </Grid>

            <Grid
              container
              item
              spacing={2}
              direction="column"
              alignItems="center"
              justifyContent="center"
            >
              <Grid item>
                <Button onClick={() => setAddItemVisible(true)}>
                  Add item
                </Button>
              </Grid>
              <Grid item>
                <Button style={{ background: "hotpink" }}>Delete item</Button>
              </Grid>
            </Grid>

            <CreateSubcategory
              open={addSubcategoryVisible}
              onHide={() => setAddSubcategoryVisible(false)}
            />
            <DeleteSubcategory
              open={deleteSubcategoryVisible}
              onHide={() => setDeleteSubcategoryVisible(false)}
            />
            <CreateItem
              open={addItemVisible}
              onHide={() => setAddItemVisible(false)}
            />
            {/* <DeleteItem
              open={deleteItemVisible}
              onHide={() => setDeleteItemVisible(false)}
            /> */}
          </Grid>
        </StyledPaper>
      </Grid>
    </ColoredBackground>
  );
};

export default Admin;
