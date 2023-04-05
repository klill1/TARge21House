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
            var newHouse = await Svc<IHouseServices>().Create(houseDto);

            var result = await Svc<IHouseServices>().GetAsync((Guid)newHouse.Id);
            Assert.Equal(newHouse, result);
        }

        [Fact]
        public async Task Should_UpdateValidHouse_WhenUpdateData()
        {
            HouseDto houseDto = CreateHouse();
            var newHouse = await Svc<IHouseServices>().Create(houseDto);

            HouseDto update = UpdateHouse();        
            var result = await Svc<IHouseServices>().Update(update);

            Assert.Equal(update.Id, houseDto.Id);
            Assert.DoesNotMatch(newHouse.Name, result.Name);
            Assert.NotEqual(newHouse.Size, result.Size);
            Assert.NotEqual(newHouse.RoomCount, result.RoomCount);
            Assert.NotEqual(newHouse.Floors, result.Floors);
            Assert.DoesNotMatch(newHouse.Color, result.Color);
            Assert.NotEqual(newHouse.ModifiedAt, result.ModifiedAt);
        }

        [Fact]
        public async Task ShouldBeDeleted_WhenIdIsFound()
        {
            HouseDto houseDto = CreateHouse();
            var newHouse = await Svc<IHouseServices>().Create(houseDto);

            var result = await Svc<IHouseServices>().Delete((Guid)newHouse.Id);
            Assert.Equal(newHouse, result);
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

        private HouseDto UpdateHouse()
        {
            HouseDto update = new()
            {
                Name = "Kong",
                Size = 5,
                RoomCount = 2,
                Floors = 1,
                Color = "yellow",
            };

            return update;
        }
    }
}