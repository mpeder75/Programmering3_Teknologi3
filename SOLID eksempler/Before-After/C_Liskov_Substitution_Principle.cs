namespace Before_After;

/// <summary>
///     The Liskov Substitution Principle (LSP) argues that objects of a superclass should be interchangeable with objects
///     of a subclass without affecting the program's validity.
///     In other words, subclasses must increase the functionality of the parent class while maintaining its behavior,
///     guaranteeing that derived classes can be used interchangeably with their base class without introducing
///     unanticipated problems.
///     This idea contributes to a consistent and trustworthy interface.
/// </summary>
internal class C_Liskov_Substitution_Principle 
{
    #region Before

    /// <summary>
    ///     Before using the Liskov Substitution Principle (LSP), the code mistakenly replaces a Triangle object with a Circle.
    ///     The Circle class is derived from Triangle, but the GetShape method's functionality changes in a way that violates
    ///     LSP because a Circle cannot be substituted for a Triangle without changing the program's intended behavior.Interface Segregation Principle
    /// </summary>
    public class Before
    {
        public class Program
        {
            private static void Main(string[] args)
            {
                Triangle triangle = new Circle();
                Console.WriteLine(triangle.GetShape());
            }
        }

        public class Triangle
        {
            public virtual string GetShape()
            {
                return "Triangle";
            }
        }

        public class Circle : Triangle
        {
            public override string GetShape()
            {
                return "Circle";
            }
        }
    }

    #endregion

    #region After

    /// <summary>
    ///     After implementing the Liskov Substitution Principle (LSP), the Shape abstract class defines the GetShape method,
    ///     which both the Triangle and Circle classes inherit. This enables the Shape reference to hold objects from either
    ///     subclass without affecting the program's validity, ensuring that Triangle and Circle can be used interchangeably
    ///     while retaining consistent behavior.
    /// </summary>
    public class After
    {
        private class Program
        {
            private static void Main(string[] args)
            {
                Shape shape = new Circle();
                Console.WriteLine(shape.GetShape());
                shape = new Triangle();
                Console.WriteLine(shape.GetShape());
            }
        }

        public abstract class Shape
        {
            public abstract string GetShape();
        }

        public class Triangle : Shape
        {
            public override string GetShape()
            {
                return "Triangle";
            }
        }

        public class Circle : Triangle
        {
            public override string GetShape()
            {
                return "Circle";
            }
        }
    }

    #endregion
}