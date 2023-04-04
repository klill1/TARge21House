using TARge21House.Core.Dto;
using TARge21House.Core.ServiceInterface;
using Xunit;

namespace TARge21House.HouseTest
{
    public class HouseTest : TestBase
    {
        [Fact]
        public async Task Should_CreateHouse()
        {
            HouseDto houseDto = CreateHouse();

            var result = await Svc<IHouseServices>().Create(houseDto);
            Assert.NotNull(result.Id);
            Assert.Equal(houseDto.Name, result.Name);
            Assert.Equal(houseDto.Size, result.Size);
            Assert.Equal(houseDto.RoomCount, result.RoomCount);
            Assert.Equal(houseDto.Floors, result.Floors);
            Assert.Equal(houseDto.Color, result.Color);
            Assert.True(DateTime.Now > result.CreatedAt);
            Assert.True(DateTime.Now > result.ModifiedAt);
        }

        [Fact]
        public async Task Should_GetAValidId_WithGetAsync()
        {
            HouseDto houseDto = CreateHouse();
            var createHouse = await Svc<IHouseServices>().Create(houseDto);

            var result = await Svc<IHouseServices>().GetAsync((Guid)createHouse.Id);
            Assert.Equal(createHouse, result);
        }

        
        private HouseDto CreateHouse()
        {
            HouseDto houseDto = new();
            houseDto.Name = "Onn";
            houseDto.Size = 100;
            houseDto.RoomCount = 5;
            houseDto.Floors = 3;
            houseDto.Color = "blue";

            return houseDto;
        }
    }
}