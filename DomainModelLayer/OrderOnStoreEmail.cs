using System.Text.Json.Serialization;

namespace DomainModelLayer
{
    public class OrderOnStoreEmail : IEmailVariables
    {
        public string TemplateUuid => "55149f71-cacc-4eb0-acc3-cb8088daa87f"; // UUID de la plantilla de MailTrap

        [JsonPropertyName("order_number")]
        public string OrderNumber { get; set; }

        [JsonPropertyName("name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Se recomienda usar uno de estos ejemplos
        /// <para>Ejemplo 1: "ser pagado y retirado"</para>
        /// <para>Ejemplo 2: "ser retirado"</para>
        /// </summary>
        [JsonPropertyName("order_action")]
        public string OrderAction { get; set; }

        [JsonPropertyName("order_link")]
        public string OrderLink { get; set; }

        [JsonPropertyName("ecommerce_name")]
        public string EcommerceName { get; set; }
    }
}
