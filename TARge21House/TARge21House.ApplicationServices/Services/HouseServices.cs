using TARge21House.Core.Domain;
using TARge21House.Core.Dto;
using TARge21House.Core.ServiceInterface;
using TARge21House.Data;

namespace TARge21House.ApplicationServices.Services
{
    public class HouseServices : IHouseServices
    {
        private readonly TARge21HouseContext _context;

        public HouseServices
        (
        TARge21HouseContext context
        )
        {
            _context = context;
        }

        public async Task<House> Create(HouseDto dto)
        {
            House house = new House();

            house.Id = Guid.NewGuid();
            house.Name = dto.Name;
            house.Size = dto.Size;
            house.RoomCount = dto.RoomCount;
            house.Floors = dto.Floors;
            house.Color = dto.Color;
            house.CreatedAt = DateTime.Now;
            house.ModifiedAt = DateTime.Now;

            await _context.Houses.AddAsync(house);
            await _context.SaveChangesAsync();

            return house;
        }
    }

    



    
}
