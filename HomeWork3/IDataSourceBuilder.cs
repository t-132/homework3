using Npgsql;

namespace HomeWork3
{
   interface IDataSourceBuilder
   {
      static NpgsqlDataSource GetBuilder() { return null; }
   }
}
