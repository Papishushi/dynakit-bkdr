/*  dynakit-bkdr, dynamic compilation client-server rootkit.
 *  Copyright (C) 2024 Daniel Molinero Lucas
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using Serilog;
using System.Text;

internal class CommandFactory
{
    private const string CLEAR = "clear";
    private const string HELP = "help";

    private bool _isRunning = true;

    ~CommandFactory() => _isRunning = false;

    internal readonly List<Command> AvailableCommands = [];

    public CommandFactory() => AvailableCommands = [
            new Command { Keyword = HELP, Execution = HelpCommand, Description = "Displays the available help for a command or function." },
            new Command { Keyword = CLEAR, Execution = ClearComand, Description = "Clears the current terminal." }];

    internal readonly record struct Command(string Keyword, Action<object[]> Execution, string Description);
    private void HelpCommand(params object[] parameters)
    {
        var sb = new StringBuilder("HELP PAGE\n");
        if (parameters.Length == 0)
            foreach(var command in AvailableCommands)
                sb.AppendLine($"\t/{command.Keyword} | {command.Description}");
        else
            foreach(var command in AvailableCommands)
                foreach (string param in parameters.Cast<string>())
                    if (param.Equals(command.Keyword, StringComparison.OrdinalIgnoreCase))
                        sb.AppendLine($"\t/{command.Keyword} | {command.Description}");
        Log.Information(sb.ToString());
    }
    private void ClearComand(params object[] parameters) => Console.Clear();

    internal async Task LogAndProcessCommands()
    {
        await Task.Run(() =>
        {
            while (_isRunning)
            {
                var input = Console.ReadLine();
                FormatInput(ref input);
                ProcessCommand(ref input);
            }
        });
    }

    internal void Add(Command command) => AvailableCommands.Add(command);
    internal void Remove(Command command) => AvailableCommands.Remove(command);

    private static void FormatInput(ref string? input)
    {
        if (!string.IsNullOrEmpty(input))
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Log.Information(input);
        }
        else
            Console.SetCursorPosition(0, Console.CursorTop - 1);
    }

    private void ProcessCommand(ref string? input)
    {
        var args = input?.Split("/", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (args?.Length <= 0) return;
        foreach (var arg in args)
        {
            foreach (var command in AvailableCommands)
            {
                if (arg.StartsWith(command.Keyword))
                {
                    var parameters = arg.Split(' ');
                    parameters = parameters.TakeLast(parameters.Length - 2).ToArray();
                    command.Execution(parameters);
                }
            }
        }
    }
}
