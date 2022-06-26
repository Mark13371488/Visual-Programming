
using System.Data;
using WinFormsApp1.Models;

namespace WinFormsApp1
{
    public partial class EditForm : Form
    {
        private readonly ConferentionContext db;
        private readonly string tabName;
        private object entity;

        private List<TextBox> boxes = null!;
        private List<DateTimePicker> dates = null!;
        private List<ComboBox> combos = null!;
        private List<NumericUpDown> numericUpDowns = null!;
        private Button button = null!;
        public EditForm(string tabName, ConferentionContext db,object entity)
        {
            InitializeComponent();
            this.tabName = tabName;
            this.db = db;
            this.entity = entity;
        }
        private void AddBuildingBoxes()
        {
            var building = entity as Building;
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
                    Size = new Size(180, 25),
                    Text = building.BuildingName
                }
            };
            this.Controls.AddRange(boxes.ToArray());
            button = new Button
            {
                Location = new Point(90, 150),
                Size = new Size(180, 50),
                Text = "Update",
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
                    UpdateBuildingInDb();
                    break;
                case "Rooms":
                    UpdateRoomInDb();
                    break;
                case "Conferentions":
                    UpdateConferentionInDb();
                    break;
                case "Sections":
                    UpdateSectionInDb();
                    break;
                case "Performances":
                    UpdatePerformanceInDb();
                    break;
                case "Performancers":
                    UpdatePerformancerInDb();
                    break;
                case "Equipment":
                    UpdateEquipmentInDb();
                    break;
                default:
                    throw new InvalidOperationException("Something went wrong");
            }
        }

        private void AddRoomBoxes()
        {
            var room = entity as Room;
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
                    DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList,
                    SelectedValue = room.BuildingId
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
                    Size = new Size(180, 25),
                    Value = room.RoomNumber
                }
            };
            this.Controls.AddRange(numericUpDowns.ToArray());
            button = new Button
            {
                Location = new Point(90, 250),
                Size = new Size(180, 50),
                Text = "Update",
                FlatStyle = FlatStyle.Popup,
                BackColor = Color.Khaki,

            };
            button.Click += new EventHandler(button_Click);
            this.Controls.Add(button);
        }

        private void AddConferentionBoxes()
        {
            var conferention = entity as Conferention;
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
                    DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList,
                    SelectedValue = conferention.BuildingId
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
                    Size = new Size(180, 25),
                    Text = conferention.Name
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
                    MinDate = conferention.StartDate,
                    Value = conferention.StartDate
                },
                new DateTimePicker
                {
                    Format = DateTimePickerFormat.Custom,
                    CustomFormat = " MM'/'dd'/'yyyy hh':'mm",
                    Name = "End",
                    Location = new Point(90, 400),
                    Size = new Size(180, 25),
                    MinDate = conferention.StartDate,
                    Value = conferention.EndDate
                }
            };
            this.Controls.AddRange(dates.ToArray());
            button = new Button
            {
                Location = new Point(90, 450),
                Size = new Size(180, 50),
                Text = "Update",
                FlatStyle = FlatStyle.Popup,
                BackColor = Color.Khaki,
            };
            button.Click += new EventHandler(button_Click);
            this.Controls.Add(button);
        }
        private void AddSectionBoxes()
        {
            var section = entity as Section;
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
                    DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList,
                    SelectedValue = section.ConferentionId
                },
                new ComboBox
                {
                    Name = "Room",
                    Location = new Point(90, 150),
                    Size = new Size(180, 25),
                    DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList,
                    SelectedValue = section.RoomId
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
                    Size = new Size(180, 25),
                    Text = section.Name
                },

                new TextBox
                {
                    Name = "NameOfHead",
                    Location = new Point(90, 350),
                    Size = new Size(180, 25),
                    Text = section.NameOfHead
                }
            };
            this.Controls.AddRange(boxes.ToArray());
            button = new Button
            {
                Location = new Point(90, 400),
                Size = new Size(180, 50),
                Text = "Update",
                FlatStyle = FlatStyle.Popup,
                BackColor = Color.Khaki,
            };
            button.Click += new EventHandler(button_Click);
            this.Controls.Add(button);
        }

        private void AddPerformanceBoxes()
        {
            var performance = entity as Performance;
            List<Label> labels = new List<Label>
            {
                new Label
                {
                    Text = "Choose section",
                    Location = new Point(150, 20),
                    Size = new Size(180,25)
                },
                new Label
                {
                    Text = "Choose performancer",
                    Location = new Point(150, 100),
                    Size = new Size(180,25)
                },
                new Label
                {
                    Text = "Theme",
                    Location = new Point(150, 180),
                    Size = new Size(180,25)
                },
                new Label
                {
                    Text = "Start of performance",
                    Location = new Point(150, 260),
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
                    DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList,
                    SelectedValue = performance.SectionId
                },
                new ComboBox
                {
                    Name = "Performancer",
                    Location = new Point(90, 130),
                    Size = new Size(180, 25),
                    DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList,
                    SelectedValue = performance.PerformanceId
                },
            };
            combos[0].Items.AddRange(QueryToString(db.Sections.Select(x => x.SectionId)));
            combos[1].Items.AddRange(QueryToString(db.Performancers.Select(x => x.PerformancerId)));
            this.Controls.AddRange(combos.ToArray());
            boxes = new List<TextBox>
            {
                new TextBox
                {
                    Name = "Theme",
                    Location = new Point(90, 210),
                    Size = new Size(180, 25),
                    Text = performance.Theme
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
                    Size = new Size(180, 25),
                    Value = performance.StartOfPerformance
                }
            };
            this.Controls.AddRange(dates.ToArray());
            numericUpDowns = new List<NumericUpDown>
            {
                new NumericUpDown
                {
                    Name = "Durability",
                    Location = new Point(90,370),
                    Size = new Size(180,25),
                    Value = (decimal)performance.Durability
                }
            };
            this.Controls.AddRange(numericUpDowns.ToArray());
            button = new Button
            {
                Location = new Point(90, 420),
                Size = new Size(180, 50),
                Text = "Update",
                FlatStyle = FlatStyle.Popup,
                BackColor = Color.Khaki,
            };
            button.Click += new EventHandler(button_Click);
            this.Controls.Add(button);
        }
        private void AddPerformancerBoxes()
        {
            var performancer = entity as Performancer;  
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
                    Size = new Size(180, 25),
                    Text = performancer.ScienceDegree
                },
                new TextBox
                {
                    Name = "Workplace",
                    Location = new Point(90, 150),
                    Size = new Size(180, 25),
                    Text = performancer.Workplace
                },
                new TextBox
                {
                    Name = "Position",
                    Location = new Point(90, 250),
                    Size = new Size(180, 25),
                    Text = performancer.Position
                },
                new TextBox
                {
                    Name = "Biography",
                    Location = new Point(90, 350),
                    Size = new Size(180, 25),
                    Text = performancer.ProBiography
                }
            };
            this.Controls.AddRange(boxes.ToArray());

            button = new Button
            {
                Location = new Point(90, 400),
                Size = new Size(180, 50),
                Text = "Update",
                FlatStyle = FlatStyle.Popup,
                BackColor = Color.Khaki,
            };
            button.Click += new EventHandler(button_Click);
            this.Controls.Add(button);
        }
        private void AddEquipmentBoxes()
        {
            var equipment = entity as Equipment;
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
                    Size = new Size(180, 25),
                    Text = equipment.Name
                }
            };
            this.Controls.AddRange(boxes.ToArray());
            button = new Button
            {
                Location = new Point(90, 150),
                Size = new Size(180, 50),
                Text = "Update",
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
        private void UpdateBuildingInDb()
        {
            if (ValidateBoxes())
            {
                var building = entity as Building;
                building.BuildingName = boxes[0].Text;
                db.SaveChanges();
                this.Close();
            }
            else
                MessageBox.Show("Enter all values");
        }
        private void UpdateRoomInDb()
        {
            if (ValidateCombos() && ValidateNumeric())
            {
                var room = entity as Room;
                room.BuildingId = Convert.ToInt32(combos[0].Text);
                room.RoomNumber = (int)numericUpDowns[0].Value;
                db.SaveChanges();
                this.Close();
            }
            else
                MessageBox.Show("Enter all values");
        }
        private void UpdateConferentionInDb()
        {
            if (ValidateCombos() && ValidateBoxes() && ValidateStartEnd())
            {
                var conferention = entity as Conferention;
                if (conferention.StartDate != dates[0].Value || conferention.EndDate != dates[1].Value)
                {
                    foreach (var item in conferention.Sections)
                    {
                        if (item.Performances != null && item.Performances.Count > 0)
                        {
                            MessageBox.Show("You can`t change date if section contains declared performances");
                            this.Close();
                            return;
                        }
                    }
                }
                conferention.BuildingId = Convert.ToInt32(combos[0].Text);
                conferention.Name = boxes[0].Text;
                conferention.StartDate = dates[0].Value;
                conferention.EndDate = dates[1].Value;
                db.SaveChanges();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid data");
            }
        }
        private void UpdateSectionInDb()
        {
            if (ValidateCombos() && ValidateBoxes())
            {
                var section = entity as Section;
                section.ConferentionId = Convert.ToInt32(combos[0].Text);
                section.RoomId = Convert.ToInt32(combos[1].Text);
                section.Name = boxes[0].Text;
                section.NameOfHead = boxes[1].Text;
                db.SaveChanges();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid data");
            }
        }
        private void UpdatePerformanceInDb()
        {
            if (ValidateCombos() && ValidateBoxes() 
                && IsPerformanceDateValid(Convert.ToInt32(combos[0].Text))
                && IsPerformancerReady(Convert.ToInt32(combos[1].Text), Convert.ToInt32(combos[0].Text), dates[0].Value))
            {
                var performance = entity as Performance;
                performance.SectionId = Convert.ToInt32(combos[0].Text);
                performance.Theme = boxes[0].Text;
                performance.PerformancerId = Convert.ToInt32(combos[1].Text);
                performance.StartOfPerformance = dates[0].Value;
                performance.Durability = (int)numericUpDowns[0].Value;
                db.SaveChanges();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid data");
            }
        }
        public bool IsPerformancerReady(int performancerId, int sectionId, DateTime dateTime)
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
            if (performances != null && performances.Count != 0)
            {
                var last = performances.OrderByDescending(s => s.StartOfPerformance).First();
                return (dateTime.Date - last.StartOfPerformance.Date).Days > 0;
            }
            else return true;
        }
        private void UpdatePerformancerInDb()
        {
            if (ValidateBoxes())
            {
                var performancer = entity as Performancer;
                performancer.ScienceDegree = boxes[0].Text;
                performancer.Workplace = boxes[1].Text;
                performancer.Position = boxes[2].Text;
                performancer.ProBiography = boxes[3].Text;
                db.SaveChanges();
                this.Close();
            }
            else
            {
                MessageBox.Show("Enter all values");
            }
        }
        private void UpdateEquipmentInDb()
        {
            if (ValidateBoxes())
            {
                var equipment = entity as Equipment;
                equipment.Name = boxes[0].Text;
                db.SaveChanges();
                this.Close();
            }
            else
            {
                MessageBox.Show("Enter all values");
            }
        }
        private bool IsPerformanceDateValid(int sectionId)
        {
            var section = db.Sections.SingleOrDefault(x => x.SectionId == sectionId);
            return section != null
                && DateTime.Compare(section.Conferention.StartDate, dates[0].Value) < 0
                && DateTime.Compare(dates[0].Value, section.Conferention.EndDate) < 0;
        }
        private bool ValidateStartEnd()
        {
            return DateTime.Compare(dates[0].Value, dates[1].Value) < 0;
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

        private void EditForm_Load(object sender, EventArgs e)
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
                    throw new InvalidOperationException("Something went wrong");
            }
        }
    }
}
