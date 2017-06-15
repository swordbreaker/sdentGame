namespace Assets.Scripts.Console.Parameters
{
    public class UshortParameter : NumericParameter<ushort>
    {
        public UshortParameter(string name, bool optional) : base(name, ushort.Parse, optional)
        {
        }
    }
}