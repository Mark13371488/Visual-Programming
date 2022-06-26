namespace WinFormsApp1.Models
{
    public class Performancer
    {
        public int PerformancerId { get; set; }
        public string ScienceDegree { get; set; } = null!;
        public string Workplace { get; set; } = null!;
        public string Position { get; set; } = null!;
        public string ProBiography { get; set; } = null!;
        public virtual ICollection<Performance> Performances { get; set; }
    }
}