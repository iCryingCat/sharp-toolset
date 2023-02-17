using System;
using System.Collections.Generic;
using System.Linq;

namespace Com.BaiZe.SharpToolSet
{
    public class Logger
    {
        private string owner;
        private bool logEnable = true;

        public Logger(string owner, bool logEnable = true)
        {
            this.owner = "{0}.cs".Format(owner);
            this.logEnable = logEnable;
        }

        public void P(object info)
        {
            string infoData = new LogDumper().DumpAsString(info);
            string msg = PrintFormat($"[{owner}]: {infoData}");
            Log(msg);
        }

        public void W(object info)
        {
            string infoData = new LogDumper().DumpAsString(info);
            string msg = WarningFormat($"[{owner}]: {infoData}");
            Log(msg);
        }

        public void E(object info)
        {
            string infoData = new LogDumper().DumpAsString(info);
            string msg = ErrorFormat($"[{owner}]: {infoData}");
            Log(msg);
        }

        private string PrintFormat(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            return msg;
        }

        private string WarningFormat(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            return msg;
        }

        private string ErrorFormat(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            return msg;
        }

        private string ThrowFormat(string msg)
        {
            return $"<color=purple>{msg}</color>";
        }

        private string ColorFormat(string color, string msg)
        {
            return $"<color={color}>{msg}</color>";
        }

        private void Log(string msg)
        {
            if (!logEnable) return;
            Console.WriteLine(msg);
        }
    }
}