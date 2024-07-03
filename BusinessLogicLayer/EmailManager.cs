using System;
using System.Configuration;
using System.Text.Json;
using DomainModelLayer;
using RestSharp;

namespace BusinessLogicLayer
{
    public class EmailManager
    {
        private RestClient _client;
        private RestRequest _request;
        private string _emailFrom;
        private string _nameFrom;

        public EmailManager()
        {
            _client = new RestClient("https://send.api.mailtrap.io/api/send");
            _emailFrom = ConfigurationManager.AppSettings["email_from"];
            _nameFrom = ConfigurationManager.AppSettings["email_name_from"];
            SetRequest();
        }

        private void SetRequest()
        {
            string apiToken = ConfigurationManager.AppSettings["api_token"];
            _request = new RestRequest();
            _request.AddHeader("Authorization", apiToken);
            _request.AddHeader("Content-Type", "application/json");
        }

        public void SendTest()
        {
            var client = new RestClient("https://send.api.mailtrap.io/api/send");
            var request = new RestRequest();
            request.AddHeader("Authorization", ConfigurationManager.AppSettings["api_token"]);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter(
                "application/json",
                "{\"from\":{\"email\":\"mailtrap@demomailtrap.com\",\"name\":\"Mailtrap Test\"},\"to\":[{\"email\":\"joaqfm@gmail.com\"}],\"template_uuid\":\"a92a080c-fc38-4a8b-a222-a484933b013e\",\"template_variables\":{\"user_name\":\"Test_User_name\",\"next_step_link\":\"Test_Next_step_link\",\"get_started_link\":\"Test_Get_started_link\",\"onboarding_video_link\":\"Test_Onboarding_video_link\"}}",
                ParameterType.RequestBody
            );

            var response = client.Post(request);
        }

        public void SendEmail<T>(EmailMessage<T> emailMessage)
            where T : IEmailVariables, new()
        {
            try
            {
                // Si no se pasaron las propiedades, obtener de Web.Config
                if (emailMessage.From.Name == null)
                {
                    emailMessage.From.Email = _emailFrom;
                    emailMessage.From.Name = _nameFrom;
                }

                // Se setea el cuerpo de la solicitud con todas las variables para el email y template
                _request.AddParameter(
                    "application/json",
                    SerializeEmail(emailMessage),
                    ParameterType.RequestBody
                );

                RestResponse response = _client.Post(_request);

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Lo sentimos :( Ocurrio un error al enviar el email.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string SerializeEmail<T>(EmailMessage<T> emailMessage)
            where T : IEmailVariables, new()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return JsonSerializer.Serialize(emailMessage, options);
        }
    }
}
