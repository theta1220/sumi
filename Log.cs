using System;
using System.Diagnostics;

namespace Pocole
{
    public class Log
    {
        public static void Info(string text)
        {
            _Print("Info", text, 3, null);
        }
        public static void Warn(string text)
        {
            _Print("Warn", text, 3, null);
        }
        public static void Error(string text)
        {
            _Print("Error", text, 3, null);
        }
        public static void Info(string text, params object[] args)
        {
            _Print("Info", text, 3, args);
        }
        public static void Warn(string text, params object[] args)
        {
            _Print("Warn", text, 3, args);
        }
        public static void Error(string text, params object[] args)
        {
            _Print("Error", text, 3, args);
        }
        private static void _Print(string title, string text, int stack, params object[] args)
        {
            var info = string.Format("[Pocole {0}]:{1}/{2}({3})",
                title,
                Util.Reflect.GetCallerClassName(stack),
                Util.Reflect.GetCallerMethodName(stack),
                Util.Reflect.GetCallerMethodLineNo(stack));
            var message = string.Format("{0}: {1}", info, text);
            Console.WriteLine(message, args);
        }
        public static void InitError()
        {
            _Print("InitError", "初期化に失敗しちゃったなり", 3, null);
        }
        public static void ParseError()
        {
            _Print("ParseError", "パースに失敗しちゃったなり", 3, null);
        }
        public static void ParseError(string source)
        {
            var message = string.Format("パースに失敗しちゃったなり:{0}", source);
            _Print("ParseError", message, 3, null);
        }
        public static void ParseError(System.Exception e, string source)
        {
            ParseError(source);
            _Print("Exeption", e.Message, 3, null);
        }
    }
}