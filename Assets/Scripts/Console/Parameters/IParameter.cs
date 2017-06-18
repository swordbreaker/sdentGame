using System;
using Assets.Scripts.Console.ConsoleParser;

namespace Assets.Scripts.Console.Parameters
{
    public interface IParameter
    {
        bool Optional { get; }
        string Name { get; }

        bool CanParse(IValue value);

        void Validate(IValue value);

        bool IsValid(IValue value);

        object Parse(IValue value, bool validate = true);

        Type GetParamType();
        string GetSyntax();
    }
}
