import * as SQLite from 'expo-sqlite';
import { DatabaseQueryResult, SqLiteDatasetBase } from '../_types/sqLite';
import { createTableQueries, sqLiteTables } from './databaseQueries';

const databaseName = 'witchHouse.db';

export class Database {
  constructor() {}

  public executeSingleGetQuery = async <TModel>(query: string): Promise<DatabaseQueryResult<TModel>> => {
    const result: DatabaseQueryResult<TModel> = {
      data: null,
      error: null,
    };
    const connection: SQLite.SQLiteDatabase = await SQLite.openDatabaseAsync(databaseName);
    try {
      console.log('Db query ~sqLiteDatabase.ts~33:', query);
      result.data = await connection.getFirstAsync(query);
    } catch (err) {
      result.error = err;
    } finally {
      await connection.closeAsync();
    }

    return result;
  };

  public executeArrayGetQuery = async <TModel>(query: string): Promise<DatabaseQueryResult<TModel>> => {
    const result: DatabaseQueryResult<TModel> = {
      data: null,
      error: null,
    };
    const connection: SQLite.SQLiteDatabase = await SQLite.openDatabaseAsync(databaseName);
    try {
      console.log('Db query ~sqLiteDatabase.ts~49:', query);
      result.data = await connection.getAllAsync(query);
    } catch (err) {
      result.error;
    } finally {
      await connection.closeAsync();
    }
    return result;
  };

  public executeInsertQuery = async (query: string): Promise<DatabaseQueryResult<boolean>> => {
    const result: DatabaseQueryResult<boolean> = {
      data: null,
      error: null,
    };
    const connection: SQLite.SQLiteDatabase = await SQLite.openDatabaseAsync(databaseName);
    try {
      console.log('Db query ~sqLiteDatabase.ts~63:', query);
      await connection.execAsync(query);

      result.data = true;
    } catch (err) {
      result.error = err;
    } finally {
      await connection.closeAsync();
    }

    return result;
  };

  public updateOrCreateUserDataEntries = async (
    queries: string[],
    moduleTableQueries: string[]
  ): Promise<'success' | 'error'> => {
    const connection: SQLite.SQLiteDatabase = await SQLite.openDatabaseAsync(databaseName);

    let result: 'error' | 'success' = 'error';
    try {
      // create query to insert sync.-, user.- credential data
      queries.forEach((q) => {
        console.log('Db query ~sqLiteDatabase.ts~78:', q);
        if (q !== undefined) {
          connection.execSync(q);
        }
      });

      // create query to insert module entries
      moduleTableQueries.forEach((q) => {
        console.log('Db query: ~sqLiteDatabase.ts~86:', q);
        if (q !== undefined) {
          connection.execSync(q);
        }
      });

      result = 'success';
    } catch (err) {
      console.log('~sqLiteDatabase.ts~75 Error:', err);
      result = 'error';
    } finally {
      await connection.closeAsync();
      return result;
    }
  };

  public createDatabase = async () => {
    const connection: SQLite.SQLiteDatabase = await SQLite.openDatabaseAsync(databaseName);
    try {
      this.createTables([
        createTableQueries.syncTable,
        createTableQueries.userTable,
        createTableQueries.credentialTable,
        createTableQueries.userModuleTable,
      ]);
    } catch (err) {
      console.log('sqliteDatabase ~ 114', err);
    } finally {
      await connection.closeAsync();
    }
  };

  public getInsertedOrUpdatedModel = async <TModel extends SqLiteDatasetBase>(
    timeStamp: string,
    query: string,
    insertQuery: string,
    updateQuery: string
  ): Promise<TModel> => {
    const connection: SQLite.SQLiteDatabase = await SQLite.openDatabaseAsync(databaseName);
    let dataSet: TModel = {} as TModel;

    try {
      dataSet = (await this.executeSingleGetQuery<TModel>(query)).data as TModel;

      if (dataSet == null) {
        await connection.execAsync(insertQuery);
      } else if (dataSet != null && new Date(dataSet.updatedAt) < new Date(timeStamp)) {
        await connection.execAsync(updateQuery);
      }

      dataSet = (await this.executeSingleGetQuery<TModel>(query)).data as TModel;
    } catch (err) {
      console.log('sqliteDatabase ~ 135');
    } finally {
      await connection.closeAsync();
      return dataSet;
    }
  };
  private createTables = async (queries: string[]): Promise<'success' | 'error'> => {
    let query: string;
    const connection: SQLite.SQLiteDatabase = await SQLite.openDatabaseAsync(databaseName);
    try {
      queries.forEach((q) => {
        query += q;
      });

      if (query.length) {
        await connection.execAsync(query);
      }
      return 'success';
    } catch (err) {
      console.log('~sqLiteDatabase.ts~111 Error:', err);
      return 'error';
    } finally {
      await connection.closeAsync();
    }
  };
}
