using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork3.Models
{
   public class AvitoProducts
   {
      public Guid productId { get; set; }
      public string productName { get; set; }
      public string productCaption { get; set; }
      public string productDecription { get; set; }
      public Guid userId { get; set; }
   }
}
