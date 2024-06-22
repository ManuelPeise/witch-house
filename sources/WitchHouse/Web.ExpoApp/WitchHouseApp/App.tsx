import 'react-native-gesture-handler';
import { NavigationContainer } from '@react-navigation/native';
import AppRouter from './_screens/public/AppRouter';
import './_lib/_translation/i18n';
import AuthContextProvider from './_context/AuthContext';
import { Provider } from 'react-redux';
import { configureStore } from '@reduxjs/toolkit';
import AppReducer from './_reducer/AppReducer';

const appStore = configureStore({
  reducer: AppReducer,
});

const App: React.FC = () => {
  return (
    <Provider store={appStore}>
      <AuthContextProvider>
        <NavigationContainer>
          <AppRouter />
        </NavigationContainer>
      </AuthContextProvider>
    </Provider>
  );
};

export default App;
