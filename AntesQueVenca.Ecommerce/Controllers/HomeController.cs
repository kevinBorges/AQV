using AntesQueVenca.Data.Repositories;
using AntesQueVenca.Domain.Entities;
using AntesQueVenca.Domain.Entities.Consumers;
using AntesQueVenca.Domain.Entities.Helpers;
using AntesQueVenca.Domain.Enuns;
using AntesQueVenca.Ecommerce.Models;
using AntesQueVenca.Ecommerce.ViewModels;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MimeKit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using AntesQueVenca.Domain;
using AntesQueVenca.Ecommerce.Resources;
using AntesQueVenca.Domain.Validations;
using Microsoft.EntityFrameworkCore;

namespace AntesQueVenca.Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ProductRepository _productRepository;
        private OrderRepository _orderRepository;
        private ProductItemRepository _productItemRepository;
        private RetailerProductItemRepository _retailerProductItemRepository;
        private CategoryRepository _categoryRepository;
        private PostalCodeDetailRepository _postalCodeDetailRepository;
        private RetailerRepository _retailerRepository;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _productRepository = new ProductRepository();
            _orderRepository = new OrderRepository();
            _productItemRepository = new ProductItemRepository();
            _retailerProductItemRepository = new RetailerProductItemRepository();
            _categoryRepository = new CategoryRepository();
            _postalCodeDetailRepository = new PostalCodeDetailRepository();
            _retailerRepository = new RetailerRepository();
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(ProductList));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ProductList()
        {
            //TODO: Tratar outros varejistas;
            var retailerProductItems = _retailerProductItemRepository.GetAll().Where(prop => prop.Quantity > 2).OrderBy(p => p.ProductItem.ExpirationDate);
            var listViewModel = new List<ProductViewModel>();

            foreach (var retailerProductItem in retailerProductItems)
            {
                listViewModel.Add(new ProductViewModel
                {
                    ProductId = retailerProductItem.ProductItemId,
                    Name = retailerProductItem.ProductItem.Product.Name,
                    Image = retailerProductItem.ProductItem.Product.Image,
                    ExpirationDate = retailerProductItem.ProductItem.ExpirationDate.ToShortDateString(),
                    Price = retailerProductItem.ProductItem.Price,
                    PriceFrom = retailerProductItem.ProductItem.PriceFrom,
                    Description = retailerProductItem.ProductItem.Product.Description,
                    Quantity = retailerProductItem.Quantity,
                    CategoryName = retailerProductItem.ProductItem.Product?.Category?.Name
                });
            }

            ViewBag.Categories = _categoryRepository.GetAll();

            return View(listViewModel);
        }

        [HttpGet]
        public IActionResult GetPartial(string productName, int category, DateFilterEnum? dateFilter, MaiorMenorFilterEnum? precoFilter, MaiorMenorFilterEnum? dateValFilter)
        {
            var retailerProductItems = _retailerProductItemRepository.GetAll().OrderBy(p => p.ProductItem.ExpirationDate).Where(prop => prop.Quantity > 2);
            if (category > 0)
                retailerProductItems = retailerProductItems.Where(prop => prop.ProductItem.Product.CategoryId == category);

            if (!string.IsNullOrEmpty(productName))
                retailerProductItems = retailerProductItems.Where(prop => prop.ProductItem.Product.Name.Contains(productName, StringComparison.InvariantCultureIgnoreCase));

            if (dateFilter != null && dateFilter != DateFilterEnum.Todos)
            {
                if (dateFilter == DateFilterEnum.Hoje)
                    retailerProductItems = retailerProductItems.Where(prop => prop.ProductItem.ExpirationDate.Date <= DateTime.Now.Date);
                if (dateFilter == DateFilterEnum.EssaSemana)
                    retailerProductItems = retailerProductItems.Where(prop => prop.ProductItem.ExpirationDate.Date <= DateTime.Now.AddDays(7).Date);
                if (dateFilter == DateFilterEnum.EsseMes)
                    retailerProductItems = retailerProductItems.Where(prop => prop.ProductItem.ExpirationDate.Date <= DateTime.Now.AddMonths(1).Date);
                if (dateFilter == DateFilterEnum.TresMeses)
                    retailerProductItems = retailerProductItems.Where(prop => prop.ProductItem.ExpirationDate.Date <= DateTime.Now.AddMonths(3).Date);
                if (dateFilter == DateFilterEnum.SeisMeses)
                    retailerProductItems = retailerProductItems.Where(prop => prop.ProductItem.ExpirationDate.Date <= DateTime.Now.AddMonths(6).Date);
            }

            if (precoFilter != null && precoFilter == MaiorMenorFilterEnum.Menor)
                retailerProductItems = retailerProductItems.OrderBy(x => x.ProductItem.Price);
            if (precoFilter != null && precoFilter == MaiorMenorFilterEnum.Maior)
                retailerProductItems = retailerProductItems.OrderByDescending(x => x.ProductItem.Price);

            if (dateValFilter != null && dateValFilter == MaiorMenorFilterEnum.Menor)
                retailerProductItems = retailerProductItems.OrderBy(x => x.ProductItem.ExpirationDate);
            if (dateValFilter != null && dateValFilter == MaiorMenorFilterEnum.Maior)
                retailerProductItems = retailerProductItems.OrderByDescending(x => x.ProductItem.ExpirationDate);



            var listViewModel = new List<ProductViewModel>();
            foreach (var retailerProductItem in retailerProductItems)
            {
                listViewModel.Add(new ProductViewModel
                {
                    ProductId = retailerProductItem.ProductItemId,
                    Name = retailerProductItem.ProductItem.Product.Name,
                    Image = retailerProductItem.ProductItem.Product.Image,
                    ExpirationDate = retailerProductItem.ProductItem.ExpirationDate.ToShortDateString(),
                    Price = retailerProductItem.ProductItem.Price,
                    PriceFrom = retailerProductItem.ProductItem.PriceFrom,
                    Description = retailerProductItem.ProductItem.Product.Description,
                    Quantity = retailerProductItem.Quantity
                });
            }

            return PartialView("_Products", listViewModel);
        }

        [HttpPost]
        public IActionResult Add(OrderViewModel orderViewModel)
        {
            var result = new Result<object>();
            try
            {
                if (string.IsNullOrEmpty(orderViewModel.Consumer.Email) || !EmailValidation.Valido(orderViewModel.Consumer.Email))
                {
                    result.Success = false;
                    result.Message = "E-mail inválido!!";
                    return Content(JsonConvert.SerializeObject(result), "application/json");
                }

                if (string.IsNullOrEmpty(orderViewModel.Consumer.CPF) || !CpfCnpjlValidation.IsValid(orderViewModel.Consumer.CPF))
                {
                    result.Success = false;
                    result.Message = "Cpf inválido!!";
                    return Content(JsonConvert.SerializeObject(result), "application/json");

                }

                if (orderViewModel.Consumer.Phone.Length <= 13)
                {
                    result.Success = false;
                    result.Message = "Telefone inválido!!";
                    return Content(JsonConvert.SerializeObject(result), "application/json");

                }

                if (orderViewModel.DeliveryType == 0)
                {
                    result.Success = false;
                    result.Message = "Selecione uma forma de retirada!!";
                    return Content(JsonConvert.SerializeObject(result), "application/json");

                }

                if (orderViewModel.DeliveryType == DeliveryTypeEnum.Delivery)
                {
                    if (string.IsNullOrEmpty(orderViewModel.Consumer.Address.Number))
                    {
                        result.Success = false;
                        result.Message = "Digite um numero";
                        return Content(JsonConvert.SerializeObject(result), "application/json");
                    }

                    if (orderViewModel.Consumer.Address.AddressType == 0)
                    {
                        result.Success = false;
                        result.Message = "Escolha o tipo de residência";
                        return Content(JsonConvert.SerializeObject(result), "application/json");
                    }

                    if (orderViewModel.PaymentType == 0)
                    {
                        result.Success = false;
                        result.Message = "Escolha a forma de pagamento!";
                        return Content(JsonConvert.SerializeObject(result), "application/json");
                    }


                }

                    var productItens = orderViewModel.CartItems.ToList();
                if (orderViewModel.Consumer != null && !string.IsNullOrEmpty(orderViewModel.Consumer.Name) && !string.IsNullOrEmpty(orderViewModel.Consumer.Email) && !string.IsNullOrEmpty(orderViewModel.Consumer.CPF) && !string.IsNullOrEmpty(orderViewModel.Consumer.Phone))//
                {
                    var consumerPerson = new PhysicalPerson
                    {
                        CPF = orderViewModel.Consumer.CPF,
                        Email = orderViewModel.Consumer.Email,
                        Name = orderViewModel.Consumer.Name,
                        PersonTypeId = (int)PersonTypeEnum.PhysicalPerson
                    };
                    if (orderViewModel.DeliveryType == DeliveryTypeEnum.Delivery)
                    {
                        if (consumerPerson.Addresses == null)
                            consumerPerson.Addresses = new List<Address>();

                        consumerPerson.Addresses.Add(orderViewModel.Consumer.Address);
                        consumerPerson.SetFieldsInsert(2);
                    }

                    var phone = orderViewModel.Consumer.Phone.Replace("(", "").Replace(" ", "").Replace("-", "").Split(")");
                    var ddd = phone[0];
                    var number = phone[1];

                    consumerPerson.Phones = new List<Phone>();
                    consumerPerson.Phones.Add(new Phone { CountryCode = "55", DDD = ddd, Number = number });
                    consumerPerson.SetFieldsInsert(2);

                    var consumer = new Consumer
                    {
                        Person = consumerPerson
                    };
                    consumer.SetFieldsInsert(2);

                    decimal deliveryPrice = 0;

                    if(orderViewModel.DeliveryType == DeliveryTypeEnum.Delivery)
                        deliveryPrice = _postalCodeDetailRepository.GetAll().Where(p => p.PostalCode.Equals(orderViewModel.Consumer.Address.PostalCode.Replace("-", "").Replace(" ", ""), StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault().Price;

                    var retailer = _retailerRepository.GetAll().FirstOrDefault();
                    var order = new Order
                    {
                        Status = Domain.Enuns.OrderStatusEnum.Pendente,
                        WithdrawDate = DateTime.Now.AddDays(2),
                        Consumer = consumer,
                        PaymentType = orderViewModel.PaymentType,
                        DeliveryType = orderViewModel.DeliveryType,
                        RetailerId = retailer.RetailerId,
                        DeliveryPrice = deliveryPrice
                    };

                    order.TotalPrice = productItens.Sum(prop => Convert.ToDecimal(prop.Total.Replace(".", ",")));

                    if (orderViewModel.DeliveryType == DeliveryTypeEnum.Delivery)
                        order.TotalPrice += calculoFrete(orderViewModel.Consumer.Address.PostalCode);

                    if (!string.IsNullOrEmpty(orderViewModel.Thing))
                    {
                        var troco = orderViewModel.Thing.Replace(".", ",");
                        order.Thing = Convert.ToDecimal(troco);
                    }

                    order.SetFieldsInsert(2);
                    _orderRepository.Add(order);
                    _orderRepository.Commit();
                    foreach (var item in productItens)
                    {
                        if (order.OrderProducts == null)
                            order.OrderProducts = new List<OrderProduct>();
                        order.OrderProducts.Add(new OrderProduct { OrderId = order.OrderId, Quantity = item.Count, ProductItemId = item.Id });
                        var retailerProductItem = _retailerProductItemRepository.GetAll().Where(p => p.ProductItemId == item.Id).FirstOrDefault();
                        retailerProductItem.Quantity -= item.Count;
                        _retailerProductItemRepository.Update(retailerProductItem);
                    }
                    _orderRepository.Update(order);
                    _orderRepository.Commit();

                    _retailerProductItemRepository.Commit();

                    SendEmailToConsumer(orderViewModel, productItens, order);
                    SendEmailToRetailer(order, productItens);

                    var data = new
                    {
                        OrderId = order.OrderId,
                        WithdrawDate = order.WithdrawDate.ToShortDateString()
                    };
                    result.Retorno = data;
                    result.Success = true;
                    result.Message = "Reserva efetuada com sucesso!";
                }
                else
                {
                    result.Success = false;
                    result.Message = "Por favor, confira os campos e tente novamente!";
                }

                return Content(JsonConvert.SerializeObject(result), "application/json");
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                return Content(JsonConvert.SerializeObject(result), "application/json");
            }
        }

        private void SendEmailToRetailer(Order order, List<Cart> cartItems)
        {
            var email = new Email();
            email.To = new List<EmailAddress>();
  //          email.To.Add(new EmailAddress { Name = "Takeo Perfumaria", Address = "takeoperfumaria@hotmail.com" });
            email.To.Add(new EmailAddress { Name = "Takeo Perfumaria", Address = "santoswemerson30@gmail.com" });//
            email.To.Add(new EmailAddress { Name = "Antes Que Vença", Address = "contato@antesquevenca.com.br" });
            email.Body = GetEmailBodyRetailer(order, cartItems);
            email.Subject = "Nova reserva - Antes Que Vença";
            Send(email, null, "Nova reserva - Antes Que Vença");
        }

        private string GetEmailBodyRetailer(Order order, List<Cart> cartItems)
        {
            var deliveryName = order.DeliveryType == DeliveryTypeEnum.Delivery ? "Entrega" : "Retirada";
            var html = "";
            html += AntesQueVencaResources.RetailerConfirmationBeforeInfo;

            html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px'>Olá! Uma nova reserva foi realizada!</p>";
            html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'><a href='https://www.admin.antesquevenca.com.br' target='_blank'>Entre</a> em seu portal para ver todos os detalhes :).</p>";
            html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'><b>Detalhes da reserva:</b></p>";
            //html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'><b>Valor total:</b></p>";
            //html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'>R$ 600,00</p>";
            html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'><b>Código:</b></p>";
            html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'>" + order.OrderId + "</p>";
            html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'><b>Formato:</b></p>";
            html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'>" + deliveryName + "</p>";
            html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'><b>Cliente:</b></p>";
            html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'>" + order.Consumer.Person.Name + "</p>";
            html += "<br />";
            html += "<br />";
            html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'><b>Produtos:</b></p>";

            foreach (var item in cartItems)
            {
                var unitPrice = decimal.Parse(item.UnitPrice.Replace(".", ",")).ToString("C");
                var totalItem = decimal.Parse(item.Total.Replace(".", ",")).ToString("C");
                html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'>Quantidade: " + item.Count + " - " + item.Name + " - Valor unitário: " + unitPrice + " - Total: " + totalItem + "</p>";
                html += "<img src='" + item.Image + "' alt='" + item.Name + "' width='100' height='100' border='0' style='border:0; outline:none; text-decoration:none; display:block;' />";
            }

            html += "</br>";
            html += "<p style='font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; Margin-bottom: 15px;'>Você pode entrar em contato com nosso SAC <a href='https://api.whatsapp.com/send?phone=5511951466837&text=SAC'>clicando aqui</a></p>";
            html += AntesQueVencaResources.RetailerConfirmationAfterInfo;
            return html;
        }

        private void SendEmailToConsumer(OrderViewModel orderViewModel, List<Cart> productItens, Order order)
        {
            var email = new Email();
            email.To = new List<EmailAddress>();
            email.To.Add(new EmailAddress { Name = orderViewModel.Consumer.Name, Address = orderViewModel.Consumer.Email });
            email.Body = GetEmailBody(productItens, order.WithdrawDate, order);
            email.Subject = "Confirmação de reserva";
            Send(email, null, "Confirmação de reserva");
        }

        private bool Send(Email email, List<object> attachments, string subject)
        {
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Antes Que Vença", "contato@antesquevenca.com.br"));
                foreach (var to in email.To)
                {
                    if (AntesQueVenca.Domain.Validations.EmailValidation.Valido(to.Address))
                        message.To.Add(new MailboxAddress(to.Name, to.Address));
                }

                if (email.Cc != null && email.Cc.Count > 0)
                {
                    foreach (var cc in email.Cc)
                    {
                        if (AntesQueVenca.Domain.Validations.EmailValidation.Valido(cc.Address))
                            message.Cc.Add(new MailboxAddress(cc.Name, cc.Address));
                    }
                }

                if (email.Bcc != null && email.Bcc.Count > 0)
                {
                    foreach (var bcc in email.Bcc)
                    {
                        if (AntesQueVenca.Domain.Validations.EmailValidation.Valido(bcc.Address))
                            message.Bcc.Add(new MailboxAddress(bcc.Name, bcc.Address));
                    }
                }
                email.Subject = subject;
                message.Subject = email.Subject;

                var builder = new BodyBuilder();


                builder.HtmlBody = email.Body + email.Body2 + email.Body3;

                if (attachments != null && attachments.Count > 0)
                {
                    foreach (var att in attachments)
                    {
                        builder.Attachments.Add("", System.IO.File.ReadAllBytes(""), MimeKit.ContentType.Parse(""));
                    }
                }

                // Now we just need to set the message body and we're done
                message.Body = builder.ToMessageBody();

                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                client.Connect("mail.antesquevenca.com.br", 587, SecureSocketOptions.StartTls);
                client.Authenticate("contato@antesquevenca.com.br", "AntesQueVenca00");
                client.Send(message);
                client.Disconnect(true);

                return true;
            }
        }

        private string GetEmailBody(List<Cart> cartItems, DateTime expirationDate, Order order)
        {
            string html = "";
            html += AntesQueVencaResources.OrderConfirmationBeforeProducts;
            decimal total = 0;
            total += cartItems.Sum(prop => Convert.ToDecimal(prop.Total.Replace(".", ",")));

            if (order.DeliveryType == DeliveryTypeEnum.Delivery)
                total += order.DeliveryPrice;

            html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'><b>Detalhes da sua reserva:</b></p>";

            html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'><b>Código da reserva:</b></p>";
            html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'>" + order.OrderId + "</p>";

            html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'><b>Valor total:</b></p>";
            html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'>" + total.ToString("C") + "</p>";

            if (order.DeliveryType == DeliveryTypeEnum.Withdrawal)
            {
                html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'><b>Sua reserva expira em:</b></p>";
                html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'>" + expirationDate.ToShortDateString() + " às " + expirationDate.ToShortTimeString() + "</p>";

                html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'><b>Retirada:</b></p>";
                html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'>Takeo Perfumaria - Av.Dr.Eduardo Cotching, 1970 - Vila Formosa, São Paulo - SP</p>";

                html += "</br>";
                html += "<p style='font-family: sans-serif; font-size: 16px; font-weight: normal; margin: 0; Margin-bottom: 15px;color:red;'>";
                html += "Para pagar e retirar seus produtos reservados é simples, dirija-se à Perfumaria Takeo localizada no endereço acima, com o seu CPF e o código de sua reserva realizada pelo Antes Que Vença, localizado no corpo deste E-mail. ";
                html += "</p>";
                html += "</br>";

                html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'><b>Horário de funcionamento:</b></p>";
                html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'>De segunda-feira a sábado, das 09:30 às 18:00.</p>";

                html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'><b>Formas de pagamento:</b></p>";
                html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'>Dinheiro - Cartão de Crédito - Cartão de Débito.</p>";

                html += "</br>";
                html += "<a href='https://www.antesquevenca.com.br/retirada.html' target='_blank' style='display: inline-block; color: #ffffff; background-color: #3498db; border: solid 1px #3498db; border-radius: 5px; box-sizing: border-box; cursor: pointer; text-decoration: none; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-transform: capitalize; border-color: #3498db;'>Ver instruções de retirada</a>";
                html += "</br>";
            }
            else if (order.DeliveryType == DeliveryTypeEnum.Delivery)
            {
                var shippingPrice = order.DeliveryPrice > 0 ? order.DeliveryPrice.ToString().Replace(".", ",") : "Grátis";
                html += "<br />";
                html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'><b>Informações importantes:</b></p>";
                
                html += "<div class='alert alert-info'>";
                html += "<div class='row'>";
                html += "<label class='text text-info' style='font-size:16px;'> *** Taxa de entrega: R$ " + shippingPrice + "</label>";
                html += "</div>";
                html += "<div class='row'>";
                html += "<label class='text text-info' style='font-size:16px;'> *** Você receberá os produtos em até 3 dias úteis.</label>";
                html += "</div>";
                html += "<div class='row'>";
                html += "<label class='text text-info' style='font-size:16px;'> *** Nossa equipe entrará em contato no dia da entrega para confirmar a entrega.</label>";
                html += "</div>";
                html += "<div class='row'>";
                html += "<strong class='text text-info' style='font-size:16px;'> *** Você pode optou por pagar em: " + Enum.GetName(order.PaymentType) + "</strong>";
                html += "</div>";
                html += "</div>";

                html += "<br />";
            }
            
            html += "<div class='row'>";
            html += "<strong class='text text-info' style='font-size:16px;'>Para cancelamento da reserva, <a href='https://api.whatsapp.com/send?phone=5511951466837&text=SAC'>clique aqui</a>, e entre em contato com nosso SAC via WhatsApp informando seu CPF e o código da reserva.</strong>";
            html += "</div>";

            html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'>Qualquer dúvida, estamos à disposição por meio de nossos canais de comunicação com o usuário: </p>";
            html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;color:red;'>contato@antesquevenca.com.br</p>";
            html += "</br>";

            html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'><b>Produtos:</b></p>";

            foreach (var item in cartItems)
            {
                var unitPrice = decimal.Parse(item.UnitPrice.Replace(".", ",")).ToString("C");
                var totalItem = decimal.Parse(item.Total.Replace(".", ",")).ToString("C");
                html += "<p style='font-family: sans-serif; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'>Quantidade: " + item.Count + " - " + item.Name + " - Valor unitário: " + unitPrice + " - Total: " + totalItem + "</p>";
                html += "<img src='" + item.Image + "' alt='" + item.Name + "' width='100' height='100' border='0' style='border:0; outline:none; text-decoration:none; display:block;' />";
            }

            html += "</br>";
            html += "<p style='font-family: sans-serif; color: red; font-size: 18px; font-weight: normal; margin: 0; Margin-bottom: 15px;'>(*) Reserva sujeita à disponibilidade de estoque do VAREJISTA.</p>";

            html += AntesQueVencaResources.OrderConfirmationAfterProducts;
            return html;
        }
        private decimal calculoFrete(string cep)
        {
            decimal frete = 0;
            if (!String.IsNullOrEmpty(cep))
            {
                var postalCode = _postalCodeDetailRepository.GetAll().Where(x => x.PostalCode == cep.Replace("-", "").Replace(" ", ""));
                if (postalCode != null && postalCode.Count() > 0)
                {

                    frete = postalCode.FirstOrDefault().Price;


                }
            }

            return frete;
        }
        public IActionResult SearchCep(string cep)
        {
            var result = new Result<object>();
            if (!String.IsNullOrEmpty(cep))
            {
                var postalCode = _postalCodeDetailRepository.GetAll().Where(x => x.PostalCode == cep.Replace("-", "").Replace(" ", ""));
                if (postalCode != null && postalCode.Count() > 0)
                {
                    result.Retorno = postalCode.FirstOrDefault().Price;
                    result.Success = true;
                }
                else
                    result.Success = false;
            }
            else
                result.Success = false;

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }
    }
}
