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

        public List<Order> List(int personId = 0) // hack : renombrar atributo OrderStatus por CurrentOrderStatus en clase Order del DML
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

                    if (_dataAccess.Reader.IsDBNull(_dataAccess.Reader.GetOrdinal("DeliveryDate"))) // hack
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
                order.OrderStatus = ReadOrderStatus(order.OrderStatus.Id);
                order.DistributionChannel = ReadDistributionChannel(order.DistributionChannel.Id);

                order.User.UserId = _usersManager.GetUserId(order.User.PersonId);

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

        private OrderStatus ReadOrderStatus(int orderStatusId)
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

        private DistributionChannel ReadDistributionChannel(int distributionChannelId)
        {
            DistributionChannel distributionChannel = new DistributionChannel();

            try
            {
                _dataAccess.SetQuery("select DistributionChannelName from DistributionChannels where DistributionChannelId = @DistributionChannelId");
                _dataAccess.SetParameter("@DistributionChannelId", distributionChannelId);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    distributionChannel.Id = distributionChannelId;
                    distributionChannel.Name = _dataAccess.Reader["DistributionChannelName"]?.ToString();
                    distributionChannel.Name = distributionChannel.Name ?? ""; // hack : Verificar si es necesaria esta linea. Buscar posibles casos similares caso que no lo sea.
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

            // hack : modularizar ListOrderStatuses()

            try
            {
                _dataAccess.SetProcedure("SP_List_Order_Statuses");
                _dataAccess.SetParameter("@DistributionChannelId", distributionChannel.Id);
                _dataAccess.ExecuteRead();

                while (_dataAccess.Reader.Read())
                {
                    OrderStatus orderStatus = new OrderStatus();
                    orderStatus.Id = (int)_dataAccess.Reader["OrderStatusId"];
                    distributionChannel.OrderStatuses.Add(orderStatus);
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

            for (int i = 0; i < distributionChannel.OrderStatuses.Count; i++)
            {
                distributionChannel.OrderStatuses[i] = ReadOrderStatus(distributionChannel.OrderStatuses[i].Id);
            }

            return distributionChannel;
        }
    }
}
