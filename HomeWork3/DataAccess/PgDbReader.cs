using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using HomeWork3.DataAccess.Models;

namespace HomeWork3.DataAccess
{
   class PgDbReader<T> where T : new()
   {
      private NpgsqlDataReader _reader;
      private NpgsqlCommand _command = null;
      private NpgsqlConnection _conn;
      private string _sql;
      private NpgsqlDataSource _src;
      private Type _tp;
      private string Table { get; set; }
      private Dictionary<string, List<System.Reflection.PropertyInfo>> prop;

      public PgDbReader(NpgsqlConnection conn)
      {
         _conn = conn;
         Init();
      }

      public PgDbReader(DataSourceBuilder src)
      {
         _src = DataSourceBuilder.getBuilder();
         Init();
      }

      private void Init()
      {
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
         
         _sql = "select ";
         prop = new Dictionary<string, List<System.Reflection.PropertyInfo>>(_tp.GetProperties().Length);
         bool comma = false;
         foreach (var field in _tp.GetProperties())
         {
            var pAtt = (ModelColumn[])field.GetCustomAttributes(typeof(ModelColumn), true);
            string name;

            if (pAtt.Length > 0)
               name = pAtt[0].Column;
            else
               name = field.Name;

            if (comma) _sql += "," + name;
            else _sql += name;
            List<System.Reflection.PropertyInfo> l;
            if (prop.TryGetValue(name, out l))
            {
               l.Add(field);
            }
            else {
               l = new List<System.Reflection.PropertyInfo>(1);
               l.Add(field);
               prop.Add(name, l);
            }

            comma = true;
         }
         _sql += " from " + Table;
      }
      private void Connect()
      {
         if (_conn is null || _conn.State == 0) _conn = DataSourceBuilder.getBuilder().OpenConnection();
      }

      void Close()
      {
         if (!_reader.IsClosed) _reader.Close();
         if (!(_command is null)) { _command.Dispose(); _command = null; }
         if (_conn.State != 0) _conn.Close();         
      }

      public PgDbReader<T> Refresh()
      {
         Connect();
         if (!(_reader is null) && !_reader.IsClosed) _reader.Close();

         if (_command is null) _command = new NpgsqlCommand(_sql, _conn);

         _reader = _command.ExecuteReader();

         return this;
      }

      public bool Read(out T t)
      {

         if (_reader is null || _reader.IsClosed)
         {
            t = default(T);
            return false;
         }

         if (!_reader.Read())
         {
            t = default(T);
            return false;
         }

         t = new T();

         for (var i = 0; i < _reader.FieldCount; i++)
         {
            var pName = _reader.GetName(i);
            List<System.Reflection.PropertyInfo> l;
            if (prop.TryGetValue(pName, out l))
            {
               foreach (var k in l)
               {                  
                  k?.SetValue(t, _reader[i]);
               }
               
            }            
         }

         return true;
      }

      ~PgDbReader()
      {
         Close();
      }
   }
}
