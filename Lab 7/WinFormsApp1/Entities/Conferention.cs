
namespace WinFormsApp1.Models
{
    public class Conferention
    {
        public int ConferentionId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual Building Building { get; set; } = null!;
        public int BuildingId { get; set; }
        public virtual ICollection<Section> Sections { get; set; } = null!;
    }
}
