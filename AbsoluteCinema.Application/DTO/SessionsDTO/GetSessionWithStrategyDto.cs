using AbsoluteCinema.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsoluteCinema.Application.DTO.AuthDTO.SessionsDTO
{
    public class GetSessionWithStrategyDto
    {
        public int? MovieId { get; set; } = null!;
        public DateTime? Date { get; set; } = null!;
        public int? HallId { get; set; } = null!;

        public string OrderByProperty { get; set; } = "Id";
        public string OrderDirection { get; set; } = "asc";
    }
}
