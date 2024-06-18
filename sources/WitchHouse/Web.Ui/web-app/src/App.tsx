import React from 'react';
import { useAuth } from './lib/AuthContext';
import axiosClient from './lib/api/axiosClient';
import './lib/translation/i18n';

const App: React.FC = () => {
  const { loginResult, onLogin, onLogout } = useAuth();

  const handleLogin = React.useCallback(async () => {
    await onLogin({ userName: 'manuel.peise@berlin', secret: 'Secret' });
  }, [onLogin]);

  return (
    <div>
      <div>{process.env.REACT_APP_Name}</div>
      {loginResult && (
        <div>
          <div>{'JWT:' + loginResult.jwtData.jwtToken}</div>
          <div>{'AuthHeader: - ' + axiosClient.defaults.headers.common['Authorization']}</div>
          <button onClick={onLogout}>Logout</button>
        </div>
      )}
      <button onClick={handleLogin}>Login</button>
    </div>
  );
};

export default App;
