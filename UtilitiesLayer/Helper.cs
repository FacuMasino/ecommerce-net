using System;
using System.Collections.Generic;
using System.Web.UI;
using DataAccessLayer;
using DomainModelLayer;

namespace UtilitiesLayer
{
    public static class Helper
    {
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
    }
}
