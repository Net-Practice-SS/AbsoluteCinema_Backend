using AbsoluteCinema.Application.DTO.EntityDTO.Abstract;
using AbsoluteCinema.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsoluteCinema.Application.DTO.EntityDTO.SessionsDTO
{
    public class GetSessionWithStrategyDto : GetDto
    {
        public int? MovieId { get; set; }
        public DateTime? DateFrom { get; set; } = null!;
        public DateTime? DateTo { get; set; } = null!;
        public int? HallId { get; set; }
    }
}
