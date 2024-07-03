using DataAccessLayer;
using DomainModelLayer;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class OrderStatusesManager
    {
        private DataAccess _dataAccess = new DataAccess();

        public List<OrderStatus> List(int distributionChannelId)
        {
            List<OrderStatus> orderStatuses = new List<OrderStatus>();

            try
            {
                _dataAccess.SetProcedure("SP_List_Order_Statuses");
                _dataAccess.SetParameter("@DistributionChannelId", distributionChannelId);
                _dataAccess.ExecuteRead();

                while (_dataAccess.Reader.Read())
                {
                    OrderStatus orderStatus = new OrderStatus();

                    orderStatus.Id = (int)_dataAccess.Reader["OrderStatusId"];
                    orderStatus.Name = (string)_dataAccess.Reader["OrderStatusName"];

                    orderStatuses.Add(orderStatus);
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

            return orderStatuses;
        }

        public OrderStatus Read(int orderStatusId)
        {
            OrderStatus orderStatus = new OrderStatus();

            try
            {
                _dataAccess.SetQuery("select OrderStatusName from OrderStatuses where OrderStatusId = @OrderStatusId");
                _dataAccess.SetParameter("@OrderStatusId", orderStatusId);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    orderStatus.Id = orderStatusId;
                    orderStatus.Name = _dataAccess.Reader["OrderStatusName"]?.ToString();
                    orderStatus.Name = orderStatus.Name ?? "";
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

            return orderStatus;
        }
    }
}
