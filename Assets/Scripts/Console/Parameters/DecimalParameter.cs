namespace Assets.Scripts.Console.Parameters
{
    public class DecimalParameter : NumericParameter<decimal>
    {
        public DecimalParameter(string name, bool optional) : base(name, decimal.Parse, optional)
        {
        }
    }
}
