namespace BusinessLogic
{
    public interface IFoo
    {
        string GetName();
    }

    public interface IBar
    {
        string GetName();
    }

    // class implementer interface IFoo
    public class Foo : IFoo
    {
        string IFoo.GetName()
        {
            throw new NotImplementedException();
        }
    }

    public class Bar : IBar
    {
        // Depedency injection
        private readonly IFoo _foo;

        // Constructor injection
        public Bar(IFoo foo)
        {
            _foo = foo;
        }

        string IBar.GetName()
        {
            return "Bar" + _foo.GetName();
        }
    }
}


