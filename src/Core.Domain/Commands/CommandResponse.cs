using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Commands
{
    public class CommandResponse
    {
        public CommandResponse(bool sucsess = false)
        {
            Sucsses = sucsess;
        }
        public static CommandResponse OK = new CommandResponse() { Sucsses = true };
        public static CommandResponse Fail = new CommandResponse() { Sucsses = false };
        public bool Sucsses { get; private set; }

    }
}
