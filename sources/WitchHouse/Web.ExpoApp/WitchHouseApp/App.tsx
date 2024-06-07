import 'react-native-gesture-handler';
import { NavigationContainer } from '@react-navigation/native';
import AppRouter from './_screens/public/AppRouter';
import './_lib/_translation/i18n';
import AuthContextProvider from './_context/AuthContext';

const App: React.FC = () => {
  return (
    <AuthContextProvider>
      <NavigationContainer>
        <AppRouter />
      </NavigationContainer>
    </AuthContextProvider>
  );
};

export default App;
