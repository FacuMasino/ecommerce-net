using System;
using System.Collections.Generic;
using DataAccessLayer;
using DomainModelLayer;

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
                _dataAccess.SetProcedure("SP_ListOrderStatuses"); // hack : error al nombrar este SP como SP_List_Order_Statuses
                _dataAccess.SetParameter("@DistributionChannelId", distributionChannelId);
                _dataAccess.ExecuteRead();

                while (_dataAccess.Reader.Read())
                {
                    OrderStatus orderStatus = new OrderStatus();

                    orderStatus.Id = (int)_dataAccess.Reader["OrderStatusId"];
                    orderStatus.Name = (string)_dataAccess.Reader["OrderStatusName"];
                    orderStatus.TransitionText = (string)_dataAccess.Reader["TransitionText"];

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
                _dataAccess.SetQuery("select OrderStatusName, TransitionText from OrderStatuses where OrderStatusId = @OrderStatusId");
                _dataAccess.SetParameter("@OrderStatusId", orderStatusId);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    orderStatus.Id = orderStatusId;
                    orderStatus.Name = (string)_dataAccess.Reader["OrderStatusName"];
                    orderStatus.TransitionText = (string)_dataAccess.Reader["TransitionText"];
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

        public int Add(OrderStatus orderStatus)
        {
            // hack : No es necesario para un MVP
            return -1;
        }

        public int GetId(OrderStatus orderStatus)
        {
            int orderStatusId = 0;

            try
            {
                _dataAccess.SetQuery("select OrderStatusId from OrderStatuses where OrderStatusName = @OrderStatusName");
                _dataAccess.SetParameter("@OrderStatusName", orderStatus.Name);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    orderStatusId = (int)_dataAccess.Reader["OrderStatusId"];
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

            return orderStatusId;
        }
    }
}
