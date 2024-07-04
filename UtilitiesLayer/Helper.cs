using System;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Web.UI;
using DataAccessLayer;
using DomainModelLayer;

namespace UtilitiesLayer
{
    public static class Helper
    {
        // TODO: Estos datos podrian estar en una tabla de configuraciones de la DB
        public static string EcommerceName
        {
            get { return WebConfigurationManager.AppSettings["ecommerce_name"]; }
        }
        public static string EcommerceUrl
        {
            get { return WebConfigurationManager.AppSettings["ecommerce_url"]; }
        }

        // hack: reemplazar GetLastId() implementando el comando output en las consultas de los métodos Add() de todos los managers.
        public static int GetLastId(string table)
        {
            int lastId = 0;
            DataAccess dataAccess = new DataAccess();

            try
            {
                dataAccess.SetQuery("select ident_current(@Table) as LastId");
                dataAccess.SetParameter("@Table", table);
                dataAccess.ExecuteRead();

                if (dataAccess.Reader.Read())
                {
                    lastId = Convert.ToInt32(dataAccess.Reader["LastId"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataAccess.CloseConnection();
            }

            return lastId;
        }

        /// <summary>
        /// Calcula el margen de ganancia de un producto
        /// </summary>
        /// <param name="price">Precio de venta</param>
        /// <param name="cost">Costo del producto</param>
        /// <returns>Porcentaje de ganancia</returns>
        public static decimal CalcReturns(decimal price, decimal cost)
        {
            if (cost <= 0)
                return 0.00m;
            return ((price / cost) - 1) * 100;
        }

        /// <summary>
        /// Genera una lista de nombres de categorías separados por comas a partir de una lista de categorías.
        /// </summary>
        /// <param name="categories">La lista de categorías a procesar.</param>
        /// <returns>Una cadena que contiene los nombres de las categorías, separados por comas.</returns>
        public static string GetCategoriesList(List<Category> categories)
        {
            string categoriesList = "";

            for (int i = 0; i < categories.Count; i++)
            {
                categoriesList += categories[i].Name;

                if (i < categories.Count - 1)
                {
                    categoriesList += ", ";
                }
            }

            return categoriesList;
        }

        /// <summary>
        /// Busca un control dentro de una página maestra especificada.
        /// </summary>
        /// <param name="masterPage">La MasterPage que contiene el control.</param>
        /// <param name="controlId">El ID del control a buscar.</param>
        /// <returns>
        /// El control con el ID especificado si se encuentra; de lo contrario, null.
        /// </returns>
        /// <remarks>
        /// Este método asume que el control se encuentra dentro de un ContentPlaceHolder con el ID "BodyPlaceHolder".
        /// </remarks>
        public static Control FindControl(MasterPage masterPage, string controlId)
        {
            Control foundCtrl = masterPage.FindControl("BodyPlaceHolder").FindControl(controlId);
            return foundCtrl;
        }

        public static string PrintCategoriesCount(object categoriesList)
        {
            var categories = categoriesList as List<Category>;

            if (categories == null || categories.Count < 2)
            {
                return "";
            }

            return $" (+{categories.Count - 1})";
        }

        public static void AssignPerson<DestinyClass, OriginClass>(
            DestinyClass destinyObject,
            OriginClass originObject
        )
            where DestinyClass : Person, new()
            where OriginClass : Person, new()
        {
            destinyObject.PersonId = originObject.PersonId;
            destinyObject.IsActive = originObject.IsActive;
            destinyObject.FirstName = originObject.FirstName;
            destinyObject.LastName = originObject.LastName;
            destinyObject.TaxCode = originObject.TaxCode;
            destinyObject.Phone = originObject.Phone;
            destinyObject.Email = originObject.Email;
            destinyObject.Birth = originObject.Birth;
            destinyObject.Address = originObject.Address;

            if (originObject is User && destinyObject is User)
            {
                (destinyObject as User).UserId = (originObject as User).UserId;
                (destinyObject as User).Username = (originObject as User).Username;
                (destinyObject as User).Password = (originObject as User).Password;
                (destinyObject as User).Role = (originObject as User).Role;
            }
        }

        public static EmailMessage<WelcomeEmail> ComposeWelcomeEmail(
            User user,
            string ecommerceName,
            string ecommerceUrl
        )
        {
            EmailMessage<WelcomeEmail> welcomeEmail = new EmailMessage<WelcomeEmail>
            {
                To = new List<EmailAddress> { new EmailAddress { Email = user.Email } },
                TemplateVariables = new WelcomeEmail
                {
                    FirstName = user.FirstName,
                    EcommerceName = ecommerceName,
                    EcommerceLink = ecommerceUrl
                }
            };

            return welcomeEmail;
        }

        public static EmailMessage<NewOrderEmail> ComposeNewOrderEmail(
            User user,
            string ecommerceName,
            string orderNumber,
            string orderLink
        )
        {
            EmailMessage<NewOrderEmail> newOrderEmail = new EmailMessage<NewOrderEmail>
            {
                To = new List<EmailAddress> { new EmailAddress { Email = user.Email } },
                TemplateVariables = new NewOrderEmail
                {
                    FirstName = user.FirstName,
                    EcommerceName = ecommerceName,
                    OrderLink = orderLink,
                    OrderNumber = orderNumber
                }
            };

            return newOrderEmail;
        }

        public static EmailMessage<OrderShippingEmail> ComposeShippingEmail(
            User user,
            string ecommerceName,
            string orderNumber,
            string orderLink,
            string orderTracking
        )
        {
            EmailMessage<OrderShippingEmail> shippingEmail = new EmailMessage<OrderShippingEmail>
            {
                To = new List<EmailAddress> { new EmailAddress { Email = user.Email } },
                TemplateVariables = new OrderShippingEmail
                {
                    FirstName = user.FirstName,
                    EcommerceName = ecommerceName,
                    OrderLink = orderLink,
                    OrderNumber = orderNumber,
                    OrderTracking = orderTracking
                }
            };

            return shippingEmail;
        }

        public static EmailMessage<OrderOnStoreEmail> ComposeOrderOnStoreEmail(
            User user,
            string ecommerceName,
            string orderNumber,
            string orderLink,
            string orderAction
        )
        {
            EmailMessage<OrderOnStoreEmail> orderOnStoreEmail = new EmailMessage<OrderOnStoreEmail>
            {
                To = new List<EmailAddress> { new EmailAddress { Email = user.Email } },
                TemplateVariables = new OrderOnStoreEmail
                {
                    FirstName = user.FirstName,
                    EcommerceName = ecommerceName,
                    OrderLink = orderLink,
                    OrderNumber = orderNumber,
                    OrderAction = orderAction
                }
            };

            return orderOnStoreEmail;
        }
    }
}
