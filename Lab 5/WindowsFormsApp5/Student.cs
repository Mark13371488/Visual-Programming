using System;

namespace WindowsFormsApp5
{
    public class Student
    {
        public Student(int StudentId, int GroupNumber, string FirstName, string LastName, DateTime DateOfBirth, int Rating)
        {
            this.StudentId = StudentId;
            this.GroupNumber = GroupNumber;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Rating = Rating;
        }
        public Student() { }
        public int StudentId { get; set; }
        public int GroupNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Rating { get; set; }
    }
}
