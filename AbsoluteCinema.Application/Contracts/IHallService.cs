using AbsoluteCinema.Application.DTO.Entities;
using AbsoluteCinema.Application.DTO.HallsDTO;

namespace AbsoluteCinema.Application.Contracts
{
    public interface IHallService
    {
        Task<IEnumerable<HallDto>> GetAllHallsAsync(GetAllHallDto getAllHallDto);
        Task<HallDto?> GetHallByIdAsync(int id);
        Task DeleteHallAsync(int id);
        Task UpdateHallAsync(UpdateHallDto updateHallDto);
        Task<int> CreateHallAsync(CreateHallDto createHallDto);
        Task<IEnumerable<HallDto>> GetHallWithStrategyAsync(GetHallWithStrategyDto getHallWithStrategyDto);
    }
}
