using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork3.DataAccess.Models
{
   public class ModelTable : Attribute
   {
      public ModelTable(string tbl) { Table = tbl; }
      public string Table { get; set; }      
   }
   public class ModelColumn : Attribute
   {
      public ModelColumn(string col) => Column = col;
      public string Column { get; set; }
   }
   public class ModelKey : Attribute
   {
      public ModelKey() => Key = true;
      public bool Key { get; set; }
   }
}
