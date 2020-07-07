using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaediaClient.Commands
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
                case CommandTypes.CT_Connect:
                    return new CommandData() { CommandType = type, CommandString = "connect", CommandDescription = "connect to server" };
                case CommandTypes.CT_Disconnect:
                    return new CommandData() { CommandType = type, CommandString = "disconnect", CommandDescription = "disconnect from server" };
                case CommandTypes.CT_SendString:
                    return new CommandData() { CommandType = type, CommandString = "send", CommandDescription = "send text to server console" };
                default:
                    return null;
            }
        }
    }

    public enum CommandTypes
    {
        CT_Quit,
        CT_Connect,
        CT_Disconnect,
        CT_SendString,
        CT_Count,

    }

    public class CommandData
    {
        public string CommandString;
        public string CommandDescription;
        public CommandTypes CommandType;
    }

}
