namespace Before_After;

/// <summary>
///     In C#, the Single Responsibility Principle (SRP) suggests that a class should only have one cause to change, i.e.,
///     one task or responsibility.
///     This idea makes code more maintainable, intelligible, and versatile by requiring that each class handle a single
///     functionality or problem.
/// </summary>
internal class A_Single_Responsibility_Principle
{
    #region Before

    /// <summary>
    ///     In this case, UserService is responsible for both enrolling users and delivering emails, which are two distinct
    ///     tasks.
    /// </summary>
    public class Before
    {
        public class UserService
        {
            public void RegisterUser(User user)
            {
                // Logic to register user
            }

            public void SendWelcomeEmail(User user)
            {
                // Logic to send a welcome email
            }
        }
    }

    #endregion

    #region After

    /// <summary>
    ///     In this code, the UserService class handles user registration and delegated email sending to the EmailService
    ///     class,
    ///     following the Single Responsibility Principle (SRP). This separation of responsibilities ensures that
    ///     UserService concentrates entirely on user-related duties, whereas EmailService handles email operations.
    /// </summary>
    public class After
    {
        public class UserService
        {
            private readonly EmailService _emailService;

            public UserService(EmailService emailService)
            {
                _emailService = emailService;
            }

            public void RegisterUser(User user)
            {
                // Logic to register user
                _emailService.SendWelcomeEmail(user);
            }
        }

        public class EmailService
        {
            public void SendWelcomeEmail(User user)
            {
                // Logic to send a welcome email
            }
        }
    }

    #endregion

    #region Helpers

    public class User
    {
    }

    #endregion
}