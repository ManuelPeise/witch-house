import React from 'react';
import { useAuth } from '../../../lib/AuthContext';

const LandingPage: React.FC = () => {
  const { loginResult } = useAuth();

  return (
    <div>
      <div>{'LandingPage: ' + loginResult?.userName}</div>
    </div>
  );
};

export default LandingPage;
