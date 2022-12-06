using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
namespace HomeWork3
{
   class DataSourceBuilder : IDataSourceBuilder
   {
      public static NpgsqlDataSource src;
      public static NpgsqlDataSource getBuilder() { return src;}
   }
}
