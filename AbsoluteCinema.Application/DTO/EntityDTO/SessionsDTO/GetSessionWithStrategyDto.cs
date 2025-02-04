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
        public int? MovieId { get; set; } = null!;
        public DateTime? Date { get; set; } = null!;
        public int? HallId { get; set; } = null!;
    }
}
