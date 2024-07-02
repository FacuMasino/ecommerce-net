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

                    if (_dataAccess.Reader.IsDBNull(_dataAccess.Reader.GetOrdinal("DeliveryDate")))
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
                order.OrderStatus = ReadOrderStatus(order.User.PersonId);

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
    }
}
