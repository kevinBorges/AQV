using AntesQueVenca.Application.ViewModels;
using AntesQueVenca.Data.Repositories;
using AntesQueVenca.Domain.Entities;
using AntesQueVenca.Domain.Entities.Helpers;
using AntesQueVenca.Domain.Enuns;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace coderush.Controllers
{
    public class DashboardController : Controller
    {
        OrderRepository orderRepository;
        ProductRepository productRepository;
        RetailerProductItemRepository retailerProductItemRepository;
        CategoryRepository categoryRepository;

        public DashboardController()
        {
            orderRepository = new OrderRepository();
            productRepository = new ProductRepository();
            retailerProductItemRepository = new RetailerProductItemRepository();
            categoryRepository = new CategoryRepository();
        }

        public IActionResult Dashboard1()
        {
            return RedirectToAction(nameof(OrderList));
        }

        [HttpGet]
        public IActionResult OrderList()
        {
            var ordersViewModel = GetOrders();
            return View(ordersViewModel);
        }

        [HttpGet]
        public IActionResult GetOrdersPartial(string consumerName, OrderStatusEnum? statusFilter, DateFilterEnum? dateFilter) 
        {
            var ordersViewModel = GetOrders(consumerName, statusFilter, dateFilter);
            return PartialView("_Orders", ordersViewModel);
        }

        private List<OrderViewModel> GetOrders(string consumerName=null, OrderStatusEnum? statusFilter=null, DateFilterEnum? dateFilter=null)
        {
            var orders = orderRepository.GetAll();

            if (!string.IsNullOrEmpty(consumerName))
                orders = orders.Where(prop => prop.Consumer.Person.Name.Contains(consumerName, StringComparison.InvariantCultureIgnoreCase));

            if (dateFilter != null && dateFilter != DateFilterEnum.Todos)
            {
                if (dateFilter == DateFilterEnum.Hoje)
                    orders = orders.Where(prop => prop.CreatedDate.Date == DateTime.Now.Date);
                if (dateFilter == DateFilterEnum.EssaSemana)
                    orders = orders.Where(prop => prop.CreatedDate.Date >= DateTime.Now.AddDays(-7).Date);
                if (dateFilter == DateFilterEnum.EsseMes)
                    orders = orders.Where(prop => prop.CreatedDate.Date >= DateTime.Now.AddMonths(-1).Date);
                if (dateFilter == DateFilterEnum.TresMeses)
                    orders = orders.Where(prop => prop.CreatedDate.Date >= DateTime.Now.AddMonths(-3).Date);
                if (dateFilter == DateFilterEnum.SeisMeses)
                    orders = orders.Where(prop => prop.CreatedDate.Date >= DateTime.Now.AddMonths(-6).Date);
            }

            if (statusFilter != null) 
                orders = orders.Where(prop => prop.Status == statusFilter); ;

            var ordersViewModel = new List<OrderViewModel>();
            foreach (var order in orders.OrderByDescending(o => o.CreatedDate))
            {
                var orderViewModel = new OrderViewModel
                {
                    Status = order.Status,
                    OrderId = order.OrderId,
                    WithdrawDate = order.WithdrawDate.ToShortDateString(),
                    Consumer = new ConsumerViewModel
                    {
                        ConsumerId = order.ConsumerId,
                        Person = new PersonViewModel
                        {
                            Email = order.Consumer?.Person?.Email,
                            Name = order.Consumer?.Person?.Name,
                            PersonTypeId = (int)order.Consumer?.Person?.PersonTypeId,
                            PersonId = (int)order.Consumer?.PersonId,
                        }
                    },
                    CreatedDate = order.CreatedDate,
                    Delivery = order.DeliveryType == AntesQueVenca.Domain.Enuns.DeliveryTypeEnum.Delivery ? "Delivery" : "Retirada",
                    TotalPrice = order.OrderProducts.Sum(prop => prop.ProductItem.Price * prop.Quantity)
                };

                ordersViewModel.Add(orderViewModel);
            }

            return ordersViewModel;
        }

        private void UpdatePhotoUrl()
        {
            var products = productRepository.GetAll();
            foreach (var product in products)
            {
                product.Image = "http://www.antesquevenca.com.br/images/Produtos/" + product.ProductId + ".jpg";
                productRepository.Update(product);
            }
            productRepository.Commit();
        }

        public IActionResult GetOrderById(int id)
        {
            var result = new Result<object>();
            if (id > 0)
            {
                var order = orderRepository.GetById(id);
                var phone = ((PhysicalPerson)order.Consumer.Person).Phones != null && ((PhysicalPerson)order.Consumer.Person).Phones.Count > 0 ? ((PhysicalPerson)order.Consumer.Person).Phones.FirstOrDefault().DDD + "-" + ((PhysicalPerson)order.Consumer.Person).Phones.FirstOrDefault().Number : "";
                var orderViewModel = new OrderViewModel
                {
                    Status = order.Status,
                    OrderId = order.OrderId,
                    WithdrawDate = order.WithdrawDate.ToShortDateString(),
                    Consumer = new ConsumerViewModel
                    {
                        ConsumerId = order.ConsumerId,
                        Person = new PersonViewModel
                        {
                            Email = order.Consumer?.Person?.Email,
                            Name = order.Consumer?.Person?.Name,
                            PersonTypeId = (int)order.Consumer?.Person?.PersonTypeId,
                            PersonId = (int)order.Consumer?.PersonId,
                            CPF = ((PhysicalPerson)order.Consumer.Person).CPF,
                            Phone = phone,
                        }
                    },
                    CriadoEm = order.CreatedDate.ToShortDateString() + " " + order.CreatedDate.ToShortTimeString(),
                    Delivery = order.DeliveryType == AntesQueVenca.Domain.Enuns.DeliveryTypeEnum.Delivery ? "Delivery" : "Retirada",
                    TotalPrice = order.OrderProducts.Sum(prop => prop.ProductItem.Price * prop.Quantity),
                    Thing = order.Thing.ToString("C"),
                    PaymentType = order.PaymentType

                };

                if(orderViewModel.Delivery == "Delivery")
                {
                    if (orderViewModel.Consumer.Person.Address == null)
                        orderViewModel.Consumer.Person.Address = new Address();

                   orderViewModel.Consumer.Person.Address = order.Consumer.Person.Addresses.FirstOrDefault();
                        //= order.Consumer.Person.Addresses.FirstOrDefault().Street + " ,N° " +
                        //order.Consumer.Person.Addresses.FirstOrDefault().Number + " - " +
                        //order.Consumer.Person.Addresses.FirstOrDefault().Neighborhood + " - " +
                        //order.Consumer.Person.Addresses.FirstOrDefault().CityName;
                }



                foreach (var orderProductItem in order.OrderProducts)
                {
                    if (orderViewModel.Products == null)
                        orderViewModel.Products = new List<ProductItemViewModel>();

                    orderViewModel.Products.Add(new ProductItemViewModel
                    {
                        ExpirationDate = orderProductItem.ProductItem.ExpirationDate,
                        Price = orderProductItem.ProductItem.Price,
                        PriceFrom = orderProductItem.ProductItem.PriceFrom,
                        Product = new ProductViewModel { Name = orderProductItem.ProductItem.Product.Name, Image = orderProductItem.ProductItem.Product.Image },
                        ProductId = orderProductItem.ProductItem.ProductId,
                        ProductItemId = orderProductItem.ProductItemId,
                        //RetailerId = orderProductItem.ProductItem.RetailerId,
                        Quantity = orderProductItem.Quantity
                    });
                }

                result.Retorno = orderViewModel;
                result.Success = true;
            }
            else
            {
                result.Message = "Erro ao consultar dados";
                result.Success = false;
            }
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        public IActionResult UpdateOrderStatus(int statusId, int orderId)
        {
            var result = new Result<object>();

            if (statusId > 0 && orderId > 0)
            {
                var order = orderRepository.GetById(orderId);
                if (order.Status != OrderStatusEnum.Expirado)
                {
                    order.Status = (OrderStatusEnum)statusId;
                    orderRepository.Update(order);
                    orderRepository.Commit();


                    if (order.Status == OrderStatusEnum.Expirado)
                    {
                        foreach (var orderProduct in order.OrderProducts)
                        {
                            var retailerProductItem = retailerProductItemRepository.GetAll().Where(p => p.ProductItemId == orderProduct.ProductItemId).FirstOrDefault();
                            retailerProductItem.Quantity += orderProduct.Quantity;
                            retailerProductItemRepository.Update(retailerProductItem);
                        }
                        retailerProductItemRepository.Commit();
                    }

                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.Message = "Atenção, reservas expiradas não podem mudar de satus.";
                }
            }
            else
            {
                result.Success = false;
            }

            return Content(JsonConvert.SerializeObject(result));
        }

    }
}