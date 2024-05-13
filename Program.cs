// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;

class WordSearchGame
{
    static void Main(string[] args)
    {
        // List of words to include in the word search puzzle
        List<string> words = new List<string> { "apple", "banana", "orange", "grape", "kiwi","mango", "tangerine","berry", "blueberries" };

        // Size of the grid
        int rows = 30;
        int cols = 30;

        // Create a grid with random letters
        char[,] grid = GenerateGrid(rows, cols);

        // Place the words in the grid
        PlaceWords(words, grid);

        // Display the word search puzzle
        Console.WriteLine("Word Search Puzzle:");
        DisplayGrid(grid);

        // Display the solution
        Console.WriteLine("\nSolution:");
        DisplaySolution(words, grid);
    }

    static char[,] GenerateGrid(int rows, int cols)
    {
        char[,] grid = new char[rows, cols];
        Random random = new Random();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                // Generate a random letter (ASCII value between 65 and 90 for uppercase letters)
                grid[i, j] = (char)(random.Next(26) + 'A');
            }
        }

        return grid;
    }

    static void PlaceWords(List<string> words, char[,] grid)
    {
        Random random = new Random();

        foreach (string word in words)
        {
            // Choose a random direction for the word (horizontal, vertical, or diagonal)
            int direction = random.Next(3);

            // Choose random starting position
            int row = random.Next(grid.GetLength(0));
            int col = random.Next(grid.GetLength(1));

            // Check if the word fits in the chosen direction
            int wordLength = word.Length;
            int maxRow = grid.GetLength(0) - 1;
            int maxCol = grid.GetLength(1) - 1;

            switch (direction)
            {
                case 0: // Horizontal
                    while (col + wordLength > maxCol)
                    {
                        col = random.Next(grid.GetLength(1));
                    }
                    for (int i = 0; i < wordLength; i++)
                    {
                        grid[row, col + i] = word[i];
                    }
                    break;
                case 1: // Vertical
                    while (row + wordLength > maxRow)
                    {
                        row = random.Next(grid.GetLength(0));
                    }
                    for (int i = 0; i < wordLength; i++)
                    {
                        grid[row + i, col] = word[i];
                    }
                    break;
                case 2: // Diagonal
                    while (row + wordLength > maxRow || col + wordLength > maxCol)
                    {
                        row = random.Next(grid.GetLength(0));
                        col = random.Next(grid.GetLength(1));
                    }
                    for (int i = 0; i < wordLength; i++)
                    {
                        grid[row + i, col + i] = word[i];
                    }
                    break;
            }
        }
    }

    static void DisplayGrid(char[,] grid)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                Console.Write(grid[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    static void DisplaySolution(List<string> words, char[,] grid)
    {
        foreach (string word in words)
        {
            Console.WriteLine($"Word: {word}");

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    // Find the starting position of the word in the grid
                    if (grid[i, j] == word[0])
                    {
                        // Check if the word can be found horizontally
                        if (CheckHorizontal(word, grid, i, j))
                        {
                            Console.WriteLine($"Horizontal: ({i}, {j}) to ({i}, {j + word.Length - 1})");
                        }

                        // Check if the word can be found vertically
                        if (CheckVertical(word, grid, i, j))
                        {
                            Console.WriteLine($"Vertical: ({i}, {j}) to ({i + word.Length - 1}, {j})");
                        }

                        // Check if the word can be found diagonally
                        if (CheckDiagonal(word, grid, i, j))
                        {
                            Console.WriteLine($"Diagonal: ({i}, {j}) to ({i + word.Length - 1}, {j + word.Length - 1})");
                        }
                    }
                }
            }
        }
    }

    static bool CheckHorizontal(string word, char[,] grid, int row, int col)
    {
        if (col + word.Length > grid.GetLength(1))
        {
            return false;
        }

        for (int i = 0; i < word.Length; i++)
        {
            if (grid[row, col + i] != word[i])
            {
                return false;
            }
        }

        return true;
    }

    static bool CheckVertical(string word, char[,] grid, int row, int col)
    {
        if (row + word.Length > grid.GetLength(0))
        {
            return false;
        }

        for (int i = 0; i < word.Length; i++)
        {
            if (grid[row + i, col] != word[i])
            {
                return false;
            }
        }

        return true;
    }

    static bool CheckDiagonal(string word, char[,] grid, int row, int col)
    {
        if (row + word.Length > grid.GetLength(0) || col + word.Length > grid.GetLength(1))
        {
            return false;
        }

        for (int i = 0; i < word.Length; i++)
        {
            if (grid[row + i, col + i] != word[i])
            {
                return false;
            }
        }

        return true;
    }
}

