use ecommerce_net_db

go

------------
-- ORDERS --
------------

create or alter function FN_Get_Status_Index(
	@DistributionChannelId int,
	@OrderStatusId int
)
returns int
as
begin
	declare @OrderStatusIndex int;

	select @OrderStatusIndex = CS.OrderStatusIndex
	from OrderStatuses S
	inner join ChannelStatuses CS on CS.OrderStatusId = S.OrderStatusId
	inner join DistributionChannels C on C.DistributionChannelId = CS.DistributionChannelId
	where C. DistributionChannelId = @DistributionChannelId and S.OrderStatusId = @OrderStatusId

	if (@OrderStatusIndex is null)
	begin
		return -1;
	end

	return @OrderStatusIndex;
end

go