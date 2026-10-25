using System;
using System.Collections.Generic;

namespace SystemFinal
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public int DaysPresent { get; set; } = 0;
        public int TotalSchoolDays { get; set; } = 200;
        public List<decimal> Grades { get; set; } = new List<decimal>();
    }

    public class StudentReport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal FinalAverage { get; set; }
        public decimal AttendanceRate { get; set; }
        public string Remarks { get; set; }
        public string ProcessedThreadId { get; set; }
    }

    public static class DataStore
    {
        // Registration List
        public static List<Student> Students { get; set; } = new List<Student>();

        // Reports Property
        public static List<StudentReport> Reports { get; set; } = new List<StudentReport>();

        // Mock 100 Students list na direktang naka-proportional ang Grado sa Attendance
        public static List<Student> MockStudents { get; set; } = GenerateMockStudents();

        private static List<Student> GenerateMockStudents()
        {
            var list = new List<Student>();

            for (int i = 1; i <= 100; i++)
            {
                // Gawing mas pantay ang distribution: May mababa, may mataas
                // Halimbawa: Si Student 1 ay may 20 days, si Student 2 ay may 22 days, atbp.
                int daysPresent = 20 + (i * 18) % 181; // Maglalaro sa 20 hanggang 200
                int totalDays = 200;

                // Attendance Ratio (0.1 hanggang 1.0)
                decimal attendanceRatio = (decimal)daysPresent / totalDays;

                // DIREKTANG FORMULA: 
                // Kapag mababa ang attendance ratio, mababa rin ang grado (mula 50 pataas).
                // Halimbawa: 20/200 (10%) -> baseScore = 50 + (0.1 * 45) = 54.5 (BAGSAK)
                // Halimbawa: 200/200 (100%) -> baseScore = 50 + (1.0 * 45) = 95.0 (PASADO)
                decimal baseScore = 50m + (attendanceRatio * 45m);

                var studentGrades = new List<decimal>
                {
                    Math.Max(50m, Math.Min(100m, baseScore + ((i % 3) - 1))),
                    Math.Max(50m, Math.Min(100m, baseScore + ((i * 2) % 5) - 2)),
                    Math.Max(50m, Math.Min(100m, baseScore + ((i * 3) % 4) - 1))
                };

                list.Add(new Student
                {
                    Id = 5000 + i,
                    Name = $"Mock Student {i}",
                    Gender = i % 2 == 0 ? "Female" : "Male",
                    ContactNumber = $"0912345{i.ToString().PadLeft(4, '0')}",
                    DaysPresent = daysPresent,
                    TotalSchoolDays = totalDays,
                    Grades = studentGrades
                });
            }
            return list;
        }
    }
}