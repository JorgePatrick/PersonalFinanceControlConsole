﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceControlConsole.Menus
{
    public static class MenuDefault
    {
        static int ScreenSizeLines = 15;
        static int ScreenSizeCols = 34;
        public static int CurrentLine = 0;
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
            Console.WriteLine("Type 0 to Exit");
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
            Console.Write("/ * to back");
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

        internal static string ReadLine(Action method, Enums.ETypeRead typeRead)
        {
            var text = Console.ReadLine();
            CheckExit(text);
            if (string.IsNullOrEmpty(text))
            {
                text = "invalid";
            }
            switch (typeRead)
            {
                case Enums.ETypeRead.String:
                    break;
                case Enums.ETypeRead.Int:
                    bool inputIsNumber = int.TryParse(text, out int input);
                    if (!inputIsNumber)
                    {
                        text = "invalid";
                    }
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

        internal static void CheckExit(string option)
        {
            if (option == "0")
            {
                Console.Clear();
                System.Environment.Exit(0);
            }
        }
        internal static void Message(string message)
        {
            DrawScreen();
            SetTitle("Message");
            CurrentLine++;
            WriteNewLine(message);
            Console.ReadKey();
        }

    }
}
