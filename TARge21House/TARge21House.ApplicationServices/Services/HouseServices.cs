﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<House> GetAsync(Guid id)
        {
            var result = await _context.Houses
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<House> Update(HouseDto dto)
        {
            var domain = new House()
            {
                Id = dto.Id,
                Name = dto.Name,
                Size = dto.Size,
                RoomCount = dto.RoomCount,
                Floors = dto.Floors,
                Color = dto.Color,
                CreatedAt = dto.CreatedAt,
                ModifiedAt = DateTime.Now
            };

            _context.Houses.Update(domain);
            await _context.SaveChangesAsync();

            return domain;
        }

        public async Task<House> Delete(Guid id)
        {
            var houseId = await _context.Houses
                .FirstOrDefaultAsync(x => x.Id == id);

            _context.Houses.Remove(houseId);
            await _context.SaveChangesAsync();

            return houseId;
        }
    }
}
