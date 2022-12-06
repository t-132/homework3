using System;
using Npgsql;
using HomeWork3.DataAccess.Models;
using System.Data;

namespace HomeWork3.DataAccess
{   
   class PgDbWriter<T> where T : new()
   {      
      private NpgsqlCommand _command = null;
      private NpgsqlConnection _conn;
      private string _sql;
      private NpgsqlDataSource _src;
      private Type _tp;
      private string Table { get; set; }

      public PgDbWriter(NpgsqlConnection conn)
      {
         _conn = conn;
         Init();
      }

      public PgDbWriter(DataSourceBuilder src)
      {
         _src = DataSourceBuilder.getBuilder();
         Init();
      }

      private void Init()
      {
         _src = DataSourceBuilder.getBuilder();
         _tp = typeof(T);
         var att = (ModelTable[])_tp.GetCustomAttributes(typeof(ModelTable), true);
         if (att.Length > 0)
         {
            Table = att[0].Table;
         }
         else
         {
            Table = _tp.Name;
         }

         _sql = $"insert into {Table}";
      }

      private void Connect()
      {
         if (_conn is null || _conn.State == 0) _conn = DataSourceBuilder.getBuilder().OpenConnection();
      }

      void Close()
      {
         if (!(_command is null)) { _command.Dispose(); _command = null; }
         if (_conn.State != 0) _conn.Close();         
      }

      public bool Write(T t)
      {
         if (t is null) return false;

         if (_command is null)
         {
            if (_conn is null || _conn.State == 0) _conn = DataSourceBuilder.getBuilder().OpenConnection();
            _command = new NpgsqlCommand(_sql, _conn);
            _command.CommandType = CommandType.Text;
         }
         _command.Parameters.Clear();

         bool comma = false;
         string sql = "";
         string values = "";

         foreach (var field in _tp.GetProperties())
         {
            var pAtt = (ModelColumn[])field.GetCustomAttributes(typeof(ModelColumn), true);
            var pAttKey = (ModelKey[])field.GetCustomAttributes(typeof(ModelKey), true);
            string name;
            var fieldValue = field.GetValue(t);

            if (pAttKey.Length > 0)
            {

               if (field.GetValue(t) is null) continue;
               if (field.PropertyType.Name == "Guid" && (Guid)fieldValue == default(Guid)) continue;
            }

            if (pAtt.Length > 0)
               name = pAtt[0].Column;
            else
               name = field.Name;

            if (comma)
            {
               sql += "," + name;
               values += ",@" + name;
            }
            else
            {
               sql += name;
               values += "@" + name;
            }
            _command.Parameters.AddWithValue($"@{name}", fieldValue);
            comma = true;
         }
         _command.CommandText = _sql + $"({sql}) values({values})";

         if (_command.ExecuteNonQuery() != 0) return true; ;


         return false;
      }

      ~PgDbWriter()
      {
         Close();
      }
   }
}
