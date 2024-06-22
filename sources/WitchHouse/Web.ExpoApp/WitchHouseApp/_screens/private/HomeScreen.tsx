import React from 'react';
import { Pressable, StyleSheet, View, Text } from 'react-native';
import { useAuth } from '../../_hooks/useAuth';
import { getFirstFromTableByKeyQueryCallback, sqLiteTables } from '../../_lib/_database/databaseQueries';
import { UserTableModel } from '../../_lib/_types/sqLite';
import { Database } from '../../_lib/_database/sqLiteDatabase';

const HomeScreen: React.FC = () => {
  const { loginResult } = useAuth();
  const database = new Database(false);

  const execute = React.useCallback(async () => {
    const result = await database.executeSingleQuery<UserTableModel>(
      getFirstFromTableByKeyQueryCallback(sqLiteTables.userTable, '*', 'userId', loginResult.userGuid)
    );

    console.log('USER:', result?.data);
  }, [database]);

  return (
    <View style={styles.container}>
      <Pressable onPress={execute}>
        <Text>Load</Text>
      </Pressable>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    padding: 10,
  },
});
export default HomeScreen;
