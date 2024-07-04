using System.Text.Json.Serialization;

namespace DomainModelLayer
{
    public class NewOrderEmail : IEmailVariables
    {
        public string TemplateUuid => "c792b41c-ce5e-4f33-8ba2-48bba09c9ec2"; // UUID de la plantilla de MailTrap

        [JsonPropertyName("order_number")]
        public string OrderNumber { get; set; }

        [JsonPropertyName("name")]
        public string FirstName { get; set; }

        [JsonPropertyName("order_link")]
        public string OrderLink { get; set; }

        [JsonPropertyName("ecommerce_name")]
        public string EcommerceName { get; set; }
    }
}
