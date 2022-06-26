using WinFormsApp1.Models;
using System.Media;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        static int ctr = 0;
        private readonly ConferentionContext conferentionContext;
        Role role;
        public Form1(Role role)
        {
            this.role = role;   
            conferentionContext = new ConferentionContext();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (role == Role.User)
            {
                CreateButton.Visible = false;
                DeleteButton.Visible = false;
                UpdateButton.Visible = false;
            }
            RefreshData();
        }
        public void RefreshData()
        {
            listView2.Items.Clear();
            dataGridView1.DataSource = conferentionContext.Buildings.ToList();
            dataGridView2.DataSource = conferentionContext.Rooms.ToList();
            dataGridView3.DataSource = conferentionContext.Conferentions.ToList();
            dataGridView4.DataSource = conferentionContext.Sections.ToList();
            dataGridView5.DataSource = conferentionContext.Performances.ToList();
            dataGridView6.DataSource = conferentionContext.Performancers.ToList();
            dataGridView7.DataSource = conferentionContext.Equipment.ToList();
            RefreshEquipment();
        }
        private void RefreshEquipment()
        {
            var equipment = conferentionContext.Equipment;
            foreach (var eq in equipment.ToList())
            {
                if (eq.Performance != null)
                {
                    foreach (var performance in eq.Performance.ToList())
                    {
                        ListViewItem item = new ListViewItem(eq.Name);
                        item.SubItems.Add(performance.Section.Room.RoomNumber.ToString());
                        item.SubItems.Add(performance.StartOfPerformance.ToString());
                        listView2.Items.Add(item);
                    }
                }
                
            }
        }
        private void CreateButton_Click(object sender, EventArgs e)
        {
            try
            {
                new CreateForm(tabControl1.SelectedTab.Text, conferentionContext).ShowDialog();
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedTab.Text)
            {
                case "Buildings":
                    DeleteBuilding();
                    break;
                case "Rooms":
                    DeleteRoom();
                    break;
                case "Conferentions":
                    DeleteConferention();
                    break;
                case "Sections":
                    DeleteSection();
                    break;
                case "Performances":
                    DeletePerformance();
                    break;
                case "Performancers":
                    DeletePerformancer();
                    break;
                case "Equipment":
                    DeleteEquipment();
                    break;
                default:
                    MessageBox.Show("Something went wrong");
                    break;
            }
        }
        private void DeleteBuilding()
        {
            string deletingItems = string.Empty;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    int id = int.Parse(row.Cells[0].Value.ToString());
                    var building = conferentionContext.Buildings.FirstOrDefault(x => x.BuildingId == id);
                    //deletingItems += IEnumerableToString(building.Rooms);
                    conferentionContext.Buildings.Remove(building);
                }
                conferentionContext.SaveChanges();
                RefreshData();
            }
            else
            {
                MessageBox.Show("Please select a row");
            }
        }
        private void DeleteRoom()
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                {
                    int id = int.Parse(row.Cells[0].Value.ToString());
                    var room = conferentionContext.Rooms.FirstOrDefault(x => x.RoomId == id);
                    conferentionContext.Rooms.Remove(room);
                }
                conferentionContext.SaveChanges();
                RefreshData();
            }
            else
            {
                MessageBox.Show("Please select a row");
            }
        }
        private void DeleteConferention()
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView3.SelectedRows)
                {
                    int id = int.Parse(row.Cells[0].Value.ToString());
                    var conferention = conferentionContext.Conferentions.FirstOrDefault(x => x.ConferentionId == id);
                    conferentionContext.Conferentions.Remove(conferention);
                }
                conferentionContext.SaveChanges();
                RefreshData();
            }
            else
            {
                MessageBox.Show("Please select a row");
            }
        }
        private void DeleteSection()
        {
            if (dataGridView4.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView4.SelectedRows)
                {
                    int id = int.Parse(row.Cells[0].Value.ToString());
                    var section = conferentionContext.Sections.FirstOrDefault(x => x.SectionId == id);
                    conferentionContext.Sections.Remove(section);
                }
                conferentionContext.SaveChanges();
                RefreshData();
            }
            else
            {
                MessageBox.Show("Please select a row");
            }
        }
        private void DeletePerformance()
        {
            if (dataGridView5.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView5.SelectedRows)
                {
                    int id = int.Parse(row.Cells[0].Value.ToString());
                    var performance = conferentionContext.Performances.FirstOrDefault(x => x.PerformanceId == id);
                    conferentionContext.Performances.Remove(performance);
                }
                conferentionContext.SaveChanges();
                RefreshData();
            }
            else
            {
                MessageBox.Show("Please select a row");
            }
        }
        private void DeletePerformancer()
        {
            if (dataGridView6.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView6.SelectedRows)
                {
                    int id = int.Parse(row.Cells[0].Value.ToString());
                    var performancer = conferentionContext.Performancers.FirstOrDefault(x => x.PerformancerId == id);
                    conferentionContext.Performancers.Remove(performancer);
                }
                conferentionContext.SaveChanges();
                RefreshData();
            }
            else
            {
                MessageBox.Show("Please select a row");
            }
        }
        private void DeleteEquipment()
        {
            if (dataGridView7.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView7.SelectedRows)
                {
                    int id = int.Parse(row.Cells[0].Value.ToString());
                    var equipment = conferentionContext.Equipment.FirstOrDefault(x => x.EquipmentId == id);
                    conferentionContext.Equipment.Remove(equipment);
                }
                conferentionContext.SaveChanges();
                RefreshData();
            }
            else
            {
                MessageBox.Show("Please select a row");
            }
        }
        public void EditBuilding()
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int id = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                var building = conferentionContext.Buildings.Find(id);
                new EditForm(tabControl1.SelectedTab.Text, conferentionContext, building).ShowDialog();
                RefreshData();
            }
            else
            {
                MessageBox.Show("Select only one row to edit");
            }
        }
        public void EditRoom()
        {
            if (dataGridView2.SelectedRows.Count == 1)
            {
                int id = int.Parse(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                var room = conferentionContext.Rooms.Find(id);
                new EditForm(tabControl1.SelectedTab.Text, conferentionContext, room).ShowDialog();
            }
            else
            {
                MessageBox.Show("Select only one row to edit");
            }
        }
        public void EditConferention()
        {
            if (dataGridView3.SelectedRows.Count == 1)
            {
                int id = int.Parse(dataGridView3.SelectedRows[0].Cells[0].Value.ToString());
                var conferention = conferentionContext.Conferentions.Find(id);
                new EditForm(tabControl1.SelectedTab.Text, conferentionContext, conferention).ShowDialog();
                RefreshData();
            }
            else
            {
                MessageBox.Show("Select only one row to edit");
            }
        }
        public void EditSection()
        {
            if (dataGridView4.SelectedRows.Count == 1)
            {
                int id = int.Parse(dataGridView4.SelectedRows[0].Cells[0].Value.ToString());
                var section = conferentionContext.Sections.Find(id);
                new EditForm(tabControl1.SelectedTab.Text, conferentionContext, section).ShowDialog();
                RefreshData();
            }
            else
            {
                MessageBox.Show("Select only one row to edit");
            }
        }
        public void EditPerformance()
        {
            if (dataGridView5.SelectedRows.Count == 1)
            {
                int id = int.Parse(dataGridView5.SelectedRows[0].Cells[0].Value.ToString());
                var performance = conferentionContext.Performances.Find(id);
                new EditForm(tabControl1.SelectedTab.Text, conferentionContext, performance).ShowDialog();
                RefreshData();
            }
            else
            {
                MessageBox.Show("Select only one row to edit");
            }
        }
        public void EditPerformancer()
        {
            if (dataGridView6.SelectedRows.Count == 1)
            {
                int id = int.Parse(dataGridView6.SelectedRows[0].Cells[0].Value.ToString());
                var performancer = conferentionContext.Performancers.Find(id);
                new EditForm(tabControl1.SelectedTab.Text, conferentionContext, performancer).ShowDialog();
                RefreshData();
            }
            else
            {
                MessageBox.Show("Select only one row to edit");
            }
        }
        public void EditEquipment()
        {
            if (dataGridView7.SelectedRows.Count == 1)
            {
                int id = int.Parse(dataGridView7.SelectedRows[0].Cells[0].Value.ToString());
                var equipment = conferentionContext.Equipment.Find(id);
                new EditForm(tabControl1.SelectedTab.Text, conferentionContext, equipment).ShowDialog();
                RefreshData();
            }
            else
            {
                MessageBox.Show("Select only one row to edit");
            }
        }
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedTab.Text)
            {
                case "Buildings":
                    EditBuilding();
                    break;
                case "Rooms":
                    EditRoom();
                    break;
                case "Conferentions":
                    EditConferention();
                    break;
                case "Sections":
                    EditSection();
                    break;
                case "Performances":
                    EditPerformance();
                    break;
                case "Performancers":
                    EditPerformancer();
                    break;
                case "Equipment":
                    EditEquipment();
                    break;
                default:
                    MessageBox.Show("Something went wrong");
                    break;
            }
        }
        private void RefreshButton_Click(object sender, EventArgs e)
        {
            RefreshData();
        }
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                List<Performancer> performancers = new List<Performancer>(); 
                var conf = conferentionContext.Conferentions
                    .Find(int.Parse(dataGridView3.SelectedRows[0].Cells[0].Value.ToString()));
                foreach (var section in conf.Sections.ToList())
                {
                    foreach(var performance in section.Performances.ToList())
                    {
                        if (!performancers.Contains(performance.Performancer))
                        {
                            performancers.Add(performance.Performancer);
                        }
                    }
                }
                dataGridView8.DataSource = performancers;
            }
            catch {}
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            TabPage tab = (sender as TabControl).SelectedTab;
            if (tab.Text == "Conferentions")
            {
                getInfoButton.Visible = true;
            }
            else
            {
                getInfoButton.Visible = false;
            }
        }

        private void getInfoButton_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows == null || dataGridView3.SelectedRows.Count > 1)
            {
                MessageBox.Show("Select one conferention");
            }
            else
            {
                var id = int.Parse(dataGridView3.SelectedRows[0].Cells[0].Value.ToString());
                var conferention = conferentionContext.Conferentions.Find(id);
                new InfoForm(conferention).ShowDialog();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            SoundPlayer sound = new SoundPlayer();
            if (checkBox2.Checked)
            {
                if (ctr%2 == 0)
                {
                    sound.SoundLocation = @"C:\Users\User\source\repos\WinFormsApp1\WinFormsApp1\Music\music1.wav";
                }
                else
                {
                    sound.SoundLocation = @"C:\Users\User\source\repos\WinFormsApp1\WinFormsApp1\Music\music2.wav";
                }
                checkBox2.Text = "Stop";
                sound.Play();
                ctr++;
            }
            else
            {
                checkBox2.Text = "Play";
                sound.Stop();
            }
        }
    }
}