namespace Before_After;

/// <summary>
///     The Dependency Inversion Principle (DIP) suggests that high-level modules should not rely on low-level modules but
///     on abstractions.
///     Furthermore, abstractions should not be dependent on details but rather the reverse.
///     This principle encourages decoupling by ensuring that code is based on interfaces or abstract classes, which
///     improves flexibility and maintenance.
/// </summary>
internal class E_Dependency_Inversion_Principle
{
    #region Before

    /// <summary>
    ///     Before using the Dependency Inversion Principle (DIP), the AutomobileController class was tightly tied to the Jeep
    ///     class, rendering it rigid and unable to function with other vehicle types, such as SUVs, without altering the
    ///     controller itself. This direct reliance on a specific implementation violates DIP, limiting the system's
    ///     extensibility and maintainability.
    /// </summary>
    public class Before
    {
        public interface IAutomobile
        {
            void Ignition();
            void Stop();
        }

        public class Jeep : IAutomobile
        {
            public void Ignition()
            {
                Console.WriteLine("Jeep start");
            }

            public void Stop()
            {
                Console.WriteLine("Jeep stopped.");
            }
        }

        public class SUV : IAutomobile
        {
            public void Ignition()
            {
                Console.WriteLine("SUV start");
            }

            public void Stop()
            {
                Console.WriteLine("SUV stopped.");
            }
        }

        public class AutomobileController
        {
            private readonly Jeep _jeep;

            public AutomobileController(Jeep jeep)
            {
                _jeep = jeep;
            }

            public void Ignition()
            {
                _jeep.Ignition();
            }

            public void Stop()
            {
                _jeep.Stop();
            }
        }

        private class Program
        {
            private static void Main(string[] args)
            {
                var jeep = new Jeep();
                var automobileController = new AutomobileController(jeep);
                automobileController.Ignition();
                automobileController.Stop();

                Console.Read();
            }
        }
    }

    #endregion

    #region After

    /// <summary>
    ///     After applying the Dependency Inversion Principle (DIP), the AutomobileController class depends on the IAutomobile
    ///     interface rather than a specific implementation like Jeep. This allows the controller to work with any IAutomobile
    ///     implementation, such as Jeep or SUV, making the system more flexible, extensible, and easier to maintain.
    /// </summary>
    public class After
    {
        public interface IAutomobile
        {
            void Ignition();
            void Stop();
        }

        public class Jeep : IAutomobile
        {
            public void Ignition()
            {
                Console.WriteLine("Jeep start");
            }

            public void Stop()
            {
                Console.WriteLine("Jeep stopped.");
            }
        }

        public class SUV : IAutomobile
        {
            public void Ignition()
            {
                Console.WriteLine("SUV start");
            }

            public void Stop()
            {
                Console.WriteLine("SUV stopped.");
            }
        }

        public class AutomobileController
        {
            private readonly IAutomobile _automobile;

            public AutomobileController(IAutomobile automobile)
            {
                _automobile = automobile;
            }

            public void Ignition()
            {
                _automobile.Ignition();
            }

            public void Stop()
            {
                _automobile.Stop();
            }
        }

        private class Program
        {
            private static void Main(string[] args)
            {
                IAutomobile jeep = new Jeep();
                var automobileController = new AutomobileController(jeep);
                automobileController.Ignition();
                automobileController.Stop();

                IAutomobile suv = new SUV();
                automobileController = new AutomobileController(suv);
                automobileController.Ignition();
                automobileController.Stop();

                Console.Read();
            }
        }
    }

    #endregion
}