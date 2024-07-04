using System.Text.Json.Serialization;

namespace DomainModelLayer
{
    public class OrderShippingEmail : IEmailVariables
    {
        public string TemplateUuid => "e95965e0-772d-4a1e-ad8c-060da51dfd3a"; // UUID de la plantilla de MailTrap

        [JsonPropertyName("order_number")]
        public string OrderNumber { get; set; }

        [JsonPropertyName("name")]
        public string FirstName { get; set; }

        [JsonPropertyName("order_tracking")]
        public string OrderTracking { get; set; }

        [JsonPropertyName("order_link")]
        public string OrderLink { get; set; }

        [JsonPropertyName("ecommerce_name")]
        public string EcommerceName { get; set; }
    }
}
