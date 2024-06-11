using System;
using DataAccessLayer;

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
    }
}
