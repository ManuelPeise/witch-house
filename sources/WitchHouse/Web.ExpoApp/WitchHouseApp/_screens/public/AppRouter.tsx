import React from 'react';
import AuthStack from '../../_stacks/AuthStack';
import { useAuth } from '../../_hooks/useAuth';
import PrivateStack from '../../_stacks/PrivateStack';

const AppRouter: React.FC = () => {
  const { isAuthenticated } = useAuth();

  return isAuthenticated ? <PrivateStack /> : <AuthStack />;
};

export default AppRouter;