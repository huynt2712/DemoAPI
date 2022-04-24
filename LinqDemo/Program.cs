// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using LinqDemo;
using System.Collections;

var studentList = new[] {
        new Student() { StudentID = 1, StudentName = "John Nigel", Mark = 73, City ="NYC"} ,
        new Student() { StudentID = 2, StudentName = "Alex Roma",  Mark = 49 , City ="CA"} ,
        new Student() { StudentID = 3, StudentName = "Noha Shamil",  Mark = 80 , City ="CA"} ,
        new Student() { StudentID = 4, StudentName = "James Palatte" , Mark = 60, City ="NYC"} ,
        new Student() { StudentID = 5, StudentName = "Ron Jenova" , Mark = 80 , City ="NYC"}
    };

var idstudent = studentList.Where(student => student.StudentID == 4).Select(student => student);
foreach (var student in idstudent)
{
    Console.WriteLine("StudentID: {0}, Name: {1}, Mark: {2}, City: {3}", student.StudentID, student.StudentName, student.Mark, student.City);
}

var studentInformation = studentList.Where(student => student.StudentName.Contains("am") && student.City == "CA").Select(student => student);
foreach (var student in studentInformation)
{
    Console.WriteLine("StudentID: {0}, Name: {1}, Mark: {2}, City: {3}", student.StudentID, student.StudentName, student.Mark, student.City);
}

var maxMarkStudent = studentList.Max(student=>student.Mark);
//var resultMaxMark = studentList.Where(student => student.Mark == maxMarkStudent).Select(student => student);
var resultMaxMark = studentList.FirstOrDefault(student => student.Mark == maxMarkStudent);
//foreach (var student in resultMaxMark)
//{
//    Console.WriteLine($"Max Mark: {student.StudentName}");
//}
Console.WriteLine($"Max Mark: {resultMaxMark.StudentName}");

var hasAllStudentsPass = studentList.All(student => student.Mark > 50);
if (hasAllStudentsPass)
{
    Console.WriteLine("PASS");
}
else { Console.WriteLine("fAIL"); }