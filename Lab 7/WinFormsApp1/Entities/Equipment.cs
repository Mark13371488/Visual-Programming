
namespace WinFormsApp1.Models
{
    public class Equipment
    {
        public int EquipmentId { get; set; }
        public string Name { get; set; } = null!;
        public virtual ICollection<Performance> Performance { get; set; }
    }
}
