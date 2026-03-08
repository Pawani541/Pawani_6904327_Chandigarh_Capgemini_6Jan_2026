using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Program
{
    class CollageManagement
    {
        // studentId -> (subject -> marks)
        Dictionary<string, Dictionary<string, int>> studentRecords = new Dictionary<string, Dictionary<string, int>>();

        // studentId -> insertion order of subjects
        Dictionary<string, LinkedList<KeyValuePair<string, int>>> studentSubjectsOrder = new Dictionary<string, LinkedList<KeyValuePair<string, int>>>();

        // subject -> (studentId -> marks)
        Dictionary<string, Dictionary<string, int>> subjectsRecords = new Dictionary<string, Dictionary<string, int>>();

        // subject -> insertion order of students
        Dictionary<string, LinkedList<KeyValuePair<string, int>>> subjectsStudentsOrder = new Dictionary<string, LinkedList<KeyValuePair<string, int>>>();

        public int AddStudent(string studentId, string subject, int marks)
        {
            // Add student if not exists
            if (!studentRecords.ContainsKey(studentId))
            {
                studentRecords[studentId] = new Dictionary<string, int>();
                studentSubjectsOrder[studentId] = new LinkedList<KeyValuePair<string, int>>();
            }

            // Add subject if not exists
            if (!subjectsRecords.ContainsKey(subject))
            {
                subjectsRecords[subject] = new Dictionary<string, int>();
                subjectsStudentsOrder[subject] = new LinkedList<KeyValuePair<string, int>>();
            }

            // If subject already exists for student
            if (studentRecords[studentId].ContainsKey(subject))
            {
                if (marks > studentRecords[studentId][subject])
                {
                    studentRecords[studentId][subject] = marks;
                    subjectsRecords[subject][studentId] = marks;

                    UpdateLinkedList(studentSubjectsOrder[studentId], subject, marks);
                    UpdateLinkedList(subjectsStudentsOrder[subject], studentId, marks);
                }
            }
            else
            {
                studentRecords[studentId][subject] = marks;
                subjectsRecords[subject][studentId] = marks;

                studentSubjectsOrder[studentId].AddLast(new KeyValuePair<string, int>(subject, marks));
                subjectsStudentsOrder[subject].AddLast(new KeyValuePair<string, int>(studentId, marks));
            }

            return 1;
        }

        public int RemoveStudent(string studentId)
        {
            if (!studentRecords.ContainsKey(studentId))
                return 0;

            foreach (var subject in studentRecords[studentId].Keys)
            {
                subjectsRecords[subject].Remove(studentId);
                RemoveFromLinkedList(subjectsStudentsOrder[subject], studentId);
            }

            studentRecords.Remove(studentId);
            studentSubjectsOrder.Remove(studentId);

            return 1;
        }

        public string TopStudent(string subject)
        {
            if (!subjectsRecords.ContainsKey(subject))
                return "";

            int maxMarks = subjectsRecords[subject].Values.Max();
            StringBuilder result = new StringBuilder();

            foreach (var entry in subjectsStudentsOrder[subject])
            {
                if (entry.Value == maxMarks)
                {
                    result.AppendLine(entry.Key + " " + entry.Value);
                }
            }

            return result.ToString().Trim();
        }

        public string Result()
        {
            StringBuilder result = new StringBuilder();

            foreach (var student in studentRecords)
            {
                double avg = student.Value.Values.Average();
                result.AppendLine(student.Key + " " + avg.ToString("F2"));
            }

            return result.ToString().Trim();
        }

        private void UpdateLinkedList(LinkedList<KeyValuePair<string, int>> list, string key, int newMarks)
        {
            var node = list.First;
            while (node != null)
            {
                if (node.Value.Key == key)
                {
                    node.Value = new KeyValuePair<string, int>(key, newMarks);
                    return;
                }
                node = node.Next;
            }
        }

        private void RemoveFromLinkedList(LinkedList<KeyValuePair<string, int>> list, string key)
        {
            var node = list.First;
            while (node != null)
            {
                if (node.Value.Key == key)
                {
                    list.Remove(node);
                    return;
                }
                node = node.Next;
            }
        }
    }

    public static void Main()
    {
        CollageManagement cm = new CollageManagement();

        cm.AddStudent("S1", "Math", 80);
        cm.AddStudent("S2", "Math", 90);
        cm.AddStudent("S3", "Math", 90);
        cm.AddStudent("S1", "Phy", 90);

        Console.WriteLine(cm.TopStudent("Math"));
        Console.WriteLine(cm.Result());

        cm.RemoveStudent("S1");
    }
}