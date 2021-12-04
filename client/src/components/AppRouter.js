import React from "react";
import { Routes, Route } from "react-router-dom";
import PageNotFound from "../pages/PageNotFound";
import { authRoutes, publicRoutes } from "../routes";

const AppRouter = () => {
  const isAuthorized = false;

  return (
    <Routes>
      {isAuthorized &&
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
