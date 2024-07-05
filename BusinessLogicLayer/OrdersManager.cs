using System;
using System.Collections.Generic;
using DataAccessLayer;
using DomainModelLayer;
using UtilitiesLayer;

namespace BusinessLogicLayer
{
    public class OrdersManager
    {
        private DataAccess _dataAccess = new DataAccess();
        private Person _person;
        private PeopleManager _peopleManager = new PeopleManager();
        private UsersManager _usersManager = new UsersManager();
        private AddressesManager _addressesManager = new AddressesManager();
        private OrderStatusesManager _orderStatusesManager = new OrderStatusesManager();
        private DistributionChannelsManager _distributionChannelsManager = new DistributionChannelsManager();
        private PaymentTypesManager _paymentTypesManager = new PaymentTypesManager();

        public List<Order> List(int personId = 0)
        {
            List<Order> orders = new List<Order>();

            try
            {
                _dataAccess.SetProcedure("SP_List_Orders");
                _dataAccess.SetParameter("@PersonId", personId);
                _dataAccess.ExecuteRead();

                while (_dataAccess.Reader.Read())
                {
                    Order order = new Order();

                    order.Id = (int)_dataAccess.Reader["OrderId"];
                    order.CreationDate = (DateTime)_dataAccess.Reader["CreationDate"];

                    if (_dataAccess.Reader.IsDBNull(_dataAccess.Reader.GetOrdinal("DeliveryDate"))) // hack : implementar operador ?
                    {
                        order.DeliveryDate = DateTime.MinValue;
                    }
                    else
                    {
                        order.DeliveryDate = _dataAccess.Reader.GetDateTime(_dataAccess.Reader.GetOrdinal("DeliveryDate"));
                    }

                    order.DeliveryAddress.Id = _dataAccess.Reader["DeliveryAddressId"] as int? ?? order.DeliveryAddress.Id;
                    order.OrderStatus.Id = (int)_dataAccess.Reader["OrderStatusId"];
                    order.User.PersonId = (int)_dataAccess.Reader["PersonId"];
                    order.DistributionChannel.Id = (int)_dataAccess.Reader["DistributionChannelId"];
                    order.PaymentType.Id = (int)_dataAccess.Reader["PaymentTypeId"];

                    orders.Add(order);
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

            foreach (Order order in orders)
            {
                order.DeliveryAddress = _addressesManager.Read(order.DeliveryAddress.Id);
                order.OrderStatus = _orderStatusesManager.Read(order.OrderStatus.Id);
                order.DistributionChannel = _distributionChannelsManager.Read(order.DistributionChannel.Id);
                order.PaymentType = _paymentTypesManager.Read(order.PaymentType.Id);

                order.User.UserId = _usersManager.GetId(order.User.PersonId);

                if (0 < order.User.UserId)
                {
                    order.User = _usersManager.Read(order.User.UserId);
                }
                else
                {
                    _person = _peopleManager.Read(order.User.PersonId);
                    Helper.AssignPerson(order.User, _person);
                }
            }

            return orders;
        }

        public Order Read(int orderId)
        {
            Order order = new Order();

            try
            {
                _dataAccess.SetProcedure("SP_Read_Order");
                _dataAccess.SetParameter("@OrderId", orderId);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    order.Id = orderId;
                    order.CreationDate = (DateTime)_dataAccess.Reader["CreationDate"];

                    if (_dataAccess.Reader.IsDBNull(_dataAccess.Reader.GetOrdinal("DeliveryDate"))) // hack : implementar operador ?
                    {
                        order.DeliveryDate = DateTime.MinValue;
                    }
                    else
                    {
                        order.DeliveryDate = _dataAccess.Reader.GetDateTime(_dataAccess.Reader.GetOrdinal("DeliveryDate"));
                    }

                    order.DeliveryAddress.Id = _dataAccess.Reader["DeliveryAddressId"] as int? ?? order.DeliveryAddress.Id;
                    order.OrderStatus.Id = (int)_dataAccess.Reader["OrderStatusId"];
                    order.User.PersonId = (int)_dataAccess.Reader["PersonId"];
                    order.DistributionChannel.Id = (int)_dataAccess.Reader["DistributionChannelId"];
                    order.PaymentType.Id = (int)_dataAccess.Reader["PaymentTypeId"];
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

            order.DeliveryAddress = _addressesManager.Read(order.DeliveryAddress.Id);
            order.OrderStatus = _orderStatusesManager.Read(order.OrderStatus.Id);
            order.DistributionChannel = _distributionChannelsManager.Read(order.DistributionChannel.Id);
            order.PaymentType = _paymentTypesManager.Read(order.PaymentType.Id);

            order.User.UserId = _usersManager.GetId(order.User.PersonId);

            if (0 < order.User.UserId)
            {
                order.User = _usersManager.Read(order.User.UserId);
            }
            else
            {
                _person = _peopleManager.Read(order.User.PersonId);
                Helper.AssignPerson(order.User, _person);
            }

            return order;
        }

        public int Add(Order order, List<ProductSet> productSets)
        {
            return -1; // hack
        }

        /// <summary>
        /// Actualiza el estado de una orden. En caso de estar usando Edit(), sería redundante ejecutar ambos métodos en serie.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderStatusId"></param>
        public void UpdateOrderStatus(int orderId, int orderStatusId)
        {
            try
            {
                _dataAccess.SetQuery("update Orders set OrderStatusId = @OrderStatusId where OrderId = @OrderId");
                _dataAccess.SetParameter("@OrderId", orderId);
                _dataAccess.SetParameter("@OrderStatusId", orderStatusId);
                _dataAccess.ExecuteAction();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }
    }
}
