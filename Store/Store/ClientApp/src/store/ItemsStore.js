import { makeAutoObservable } from "mobx";

export default class ItemsStore {
  constructor() {
    this._categories = [];
    this._subcategories = [];
    this._parentId = 1;
    makeAutoObservable(this);
  }

  setCategories(mainCategories) {
    this._categories = mainCategories;
  }
  setSubcategories(allCategories) {
    this._subcategories = allCategories.filter(
      (c) => c.parentID == this._parentId
    );
  }
  setParentId(parentId) {
    this._parentId = parentId;
  }

  get categories() {
    return this._categories;
  }
  get subcategories() {
    return this._subcategories;
  }
  get parentId() {
    return this._parentId;
  }
}
