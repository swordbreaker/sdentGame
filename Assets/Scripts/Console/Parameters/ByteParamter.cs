namespace Assets.Scripts.Console.Parameters
{
    public class ByteParamter : NumericParameter<byte>
    {
        public ByteParamter(string name, bool optional) : base(name, byte.Parse, optional) {}
    }
}
