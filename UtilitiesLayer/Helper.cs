using DataAccessLayer;
using System;

namespace UtilitiesLayer
{
    public static class Helper
    {
        // HACK: Reemplazar GetLastId() implementando el comando output en las consultas de los métodos Add() de todos los managers.
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
    }
}
