using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork3.DataAccess.Models
{
   [ModelTable("public.productimages")]
   public class ProductImages
   {
      [ModelColumn("productimageid")]
      [ModelKey()]
      public Guid productImageId { get; set; }
      [ModelColumn("num")]
      public long num { get; set; }
      [ModelColumn("productId")]
      public Guid productId { get; set; }
      [ModelColumn("decription")]
      public string decription { get; set; }
      [ModelColumn("imageref")]
      public string imageRef { get; set; }
   }
}
