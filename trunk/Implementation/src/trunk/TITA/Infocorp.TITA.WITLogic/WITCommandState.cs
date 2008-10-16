using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.DataTypes;

namespace Infocorp.TITA.WITLogic
{

    class WITCommandState
    {
        private static int _lastUsedId = 0;
        private static List<DTCommandInfo> _commands = new List<DTCommandInfo>();
        private static WITCommandState _instance;

        private WITCommandState() { }

        public static WITCommandState Instance()
        {
            if (_instance == null)
            {
                _instance = new WITCommandState();
            }

            return _instance;
        }

        public void AddCommand(DTCommandInfo command)
        {
            bool exists = _commands.Contains(command);
            bool add = true;
            switch (command.CommandType)
            {
                case CommandType.ADD:
                    DTFieldCounter field = new DTFieldCounter("ID", true, true, true, --_lastUsedId);
                    command.Item.Fields.Add(field);

                    break;
                case CommandType.MODIFY:
                    if (exists)
                    {
                        RemoveCommand(command);
                    }
                    break;
                case CommandType.DELETE:
                    if (exists)
                    {
                        add = false;
                        RemoveCommand(command);
                    }

                    break;
                default:
                    break;
            }
            if (add)
            {
                _commands.Add(command);
            }
        }

        public void RemoveCommand(DTCommandInfo command)
        {
            _commands.Remove(command);
        }

        public List<DTCommandInfo> GetCommands(string contractId)
        {
            return _commands.FindAll(delegate(DTCommandInfo cinfo) { return cinfo.ContractId.ToLower().Trim() == contractId.ToLower().Trim(); });
        }

        public List<DTCommandInfo> GetCommands(ItemType itemType, string contractId)
        {
            List<DTCommandInfo> commands = null;
            switch (itemType)
            {
                case Infocorp.TITA.WITLogic.ItemType.ISSUE:
                    commands = _commands.FindAll(delegate(DTCommandInfo cinfo) { return cinfo.CommandItemType == ItemType.ISSUE && cinfo.ContractId.ToLower().Trim() == contractId.ToLower().Trim(); });
                    break;
                case Infocorp.TITA.WITLogic.ItemType.TASK:
                    commands = _commands.FindAll(delegate(DTCommandInfo cinfo) { return cinfo.CommandItemType == ItemType.TASK && cinfo.ContractId.ToLower().Trim() == contractId.ToLower().Trim(); });
                    break;
                case Infocorp.TITA.WITLogic.ItemType.WORKPACKAGE:
                    commands = _commands.FindAll(delegate(DTCommandInfo cinfo) { return cinfo.CommandItemType == ItemType.WORKPACKAGE && cinfo.ContractId.ToLower().Trim() == contractId.ToLower().Trim(); });
                    break;
                default:
                    //No debería pasar...
                    break;
            }

            return commands;
        }

        public void ClearCommands(string contractId)
        {
            _commands.RemoveAll(delegate(DTCommandInfo cinfo) { return cinfo.ContractId.ToLower().Trim() == contractId.ToLower().Trim(); });
        }
    }
}