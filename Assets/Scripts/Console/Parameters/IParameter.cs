using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

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
