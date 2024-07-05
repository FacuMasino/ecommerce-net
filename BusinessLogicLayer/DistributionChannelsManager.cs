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
                    distributionChannel.Name = (string)_dataAccess.Reader["DistributionChannelName"];
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

        public int Add(DistributionChannel distributionChannel)
        {
            // hack : No es necesario para un MVP
            return -1;
        }

        public int GetId(DistributionChannel distributionChannel)
        {
            int distributionChannelId = 0;

            try
            {
                _dataAccess.SetQuery("select DistributionChannelId from DistributionChannels where DistributionChannelName = @DistributionChannelName");
                _dataAccess.SetParameter("@DistributionChannelName", distributionChannel.Name);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    distributionChannelId = (int)_dataAccess.Reader["DistributionChannelId"];
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

            return distributionChannelId;
        }
    }
}
