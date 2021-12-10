import React, { useState, useEffect } from "react";
import {
  TextField,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  OutlinedInput,
  Input,
  Typography,
} from "@mui/material";
import { styled } from "@mui/material/styles";
import { fetchWrapper, get, post } from "../../utils/fetchWrapper";

const DeleteSubcategory = ({ open, onHide }) => {
  const [subcategory, setSubcategory] = useState("");
  const [subcategoryId, setSubcategoryId] = useState(0);

  const deleteSubcategory = () => {
    post("api/Categories/DeleteCategory", console.log, {
      id: subcategoryId,
    });

    setSubcategory("");
    setSubcategoryId(0);
    setCategories([]);
    onHide();
  };

  const [categories, setCategories] = useState([]);
  useEffect(() => {
    get("api/Categories/GetCategories", setCategories);
  }, []);

  const StyledInput = styled(TextField)(({ theme }) => ({
    margin: "dense",
    variant: "outlined",
    width: "100%",
    margin: theme.spacing(2, 0),
  }));

  const ITEM_HEIGHT = 48;
  const ITEM_PADDING_TOP = 8;
  const MenuProps = {
    PaperProps: {
      style: {
        maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
        width: 300,
      },
    },
  };

  return (
    <Dialog open={open} onClose={onHide}>
      <DialogTitle>Delete subcategory</DialogTitle>
      <DialogContent>
        <FormControl sx={{ width: 400, marginBlock: 5 }}>
          <InputLabel id="demo-multiple-name-label">Category</InputLabel>
          <Select
            labelId="demo-multiple-name-label"
            id="demo-multiple-name"
            input={<OutlinedInput label="Choose category" />}
            MenuProps={MenuProps}
          >
            {categories
              .filter((c) => c.id > 1)
              .map((subcategory) => (
                <MenuItem
                  key={subcategory.id}
                  value={subcategory}
                  onClick={() => (
                    setSubcategoryId(subcategory.id),
                    setSubcategory(subcategory.name)
                  )}
                >
                  {subcategory.name}
                </MenuItem>
              ))}
          </Select>
        </FormControl>
      </DialogContent>
      <DialogActions style={{ paddingInline: 25, paddingBottom: 20 }}>
        <Button onClick={onHide}>Cancel</Button>
        <Button onClick={deleteSubcategory}>Delete</Button>
      </DialogActions>
    </Dialog>
  );
};

export default DeleteSubcategory;
