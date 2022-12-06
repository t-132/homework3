using System;
using System.Collections.Generic;
using System.Text;
using HomeWork3.Models;
using System.Collections;
using Npgsql;
using System.Reflection;
using HomeWork3.DataAccess.Models;
using System.Data;

namespace HomeWork3.DataAccess
{
   public interface IAppRepo
   {
       IEnumerator<AvitoProductImages> GetAvitoProductImages();
       IEnumerator<AvitoProducts> GetAvitoProduct();
       IEnumerator<AvitoUser> GetAvitoUser();
       bool AddUser(ref AvitoUser obj);
       bool AddProducts(ref AvitoProducts obj);
       bool AddProductsImage(ref AvitoProductImages obj);
   }
}
