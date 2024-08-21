namespace Before_After;

/// <summary>
///     The Open/Closed Principle (OCP) of C# specifies that a class should be available for extension but closed for
///     modification.
///     This means that you should be able to add new functionality or behavior to a class without changing the old code,
///     which is often accomplished through interfaces, abstract classes, and inheritance.
///     OCP helps to construct maintainable and scalable systems by limiting changes to current code when new features are
///     implemented.
/// </summary>
internal class B_Open_Closed_Principle
{
    #region Before

    /// <summary>
    ///     Before using the Open/Closed Principle (OCP), the CalcInterest function of the Account class calculates interest
    ///     using
    ///     conditional logic based on account type. This approach necessitates updating the method whenever a new account type
    ///     or
    ///     interest calculation rule is implemented, making the class less adaptable and more difficult to maintain.
    /// </summary>
    public class Before
    {
        public class Account
        {
            public decimal Interest { get; set; }

            public decimal Balance { get; set; }

            // members and function declaration
            public decimal CalcInterest(string accType)
            {
                if (accType == "Regular") // savings
                {
                    Interest = Balance * 4 / 100;
                    if (Balance < 1000) Interest -= Balance * 2 / 100;
                    if (Balance < 50000) Interest += Balance * 4 / 100;
                }
                else if (accType == "Salary") // salary savings
                {
                    Interest = Balance * 5 / 100;
                }
                else if (accType == "Corporate") // Corporate
                {
                    Interest = Balance * 3 / 100;
                }

                return Interest;
            }
        }
    }

    #endregion

    #region After

    /// <summary>
    ///     After implementing the Open/Closed Principle (OCP), the Account class is refactored into an IAccount interface with
    ///     numerous implementations for various account kinds. Each account type (RegularSavingAccount, SalarySavingAccount,
    ///     and CorporateAccount) now implements the CalcInterest method individually, allowing new account types to be
    ///     introduced without altering old code, hence increasing flexibility and maintenance.
    /// </summary>
    public class After
    {
        private interface IAccount
        {
            // members and function declaration, properties
            decimal Balance { get; set; }
            decimal CalcInterest();
        }

        //regular savings account 
        public class RegularSavingAccount : IAccount
        {
            public decimal Balance { get; set; } = 0;

            public decimal CalcInterest()
            {
                var Interest = Balance * 4 / 100;
                if (Balance < 1000) Interest -= Balance * 2 / 100;
                if (Balance < 50000) Interest += Balance * 4 / 100;

                return Interest;
            }
        }

        //Salary savings account 
        public class SalarySavingAccount : IAccount
        {
            public decimal Balance { get; set; } = 0;

            public decimal CalcInterest()
            {
                var Interest = Balance * 5 / 100;
                return Interest;
            }
        }

        //Corporate Account
        public class CorporateAccount : IAccount
        {
            public decimal Balance { get; set; } = 0;

            public decimal CalcInterest()
            {
                var Interest = Balance * 3 / 100;
                return Interest;
            }
        }
    }

    #endregion
}