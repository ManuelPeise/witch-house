import ReactDOM from 'react-dom/client';
import reportWebVitals from './reportWebVitals';
import AuthenticationContext from './lib/AuthContext';
import Router from './lib/Router';
import './index.css';

const root = ReactDOM.createRoot(document.getElementById('root') as HTMLElement);

root.render(
  <AuthenticationContext>
    <Router />
  </AuthenticationContext>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
