namespace Before_After;

/// <summary>
///     The Interface Segregation Principle (ISP) argues that no client should be compelled to rely on methods that it does
///     not use.
///     This entails creating small, particular interfaces rather than one huge, general-purpose interface.
///     ISP contributes to a more modular and maintainable codebase in which clients only implement the capabilities they
///     require.
/// </summary>
internal class D_Interface_Segregation_Principle
{
    #region Before

    /// <summary>
    ///     Before using the Interface Segregation Principle (ISP), the IOrder interface mandates both the OnlineOrder and
    ///     OfflineOrder classes to implement methods that they do not require. OfflineOrder is specifically necessary to use
    ///     the CCProcess technique for credit card processing, which is not applicable to offline orders. This generates extra
    ///     and perhaps error-prone code because OfflineOrder throws a NotImplementedException for the CCProcess method.
    /// </summary>
    public class Before
    {
        public interface IOrder
        {
            void AddToCart();
            void CCProcess();
        }

        public class OnlineOrder : IOrder
        {
            public void AddToCart()
            {
                //Do Add to Cart
            }

            public void CCProcess()
            {
                //process through credit card
            }
        }

        public class OfflineOrder : IOrder
        {
            public void AddToCart()
            {
                //Do Add to Cart
            }

            public void CCProcess()
            {
                //Not required for Cash/ offline Order
                throw new NotImplementedException();
            }
        }
    }

    #endregion

    #region After

    /// <summary>
    ///     After implementing the Interface Segregation Principle (ISP), the IOrder interface only has the AddToCart method,
    ///     but the IOnlineOrder interface is designed for credit card processing. This allows OnlineOrder to implement both
    ///     interfaces while OfflineOrder just implements IOrder, guaranteeing that each class relies on the functions it
    ///     requires.
    /// </summary>
    public class After
    {
        public interface IOrder
        {
            void AddToCart();
        }

        public interface IOnlineOrder
        {
            void CCProcess();
        }

        public class OnlineOrder : IOrder, IOnlineOrder
        {
            public void AddToCart()
            {
                //Do Add to Cart
            }

            public void CCProcess()
            {
                //process through credit card
            }
        }

        public class OfflineOrder : IOrder
        {
            public void AddToCart()
            {
                //Do Add to Cart
            }
        }
    }

    #endregion
}