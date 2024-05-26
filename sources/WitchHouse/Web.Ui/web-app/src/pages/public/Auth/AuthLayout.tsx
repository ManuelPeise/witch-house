import React from 'react';
import { Outlet } from 'react-router-dom';
import './auth.css';

const AuthLayout: React.FC = () => {
  return (
    <div className="container">
      <div>
        <Outlet />
      </div>
    </div>
  );
};

export default AuthLayout;
