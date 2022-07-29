// See https://aka.ms/new-console-template for more information
using StringBuilder1;
using System.Text;

Console.WriteLine("Hello, World!");

//var str = new StringBuilder("A");//=> 1 biến a => tốt hơn
//str.Append("B");
//str.Append("C");
//str.Append("D");

//Console.WriteLine(str.ToString());


string strNormal = "A" + "B" + "C" + "D"; //4 biến ABCD

//strNormal = "A" new 1 biến
//StrNormal = strNormal + "B" new 1 biến
//strNormal = strNormal + "C" new 1 biến
//strNormal = strNormal + "D" new 1 biến



//50

//struct là 1 kiểu dữ liệu trong c#
//tuple c#9 


//Tuple cho phép lưu trữ nhiều kiểu dữ liệu trong 1 biến
//Tuple<int, string, float, bool> test = new Tuple<int, string, float, bool>(1, "str", 3.2f, true);
//Console.WriteLine(test.Item1);
//Console.WriteLine(test.Item2);

//int Test1(ref string test, ref int number) //=> 1 kết quả về kiểu int
//{
//    test = "Str";
//    number = 2;
//    return 1;
//}

//string Test2() //=> 1 kết quả trả về kiểu string
//{
//    return "str";
//}

////Ref, out


//Tuple<int, string, float, bool> Test() //3 4 5 kết quả
//{

//    return new Tuple<int, string, float, bool>(1, "str", 3.2f, true);
//}

//var testResult = Test();
//Console.WriteLine(testResult.Item4);

//var testInput = "";
//var numberParam = 0;
//var testResult2 = Test1(ref testInput, ref numberParam);
//Console.WriteLine(testResult2); //1
//Console.WriteLine(testInput); //Str
//Console.WriteLine(numberParam); //2



//indexer => Custom index trong array


//exception try cactch finally
//Inheritance kế thừa  

//Kế thừa: đa kế thừa và đơn kế thừa => c# đơn kế thừa => 1 class con chỉ có thể kế thừa 1 class cha
//single inheritance
//interface => 1 class sử dụng nhiều interface

//interface default implement

//var pointA = new Point(3, 6);

//Console.WriteLine(pointA.x);
//Console.WriteLine(pointA.GetString());


//Struct 1 kiểu dữ liệu trong c#
//Property - thuộc tính
//Constructor - hàm khởi tạo
//Method - phương thức 


//Struct vs class


//StringDataStore strStore = new StringDataStore();

//strStore[0] = "One";
//strStore[1] = "Two";
//strStore[2] = "Three"; //index = 2
//strStore[3] = "Four";

//Console.WriteLine(strStore[2]);

//Indexer là kỹ thuật cho phép custom (điểu chỉnh)
//get, set khi lấy ra hoặc gán giá trị theo index trong mảng

//exception try cactch finally

//Controller => service => respository

//respository: method try catch throw ex

//service: method try catch throw ex

//Controller: method try catch get exception => xử lý => show message

//abstract vs interface

//1. kỹ thuật abstraction trừ tượng c#
//2. define default implementation
//3. allow abstract method

try
{
    Console.WriteLine("try");
    int n = int.Parse("test");

    //new connection string
    //new cache, new variable
    
}catch(Exception ex)
{
    Console.WriteLine("catch" + ex.Message);

    //Show lỗi cho end user ex.Message

    //Friendly message: end user có thể hiểu được
}
finally //luôn luôn chạy
{
    Console.WriteLine("finally");

    //Đóng connection string
    //clear cache, clear varible
}
Console.ReadLine();
