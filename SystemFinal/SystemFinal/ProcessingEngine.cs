using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SystemFinal
{
    public static class ProcessingEngine
    {
        public static List<StudentReport> ProcessRecordsConcurrently(List<Student> students)
        {
            var concurrentReports = new ConcurrentBag<StudentReport>();

            Parallel.ForEach(students, student =>
            {
                decimal average = student.Grades != null && student.Grades.Count > 0
                    ? student.Grades.Average()
                    : 0;

                decimal attendance = student.TotalSchoolDays > 0
                    ? ((decimal)student.DaysPresent / student.TotalSchoolDays) * 100
                    : 0;

                string remarks = average >= 75 ? "Passed" : "Failed";
                string threadId = $"Thread #{Thread.CurrentThread.ManagedThreadId}";

                concurrentReports.Add(new StudentReport
                {
                    Id = student.Id,
                    Name = student.Name,
                    FinalAverage = decimal.Round(average, 2),
                    AttendanceRate = decimal.Round(attendance, 2),
                    Remarks = remarks,
                    ProcessedThreadId = threadId
                });
            });

            return concurrentReports.OrderBy(r => r.Id).ToList();
        }
    }
}