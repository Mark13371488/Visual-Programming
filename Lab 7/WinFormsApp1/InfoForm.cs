
using WinFormsApp1.Models;

namespace WinFormsApp1
{
    public partial class InfoForm : Form
    {
        private readonly Conferention conferention;
        public InfoForm(Conferention conferention)
        {
            this.conferention = conferention;
            InitializeComponent();
        }

        private void InfoForm_Load(object sender, EventArgs e)
        {
            label4.Text = conferention.Name;
            InfoToView(conferention.Sections);
        }
        public void InfoToView(ICollection<Section> sections)
        {
            foreach (var section in sections.ToList())
            {
                ListViewItem item = new ListViewItem(section.Number.ToString());
                item.SubItems.Add(section.Name.ToString());
                item.SubItems.Add(section.RoomId.ToString());
                listView1.Items.Add(item);
                if (section.Performances != null)
                {
                    foreach (var performance in section.Performances.ToList())
                    {
                        ListViewItem item1 = new ListViewItem(performance.Theme);
                        item1.SubItems.Add(performance.StartOfPerformance.ToString());
                        item1.SubItems.Add(performance.SectionId.ToString());
                        listView2.Items.Add(item1);
                        ListViewItem item2 = new ListViewItem(performance.Performancer.PerformancerId.ToString());
                        item2.SubItems.Add(performance.Performancer.ProBiography.ToString());
                        if (listView3.FindItemWithText(item2.SubItems[0].Text) == null)
                        {
                            listView3.Items.Add(item2);
                        }
                    }
                }             
            }
        }
    }
}
