
namespace WinFormsApp1.Models
{
    public class Building
    {
        public int BuildingId { get; set; }
        public string BuildingName { get; set; } = null!;
        public virtual ICollection<Room> Rooms { get; set; } = null!;
        public virtual Conferention Conferention { get; set; }
    }
}
