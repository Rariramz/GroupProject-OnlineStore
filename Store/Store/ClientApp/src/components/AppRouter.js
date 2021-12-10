import React, { useState, useEffect } from "react";
import { Routes, Route } from "react-router-dom";
import PageNotFound from "../pages/PageNotFound";
import { authRoutes, publicRoutes } from "../routes";
import { Context } from "../index";
import { fetchWrapper, get, post } from "../utils/fetchWrapper";

const AppRouter = () => {
  const [isAuth, setIsAuth] = useState(false);

  useEffect(() => {
    get("api/Account/Info", (res) => setIsAuth(res.success));
  }, []);

  return (
    <Routes>
      {isAuth &&
        authRoutes.map(({ path, component }) => (
          <Route key={path} path={path} element={component} exact />
        ))}
      {publicRoutes.map(({ path, component }) => (
        <Route key={path} path={path} element={component} exact />
      ))}
      <Route path="*" element={<PageNotFound />} />
    </Routes>
  );
};

export default AppRouter;
