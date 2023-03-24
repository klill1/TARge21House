namespace TARge21House.Core.Dto
{
    public class HouseDto
    {
        public Guid Id { get; set; }
        public int Size { get; set; }
        public int RoomCount { get; set; }
        public int Floors { get; set; }
        public string Color { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
