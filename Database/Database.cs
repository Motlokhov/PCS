using System.Data.SqlServerCe;
using System.Collections.Generic;
using System;
using System.IO;
using System.Reflection;

namespace Database
{
    public enum TypeConnection {Select,Insert,Delete,Update};
    public enum DatabasePath { Interpretor, Results };
    public class Database
    {
        SqlCeConnection connection =new SqlCeConnection();
        SqlCeCommand command =new SqlCeCommand();
        public SqlCeDataReader Reader { get; private set; }

        //необходима реализация метода Dispose для Database
        public Database()
        {
            string connect = "Data Source ="+ Directory.GetCurrentDirectory()+@"\Interpretor.sdf";
            //string connect = @"Data Source = C:\Work\Psyhologic calculating system\PCS Forms\" + ReturnPathToDataBase(path);
            this.connection.ConnectionString = connect;
            this.command.Connection = connection;
            this.connection.Open();
        }

        public void ReadData(string command_text)
        {
            this.command.CommandText = command_text;
            this.Reader = this.command.ExecuteReader();
        }

        public int ExecuteNonQuery(string command_text)
        {
            this.command.CommandText = command_text;
            return command.ExecuteNonQuery();
        }

        public object ExecuteScalar(string command_text)
        {
            this.command.CommandText = command_text;
            return command.ExecuteScalar();
        }

        private void ConnectionOpen()
        {
            if (this.connection.State != System.Data.ConnectionState.Open)
                this.connection.Open();
        }

        public void ConnectionClose()
        {
            if (this.connection.State != System.Data.ConnectionState.Closed)
                this.connection.Close();
        }
    }
}
