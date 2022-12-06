using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using HomeWork3.Models;
using HomeWork3.DataAccess;
using System;
using System.Linq;
using System.Threading;

namespace HomeWork3
{
   public class AppService : IAppService
   {
      private readonly ILogger<AppService> _log;
      private readonly IConfiguration _config;
      private IAppRepo _repo;

      public AppService(ILogger<AppService> log, IConfiguration config, IAppRepo repo)
      {
         _log = log;         
         _config = config;
         _repo = repo;
      }

      public void Run()
      {
         AnsiConsole.WriteLine("All table Data");
         PrintData();
         AnsiConsole.WriteLine();
         Thread.Sleep(1000);
         string[] tables = { "users", "product", "image" };

         for (; AnsiConsole.Confirm("Добавить стороку в таблицу");)
         {
            var tbl = AnsiConsole.Prompt(new TextPrompt<string>("введите таблицу:")
            .PromptStyle("green")
            .ValidationErrorMessage("[red]That's not a valid [/]")
            .Validate(t => { if (tables.Any(t.Contains)) return ValidationResult.Success(); return ValidationResult.Error(); }));

            if(Add(tbl)) AnsiConsole.WriteLine("1 строка добавлен");
            else AnsiConsole.WriteLine("Не получилось");
         }
         
         AnsiConsole.MarkupLine("Ok... :(");
         return;
      }

      private Table CreateTable(Type tp, string title) 
      {
         var tbl = new Table();
         tbl.Border(TableBorder.None).Title(title).Expand();

         foreach (var props in tp.GetProperties())
         {
            tbl.AddColumn(new TableColumn(props.Name).Centered());            
         }
         return tbl;
      }

      private void PrintData()
      {
         PrintDataUsres();         
         PrintDataProducts();
         PrintDataImages();
         
      }
      private void PrintDataUsres()
      {
         var tbl = CreateTable(typeof(AvitoUser), "user");
         var e = _repo.GetAvitoUser();
         for(; e.MoveNext();)
         {
            AvitoUser user = e.Current;
            string[] row =  new string[] { $"{user.userId}", $"{user.userName}", $"{user.firstName}" , $"{user.lastName}", $"{user.phone}", $"{user.email}"};
            tbl.AddRow(row);
         }
         AnsiConsole.Write(tbl);
      }
      private void PrintDataProducts()
      {
         var tbl = CreateTable(typeof(AvitoProducts),"product");
         var e = _repo.GetAvitoProduct();
         for (; e.MoveNext();)
         {
            AvitoProducts obj = e.Current;
            string[] row = new string[] { $"{obj.productId}", $"{obj.productName}", $"{obj.productCaption}", $"{obj.productDecription}", $"{obj.userId}" };
            tbl.AddRow(row);
         }
         AnsiConsole.Write(tbl);
      }
      private void PrintDataImages()
      {
         var tbl = CreateTable(typeof(AvitoProductImages), "image");
         var e = _repo.GetAvitoProductImages();
         for (; e.MoveNext();)
         {
            AvitoProductImages obj = e.Current;
            string[] row = new string[] { $"{obj.productImageId}", $"{obj.num}", $"{obj.decription}", $"{obj.productId}"};
            tbl.AddRow(row);
         }
         AnsiConsole.Write(tbl);
      }
      
      private bool Add(string tbl) 
      {         
         if (tbl.Equals("users")) return AddIntoTbl(typeof(AvitoUser));
         else if(tbl.Equals("product")) return AddIntoTbl(typeof(AvitoProducts)); 
         else return AddIntoTbl(typeof(AvitoProductImages));         
      }

      private bool AddIntoTbl(Type tp) 
      {
         object defaultValue = Activator.CreateInstance(tp);

         foreach (var props in tp.GetProperties())
         {            
            //object defaultValue = props.PropertyType.IsValueType ? Activator.CreateInstance(props.PropertyType) : null;
            if (props.PropertyType.Name == "Guid") 
            {
               var val1 = AnsiConsole.Ask<Guid>($"{props.Name}", default(Guid));
               props.SetValue(defaultValue, val1);  //Convert.ChangeType(val1, props.PropertyType));
               continue;            
            }

            if (props.PropertyType.Name == "Int64")
            {
               var val2 = AnsiConsole.Ask<Int64>($"{props.Name}", 0);
               props.SetValue(defaultValue, val2);
               continue;
            }
              

            var val = AnsiConsole.Ask<string>($"{props.Name}", default(string));
            props.SetValue(defaultValue, val);
         }

         if (tp.Name == "AvitoUser") 
         {
            AvitoUser obj = (AvitoUser)defaultValue;
            return _repo.AddUser(ref obj); 
         }

         if (tp.Name == "AvitoProducts")
         {
            AvitoProducts obj = (AvitoProducts)defaultValue;
            return _repo.AddProducts(ref obj);
         }

         if (tp.Name == "AvitoProductImages")
         {
            AvitoProductImages obj = (AvitoProductImages)defaultValue;
            return _repo.AddProductsImage(ref obj);
         }
         return false;
      }
   }
}

