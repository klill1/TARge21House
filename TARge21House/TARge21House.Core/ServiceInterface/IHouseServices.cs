using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TARge21House.Core.Domain;
using TARge21House.Core.Dto;

namespace TARge21House.Core.ServiceInterface
{
    public interface IHouseServices
    {
        Task<House> Create(HouseDto dto);
        Task<House> GetAsync(Guid id);
        Task<House> Update(HouseDto dto);
    }
}
