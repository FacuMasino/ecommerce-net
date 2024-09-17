using System;
using System.Collections.Generic;
using DataAccessLayer;
using DomainModelLayer;

namespace BusinessLogicLayer
{
    public class OrderStatusesManager
    {
        private DataAccess _dataAccess = new DataAccess();
        private RolesManager _rolesManager = new RolesManager();

        public enum Ids
        {
            ProcessingPaymentId = 1,
            OrderCompletedId = 5,
            PaymentAndWithdrawalPendingId = 6,
            OrderCancelledId = 9,
            DeliveryAndPaymentPendingId = 10
        }

        public List<OrderStatus> List(int distributionChannelId, int currentOrderStatusId = 0)
        {
            List<OrderStatus> orderStatuses = new List<OrderStatus>();

            try
            {
                _dataAccess.SetProcedure("SP_List_Order_Statuses");
                _dataAccess.SetParameter("@DistributionChannelId", distributionChannelId);
                _dataAccess.SetParameter("@CurrentOrderStatusId", currentOrderStatusId);
                _dataAccess.ExecuteRead();

                while (_dataAccess.Reader.Read())
                {
                    OrderStatus orderStatus = new OrderStatus();

                    orderStatus.Id = (int)_dataAccess.Reader["OrderStatusId"];
                    orderStatus.Name = (string)_dataAccess.Reader["OrderStatusName"];
                    orderStatus.TransitionText = (string)_dataAccess.Reader["TransitionText"];
                    orderStatus.AcceptedText = (string)_dataAccess.Reader["AcceptedText"];
                    orderStatus.Role.Id = Convert.ToInt32(_dataAccess.Reader["RoleId"]);

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

            foreach (OrderStatus orderStatus in orderStatuses)
            {
                orderStatus.Role = _rolesManager.Read(orderStatus.Role.Id);
            }

            return orderStatuses;
        }

        public OrderStatus Read(int orderStatusId)
        {
            OrderStatus orderStatus = new OrderStatus();

            try
            {
                _dataAccess.SetProcedure("SP_Read_Order_Status");
                _dataAccess.SetParameter("@OrderStatusId", orderStatusId);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    orderStatus.Id = orderStatusId;
                    orderStatus.Name = (string)_dataAccess.Reader["OrderStatusName"];
                    orderStatus.TransitionText = (string)_dataAccess.Reader["TransitionText"];
                    orderStatus.AcceptedText = (string)_dataAccess.Reader["AcceptedText"];
                    orderStatus.Role.Id = Convert.ToInt32(_dataAccess.Reader["RoleId"]);
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

            orderStatus.Role = _rolesManager.Read(orderStatus.Role.Id);

            return orderStatus;
        }

        public int Add(OrderStatus orderStatus)
        {
            // hack : No es necesario para un MVP
            return -1;
        }

        public void Edit(OrderStatus orderStatus)
        {
            // hack : No es necesario para un MVP
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

        public int GetNextStatusId(int distributionChannelId, int currentOrderStatusId)
        {
            int orderStatusId = 0;

            try
            {
                _dataAccess.SetProcedure("SP_Get_Next_Status_Id");
                _dataAccess.SetParameter("@DistributionChannelId", distributionChannelId);
                _dataAccess.SetParameter("@CurrentOrderStatusId", currentOrderStatusId);
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

        public void HandleOrderStatusId(OrderStatus orderStatus)
        {
            int foundId = GetId(orderStatus);

            if (foundId == 0)
            {
                orderStatus.Id = Add(orderStatus);
            }
            else if (foundId == orderStatus.Id)
            {
                Edit(orderStatus);
            }
            else
            {
                orderStatus.Id = foundId;
            }
        }
    }
}
