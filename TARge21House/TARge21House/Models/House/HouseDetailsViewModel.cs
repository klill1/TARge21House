namespace TARge21House.Models.House
{
    public class HouseDetailsViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public int RoomCount { get; set; }
        public int Floors { get; set; }
        public string Color { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
