namespace Assets.Scripts.Console.Parameters
{
    public class DoubleParameter : NumericParameter<double>
    {
        public DoubleParameter(string name, bool optional = false) : base(name, double.Parse, optional) {}
    }
}
