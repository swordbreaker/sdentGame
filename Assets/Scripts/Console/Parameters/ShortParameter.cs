namespace Assets.Scripts.Console.Parameters
{
    public class ShortParameter : NumericParameter<short>
    {
        public ShortParameter(string name, bool optional) : base(name, short.Parse, optional)
        {
        }
    }
}
