
namespace WinFormsApp1.Models
{
    public class Section
    {
        public int SectionId { get; set; }
        public string Name { get; set; } = null!;
        public uint Number { get; set; }
        public string NameOfHead { get; set; } = null!;
        public virtual Room Room { get; set; }
        public int? RoomId { get; set; }
        public virtual ICollection<Performance> Performances { get; set; } = null!;
        public virtual Conferention Conferention { get; set; } = null!; 
        public int ConferentionId { get; set; }
    }
}
