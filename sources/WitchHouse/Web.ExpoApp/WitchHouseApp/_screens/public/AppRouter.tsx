import React from 'react';
import AuthStack from '../../_stacks/AuthStack';
import { useAuth } from '../../_hooks/useAuth';
import PrivateStack from '../../_stacks/PrivateStack';

const AppRouter: React.FC = () => {
  const { isAuthenticated, isLoading } = useAuth();

  return isAuthenticated ? <PrivateStack /> : <AuthStack isLoading={isLoading} />;
};

export default AppRouter;
