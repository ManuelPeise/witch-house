import React from 'react';
import { AuthContext } from '../_context/AuthContext';

export const useAuth = () => {
  return React.useContext(AuthContext);
};
