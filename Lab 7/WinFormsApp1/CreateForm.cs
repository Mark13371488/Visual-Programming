using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Models;

namespace WinFormsApp1
{
    public partial class CreateForm : Form
    {
        private readonly ConferentionContext db;
        private readonly string tabName;

        private CheckedListBox checkedList = null!;
        private List<TextBox> boxes = null!;
        private List<DateTimePicker> dates = null!;
        private List<ComboBox> combos = null!;
        private List<NumericUpDown> numericUpDowns = null!;
        private Button button = null!;
        public CreateForm(string tabName,ConferentionContext db)
        {
            InitializeComponent();
            this.tabName = tabName;
            this.db = db;
        }

        private void CreateForm_Load(object sender, EventArgs e)
        {
            switch (tabName)
            {
                case "Buildings":
                    AddBuildingBoxes();
                    break;
                case "Rooms":
                    AddRoomBoxes();
                    break;
                case "Conferentions":
                    AddConferentionBoxes();
                    break;
                case "Sections":
                    AddSectionBoxes();
                    break;
                case "Performances":
                    AddPerformanceBoxes();
                    break;
                case "Performancers":
                    AddPerformancerBoxes();
                    break;
                case "Equipment":
                    AddEquipmentBoxes();
                    break;
                default:
                    MessageBox.Show("Something went wrong");
                    this.Close();
                    break;
            }
        }
        private void AddBuildingBoxes()
        {
            List<Label> labels = new List<Label>
            {
                new Label
                {
                    Text = "Building name",
                    Location = new Point(150, 70),
                    Size = new Size(180,25)
                }
            };
            this.Controls.AddRange(labels.ToArray());
            boxes = new List<TextBox>
            {
                new TextBox
                {
                    Name = "BuildingName",
                    Location = new Point(90, 100),
                    Size = new Size(180, 25)
                }
            };
            this.Controls.AddRange(boxes.ToArray());
            button = new Button
            {
                Location = new Point(90, 150),
                Size = new Size(180, 50),
                Text = "Add",
                FlatStyle = FlatStyle.Popup,
                BackColor = Color.Khaki,
            };
            button.Click += new EventHandler(button_Click);
            this.Controls.Add(button);
        }

        private void button_Click(object sender, EventArgs e)
        {
            switch (tabName)
            {
                case "Buildings":
                    AddBuildingToDb();
                    break;
                case "Rooms":
                    AddRoomToDb();
                    break;
                case "Conferentions":
                    AddConferentionToDb();
                    break;
                case "Sections":
                    AddSectionToDb();
                    break;
                case "Performances":
                    AddPerformanceToDb();
                    break;
                case "Performancers":
                    AddPerformancerToDb();
                    break;
                case "Equipment":
                    AddEquipmentToDb();
                    break;
                default:
                    throw new InvalidOperationException("Something went wrong");
            }
        }

        private void AddRoomBoxes()
        {
            List<Label> labels = new List<Label>
            {
                new Label
                {
                    Text = "Building id",
                    Location = new Point(150, 70),
                    Size = new Size(180,25)
                },
                new Label
                {
                    Text = "Room number",
                    Location = new Point(150, 170),
                    Size = new Size(180,25)
                },
            };
            this.Controls.AddRange(labels.ToArray());
            combos = new List<ComboBox>
            {
                new ComboBox
                {
                    Name = "buid",
                    Location = new Point(90, 100),
                    Size = new Size(180, 25),
                    DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
                },
            };
            combos[0].Items.AddRange(QueryToString<int>(db.Buildings.Select(x => x.BuildingId)));
            this.Controls.AddRange(combos.ToArray());
            numericUpDowns = new List<NumericUpDown>
            {
                new NumericUpDown
                {
                    Name = "RoomNumber",
                    Location = new Point(90, 200),
                    Size = new Size(180, 25)
                }
            };
            this.Controls.AddRange(numericUpDowns.ToArray());
            button = new Button
            {
                Location = new Point(90, 250),
                Size = new Size(180, 50),
                Text = "Add",
                FlatStyle = FlatStyle.Popup,
                BackColor = Color.Khaki,

            };
            button.Click += new EventHandler(button_Click);
            this.Controls.Add(button);
        }

        private void AddConferentionBoxes()
        {
            List<Label> labels = new List<Label>
            {
                new Label
                {
                    Text = "Choose building",
                    Location = new Point(150, 70),
                    Size = new Size(180,25)
                },
                new Label
                {
                    Text = "Name",
                    Location = new Point(150, 170),
                    Size = new Size(180,25)
                },
                new Label
                {
                    Text = "Start date",
                    Location = new Point(150, 270),
                    Size = new Size(180,25)
                },
                new Label
                {
                    Text = "End date",
                    Location = new Point(150, 370),
                    Size = new Size(180,25)
                },
            };
            this.Controls.AddRange(labels.ToArray());
            combos = new List<ComboBox>
            {
                new ComboBox
                {
                    Name = "buid",
                    Location = new Point(90, 100),
                    Size = new Size(180, 25),
                    DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
                },
            };
            combos[0].Items.AddRange(QueryToString(db.Buildings.Where(x => x.Conferention == null).Select(y => y.BuildingId)));
            this.Controls.AddRange(combos.ToArray());
            boxes = new List<TextBox>
            {
                new TextBox
                {
                    Name = "Conferention name",
                    Location = new Point(90, 200),
                    Size = new Size(180, 25)
                }
            };
            this.Controls.AddRange(boxes.ToArray());
            dates = new List<DateTimePicker>
            {
                new DateTimePicker
                {
                    Format = DateTimePickerFormat.Custom,
                    CustomFormat = " MM'/'dd'/'yyyy hh':'mm",
                    Name = "Start",
                    Location = new Point(90, 300),
                    Size = new Size(180, 25),
                    MinDate = DateTime.Now
                },
                new DateTimePicker
                {
                    Format = DateTimePickerFormat.Custom,
                    CustomFormat = " MM'/'dd'/'yyyy hh':'mm",
                    Name = "End",
                    Location = new Point(90, 400),
                    Size = new Size(180, 25),
                    MinDate = DateTime.Now
                }
            };
            this.Controls.AddRange(dates.ToArray());
            button = new Button
            {
                Location = new Point(90, 450),
                Size = new Size(180, 50),
                Text = "Add",
                FlatStyle = FlatStyle.Popup,
                BackColor = Color.Khaki,
            };
            button.Click += new EventHandler(button_Click);
            this.Controls.Add(button);
        }
        private void AddSectionBoxes()
        {
            List<Label> labels = new List<Label>
            {
                new Label
                {
                    Text = "Choose conferention",
                    Location = new Point(150, 20),
                    Size = new Size(180,25)
                },
                new Label
                {
                    Text = "Choose room",
                    Location = new Point(150, 120),
                    Size = new Size(180,25)
                },
                new Label
                {
                    Text = "Section name",
                    Location = new Point(150, 220),
                    Size = new Size(180,25)
                },
                new Label
                {
                    Text = "Name of head",
                    Location = new Point(150, 320),
                    Size = new Size(180,25)
                },
            };
            this.Controls.AddRange(labels.ToArray());
            combos = new List<ComboBox>
            {
                new ComboBox
                {
                    Name = "Conf",
                    Location = new Point(90, 50),
                    Size = new Size(180, 25),
                    DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
                },
                new ComboBox
                {
                    Name = "Room",
                    Location = new Point(90, 150),
                    Size = new Size(180, 25),
                    DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
                },
            };
            combos[0].Items.AddRange(QueryToString<int>(db.Conferentions.Select(x => x.ConferentionId)));
            combos[0].SelectedIndexChanged += new EventHandler(Conf_changed);
            this.Controls.AddRange(combos.ToArray());
            boxes = new List<TextBox>
            {
                new TextBox
                {
                    Name = "Name",
                    Location = new Point(90, 250),
                    Size = new Size(180, 25)
                },

                new TextBox
                {
                    Name = "NameOfHead",
                    Location = new Point(90, 350),
                    Size = new Size(180, 25)
                }
            };
            this.Controls.AddRange(boxes.ToArray());
            button = new Button
            {
                Location = new Point(90, 400),
                Size = new Size(180, 50),
                Text = "Add",
                FlatStyle = FlatStyle.Popup,
                BackColor = Color.Khaki,
            };
            button.Click += new EventHandler(button_Click);
            this.Controls.Add(button);
        }

        private void AddPerformanceBoxes()
        {
            List<Label> labels = new List<Label>
            {
                new Label
                {
                    Text = "Choose section",
                    Location = new Point(140, 20),
                    Size = new Size(180,25)
                },
                new Label
                {
                    Text = "Choose performancer",
                    Location = new Point(130, 100),
                    Size = new Size(180,25)
                },
                new Label
                {
                    Text = "Theme",
                    Location = new Point(155, 180),
                    Size = new Size(180,25)
                },
                new Label
                {
                    Text = "Start of performance",
                    Location = new Point(130, 260),
                    Size = new Size(180,25)
                },
                new Label
                {
                    Text = "Durability",
                    Location = new Point(150, 340),
                    Size = new Size(180,25)
                },
            };
            this.Controls.AddRange(labels.ToArray());
            combos = new List<ComboBox>
            {
                new ComboBox
                {
                    Name = "Section",
                    Location = new Point(90, 50),
                    Size = new Size(180, 25),
                    DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
                },
                new ComboBox
                {
                    Name = "Performancer",
                    Location = new Point(90, 130),
                    Size = new Size(180, 25),
                    DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
                },
            };
            checkedList = new CheckedListBox
            {
                Location = new Point(90, 420),
                Size = new Size(180, 75),
            };
            checkedList.Items.AddRange(QueryToString(db.Equipment.Select(x => x.Name)));
            this.Controls.Add(checkedList);
            combos[0].Items.AddRange(QueryToString(db.Sections.Select(x => x.SectionId)));
            combos[1].Items.AddRange(QueryToString(db.Performancers.Select(x => x.PerformancerId)));
            this.Controls.AddRange(combos.ToArray());
            boxes = new List<TextBox>
            {
                new TextBox
                {
                    Name = "Theme",
                    Location = new Point(90, 210),
                    Size = new Size(180, 25)
                },
            };
            this.Controls.AddRange(boxes.ToArray());
            dates = new List<DateTimePicker>
            {
                new DateTimePicker
                {
                    Format = DateTimePickerFormat.Custom,
                    CustomFormat = " MM'/'dd'/'yyyy hh':'mm",
                    Name = "Start",
                    Location = new Point(90, 290),
                    Size = new Size(180, 25)
                }
            };
            this.Controls.AddRange(dates.ToArray());
            numericUpDowns = new List<NumericUpDown>
            {
                new NumericUpDown
                {
                    Name = "Durability",
                    Location = new Point(90,370),
                    Size = new Size(180,25)
                }
            };
            this.Controls.AddRange(numericUpDowns.ToArray());
            button = new Button
            {
                Location = new Point(90, 495),
                Size = new Size(180, 50),
                Text = "Add",
                FlatStyle = FlatStyle.Popup,
                BackColor = Color.Khaki,
            };
            button.Click += new EventHandler(button_Click);
            this.Controls.Add(button);
        }
        private void AddPerformancerBoxes()
        {
            List<Label> labels = new List<Label>
            {
                new Label
                {
                    Text = "Science degree",
                    Location = new Point(150, 20),
                    Size = new Size(180,25)
                },
                new Label
                {
                    Text = "Workplace",
                    Location = new Point(150, 120),
                    Size = new Size(180,25)
                },
                new Label
                {
                    Text = "Position",
                    Location = new Point(150, 220),
                    Size = new Size(180,25)
                },
                new Label
                {
                    Text = "Biography",
                    Location = new Point(150, 320),
                    Size = new Size(180,25)
                },
            };
            this.Controls.AddRange(labels.ToArray());
            boxes = new List<TextBox>
            {
                new TextBox
                {
                    Name = "ScienceDegree",
                    Location = new Point(90, 50),
                    Size = new Size(180, 25)
                },
                new TextBox
                {
                    Name = "Workplace",
                    Location = new Point(90, 150),
                    Size = new Size(180, 25)
                },
                new TextBox
                {
                    Name = "Position",
                    Location = new Point(90, 250),
                    Size = new Size(180, 25)
                },
                new TextBox
                {
                    Name = "Biography",
                    Location = new Point(90, 350),
                    Size = new Size(180, 25)
                }
            };
            this.Controls.AddRange(boxes.ToArray());

            button = new Button
            {
                Location = new Point(90, 400),
                Size = new Size(180, 50),
                Text = "Add",
                FlatStyle = FlatStyle.Popup,
                BackColor = Color.Khaki,
            };
            button.Click += new EventHandler(button_Click);
            this.Controls.Add(button);
        }
        private void AddEquipmentBoxes()
        {
            List<Label> labels = new List<Label>
            {
                new Label
                {
                    Text = "Equipment name",
                    Location = new Point(150, 70),
                    Size = new Size(180,25)
                }
            };
            this.Controls.AddRange(labels.ToArray());
            boxes = new List<TextBox>
            {
                new TextBox
                {
                    Name = "Name",
                    Location = new Point(90, 100),
                    Size = new Size(180, 25)
                }
            };
            this.Controls.AddRange(boxes.ToArray());
            button = new Button
            {
                Location = new Point(90, 150),
                Size = new Size(180, 50),
                Text = "Add",
                FlatStyle = FlatStyle.Popup,
                BackColor = Color.Khaki,
            };
            button.Click += new EventHandler(button_Click);
            this.Controls.Add(button);
        }
        private void Conf_changed(object sender, EventArgs e)
        {
            try
            {
                combos[1].Items.Clear();
                int selectedConf = int.Parse(combos[0].Text);
                var conf = db.Conferentions.SingleOrDefault(x => x.ConferentionId == selectedConf);
                var rooms = conf.Building.Rooms.Where(r => r.Section == null).Select(x => x.RoomId);
                combos[1].Items.AddRange(QueryToString<int>(rooms.AsQueryable()));  
            }
            catch { }
        }
        private void AddBuildingToDb()
        {
            if (ValidateBoxes())
            {
                var building = new Building()
                {
                    BuildingName = boxes[0].Text
                };
                db.Buildings.Add(building);
                db.SaveChanges();
                this.Close();
            }
            else
                MessageBox.Show("Enter all values");
        }
        private void AddRoomToDb()
        {
            if (ValidateCombos()&&ValidateNumeric())
            {
                var room = new Room()
                {
                    BuildingId = Convert.ToInt32(combos[0].Text),
                    RoomNumber = (int)numericUpDowns[0].Value
                };
                db.Rooms.Add(room);
                db.SaveChanges();
                this.Close();
            }
            else
                MessageBox.Show("Enter all values");
        }
        private void AddConferentionToDb()
        {
            if (ValidateCombos() && ValidateBoxes() && ValidateStartEnd())
            {
                var conferention = new Conferention()
                {
                    BuildingId = Convert.ToInt32(combos[0].Text),
                    Name = boxes[0].Text,
                    StartDate = dates[0].Value,
                    EndDate = dates[1].Value,
                };
                db.Conferentions.Add(conferention);
                db.SaveChanges();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid data");
            }
        }
        private void AddSectionToDb()
        {
            if (ValidateCombos() && ValidateBoxes())
            {
                var section = new Section()
                {
                    ConferentionId = Convert.ToInt32(combos[0].Text),
                    RoomId = Convert.ToInt32(combos[1].Text),
                    Name = boxes[0].Text,
                    Number = MaxSectionNumber(Convert.ToInt32(combos[0].Text)) +1,
                    NameOfHead = boxes[1].Text
                };
                db.Sections.Add(section);
                db.SaveChanges();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid data");
            }
        }
        private void AddPerformanceToDb()
        {
            int sectionId = Convert.ToInt32(combos[0].Text);
            int durability = (int)numericUpDowns[0].Value;
            List<int> ids = new List<int>();
            foreach (var eq in checkedList.CheckedItems)
            {
                ids.Add(db.Equipment.First(x => x.Name == eq.ToString()).EquipmentId);
            }
            if (ValidateCombos() && ValidateBoxes() 
                && IsPerformanceDateValid(sectionId, durability) 
                && IsPerformancerReady(Convert.ToInt32(combos[1].Text),sectionId, dates[0].Value))
            {
                var performance = new Performance()
                {
                    Index = MaxPerformanceIndex(Convert.ToInt32(combos[0].Text)) +1,
                    Theme = boxes[0].Text,
                    SectionId = Convert.ToInt32(combos[0].Text),
                    PerformancerId = Convert.ToInt32(combos[1].Text),
                    StartOfPerformance = dates[0].Value,
                    Durability = (int)numericUpDowns[0].Value,
                    Equipment = GetEquipment(ids).ToArray()
                };
                db.Performances.Add(performance);
                db.SaveChanges();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid data");
            }
        }
        private IEnumerable<Equipment> GetEquipment(List<int> ids)
        {
            foreach (var id in ids)
            {
                yield return db.Equipment.Find(id);
            }
        }  
        public bool IsPerformancerReady(int performancerId,int sectionId,DateTime dateTime)
        {
            List<Performance> performances = new List<Performance>();
            foreach (var section1 in db.Sections.ToList())
            {
                if (section1.Performances is null || section1.Performances.Count == 0 || section1.SectionId == sectionId)
                {
                    continue;
                }
                performances.AddRange(section1.Performances.Where(x => x.PerformancerId == performancerId));
            }
            if(performances != null && performances.Count != 0)
            {
                var last = performances.OrderByDescending(s => s.StartOfPerformance).First();
                return (dateTime.Date - last.StartOfPerformance.Date).Days > 0;
            }
            else return true;
        }
        private void AddPerformancerToDb()
        {
            if (ValidateBoxes())
            {
                var performancer = new Performancer()
                {
                    ScienceDegree = boxes[0].Text,
                    Workplace = boxes[1].Text,
                    Position = boxes[2].Text,
                    ProBiography = boxes[3].Text
                };
                db.Performancers.Add(performancer);
                db.SaveChanges();
                this.Close();
            }
            else
            {
                MessageBox.Show("Enter all values");
            }
        }
        private void AddEquipmentToDb()
        {
            if (ValidateBoxes())
            {
                var equipment = new Equipment()
                {
                    Name = boxes[0].Text
                };
                db.Equipment.Add(equipment);
                db.SaveChanges();
                this.Close();
            }
            else
            {
                MessageBox.Show("Enter all values");
            }
        }
        private int MaxPerformanceIndex(int sectionId)
        {
            var section = db.Sections.Find(sectionId);
            if (section.Performances is null || section.Performances.Count == 0)
            {
                return 0;
            }
            else
            {
                return section.Performances.Max(x => x.Index);
            }
        }
        private uint MaxSectionNumber(int confId)
        {
            var conferention = db.Conferentions.Find(confId);
            if (conferention.Sections is null || conferention.Sections.Count == 0)
            {
                return 0;
            }
            else
            {
                return conferention.Sections.Max(x => x.Number);
            }
        }
        private bool IsPerformanceDateValid(int sectionId,int durability)
        {
            var startdate = dates[0].Value;
            var enddate = dates[0].Value + new TimeSpan(0, durability, 0);
            var section = db.Sections.SingleOrDefault(x => x.SectionId == sectionId);
            return section != null
                && DateTime.Compare(section.Conferention.StartDate, startdate) < 0
                && DateTime.Compare(enddate, section.Conferention.EndDate) < 0
                && !IsDateContainsOtherDate(section,startdate)
                && !IsDateContainsOtherDate(section,enddate);
        }
        private bool IsDateContainsOtherDate(Section section,DateTime date)
        {
            if (section.Performances is null || section.Performances.Count == 0)
            {
                return false;
            }
            foreach(var performance in section.Performances)
            {
                DateTime end = performance.StartOfPerformance + new TimeSpan(0, performance.Durability, 0);
                if (DateTime.Compare(performance.StartOfPerformance, date) < 0
                && DateTime.Compare(date, end) < 0)
                {
                    return true;
                }
            }
            return false;
        }
        private bool ValidateStartEnd()
        {
            return DateTime.Compare(dates[0].Value,dates[1].Value) < 0;
        }
        private bool ValidateBoxes()
        {
            foreach (var box in boxes)
            {
                if (string.IsNullOrEmpty(box.Text))
                    return false;
            }
            return true;
        }
        private bool ValidateNumeric()
        {
            foreach (var number in numericUpDowns)
            {
                if (number.Value.Equals(null))
                    return false;
            }
            return true;
        }
        private bool ValidateCombos()
        {
            foreach (var c in combos)
            {
                if (string.IsNullOrEmpty(c.Text))
                    return false;
            }
            return true;
        }
        public string[] QueryToString<T>(IQueryable<T> ts)
        {
            List<string> elems = new List<string>();
            foreach (var t in ts)
            {
                elems.Add(t.ToString());
            }
            return elems.ToArray();
        }
    }
}
