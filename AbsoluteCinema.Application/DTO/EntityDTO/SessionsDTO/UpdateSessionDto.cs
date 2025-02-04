using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsoluteCinema.Application.DTO.EntityDTO.SessionsDTO
{
    public class UpdateSessionDto
    {
        public int Id { get; set; }
        public int? MovieId { get; set; }
        public DateTime? Date { get; set; }
        public int? HallId { get; set; }
    }
}
