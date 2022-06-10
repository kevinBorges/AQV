using AntesQueVenca.Domain.Enuns;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AntesQueVenca.Ecommerce.ViewModels
{
    public class OrderViewModel
    {
        public ConsumerViewModel Consumer { get; set; }

        public DeliveryTypeEnum DeliveryType { get; set; }
        
        public PaymentTypeEnum PaymentType { get; set; }

        public List<ProductViewModel> Products { get; set; }

        public Cart[] CartItems { get; set; }
        
        public string Thing { get; set; }
        public decimal DeliveryPrice { get; set; }
    }

    public class Cart
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("unitPrice")]
        public string UnitPrice { get; set; }
        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("total")]
        public string Total { get; set; }

        [JsonProperty("priceFrom")]
        public string PriceFrom { get; set; }

        

    }

}
