using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DomainModelLayer
{
    public interface IEmailVariables
    {
        string TemplateUuid { get; }
    }

    public class EmailAddress
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class EmailMessage<T>
        where T : IEmailVariables, new()
    {
        [JsonPropertyName("from")]
        public EmailAddress From { get; set; }

        [JsonPropertyName("to")] // Es posible mas de un destinatario
        public List<EmailAddress> To { get; set; }

        [JsonPropertyName("template_uuid")]
        public string TemplateUuid => TemplateVariables.TemplateUuid; // Obtiene el UUID que esta en la clase plantilla

        [JsonPropertyName("template_variables")]
        public T TemplateVariables { get; set; }

        public EmailMessage()
        {
            From = new EmailAddress();
            To = new List<EmailAddress>();
            TemplateVariables = new T();
        }

        public EmailMessage(string from, string nameFrom)
        {
            From = new EmailAddress { Email = from };
            To = new List<EmailAddress>();
            TemplateVariables = new T();
        }
    }
}
