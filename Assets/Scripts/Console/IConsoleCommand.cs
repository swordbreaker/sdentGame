using Assets.Scripts.Console.ConsoleParser;

namespace Assets.Scripts.Console
{
    public interface IConsoleCommand
    {
        string CommandName { get;}
        string ReturnMessage { get; set; }
        void Execute(params IValue[] arguments);
        string GetCommandSyntax();
    }
}
