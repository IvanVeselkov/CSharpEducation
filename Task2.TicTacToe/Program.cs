using System;
using System.Collections.Generic;

namespace Task2.TicTacToe
{
    internal class Program
    {
        private static char[,] field = new char[3, 3] { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };

        private static List<char[]> lines = new List<char[]>();

        private const char XPlayerSymbol = 'x';
        private const char OPlayerSymbol = 'o';

        /// <summary>
        /// Вывод игрового поля в консоль.
        /// </summary>
        /// <param name="elements">Двумерный массив поля игры</param>
        private static void ShowField(char[,] elements)
        {
            Console.WriteLine(string.Format("  {0} {1} {2} y", 0, 1, 2));
            for (int i = 0; i < 3; i++)
                Console.WriteLine(string.Format("{3}|{0}|{1}|{2}|", elements[i, 0], elements[i, 1], elements[i, 2], i));
            Console.WriteLine("x");
        }

        /// <summary>
        /// Алгоритм хода игрока.
        /// </summary>
        /// <param name="PlayerName">Имя пользователя</param>
        /// <param name="playerSymbol">Символ игрока X или O</param>
        private static void PlayerStep(string PlayerName, char playerSymbol)
        {
            Console.WriteLine(string.Format("Ход игрока {0}", PlayerName));
            Console.WriteLine("Введи номер ячейки (x;y). Левая верхняя ячейка с координатами (0;0)."); ;
            int x, y;
            bool canExit = false;
            do
            {
                do
                {
                    Console.WriteLine("Введи координату x");
                    x = int.Parse(Console.ReadLine());
                    if (x >= 0 && x <= 2)
                    {
                        break;
                    }
                } while (true);
                do
                {
                    Console.WriteLine("Введи координату y");
                    y = int.Parse(Console.ReadLine());
                    if (y >= 0 && y <= 2)
                    {
                        break;
                    }
                } while (true);

                canExit = CheckStep(x, y, playerSymbol);
                if (!canExit)
                {
                    Console.WriteLine("Данная ячейка занята, выбери другую");
                }
            } while (!canExit);
        }

        /// <summary>
        /// Проверка ячейки поля на возможность поставить туда символ
        /// </summary>
        /// <param name="x">Координата обозначающая строку игрового поля</param>
        /// <param name="y">Координата обозначающая столбец игрового поля</param>
        /// <param name="playerSymbol">Символ игрока X или O</param>
        /// <returns></returns>
        private static bool CheckStep(int x, int y, char playerSymbol)
        {
            if (field[x, y] == ' ')
            {
                field[x, y] = playerSymbol;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Метод описывает список действий при победе
        /// </summary>
        /// <param name="playerSymbol">Метод содержащий алгоритм действий игрока.</param>
        private static void Win(char playerSymbol)
        {
            Console.WriteLine(string.Format("Победа {0}", playerSymbol));
        }

        /// <summary>
        /// Метод обновления данных для проверки победы
        /// </summary>
        private static void UpdateLines()
        {
            lines.Add(new char[3] { field[0, 0], field[0, 1], field[0, 2] });
            lines.Add(new char[3] { field[1, 0], field[1, 1], field[1, 2] });
            lines.Add(new char[3] { field[2, 0], field[2, 1], field[2, 2] });
            lines.Add(new char[3] { field[0, 0], field[1, 0], field[2, 0] });
            lines.Add(new char[3] { field[0, 1], field[1, 1], field[2, 1] });
            lines.Add(new char[3] { field[0, 2], field[1, 2], field[2, 2] });
            lines.Add(new char[3] { field[0, 0], field[1, 1], field[2, 2] });
            lines.Add(new char[3] { field[2, 0], field[1, 1], field[0, 2] });
        }

        /// <summary>
        /// Метод проверки условий конца игры
        /// </summary>
        /// <returns>Возращает истини если игра продолжается и ложь если игра закочилась</returns>
        private static bool СheckingTheLines()
        {
            bool TheResultOfTheCheck = true;
            for (int i = 0; i < lines.Count; i++)
            {
                bool flagForXPlayer = true;
                bool flagForOPlayer = true;
                for (int j = 0; j < 3; j++)
                {
                    if (lines[i][j] == XPlayerSymbol)
                    {
                        flagForOPlayer = false;
                    }
                    if (lines[i][j] == OPlayerSymbol)
                    {
                        flagForXPlayer = false;
                    }
                    if (lines[i][j] == ' ')
                    {
                        flagForOPlayer = false;
                        flagForXPlayer = false;
                        break;
                    }
                }
                if (flagForOPlayer)
                {
                    Win(OPlayerSymbol);
                    TheResultOfTheCheck = false;
                }
                if (flagForXPlayer)
                {
                    Win(XPlayerSymbol);
                    TheResultOfTheCheck = false;
                }
            }

            return TheResultOfTheCheck;
        }

        /// <summary>
        /// Метод содержащий алгоритм действий бота.
        /// Текущий метод использует случайную установки символа на поле
        /// </summary>
        /// <param name="botSymbol">Символ отрисовки хода бота</param>
        private static void BotRandomStep(char botSymbol)
        {
            Console.WriteLine("Бот делает ход ...");

            List<(int, int)> emptyCells = new List<(int, int)>();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (field[i, j] == ' ')
                    {
                        emptyCells.Add((i, j));
                    }
                }
            }

            Random r = new Random();
            int numberCell = r.Next(0, emptyCells.Count - 1);
            (int, int) cell = emptyCells[numberCell];
            field[cell.Item1, cell.Item2] = botSymbol;
        }

        private static void Main(string[] args)
        {
            UpdateLines();
            Console.WriteLine("TicTacToe");
            Console.WriteLine("Выберите режим игры: \n \t 0 - игра с игроком \n \t 1 - игра против компьютера");
            int gameMode = 0;
            do
            {
                gameMode = int.Parse(Console.ReadLine());
            } while (gameMode < 0 || gameMode > 1);

            ShowField(field);
            int playerId = 0;
            do
            {
                switch (playerId)
                {
                    case 0:
                        PlayerStep("Первый игрок (x)", XPlayerSymbol);
                        playerId = 1;
                        break;

                    case 1:
                        if (gameMode == 0)
                            PlayerStep("Второй игрок (o)", OPlayerSymbol);
                        else
                            BotRandomStep(OPlayerSymbol);
                        playerId = 0;
                        break;
                }
                ShowField(field);
                //Обновление значений в линиях
                lines.Clear();
                UpdateLines();
            } while (СheckingTheLines());
        }
    }
}