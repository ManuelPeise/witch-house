import React from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { useAuth } from './AuthContext';
import AuthLayout from '../pages/public/Auth/AuthLayout';
import LoginPage from '../pages/public/Auth/LoginPage';
import './translation/i18n';
import RegisterPage from '../pages/public/Auth/RegisterPage';
import LandingPage from '../pages/private/landingPage/LandingPage';
import PrivatePagePayout from '../pages/private/PrivatePageLayout';
import ProfilePageContainer from '../pages/private/profile/ProfilePageContainer';
import LogPage from '../pages/private/administration/LogPage';

export enum RouteTypes {
  Login = '/',
  Register = '/register',
  LandingPage = '/',
  Profile = '/profile',
  Log = 'administration/log',
}

const Router: React.FC = () => {
  const { loginResult } = useAuth();

  var routes = React.useMemo(() => {
    if (loginResult && loginResult?.success) {
      return (
        <Routes>
          <Route path={RouteTypes.LandingPage} Component={PrivatePagePayout}>
            <Route index Component={LandingPage} />
            <Route path={RouteTypes.Profile} Component={ProfilePageContainer} />
            <Route path={RouteTypes.Log} Component={LogPage} />
          </Route>
        </Routes>
      );
    }
    return (
      <Routes>
        <Route path={RouteTypes.Login} Component={AuthLayout}>
          <Route path={RouteTypes.Login} Component={LoginPage} />
          <Route path={RouteTypes.Register} Component={RegisterPage} />
        </Route>
      </Routes>
    );
  }, [loginResult]);

  return <BrowserRouter>{routes}</BrowserRouter>;
};

export default Router;
