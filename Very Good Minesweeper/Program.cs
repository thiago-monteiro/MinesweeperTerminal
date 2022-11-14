using System;
using System.Threading;

namespace Very_Good_Minesweeper
{
    class Program
    {
        public static System.Random random = new System.Random();
        public static bool[,] map = new bool[50, 50];
        public static bool[,] temp = new bool[50, 50];
        public static int H = map.GetLength(0);
        public static int W = map.GetLength(1);
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Very Good Minesweeper\n");
            for (var i = 0; i < H; i++)
            {
                for (var j = 0; j < W; j++)
                {
                    map[i, j] = randomBool();
                }

            }

            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < H; j++)
                {
                    temp[i, j] = false;
                }
            }

            turn();
        }

        public static bool randomBool()
        {
            bool result = random.Next(1, 16) > 14;
            return result;
        }

        public static void displayBombLoc()
        {
            for (var i = 0; i < H; i++)
            {
                string currentLine = "";
                for (var j = 0; j < W; j++)
                {
                    if (map[i, j])
                    {
                        currentLine += "* ";
                    }
                    else
                    {
                        currentLine += "  ";
                    }
                }
                Console.WriteLine(currentLine);
            }
        }

        public static void displayTilesLoc()
        {
            for (var i = 0; i < H; i++)
            {
                string currentLine = "";
                for (var j = 0; j < W; j++)
                {
                    if (temp[i, j]) 
                    { 
                        if (map[i, j])
                        {
                            currentLine += "* ";
                        }
                        else
                        {
                            if (checkNeighbors(i, j) == 0) {
                                currentLine += "  ";
                            }
                            else
                            {
                                currentLine += checkNeighbors(i, j).ToString() + " ";
                            }
                            
                        }
                    }
                    else
                    {
                        currentLine += "- ";
                    }
                }
                Console.WriteLine(currentLine);
            }
        }

        public static int checkNeighbors(int row, int col)
        {
            int found = 0;

            // North

            if (row - 1 >= 0 && map[row - 1, col])
            {
                found++;
            }

            // South

            if (row + 1 <= H - 1 && map[row + 1, col])
            {
                found++;
            }

            // West

            if (col - 1 >= 0 && map[row, col - 1])
            {
                found++;
            }

            // East

            if (col + 1 <= W - 1 && map[row, col + 1])
            {
                found++;
            }

            // South East

            if (row + 1 <= H - 1 && col + 1 <= W - 1 && map[row + 1, col + 1])
            {
                found++;
            }

            // South West

            if (row + 1 <= H - 1 && col - 1 >= 0 && map[row + 1, col - 1])
            {
                found++;
            }

            // North East

            if (row - 1 >= 0 && col + 1 <= W - 1 && map[row - 1, col + 1])
            {
                found++;
            }

            // North West

            if (row - 1 >= 0 && col - 1 >= 0 && map[row - 1, col - 1])
            {
                found++;
            }

            return found;
        }

        public static void scanForBlanks(int row, int col)
        {
            temp[row, col] = true;

            // North

            if (row - 1 >= 0 && !map[row - 1, col] && !temp[row - 1, col])
            {
                if (checkNeighbors(row - 1, col) == 0) scanForBlanks(row - 1, col);
                else temp[row - 1, col] = true;
            }

            // South

            if (row + 1 <= H - 1 && !map[row + 1, col] && !temp[row + 1, col])
            {
                if (checkNeighbors(row + 1, col) == 0) scanForBlanks(row + 1, col);
                else temp[row + 1, col] = true;
            }

            // West

            if (col - 1 >= 0 && !map[row, col - 1] && !temp[row, col - 1])
            {
                if (checkNeighbors(row, col - 1) == 0) scanForBlanks(row, col - 1);
                else temp[row, col - 1] = true;
            }

            // East

            if (col + 1 <= W - 1 && !map[row, col + 1] && !temp[row, col + 1])
            {
                if (checkNeighbors(row, col + 1) == 0) scanForBlanks(row, col + 1);
                else temp[row, col + 1] = true;
            }

            
            // South East

            if (row + 1 <= H - 1 && col + 1 <= W - 1 && !map[row + 1, col + 1] && !temp[row + 1, col + 1])
            {
                if (checkNeighbors(row + 1, col + 1) == 0) scanForBlanks(row + 1, col + 1);
                else temp[row + 1, col + 1] = true;
            }

            // South West

            if (row + 1 <= H - 1 && col - 1 >= 0 && !map[row + 1, col - 1] && !temp[row + 1, col - 1])
            {
                if (checkNeighbors(row + 1, col - 1) == 0) scanForBlanks(row + 1, col - 1);
                else temp[row + 1, col - 1] = true;
            }

            // North East

            if (row - 1 >= 0 && col + 1 <= W - 1 && !map[row - 1, col + 1] && !temp[row - 1, col + 1])
            {
                if (checkNeighbors(row - 1, col + 1) == 0) scanForBlanks(row - 1, col + 1);
                else temp[row - 1, col + 1] = true;
            }

            // North West

            if (row - 1 >= 0 && col - 1 >= 0 && !map[row - 1, col - 1] && !temp[row - 1, col - 1])
            {
                if (checkNeighbors(row - 1, col - 1) == 0) scanForBlanks(row - 1, col - 1);
                else temp[row - 1, col - 1] = true;
            }

        }

        public static void turn()
        {
            displayTilesLoc();
            //displayBombLoc();

            Console.WriteLine("Type row number here: ");
            int row = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Type column number here: ");
            int col = Int32.Parse(Console.ReadLine());

            Console.WriteLine("At the row: " + row.ToString() + " at the column: " + col.ToString() + " there is a " + temp[row, col].ToString());

            if (map[row - 1, col - 1])
            {
                Console.WriteLine("--~-- YOU ARE DIE --~--");
            }
            else
            {
                scanForBlanks(row - 1, col - 1);
                turn();
            }

            Thread.Sleep(3000);
        }
    }
}
