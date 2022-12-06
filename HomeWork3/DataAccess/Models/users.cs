using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork3.DataAccess.Models
{
   [ModelTable("public.users")]
   public class Users
   {
      [ModelColumn("userid")]
      [ModelKey()]
      public Guid userId { get; set; }
      [ModelColumn("username")]
      public string userName { get; set; }
      [ModelColumn("firstname")]
      public string firstName { get; set; }
      [ModelColumn("lastname")]
      public string lastName { get; set; } = null!;
      [ModelColumn("phone")]
      public string phone { get; set; }
      [ModelColumn("email")]
      public string email { get; set; }
   }
}
