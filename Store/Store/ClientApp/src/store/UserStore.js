import { makeAutoObservable } from "mobx";

export default class UserStore {
  constructor() {
    this._isAuth = false;
    this._isAdmin = false;
    makeAutoObservable(this);
  }

  setIsAuth(bool) {
    this._isAuth = bool;
  }
  setIsAdmin(bool) {
    this._isAdmin = bool;
  }

  get isAuth() {
    return this._isAuth;
  }
  get isAdmin() {
    return this._isAdmin;
  }
}
