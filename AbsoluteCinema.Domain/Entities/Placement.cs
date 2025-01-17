using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsoluteCinema.Domain.Entities
{
    internal class Placement
    {
        public int Id { get; set; }
        public int Place { get; set; }
        public int Row { get; set; }
        public double Prices { get; set; }
    }
}
