using System;

namespace Task2.TicTacToe
{
    class Program
    {
        static char[,] field = new char[3, 3] { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };
        /// <summary>
        /// Вывод поля в коонсоль
        /// </summary>
        /// <param name="elements"></param>
        private static void ShowField(char[,] elements)
        {
            Console.WriteLine(string.Format("  {0} {1} {2} y", 0, 1, 2));
            for (int i = 0; i < 3; i++)
                Console.WriteLine(string.Format("{3}|{0}|{1}|{2}|", elements[i, 0], elements[i, 1], elements[i,  2],i));
            Console.WriteLine("x");
        }

        /// <summary>
        /// Ход игрока
        /// </summary>
        /// <param name="PlayerName"></param>
        /// <param name="playerSymbol"></param>
        private static void PlayerStep(string PlayerName,char playerSymbol)
        {
            Console.WriteLine(string.Format("Ход игрока {0}", PlayerName));
            Console.WriteLine("Введи номер ячейки (x;y). Левая верхняя ячейка с координатами (0;0).");;
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

                canExit = CheckStep(x, y,playerSymbol);
                if(!canExit)
                {
                    Console.WriteLine("Данная ячейка занята, выбери другую");
                }
            } while (!canExit);
        }

        /// <summary>
        /// Проверка шага игры
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="playerSymbol"></param>
        /// <returns></returns>
        private static bool CheckStep(int x,int y,char playerSymbol)
        {
            if(field[x,y]==' ')
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
        /// Метод проверки конца игры
        /// Если истина то поле для хода есть
        /// Если ложь то поля для хода нет.
        /// 
        /// Проверка для победы
        /// 
        /// Проверка для ничьи
        /// </summary>
        /// <returns></returns>
        private static bool CheckField()
        {
            bool checkLineX = true;
            bool checkLineO = true;
            //проверка строк
            //горизонтальные
            for (int i=0;i<3;i++)
            {
                checkLineX = true;
                checkLineO = true;
                for(int j = 0;j<3;j++)
                {
                    if (field[i,j]=='x')
                    {
                        checkLineO = false;
                    }
                    if (field[i, j] == 'o')
                    {
                        checkLineX = false;
                    }
                    if (field[i, j] == ' ')
                    {
                        checkLineO = false;
                        checkLineX = false;
                    }
                }
                if(checkLineO)
                {
                    //победа О
                    Win('o');
                    return false;
                }
                if(checkLineX)
                {
                    //победа X
                    Win('x');
                    return false;
                }
            }
            
            //вертикальные
            for (int i = 0; i < 3; i++)
            {
                checkLineX = true;
                checkLineO = true;
                for (int j = 0; j < 3; j++)
                {
                    if (field[j, i] == 'x')
                    {
                        checkLineO = false;
                    }
                    if (field[j, i] == 'o')
                    {
                        checkLineX = false;
                    }
                    if (field[j, i] == ' ')
                    {
                        checkLineO = false;
                        checkLineX = false;
                    }
                }
                if (checkLineO)
                {
                    //победа О
                    Win('o');
                    return false;
                }
                if (checkLineX)
                {
                    //победа X
                    Win('x');
                    return false;
                }
            }

            //диагональ положительная
            for (int j = 0; j < 3; j++)
            {
                if (field[j, j] == 'x')
                {
                    checkLineO = false;
                }
                if (field[j, j] == 'o')
                {
                    checkLineX = false;
                }
                if (field[j, j] == ' ')
                {
                    checkLineO = false;
                    checkLineX = false;
                }
            }
            if (checkLineO)
            {
                //победа О
                Win('o');
                return false;
            }
            if (checkLineX)
            {
                //победа X
                Win('x');
                return false;
            }

            //диагональ отрицательная
            for (int j = 0; j < 3; j++)
            {
                if (field[2-j, 2 - j] == 'x')
                {
                    checkLineO = false;
                }
                if (field[2 - j, 2 - j] == 'o')
                {
                    checkLineX = false;
                }
                if (field[2 - j, 2 - j] == ' ')
                {
                    checkLineO = false;
                    checkLineX = false;
                }
            }
            if (checkLineO)
            {
                //победа О
                Win('o');
                return false;
            }
            if (checkLineX)
            {
                //победа X
                Win('x');
                return false;
            }


            //проверка есть ли поле
            for (int i =0;i<3;i++)
            {
                for(int j=0;j<3;j++)
                {
                    if(field[i,j]==' ')
                    {
                        return true;
                    }
                }
            }
            Console.WriteLine("Ничья");
            return false;
        }

        private static void Win(char playerSymbol)
        {
            Console.WriteLine(string.Format("Победа {0}", playerSymbol));
        }
        static void Main(string[] args)
        {
            ShowField(field);

            int playerId = 0; //0 - первый игрок. 1 - второй игрок
            do
            {
                switch(playerId)
                {
                    case 0:
                        PlayerStep("Первый игрок (x)", 'x');
                        playerId = 1;
                        break;
                    case 1:
                        PlayerStep("Второй игрок (o)", 'o');
                        playerId = 0;
                        break;
                }
                ShowField(field);

            } while (CheckField());
        }
    }
}
