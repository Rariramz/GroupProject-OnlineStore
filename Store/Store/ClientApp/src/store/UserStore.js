import { makeAutoObservable } from "mobx";

export default class UserStore {
  constructor() {
    this._isAuth = true;
    this._isAdmin = true;
    makeAutoObservable(this);
  }

  setIsAuth(bool) {
    this._isAuth = bool;
  }
  get isAuth() {
    return this._isAuth;
  }

  setIsAdmin(bool) {
    this._isAdmin = bool;
  }
  get isAdmin() {
    return this._isAdmin;
  }
}
