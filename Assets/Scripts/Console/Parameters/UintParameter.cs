namespace Assets.Scripts.Console.Parameters
{
    public class UintParameter : NumericParameter<uint>
    {
        public UintParameter(string name, bool optional) : base(name, uint.Parse, optional)
        {
        }
    }
}
