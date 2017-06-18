using Assets.Scripts.Console.ConsoleParser;

namespace Assets.Scripts.Console.Parameters
{
    public abstract class VariableParameter : Parameter
    {
        protected VariableParameter(string name, bool optional = false) : base(name, optional)
        {

        }

        protected override object ParseValue(IValue value)
        {
            var variable = (Variable) value;
            return ParseValue(variable.Value);
        }

        protected abstract object ParseValue(string value);

        public override bool CanParse(IValue value)
        {
            var variable = value as Variable;
            return variable != null && CanParse(variable.Value);
        }

        protected abstract bool CanParse(string value);
    }
}
