namespace Assets.Scripts.Console.Parameters
{
    public class IntParameter : NumericParameter<int>
    {
        public IntParameter(string name, bool optional) : base(name, int.Parse, optional)
        {
        }
    }
}
