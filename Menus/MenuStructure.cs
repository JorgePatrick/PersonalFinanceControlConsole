using PersonalFinanceControlConsole.Menus.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceControlConsole.Menus
{
    public static class MenuStructure
    {
        public const int ScreenSizeLines = 20;
        public const int ScreenSizeCols = 34;
        public const string Exit = "0";
        public const string Back = "*";
        private static int CurrentLine = 0;

        public static void DrawScreen()
        {
            Console.Clear();
            DrawFirstLastLine();
            for (int lines = 0; lines <= ScreenSizeLines; lines++)
            {
                DrawMiddleLine();
            }
            DrawFirstLastLine();
            Header();
            Footnote();
        }
        private static void DrawFirstLastLine()
        {
            Console.Write("+");
            for (int i = 0; i <= ScreenSizeCols; i++)
            {
                Console.Write("-");
            }
            Console.Write("+");
            Console.Write("\n");
        }

        internal static void AddLine(int lines)
        {
            for (int i = 0; i < lines; i++)
            {
                CurrentLine++;
            }
        }

        private static void DrawMiddleLine()
        {
            Console.Write("|");
            for (int i = 0; i <= ScreenSizeCols; i++)
            {
                Console.Write(" ");
            }
            Console.Write("|");
            Console.Write("\n");
        }
        private static void Header()
        {
            Console.SetCursorPosition((ScreenSizeCols / 2 - 6), 2);
            Console.WriteLine("Finance Control");
            Console.SetCursorPosition(3, 3);
            for (int i = 0; i <= (ScreenSizeCols - 4); i++)
            {
                Console.Write("=");
            }
        }

        private static void Footnote()
        {
            Console.SetCursorPosition(3, ScreenSizeLines);
            Console.WriteLine("Type " + Exit + " to Exit");
        }

        public static void SetTitle(string title)
        {
            Console.SetCursorPosition(3, 4);
            Console.Write(title);
            Console.SetCursorPosition(3, 5);
            for (int i = 0; i <= (ScreenSizeCols - 4); i++)
            {
                Console.Write("-");
            }
            CurrentLine = 6;
        }

        internal static void SetGoBackOption()
        {
            Console.SetCursorPosition(18, ScreenSizeLines);
            Console.Write("/ " + Back + " to Back");
        }
        internal static void WriteNewLine(string text)
        {
            CurrentLine++;
            Console.SetCursorPosition(3, CurrentLine);
            Console.WriteLine(text);
        }

        internal static void WriteLine(string text)
        {
            CurrentLine++;
            Console.SetCursorPosition(3, CurrentLine);
            Console.Write(text);
        }

        private static void WriteSameLine(string text)
        {
            Console.SetCursorPosition(0, CurrentLine);
            DrawMiddleLine();
            Console.SetCursorPosition(3, CurrentLine);
            Console.Write(text);
        }

        internal static string ReadLine(Action method, ETypeRead typeRead, int optionMaxValue = 0)
        {
            var text = Console.ReadLine();
            CheckExit(text);
            if (string.IsNullOrEmpty(text))
            {
                text = "invalid";
            }
            switch (typeRead)
            {
                case ETypeRead.String:
                    break;
                case ETypeRead.Int:
                    text = CheckInvalidNumber(text);
                    break;
                case ETypeRead.YesOrNo:
                    text = CheckInvalidYesOrNo(text);
                    break;
                case ETypeRead.NumberList:
                    text = CheckInvalidNumberList(text, optionMaxValue);
                    break;
                default:
                    break;
            }
            if (text == "invalid" && method != null)
            {
                method();
            }
            return text;
        }

        internal static string ReadMany(string text, ETypeRead typeRead)
        {
            string input;
            AddLine(1);
            do
            {
                WriteSameLine(text);
                input = MenuStructure.ReadLine(null, typeRead);
            } while (input == "invalid");
            return input;
        }

        internal static void ReadKey()
        {
            WriteLine("");
            Console.ReadKey();
        }

        private static string CheckInvalidNumberList(string text, int maxValue)
        {
            if (text == Back)
                return text;

            text = CheckInvalidNumber(text);
            if (text == "invalid")
                return text;

            if (int.Parse(text) > maxValue)
                return "invalid";
            else
                return text;
        }

        private static string CheckInvalidNumber(string text)
        {
            bool inputIsNumber = int.TryParse(text, out int input);
            if (!inputIsNumber)
            {
                text = "invalid";
            }
            return text;
        }

        private static string CheckInvalidYesOrNo(string text)
        {
            text = text.ToUpper();
            if (text != "Y" && text != "N")
            {
                text = "invalid";
            }
            return text;
        }

        internal static void CheckExit(string option)
        {
            if (option == Exit)
            {
                Console.Clear();
                System.Environment.Exit(0);
            }
        }
    }
}
