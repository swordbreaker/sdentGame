namespace Assets.Scripts.Console.Parameters
{
    public class FloatParameter : NumericParameter<float>
    {
        public FloatParameter(string name, bool optional = false) : base(name, float.Parse, optional) {}
    }
}
