using System.Text.Json.Serialization;

namespace DomainModelLayer
{
    public class WelcomeEmail : IEmailVariables
    {
        public string TemplateUuid => "a92a080c-fc38-4a8b-a222-a484933b013e"; // UUID de la plantilla de MailTrap

        [JsonPropertyName("user_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("ecommerce_name")]
        public string EcommerceName { get; set; }

        [JsonPropertyName("ecommerce_link")]
        public string EcommerceLink { get; set; }
    }
}
