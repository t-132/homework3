using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork3.Models
{
   public class AvitoProductImages
   {
      public Guid productImageId { get; set; }
      public long num { get; set; }
      public Guid productId { get; set; }
      public string decription { get; set; }
      public string imageRef { get; set; }
}
}
