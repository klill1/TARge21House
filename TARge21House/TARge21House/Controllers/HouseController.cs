using Microsoft.AspNetCore.Mvc;
using TARge21House.Core.Dto;
using TARge21House.Core.ServiceInterface;
using TARge21House.Data;
using TARge21House.Models.House;

namespace TARge21House.Controllers
{
    public class HouseController : Controller
    {
        private readonly TARge21HouseContext _context;
        private readonly IHouseServices _houseServices;

        public HouseController
            (
                TARge21HouseContext context,
                IHouseServices houseServices
            )
        {
            _context = context;
            _houseServices = houseServices;
        }

        public IActionResult Index()
        {
            var result = _context.Houses
                .OrderByDescending(y => y.CreatedAt)
                .Select(x => new HouseIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Size = x.Size,
                    RoomCount = x.RoomCount,
                    Floors = x.Floors,
                    Color = x.Color
                });
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            HouseCreateUpdateViewModel house = new HouseCreateUpdateViewModel();

            return View("CreateUpdate", house);
        }

        [HttpPost]
        public async Task<IActionResult> Create(HouseCreateUpdateViewModel vm)
        {
            var dto = new HouseDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                Size = vm.Size,
                RoomCount = vm.RoomCount,
                Floors = vm.Floors,
                Color = vm.Color,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt
            };

            var result = await _houseServices.Create(dto);

            if (result is null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var house = await _houseServices.GetAsync(id);
            if (house is null)
            {
                return NotFound();
            }

            var vm = new HouseCreateUpdateViewModel();

            vm.Id = house.Id;
            vm.Name = house.Name;
            vm.Size = house.Size;
            vm.RoomCount = house.RoomCount;
            vm.Floors = house.Floors;
            vm.Color = house.Color;
            vm.CreatedAt = house.CreatedAt;
            vm.ModifiedAt = house.ModifiedAt;

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(HouseCreateUpdateViewModel vm)
        {
            var dto = new HouseDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                Size = vm.Size,
                RoomCount = vm.RoomCount,
                Floors = vm.Floors,
                Color = vm.Color,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt
            };

            var result = await _houseServices.Update(dto);

            if(result is null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var house = await _houseServices.GetAsync(id);
            if (house is null)
            {
                return NotFound();
            }

            var vm = new HouseDetailsViewModel();

            vm.Id = house.Id;
            vm.Name = house.Name;
            vm.Size = house.Size;
            vm.RoomCount = house.RoomCount;
            vm.Floors = house.Floors;
            vm.Color = house.Color;
            vm.CreatedAt = house.CreatedAt;
            vm.ModifiedAt = house.ModifiedAt;

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var house = await _houseServices.GetAsync(id);
            if (house is null)
            {
                return NotFound();
            }

            var vm = new HouseDeleteViewModel();

            vm.Id = house.Id;
            vm.Name = house.Name;
            vm.Size = house.Size;
            vm.RoomCount = house.RoomCount;
            vm.Floors = house.Floors;
            vm.Color = house.Color;
            vm.CreatedAt = house.CreatedAt;
            vm.ModifiedAt = house.ModifiedAt;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var houseId = await _houseServices.Delete(id);
            if (houseId is null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
