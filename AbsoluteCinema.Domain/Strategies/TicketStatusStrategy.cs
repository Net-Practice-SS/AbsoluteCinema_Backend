using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Domain.Interfaces;

namespace AbsoluteCinema.Domain.Strategies
{
    public class TicketStatusStrategy : IEntityStrategy<TicketStatus>
    {
        private readonly string _name;

        public TicketStatusStrategy(string name = null!)
        {
            _name = name;
        }

        public IQueryable<TicketStatus> ApplyFilter(IQueryable<TicketStatus> query)
        {
            if(!string.IsNullOrWhiteSpace(_name))
            {
                query = query.Where(ts => ts.Name.Contains(_name));
            }

            return query;
        }
    }
}
