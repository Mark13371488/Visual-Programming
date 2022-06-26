namespace WinFormsApp1.Models
{
    public class Performance
    {
        public int PerformanceId { get; set; }
        public int Index { get; set; }
        public string Theme { get; set; } = null!;
        public virtual Performancer Performancer { get; set; } = null!;
        public int PerformancerId { get; set; }
        public virtual ICollection<Equipment> Equipment { get; set; }
        public virtual Section Section { get; set; } = null!;
        public int SectionId { get; set; }
        public DateTime StartOfPerformance { get; set; }
        public int Durability { get; set; }
    }
}