using System;
using System.Collections.Generic;
using System.Linq;

public class Student
{
    public int StudentID { get; set; }
    public string StudentName { get; set; }
    public int Age { get; set; }
    public string Major { get; set; }
    public double Tuition { get; set; }
}

public class StudentClubs
{
    public int StudentID { get; set; }
    public string ClubName { get; set; }
}

public class StudentGPA
{
    public int StudentID { get; set; }
    public double GPA { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        // Student collection
        IList<Student> studentList = new List<Student>() {
            new Student() { StudentID = 1, StudentName = "Frank Furter", Age = 55, Major = "Hospitality", Tuition = 3500.00 },
            new Student() { StudentID = 2, StudentName = "Gina Host", Age = 21, Major = "Hospitality", Tuition = 4500.00 },
            new Student() { StudentID = 3, StudentName = "Cookie Crumb", Age = 21, Major = "CIT", Tuition = 2500.00 },
            new Student() { StudentID = 4, StudentName = "Ima Script", Age = 48, Major = "CIT", Tuition = 5500.00 },
            new Student() { StudentID = 5, StudentName = "Cora Coder", Age = 35, Major = "CIT", Tuition = 1500.00 },
            new Student() { StudentID = 6, StudentName = "Ura Goodchild", Age = 40, Major = "Marketing", Tuition = 500.00 },
            new Student() { StudentID = 7, StudentName = "Take Mewith", Age = 29, Major = "Aerospace Engineering", Tuition = 5500.00 }
        };

        // Student GPA Collection
        IList<StudentGPA> studentGPAList = new List<StudentGPA>() {
            new StudentGPA() { StudentID = 1, GPA = 4.0 },
            new StudentGPA() { StudentID = 2, GPA = 3.5 },
            new StudentGPA() { StudentID = 3, GPA = 2.0 },
            new StudentGPA() { StudentID = 4, GPA = 1.5 },
            new StudentGPA() { StudentID = 5, GPA = 4.0 },
            new StudentGPA() { StudentID = 6, GPA = 2.5 },
            new StudentGPA() { StudentID = 7, GPA = 1.0 }
        };

        // Club collection
        IList<StudentClubs> studentClubList = new List<StudentClubs>() {
            new StudentClubs() { StudentID = 1, ClubName = "Photography" },
            new StudentClubs() { StudentID = 1, ClubName = "Game" },
            new StudentClubs() { StudentID = 2, ClubName = "Game" },
            new StudentClubs() { StudentID = 5, ClubName = "Photography" },
            new StudentClubs() { StudentID = 6, ClubName = "Game" },
            new StudentClubs() { StudentID = 7, ClubName = "Photography" },
            new StudentClubs() { StudentID = 3, ClubName = "PTK" },
        };

        // Group by GPA and display the student's IDs
        var groupedByGPA = studentGPAList.GroupBy(s => s.GPA);
        Console.WriteLine("Grouped by GPA and display the student's IDs:");
        foreach (var group in groupedByGPA)
        {
            Console.WriteLine("GPA: {0}", group.Key);
            foreach (var student in group)
            {
                Console.WriteLine("Student ID: {0}", student.StudentID);
            }
        }

        // Sort by Club, then group by Club and display the student's IDs
        var sortedAndGroupedByClub = studentClubList.OrderBy(c => c.ClubName).GroupBy(c => c.ClubName);
        Console.WriteLine("\nSorted by Club, then group by Club and display the student's IDs:");
        foreach (var group in sortedAndGroupedByClub)
        {
            Console.WriteLine("Club: {0}", group.Key);
            foreach (var studentClub in group)
            {
                Console.WriteLine("Student ID: {0}", studentClub.StudentID);
            }
        }

        // Count the number of students with a GPA between 2.5 and 4.0
        var countGPA = studentGPAList.Count(s => s.GPA >= 2.5 && s.GPA <= 4.0);
        Console.WriteLine("\nNumber of students with GPA between 2.5 and 4.0: {0}", countGPA);

        // Average all student's tuition
        var averageTuition = studentList.Average(s => s.Tuition);
        Console.WriteLine("\nAverage tuition of all students: {0}", averageTuition);

        // Find the student paying the most tuition and display their name, major, and tuition
        double maxTuition = 0;
        Student studentWithMaxTuition = null;

        foreach (var student in studentList)
        {
            if (student.Tuition > maxTuition)
            {
                maxTuition = student.Tuition;
                studentWithMaxTuition = student;
            }
        }

        if (studentWithMaxTuition != null)
        {
            Console.WriteLine("Student paying the most tuition:");
            Console.WriteLine("Name: {0}, Major: {1}, Tuition: {2}", studentWithMaxTuition.StudentName, studentWithMaxTuition.Major, studentWithMaxTuition.Tuition);
        }
        else
        {
            Console.WriteLine("No student found.");
        }

        // Join the student list and student GPA list on student ID and display the student's name, major, and GPA
        var joinedList = studentList.Join(studentGPAList, s => s.StudentID, g => g.StudentID, (s, g) => new { s.StudentName, s.Major, g.GPA });
        Console.WriteLine("\nJoined list of students and their GPA:");
        foreach (var item in joinedList)
        {
            Console.WriteLine("Name: {0}, Major: {1}, GPA: {2}", item.StudentName, item.Major, item.GPA);
        }

        // Join the student list and student club list. Display the names of only those students who are in the Game club
        var studentsInGameClub = studentList.Join(studentClubList.Where(c => c.ClubName == "Game"), s => s.StudentID, c => c.StudentID, (s, c) => s.StudentName);
        Console.WriteLine("\nStudents who are in the Game club:");
        foreach (var studentName in studentsInGameClub)
        {
            Console.WriteLine(studentName);
        }
    }
}