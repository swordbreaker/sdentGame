using System;

namespace Assets.Scripts.Console.Parameters
{
    public interface IParameter
    {
        bool Optional { get; }
        string Name { get; }

        bool CanParse(string value);

        void Validate(string value);

        bool IsValid(string value);

        object Parse(string value);

        Type GetParamType();
    }
}
