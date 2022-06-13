using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private int id = 0;

        private string xsdpath = "C://Users//User//AppData//Local//Temp//Новий текстовий документ .xsd";

        private string openfilepath = "";

        List<Student> students = new List<Student>();
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxGroup.Text) || string.IsNullOrEmpty(textFirstName.Text))
            {    
                buttonAdd.Visible = false;     
                return;
            }
            students.Add(new Student(id++, int.Parse(textBoxGroup.Text), textFirstName.Text, textLastName.Text, BirthdayBox.Value, Convert.ToInt32(Rating.Value)));
            ViewRefresh();
            button2.Visible = true;
            button2.PerformClick();
            button3.PerformClick();
        }

        private void ViewRefresh()
        {
            listView1.Items.Clear();
            foreach (Student student in students)
            {
                ListViewItem item = new ListViewItem(student.StudentId.ToString());

                item.SubItems.Add(student.GroupNumber.ToString());
                item.SubItems.Add(student.FirstName);
                item.SubItems.Add(student.LastName);
                item.SubItems.Add(student.DateOfBirth.ToString());
                item.SubItems.Add(student.Rating.ToString());

                listView1.Items.Add(item);
            }
        }
        private void Find()
        {
            listView1.Items.Clear();
            foreach (Student student in students)
            {
                if (string.IsNullOrEmpty(textBox1.Text) || student.GroupNumber == int.Parse(textBox1.Text))
                {
                    ListViewItem item = new ListViewItem(student.StudentId.ToString());

                    item.SubItems.Add(student.GroupNumber.ToString());
                    item.SubItems.Add(student.FirstName);
                    item.SubItems.Add(student.LastName);
                    item.SubItems.Add(student.DateOfBirth.ToString());
                    item.SubItems.Add(student.Rating.ToString());

                    listView1.Items.Add(item);
                }
            }
        }
        private int GetStudentIndexById(int id)
        {
            int i = 0;
            foreach (Student student in students)
            {
                if (student.StudentId == id)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
            {
                try
                {   
                    students.RemoveAt(GetStudentIndexById(Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text)));
                    ViewRefresh();
                }
                catch { }
            }
        }


        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                return;
            }
            textBoxGroup.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textFirstName.Text = listView1.SelectedItems[0].SubItems[2].Text;
            textLastName.Text = listView1.SelectedItems[0].SubItems[3].Text;
            BirthdayBox.Text = listView1.SelectedItems[0].SubItems[4].Text;
            Rating.Text = listView1.SelectedItems[0].SubItems[5].Text;
            button1.Visible = true;
            button3.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Student student1 = new Student(Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text), int.Parse(textBoxGroup.Text), textFirstName.Text, textLastName.Text, BirthdayBox.Value, Convert.ToInt32(Rating.Value));

            students[GetStudentIndexById(student1.StudentId)]=student1;

            ViewRefresh();
            button2.Visible = true;
            button2.PerformClick();
            button1.Visible = false;
            button3.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxGroup.Clear();
            textFirstName.Clear();
            textLastName.Clear();
            Rating.Value = 0;            
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            string filename = "";
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Title = "SaveFileDialog Export2File";
            sfd.Filter = "Text File (.xml) | *.xml";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filename = sfd.FileName.ToString();
                if (filename != "")
                {
                    var xmlSerializer = new XmlSerializer(typeof(List<Student>));
                    using (var writer = new StreamWriter(filename))
                    {
                        xmlSerializer.Serialize(writer, students);
                    }
                }
            }
        }
        private void Validate(string filename)
        {
            XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
            xmlReaderSettings.ValidationType = ValidationType.Schema;
            xmlReaderSettings.Schemas.Add(null, xsdpath);
            using(XmlReader reader = XmlReader.Create(filename, xmlReaderSettings))
            {
                while (reader.Read()) { }
            }
        }
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            string filename = "";
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "OpenFileDialog Export2File";
            ofd.Filter = "Text File (.xml) | *.xml";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filename = ofd.FileName.ToString();
                if (filename != "")
                {
                    try
                    {
                        Validate(filename);
                        var xmlSerializer = new XmlSerializer(typeof(List<Student>));
                        using (var reader = new StreamReader(filename))
                        {
                            var students1 = (List<Student>)xmlSerializer.Deserialize(reader);
                            students = students1;
                        }
                        openfilepath = filename;
                    }
                    catch(Exception ex) {
                        MessageBox.Show(ex.Message);
                    }
                    ViewRefresh();
                }
            }
        }
        private void numericUpDownVacation_ValueChanged(object sender, EventArgs e)
        {
            buttonAdd.Visible = true;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            students.Sort((s1, s2) => DateTime.Compare(s1.DateOfBirth, s2.DateOfBirth));
            Find();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            students.Sort((s1,s2) => s2.Rating.CompareTo(s1.Rating));
            Find();
        }

        private void textFirstName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void textBoxGroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ViewRefresh();
        }
    }
}
