using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsoluteCinema.Domain.Entities
{
    public class TicketStatus
    {
        public int Id { get; set; } 
        public string Name { get; set; }

        public ICollection<Ticket> Ticket { get; set; }
    }
}
