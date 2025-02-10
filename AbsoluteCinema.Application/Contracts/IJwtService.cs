using System.Security.Claims;

namespace AbsoluteCinema.Application.Contracts {
    public interface IJwtService {
        public string CreateJWTToken(IEnumerable<Claim> claims);
        public IEnumerable<Claim> GetClaims(string email);
    }
}
