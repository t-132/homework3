using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork3.Models
{
   public class AvitoUser
   {
      public Guid userId { get; set; }
      public string userName { get; set; }
      public string firstName { get; set; } 
      public string lastName { get; set; } = null!;
      public string phone { get; set; }
      public string email { get; set; }
   }   
}
