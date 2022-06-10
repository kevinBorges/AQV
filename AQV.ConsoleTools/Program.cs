using AntesQueVenca.Data.Context;
using AntesQueVenca.Data.Repositories;
using AntesQueVenca.Domain.Entities;
using GemBox.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace AQV.ConsoleTools
{
    class Program
    {
        static void Main(string[] args)
        {
            //ImportarBaseDeCategorias();
            //ImportarBaseDeProdutos();
            ImportarBaseDeProdutosParaOVarejista();
            //ImportarBaseDeCidades();
            //ImportarBaseDeCeps();
        }

        private static void ImportarBaseDeCeps()
        {
            PostalCodeDetailRepository repository = new PostalCodeDetailRepository();

            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            var workbook = ExcelFile.Load(@"C:\Users\wemer\Documents\ceps.xlsx");
            var sb = new StringBuilder();
            int count = 1;
            // Iterate through all worksheets in an Excel workbook.
            foreach (var worksheet in workbook.Worksheets)
            {
                foreach (var row in worksheet.Rows)
                {
                    decimal price = 0;
                    if (!row.AllocatedCells[1].Value.ToString().Equals("GRÁTIS", StringComparison.InvariantCultureIgnoreCase))
                        price = decimal.Parse(row.AllocatedCells[1].Value.ToString());

                    var cep = row.AllocatedCells[0].Value.ToString().Replace("-", "");
                    var cepDetail = new PostalCodeDetail
                    {
                        PostalCode = cep,
                        Price = price
                    };
                    repository.Add(cepDetail);


                    Console.WriteLine(count + " - " + cep);
                    count++;
                }
                repository.Commit();
            }
        }

        private static void ImportarBaseDeCategorias()
        {
            CategoryRepository categoryRepository = new CategoryRepository();

            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            var workbook = ExcelFile.Load(@"C:\Users\wemer\Documents\aqvProducts2.xlsx");
            var sb = new StringBuilder();

            // Iterate through all worksheets in an Excel workbook.
            foreach (var worksheet in workbook.Worksheets)
            {
                foreach (var row in worksheet.Rows)
                {
                    var categories = categoryRepository.GetAll();
                    var category = categories.Where(p => !p.Deleted && p.Name.Equals(row.AllocatedCells[0].Value.ToString(), StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                    if (category == null)
                    {
                        var newCategory = new Category
                        {
                            Name = row.AllocatedCells[0].Value.ToString()
                        };
                        newCategory.SetFieldsInsert(1);
                        categoryRepository.Add(newCategory);
                        categoryRepository.Commit();

                        Console.WriteLine(newCategory.Name);
                    }
                }
            }
        }

        private static void ImportarBaseDeProdutos()
        {
            ProductRepository productRepository = new ProductRepository();

            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            var workbook = ExcelFile.Load(@"C:\Users\wemer\Documents\aqvProducts.xlsx");
            var sb = new StringBuilder();

            CategoryRepository categoryRepository = new CategoryRepository();
            var categories = categoryRepository.GetAll();
            // Iterate through all worksheets in an Excel workbook.
            foreach (var worksheet in workbook.Worksheets)
            {
                foreach (var row in worksheet.Rows)
                {
                    var productName = row.AllocatedCells[1].Value.ToString();
                    var productImage = row.AllocatedCells[2].Value.ToString();
                    var description = row.AllocatedCells[7].Value?.ToString();
                    var categoryId = categories.Where(c => c.Name.Equals(row.AllocatedCells[0].Value.ToString(), StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault().CategoryId;
                    var products = productRepository.GetAll();
                    var product = products.Where(p => !p.Deleted && p.Name.Equals(productName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                    if (product == null)
                    {
                        var newProduct = new Product
                        {
                            Name = productName,
                            CategoryId = categoryId,
                            Description = description,
                            Image = productImage
                        };
                        newProduct.SetFieldsInsert(2);
                        productRepository.Add(newProduct);

                        Console.WriteLine(productName);
                    }
                    else
                    {
                        product.Description = description;
                        product.Image = productImage;
                        product.CategoryId = categoryId;
                        product.Name = productName;
                        product.SetFieldsUpdate(2);
                        productRepository.Update(product);
                        Console.WriteLine("Update -----" + productName);
                    }
                    productRepository.Commit();
                }
            }
        }



        private static void ImportarBaseDeProdutosParaOVarejista()
        {
            ProductItemRepository productItemRepository = new ProductItemRepository();
            RetailerProductItemRepository retailerProductItemRepository = new RetailerProductItemRepository();
            ProductRepository productRepository = new ProductRepository();

            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            var workbook = ExcelFile.Load(@"C:\Users\wemer\Documents\aqvProducts2.xlsx");
            var sb = new StringBuilder();

            // Iterate through all worksheets in an Excel workbook.
            foreach (var worksheet in workbook.Worksheets)
            {
                foreach (var row in worksheet.Rows)
                {
                    //if (row.Index >= 0)
                    //{
                    var products = productRepository.GetAll();
                    var productName = row.AllocatedCells[1].Value.ToString();
                    var product = products.Where(p => p.Name.Equals(productName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                    if (product != null)
                    {
                        var id = product.ProductId;
                        var quantidade = int.Parse(row.AllocatedCells[3].Value.ToString());
                        var dateValue = row.AllocatedCells[4].Value.ToString().Substring(0, 10);
                        var dataValidade = DateTime.ParseExact(dateValue, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        var priceFrom = decimal.Parse(row.AllocatedCells[5].Value.ToString());
                        var price = decimal.Parse(row.AllocatedCells[6].Value.ToString());

                        var retailerProductItem = retailerProductItemRepository.GetAll().Where(prop => prop.ProductItem.ProductId == product.ProductId && prop.ProductItem.ExpirationDate == dataValidade && !prop.ProductItem.Deleted).FirstOrDefault();
                        if (retailerProductItem == null)
                        {
                            var item = new ProductItem
                            {
                                ExpirationDate = dataValidade,
                                Price = price,//.Replace(",", ".")),
                                PriceFrom = priceFrom,//.Replace(",", ".")),
                                Sold = false,
                                ProductId = id
                            };
                            item.SetFieldsInsert(2);

                            retailerProductItem = new RetailerProductItem
                            {
                                RetailerId = 1,
                                Quantity = quantidade,
                                ProductItem = item
                            };

                            retailerProductItemRepository.Add(retailerProductItem);
                            Console.WriteLine(product.Name);
                        }
                        else
                        {
                            retailerProductItem.ProductItem.ExpirationDate = dataValidade;
                            retailerProductItem.ProductItem.Price = price;
                            retailerProductItem.ProductItem.PriceFrom = priceFrom;
                            retailerProductItem.Quantity = quantidade;
                            retailerProductItemRepository.Update(retailerProductItem);
                            Console.WriteLine("Update ---------" + product.Name);
                        }
                    }
                    //}
                    retailerProductItemRepository.Commit();
                }
            }
        }

        private static void ImportarBaseDeCidades()
        {
            List<Root> cidadesFromIbge = new List<Root>();
            var stateRepository = new StateRepository();
            var cityRepository = new CityRepository();

            WebRequest request = WebRequest.Create(
              "https://servicodados.ibge.gov.br/api/v1/localidades/municipios");
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                cidadesFromIbge = JsonConvert.DeserializeObject<List<Root>>(responseFromServer);
            }

            response.Close();
            Console.WriteLine("---------- total: " + cidadesFromIbge.Count);
            foreach (var item in cidadesFromIbge)
            {
                Console.WriteLine(item.id);
                //var ufId = stateRepository.GetById(item.microrregiao.mesorregiao.UF.id).StateId;
                var city = new City { Active = true, IbgeId = item.id, StateId = item.microrregiao.mesorregiao.UF.id, Name = item.nome };
                cityRepository.Add(city);
            }

            cityRepository.Commit();

        }
    }

    public class Regiao
    {
        public int id { get; set; }
        public string sigla { get; set; }
        public string nome { get; set; }
    }

    public class UF
    {
        public int id { get; set; }
        public string sigla { get; set; }
        public string nome { get; set; }
        public Regiao regiao { get; set; }
    }

    public class Mesorregiao
    {
        public int id { get; set; }
        public string nome { get; set; }
        public UF UF { get; set; }
    }

    public class Microrregiao
    {
        public int id { get; set; }
        public string nome { get; set; }
        public Mesorregiao mesorregiao { get; set; }
    }

    public class RegiaoIntermediaria
    {
        public int id { get; set; }
        public string nome { get; set; }
        public UF UF { get; set; }
    }

    public class RegiaoImediata
    {
        public int id { get; set; }
        public string nome { get; set; }

        [JsonProperty("regiao-intermediaria")]
        public RegiaoIntermediaria RegiaoIntermediaria { get; set; }
    }

    public class Root
    {
        public int id { get; set; }
        public string nome { get; set; }
        public Microrregiao microrregiao { get; set; }

        [JsonProperty("regiao-imediata")]
        public RegiaoImediata RegiaoImediata { get; set; }
    }
}
