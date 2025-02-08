using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AbsoluteCinema.Application.DTO.AuthDTO;
using AbsoluteCinema.Domain.Entities.Interfaces;

namespace AbsoluteCinema.Application.Contracts {
    public interface IJwtService {
        public string CreateJWTToken(IEnumerable<Claim> claims);
        public IEnumerable<Claim> GetClaims(string email);
    }
}
