using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;


namespace HomeWork3.DataAccess
{
   interface IAppDbConnection
   {
      NpgsqlConnection getConnection();
   }

   class AppDbConnection : IAppDbConnection
   {      
      private NpgsqlConnection _conn;
      AppDbConnection(NpgsqlDataSource source)
      {         
         _conn = source.OpenConnection();
      }
      public NpgsqlConnection getConnection() { return _conn; }
   }
}
