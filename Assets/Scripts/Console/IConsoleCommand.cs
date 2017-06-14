using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Console
{
    public interface IConsoleCommand
    {
        string CommandName { get;}
        void Execute(params string[] arguments);
    }
}
