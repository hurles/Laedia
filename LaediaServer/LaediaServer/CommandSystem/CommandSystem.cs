using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaediaServer.Server
{
    public static class CommandSystem
    {

        public static Dictionary<string, CommandData> CommandsDict = InitCommandsDict();

        public static Dictionary<string, CommandData> InitCommandsDict()
        {
            var outDict = new Dictionary<string, CommandData>();
            for (int i = 0; i < (int)CommandTypes.CT_Count; i++)
            {
                var commandData = GetCommandDataForCommandType((CommandTypes)i);
                if (!outDict.ContainsKey(commandData.CommandString))
                    outDict.Add(commandData.CommandString, commandData);
            }
            return outDict;
        }

        public static CommandData GetCommandData(string command)
        {
            CommandsDict.TryGetValue(command, out var commandData);
            return commandData;
        }

        public static CommandData GetCommandDataForCommandType(CommandTypes type)
        {
            switch (type)
            {
                case CommandTypes.CT_Quit:
                    return new CommandData() { CommandType = type, CommandString = "quit", CommandDescription = "quit gracefully" };
                default:
                    return null;
            }
        }
    }

    public enum CommandTypes
    {
        CT_Quit,
        CT_Count
    }

    public class CommandData
    {
        public string CommandString;
        public string CommandDescription;
        public CommandTypes CommandType;
    }

}