namespace Assets.Scripts.Console.Parameters
{
    public class SbyteParameter : NumericParameter<sbyte>
    {
        public SbyteParameter(string name, bool optional) : base(name, sbyte.Parse, optional) {}
    }
}
