using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsoluteCinema.Domain.Entities
{
    public class Placement
    {
        public int Id { get; set; }
        public int Place { get; set; }
        public int Row { get; set; }
        public double Price { get; set; }

        public ICollection<Ticket> Ticket { get; set; }
    }
}
