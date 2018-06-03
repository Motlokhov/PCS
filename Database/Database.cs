using System.Data.SqlServerCe;
using System.Collections.Generic;
using System;
using System.IO;
using System.Reflection;

namespace Database
{

    public class Database:IDisposable
    {
        private SqlCeConnection connection =new SqlCeConnection();
        private SqlCeCommand command =new SqlCeCommand();
        private SqlCeDataReader reader;

        //необходима реализация метода Dispose для Database
        private Database()
        {
            string connect = "Data Source =" + Directory.GetCurrentDirectory() + @"\PSC.sdf";
            //string connect = @"Data Source = C:\Test\PCS\Debug\PSC.sdf";
            this.connection.ConnectionString = connect;
            this.command.Connection = connection;
            this.connection.Open();
        }

        public static Database Construct() 
        {
            return new Database();
        }

        public SqlCeDataReader ReadData(string command_text)
        {
            command.CommandText = command_text;
            reader = command.ExecuteReader();
            return reader;
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

        public void Dispose()
        {
            GC.Collect();
        }
    }
}
