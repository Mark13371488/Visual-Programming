
namespace WinFormsApp1.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public int RoomNumber { get; set; }
        public virtual Building Building { get; set; } = null!;
        public int BuildingId { get; set; }
        public virtual Section Section { get; set; }
    }
}
