using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mono.Data.Sqlite;
using System.Data;
using System;
using System.Reflection;

namespace Assets.Script.GameStruct
{
    public class Sqlite
    {
        //连接器
        private SqliteConnection dbConnection;
        //查询命令
        private SqliteCommand dbCommand;
        //sql 读取器
        private SqliteDataReader dataReader;


        public Sqlite(string path)
        {
            OpenDataBase(path);
        }

        private void OpenDataBase(string path)
        {
            string connectionString = "URI=file:" + Application.dataPath + "/Resources/" + path;
            dbConnection = new SqliteConnection(connectionString);
            Debug.Log("Open database: "+ connectionString);
            dbConnection.Open();
            //dbConnection.Open();

            //CloseDataBase();
            //DataTable dataTable = dbConnection.ExecuteQuery(sqlQuery);

            //foreach (DataRow dr in dataTable.Rows)
            //{
            //    string name = (string)dr["name"];
            //    Debug.Log("name:" + name);
            //}
        }

        public SqliteDataReader SelectDataBase(string tableName)
        {
            dbCommand = dbConnection.CreateCommand();
            string sqlQuery = "SELECT * FROM " + tableName;
            dbCommand.CommandText = sqlQuery;
            dataReader = dbCommand.ExecuteReader();
            return dataReader;
        }

        public void CloseDataBase()
        {
            dbCommand.Cancel();
            dataReader.Close();
            dbConnection.Close();
            dbConnection.Dispose();
            Debug.Log("closed");
        }
    }
}
