using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AbsoluteCinema.Application.Contracts;
using Microsoft.Extensions.Configuration;

namespace AbsoluteCinema.Application.Services {
    public interface  IJwtService {

        public string CreateJWTToken(IEnumerable<Claim> claims);
        public IEnumerable<Claim> GetClaims(string email);

    }
}
