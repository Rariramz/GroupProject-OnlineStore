import React, { createContext } from "react";
import ReactDOM from "react-dom";
import App from "./App";
import UserStore from "./store/UserStore";
import ItemsStore from "./store/ItemsStore";

export const Context = createContext(null);

ReactDOM.render(
  <Context.Provider
    value={{
      user: new UserStore(),
      items: new ItemsStore(),
    }}
  >
    <App />
  </Context.Provider>,
  document.getElementById("root")
);
