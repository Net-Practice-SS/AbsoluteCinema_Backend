using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Domain.Interfaces;

namespace AbsoluteCinema.Domain.Strategies
{
    public class HallStrategy : IEntityStrategy<Hall>
    {
        private readonly string? _name;
        private readonly int? _rowCount;
        private readonly int? _placeCount;

        public HallStrategy(
            string? name = null!,
            int? rowCount = null!,
            int? placeCount = null!)
        {
            _name = name;
            _rowCount = rowCount;
            _placeCount = placeCount;
        }

        public IQueryable<Hall> ApplyFilter(IQueryable<Hall> query)
        {
            if (!string.IsNullOrWhiteSpace(_name))
            {
                query = query.Where(h => h.Name.Contains(_name));
            }
            if (_rowCount.HasValue)
            {
                query = query.Where(h => h.RowCount == _rowCount.Value);
            }
            if (_placeCount.HasValue)
            {
                query = query.Where(h => h.PlaceCount == _placeCount.Value);
            }

            return query;
        }
    }
}
