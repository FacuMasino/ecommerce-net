using System;
using DataAccessLayer;
using DomainModelLayer;

namespace BusinessLogicLayer
{
    public class PaymentTypesManager
    {
        private DataAccess _dataAccess = new DataAccess();

        public PaymentType Read(int paymentTypeId)
        {
            PaymentType paymentType = new PaymentType();

            try
            {
                _dataAccess.SetQuery("select PaymentTypeName from PaymentTypes where PaymentTypeId = @PaymentTypeId");
                _dataAccess.SetParameter("@PaymentTypeId", paymentTypeId);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    paymentType.Id = paymentTypeId;
                    paymentType.Name = (string)_dataAccess.Reader["PaymentTypeName"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dataAccess.CloseConnection();
            }

            return paymentType;
        }
    }
}
