using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork3.DataAccess.Models
{
   
   [ModelTable("public.products")]
   public class Products
   {
      [ModelColumn("productid")]
      [ModelKey()]
      public Guid productId { get; set; }
      [ModelColumn("productname")]
      public string productName { get; set; }
      [ModelColumn("productcaption")]
      public string productcaption { get; set; }
      [ModelColumn("productdecription")]
      public string productDecription { get; set; }
      [ModelColumn("userid")]
      public Guid userId { get; set; }
   }
}
