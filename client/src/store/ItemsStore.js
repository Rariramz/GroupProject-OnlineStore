import { makeAutoObservable } from "mobx";

export default class ItemsStore {
  constructor() {
    this._categories = [];
    this._subcategories = [];
    this._items = [];
    this._selectedCategory = {};
    this._selectedSubcategory = {};
    this._page = 1;
    this._totalCount = 0;
    this._limit = 3;
    makeAutoObservable(this);
  }

  setCategories(categories) {
    this._categories = categories;
  }

  setSubcategories(subcategories) {
    this._subcategories = subcategories;
  }

  setItems(items) {
    this._items = items;
  }

  setSelectedCategory(category) {
    this.setPage(1);
    this._selectedCategory = category;
  }
  setSelectedSubcategory(subcategory) {
    this.setPage(1);
    this._selectedSubcategory = subcategory;
  }
  setPage(page) {
    this._page = page;
  }
  setTotalCount(count) {
    this._totalCount = count;
  }

  get categories() {
    return this._categories;
  }
  get subcategories() {
    return this._brands;
  }
  get devices() {
    return this._devices;
  }
  get selectedCategory() {
    return this._selectedCategory;
  }
  get selectedSubcategory() {
    return this._selectedSubcategory;
  }
  get totalCount() {
    return this._totalCount;
  }
  get page() {
    return this._page;
  }
  get limit() {
    return this._limit;
  }
}
