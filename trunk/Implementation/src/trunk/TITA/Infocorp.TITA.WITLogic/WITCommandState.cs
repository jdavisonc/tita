﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infocorp.TITA.DataTypes;

namespace Infocorp.TITA.WITLogic
{
    public enum CommandType : int
    {
        ADD,
        MODIFY,
        DELETE
    }

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
            if (command.CommandType == CommandType.DELETE && Commands.Contains(command))
            {
                RemoveCommand(command);
            }
            else
            {
                if (command.CommandType == CommandType.ADD)
                {
                    DTField field = new DTField("ID", DTField.Types.Integer, true, null, (--_lastUsedId).ToString());
                    command.Issue.Fields.Add(field);
                }

                _commands.Add(command);
            }
        }

        public void RemoveCommand(DTCommandInfo command)
        {
            _commands.Remove(command);
        }

        public List<DTCommandInfo> Commands
        {
            get { return _commands; }
        }
    }
}
