﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using iMenyn.Data.Models;

namespace iMenyn.Data.Helpers
{
    public static class FakeDataHelper
    {
        private static readonly Random _random = new Random((int)DateTime.Now.Ticks);

        public static Enterprise CreateFakeEnterprise(Abstract.Db.IDb Db,bool modified)
        {
            var enterprise = new Enterprise();
            var menu = new Menu();
            var categories = new List<Category>();

            var products = new List<Product>();

            for (var i = 0; i < _random.Next(6, 20); i++)
            {
                var category = new Category
                                      {
                                          Id = GeneralHelper.GetGuid(),
                                          Name = RandomString(),
                                          Products = new List<string>()
                                      };
                for (var j = 0; j < _random.Next(6, 40); j++)
                {
                    var product = new Product
                                      {
                                          Id = ProductHelper.GenerateId(),
                                          Name = RandomString(),
                                          Prices = new List<ProductPrice>()
                                      };
                    var productPrices = new List<ProductPrice>();
                    for (var k = 0; k < _random.Next(1,4); k++)
                    {
                        var productPrice = new ProductPrice {Price = RandomNumber()};
                        if(RandomBool())
                            productPrice.Description = RandomString();

                        productPrices.Add(productPrice);
                    }
                    product.Prices = productPrices;
                    products.Add(product);
                    category.Products.Add(product.Id);
                }
                categories.Add(category);
            }


            menu.Categories = categories;

            enterprise.Menu = menu;
            enterprise.IsNew = true;

            //TODO
            enterprise.Categories = RandomListString();

            Db.Enterprises.CreateEnterprise(enterprise);
            //enterprise.ModifiedMenu = modified
            Db.Products.AddProductsToDb(products);


            return enterprise;
        }

        private static string RandomString()
        {
            var builder = new StringBuilder();
            for (var i = 0; i < _random.Next(4,15); i++)
            {
                var ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * _random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }
        private static int RandomNumber()
        {
            return _random.Next(10,200);
        }
        private static bool RandomBool()
        {

            return false;
        }
        private static List<string> RandomListString(int maxLength = 5)
        {
            var list = new List<string>();
            for (var i = 0; i < _random.Next(1, maxLength); i++)
            {
                list.Add(RandomString());
            }
            return list;
        } 
    }
}