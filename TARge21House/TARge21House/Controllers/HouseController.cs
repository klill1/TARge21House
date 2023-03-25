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
    }
}
