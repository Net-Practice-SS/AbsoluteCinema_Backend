using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Domain.Interfaces;

namespace AbsoluteCinema.Domain.Strategies
{
    public class TicketStrategy : IEntityStrategy<Ticket>
    {
        private readonly int? _sessionId;
        private readonly int? _userId;
        private readonly int? _row;
        private readonly int? _place;
        private readonly int? _statusId;
        private readonly double? _price;

        public TicketStrategy(
            int? sessionId = null!, 
            int? userId = null!, 
            int? row = null!, 
            int? place = null!, 
            int? statusId = null!, 
            double? price = null!)
        {
            _sessionId = sessionId;
            _userId = userId;
            _row = row;
            _place = place;
            _statusId = statusId;
            _price = price;
        }

        public IQueryable<Ticket> ApplyFilter(IQueryable<Ticket> query)
        {
            if (_sessionId.HasValue)
            {
                query = query.Where(t => t.SessionId == _sessionId.Value);
            }
            if (_userId.HasValue)
            {
                query = query.Where(t => t.UserId == _userId.Value);
            }
            if (_row.HasValue)
            {
                query = query.Where(t => t.Row == _row.Value);
            }
            if (_place.HasValue)
            {
                query = query.Where(t => t.Place == _place.Value);
            }
            if (_statusId.HasValue)
            {
                query = query.Where(t => t.StatusId == _statusId.Value);
            }
            if (_price.HasValue)
            {
                query = query.Where(t => t.Price == _price.Value);
            }

            return query;
        }
    }
}
