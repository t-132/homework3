using System;
using System.Collections.Generic;
using System.Text;
using HomeWork3.DataAccess.Models;
using HomeWork3.Models;

namespace HomeWork3.DataAccess
{
   public class AppRepo : IAppRepo
   {      
      public AvitoProductImages Image;
      public AvitoProducts Product;
      public AvitoUser User;

      public IEnumerator<AvitoProductImages> GetAvitoProductImages() 
      {
         DataSourceBuilder src = new DataSourceBuilder();
         ProductImages pImg;
         AvitoProductImages pImage;
         var reader = new PgDbReader<ProductImages>(src);
         reader.Refresh();
               
         for (; reader.Read(out pImg);)
         {
            
            pImage = new AvitoProductImages();
            pImage.decription = pImg.decription;
            pImage.imageRef = pImg.imageRef;
            pImage.num = pImg.num;
            pImage.productId = pImg.productId;
            pImage.productImageId = pImg.productImageId;
            Image = pImage;
            yield return pImage;
         }         
         yield break;                     
      }
      public IEnumerator<AvitoProducts> GetAvitoProduct()
      {
         DataSourceBuilder src = new DataSourceBuilder();
         Products pPrd;
         AvitoProducts pProduct;
         var reader = new PgDbReader<Products>(src);
         reader.Refresh();

         for (; reader.Read(out pPrd);)
         {
            pProduct = new AvitoProducts();
            pProduct.productCaption = pPrd.productcaption;
            pProduct.productDecription = pPrd.productDecription;
            pProduct.productId = pPrd.productId;
            pProduct.productName = pPrd.productName;
            pProduct.userId = pPrd.userId;
            
            Product = pProduct;
            yield return pProduct;
         }
         yield break;
      }
      public IEnumerator<AvitoUser> GetAvitoUser()
      {
         DataSourceBuilder src = new DataSourceBuilder();
         Users pUsr;
         AvitoUser pUser;
         var reader = new PgDbReader<Users>(src);
         reader.Refresh();

         for (; reader.Read(out pUsr);)
         {
            pUser = new AvitoUser();
            pUser.userId = pUsr.userId;
            pUser.userName = pUsr.userName;
            pUser.firstName = pUsr.firstName;
            pUser.lastName = pUsr.lastName;            
            pUser.phone = pUsr.phone;
            pUser.email = pUsr.email;
            
            User = pUser;
            yield return pUser;
         }
         yield break;
      }

      public bool AddUser(ref AvitoUser obj)
      {
         AvitoUser pUser;

         if (obj is null)
         {
            pUser = User;
         }else pUser = obj;

         var pUsr = new Users();     
         
         pUsr.userId = pUser.userId;
         pUsr.userName = pUser.userName;
         pUsr.firstName = pUser.firstName;
         pUsr.lastName = pUser.lastName;
         pUsr.phone = pUser.phone;
         pUsr.email = pUser.email;
         

         DataSourceBuilder src = new DataSourceBuilder();
         var wrt = new PgDbWriter<Users>(src);
         return wrt.Write(pUsr);         
      }

      public bool AddProducts(ref AvitoProducts obj) 
      {
         AvitoProducts pProduct;

         if (obj is null)
         {
            pProduct = Product;
         }else pProduct = obj;

         var pPrd = new Products();

         pPrd.productcaption = pProduct.productCaption;
         pPrd.productDecription = pProduct.productDecription;
         pPrd.productId = pProduct.productId;
         pPrd.productName = pProduct.productName;
         pPrd.userId = pProduct.userId == default(Guid) ? User.userId: pProduct.userId;
            
         DataSourceBuilder src = new DataSourceBuilder();
         var wrt = new PgDbWriter<Products>(src);
         return wrt.Write(pPrd);         
      }
      public bool AddProductsImage(ref AvitoProductImages obj)
      {
         AvitoProductImages pImage; 

         if (obj is null)
         {
            pImage = Image;
         }
         else pImage = obj;

         var pImg = new ProductImages();
         pImg.imageRef = pImage.imageRef;
         pImg.num = pImage.num;
         pImg.productId = pImage.productId == default(Guid)? Product.productId: pImage.productId;
         pImg.decription = pImage.decription;

         DataSourceBuilder src = new DataSourceBuilder();
         var wrt = new PgDbWriter<ProductImages> (src);
         return wrt.Write(pImg);
      }
   }
}
