using System;
using DataAccessLayer;
using DomainModelLayer;

namespace BusinessLogicLayer
{
    public class DistributionChannelsManager
    {
        private DataAccess _dataAccess = new DataAccess();
        private OrderStatusesManager _orderStatusesManager = new OrderStatusesManager();

        public DistributionChannel Read(int distributionChannelId)
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

            distributionChannel.OrderStatuses = _orderStatusesManager.List(distributionChannel.Id);

            return distributionChannel;
        }
    }
}
