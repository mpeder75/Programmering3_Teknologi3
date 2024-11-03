
// Exercise 1. Filter a list of integers to find all numbers greater than 5.
List<int> numbers1 = new List<int> { 1, 2, 3, 6, 7, 8, 4, 5 };

var numGreaterThanFive = numbers1.Where(numbers => numbers >= 5);


// Exercise 2. Convert a list of strings to uppercase.
List<string> words1 = new List<string> { "hello", "world", "blazor", "lambda" };

var convertToUppercase = words1.Select(x => x.ToUpper()).ToList();


// Exercise 3. Filter a list of integers to find all odd numbers and then double them.
List<int> numbers2 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

var filterThenDouble = numbers2.Where(x => x % 2 == 0).Select(x => x * 2).ToList();




// Exercise 4. Create a list of strings and find the length of each string.
List<string> words2 = new List<string> { "apple", "banana", "cherry" };
var stringList = words2.Select(x => x.Length).ToList();



//// Exercise 5. Given a list of Person objects, filter out those who are older than 30.
//List<Person> people = new List<Person>
//{
//    new Person { Name = "Alice", Age = 25 },
//    new Person { Name = "Bob", Age = 35 },
//    new Person { Name = "Charlie", Age = 30 }
//};

//var peopleOlderThan30 = people.Where(person => person.Age > 30).ToList();

//public class Person
//{
//    public string Name { get; set; }
//    public int Age { get; set; }
//}


// Exercise 6. Sum all the numbers in a list of integers.
List<int> numbers3 = new List<int> { 1, 2, 3, 4, 5 };

var summedIntegers = numbers3.Sum();



// 7. Count the number of elements in a list of strings that contain the letter 'a'.
List<string> words = new List<string> { "apple", "banana", "cherry", "date" };

var numberOfElements = words.Count(word => word.Contains('a'));
