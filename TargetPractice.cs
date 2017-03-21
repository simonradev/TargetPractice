namespace TargetPracticeExcercise
{
    using System;
    using System.Linq;
    using System.Text;

    public class TargetPractice
    {
        public static char[][] matrix;

        public static void Main()
        {
            int[] dimensionsOfTheMatrix = ReadAnArrayFromTheConsole();

            int rows = dimensionsOfTheMatrix[0];
            int cols = dimensionsOfTheMatrix[1];

            matrix = new char[rows][];

            string snake = Console.ReadLine();
            int snakeIndex = 0;
            for (int currRow = 0; currRow < rows; currRow++)
            {
                matrix[rows - 1 - currRow] = new char[cols];

                if (currRow % 2 == 0)
                {
                    for (int currCol = cols - 1; currCol >= 0; currCol--)
                    {
                        matrix[rows - 1 - currRow][currCol] = snake[snakeIndex % snake.Length];

                        snakeIndex++;
                    }
                }
                else
                {
                    for (int currCol = 0; currCol < cols; currCol++)
                    {
                        matrix[rows - 1 - currRow][currCol] = snake[snakeIndex % snake.Length];

                        snakeIndex++;
                    }
                }
            }

            int[] commandInfo = ReadAnArrayFromTheConsole();

            int targetRow = commandInfo[0];
            int targetCol = commandInfo[1];
            int radius = commandInfo[2];

            ShootTheTarget(targetRow, targetCol, radius);

            FindTheCharsWithDefaultValuesAndCollapseTheCol();

            PrintMatrix();
        }

        private static void FindTheCharsWithDefaultValuesAndCollapseTheCol()
        {
            for (int row = matrix.Length - 1; row >= 0; row--)
            {
                for (int col = 0; col < matrix[row].Length; col++)
                {
                    if (matrix[row][col] != '\0')
                    {
                        continue;
                    }

                    CollapseTheCol(row, col);
                }
            }
        }

        private static void CollapseTheCol(int row, int col)
        {
            while (matrix[row][col] == '\0')
            {
                for (int currRow = row; currRow >= 1; currRow--)
                {
                    matrix[currRow][col] = matrix[currRow - 1][col];
                }

                matrix[0][col] = ' ';
            }
        }

        private static void ShootTheTarget(int targetRow, int targetCol, int radius)
        {
            for (int row = 0; row < matrix.Length; row++)
            {
                for (int col = 0; col < matrix[row].Length; col++)
                {
                    bool cellIsInTheRadius = Math.Pow((col - targetCol), 2) + Math.Pow((row - targetRow), 2) <= Math.Pow(radius, 2);

                    if (cellIsInTheRadius)
                    {
                        matrix[row][col] = '\0';
                    }
                }
            }
        }

        private static int[] ReadAnArrayFromTheConsole()
        {
            return Console.ReadLine().Split().Select(int.Parse).ToArray();
        }

        private static void PrintMatrix()
        {
            StringBuilder result = new StringBuilder();
            for (int row = 0; row < matrix.Length; row++)
            {
                for (int col = 0; col < matrix[row].Length; col++)
                {
                    result.Append(matrix[row][col]);
                }

                result.AppendLine();
            }

            Console.Write(result.ToString());
        }
    }
}