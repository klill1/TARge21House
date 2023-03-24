using Microsoft.AspNetCore.Mvc;
using TARge21House.Data;
using TARge21House.Models.House;

namespace TARge21House.Controllers
{
    public class HouseController : Controller
    {
        private readonly TARge21HouseContext _context;

        public HouseController
            (
                TARge21HouseContext context
            )
        {
            _context = context;
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
    }
}
