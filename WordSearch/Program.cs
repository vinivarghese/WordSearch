using System;
using System.Data;
using System.Linq;

namespace WordSearch
{
    class Program
    {
        static char[,] Grid = new char[,] {
            {'C', 'P', 'K', 'X', 'O', 'I', 'G', 'H', 'S', 'F', 'C', 'H'}, //--0
            {'Y', 'G', 'W', 'R', 'I', 'A', 'H', 'C', 'Q', 'R', 'X', 'K'}, //--1
            {'M', 'A', 'X', 'I', 'M', 'I', 'Z', 'A', 'T', 'I', 'O', 'N'}, //--2
            {'E', 'T', 'W', 'Z', 'N', 'L', 'W', 'G', 'E', 'D', 'Y', 'W'}, //--3
            {'M', 'C', 'L', 'E', 'L', 'D', 'N', 'V', 'L', 'G', 'P', 'T'}, //--4
            {'O', 'J', 'A', 'A', 'V', 'I', 'O', 'T', 'E', 'E', 'P', 'X'}, //--5
            {'C', 'D', 'B', 'P', 'H', 'I', 'A', 'W', 'V', 'X', 'U', 'I'}, //--6
            {'L', 'G', 'O', 'S', 'S', 'B', 'R', 'Q', 'I', 'A', 'P', 'K'}, //--7
            {'E', 'O', 'I', 'G', 'L', 'P', 'S', 'D', 'S', 'F', 'W', 'P'}, //--8
            {'W', 'F', 'K', 'E', 'G', 'O', 'L', 'F', 'I', 'F', 'R', 'S'}, //--9
            {'O', 'T', 'R', 'U', 'O', 'C', 'D', 'O', 'O', 'F', 'T', 'P'}, //--10
            {'C', 'A', 'R', 'P', 'E', 'T', 'R', 'W', 'N', 'G', 'V', 'Z'}  //--11
            //0    1    2    3    4    5    6    7    8    9    10   11
        };

        static string[] Words = new string[] 
        {
            "CARPET",
            "CHAIR",
            "DOG",
            "BALL",
            "DRIVEWAY",
            "FISHING",
            "FOODCOURT",
            "FRIDGE",
            "GOLF",
            "MAXIMIZATION",
            "PUPPY",
            "SPACE",
            "TABLE",
            "TELEVISION",
            "WELCOME",
            "WINDOW"
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Word Search");

            for (int y = 0; y < 12; y++)
            {
                for (int x = 0; x < 12; x++)
                {
                    Console.Write(Grid[y, x]);
                    Console.Write(' ');
                }
                Console.WriteLine("");
            }

            Console.WriteLine("");
            Console.WriteLine("Found Words");
            Console.WriteLine("------------------------------");

            FindWords();

            Console.WriteLine("------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Press any key to end");
            Console.ReadKey();
        }

        private static void FindWords()
        {
            //Find each of the words in the grid, outputting the start and end location of
            //each word, e.g.
            //PUPPY found at (10,7) to (10, 3)

            foreach(string word in Words)
            {
                char[] wordChars = word.ToCharArray();
                char[] reverseWordChars = wordChars.Reverse().ToArray();
                string reverseWord = new string(reverseWordChars);

                string outputMessage = "";
                int startX = -1, endX = -1, startY = -1, endY = -1;

                for (int i = 0; i < Grid.GetLength(0); i++)
                {
                    for (int j = 0; j < Grid.GetLength(1); j++)
                    {
                        outputMessage = "";
                        startX = endX = startY = endY = -1;

                        if (Grid[i,j] == wordChars[0])
                        {
                            if(GenerateHorizontalWordFromChar(i, j, word))
                            {
                                startX = j;
                                startY = i;
                                endX = j + (wordChars.Length - 1);
                                endY = i;
                                break;
                            }

                            if (GenerateVerticalWordFromChar(i, j, word))
                            {
                                startX = j;
                                startY = i;
                                endX = j;
                                endY = i + (wordChars.Length - 1);
                                break;
                            }

                            if (GenerateBackwardDiagonalWordFromChar(i, j, word))
                            {
                                startX = j;
                                startY = i;
                                endX = j + (wordChars.Length - 1);
                                endY = i + (wordChars.Length - 1);
                                break;
                            }

                            if (GenerateForwardDiagonalWordFromChar(i, j, word))
                            {
                                startX = j;
                                startY = i;
                                endX = j - (wordChars.Length - 1);
                                endY = i - (wordChars.Length - 1);
                                break;
                            }
                        }

                        if (Grid[i, j] == reverseWordChars[0])
                        {
                            if (GenerateHorizontalWordFromChar(i, j, reverseWord))
                            {
                                startX = j + (reverseWordChars.Length - 1);
                                startY = i;
                                endX = j;
                                endY = i;
                                break;
                            }

                            if (GenerateVerticalWordFromChar(i, j, reverseWord))
                            {
                                startX = j;
                                startY = i + (wordChars.Length - 1);
                                endX = j;
                                endY = i;
                                break;
                            }

                            if (GenerateBackwardDiagonalWordFromChar(i, j, reverseWord))
                            {
                                startX = j + (reverseWordChars.Length - 1);
                                startY = i + (reverseWordChars.Length - 1);
                                endX = j;
                                endY = i;
                                break;
                            }

                            if (GenerateForwardDiagonalWordFromChar(i, j, reverseWord))
                            {
                                startX = j - (reverseWordChars.Length - 1);
                                startY = i + (reverseWordChars.Length - 1);
                                endX = j;
                                endY = i;
                                break;
                            }
                        }

                        
                    }

                    if(startX >= 0 && endX >= 0 && startY >= 0 && endY >= 0)
                    {
                        outputMessage = GenerateOutputMessage(startX, endX, startY, endY, word);
                        Console.WriteLine(outputMessage);
                    }                  
                }
            }

            
        }

        private static bool GenerateHorizontalWordFromChar(int i, int j, string word)
        {
            string generatedWord = "";
            int wordLength = word.Length;

            char[] wordArray = word.ToCharArray();
            int count = 1;

            while (wordLength > 0 && j >= 0 && j < Grid.GetLength(1))
            {
                char[] currentCharArray = wordArray.Take(count).ToArray();
                string currentWord = new string(currentCharArray);

                generatedWord += Grid[i, j];

                if (generatedWord != currentWord)
                {
                    return false;
                }

                j++;
                wordLength--;
                count++;
            }


            if (String.Equals(word, generatedWord))
            {
                return true;
            }

            return false;
        }

        private static bool GenerateVerticalWordFromChar(int i, int j, string word)
        {
            string generatedWord = "";
            int wordLength = word.Length;

            char[] wordArray = word.ToCharArray();
            int count = 1;

            while (wordLength > 0 && i >= 0 && i < Grid.GetLength(0))
            {
                char[] currentCharArray = wordArray.Take(count).ToArray();
                string currentWord = new string(currentCharArray);

                generatedWord += Grid[i, j];

                if (generatedWord != currentWord)
                {
                    return false;
                }

                i++;
                wordLength--;
                count++;
            }

            if (String.Equals(word, generatedWord))
            {
                return true;
            }

            return false;
        }

        private static bool GenerateBackwardDiagonalWordFromChar(int i, int j, string word)
        {
            string generatedWord = "";
            int wordLength = word.Length;
            char[] wordArray = word.ToCharArray();
            int count = 1;

            while (wordLength > 0 && i >= 0 && i < Grid.GetLength(0) && j >= 0 && j < Grid.GetLength(1))
            {
                char[] currentCharArray = wordArray.Take(count).ToArray();
                string currentWord = new string(currentCharArray);

                generatedWord += Grid[i, j];

                if (generatedWord != currentWord)
                {
                    return false;
                }

                i++;
                j++;
                wordLength--;
                count++;
            }

            if (String.Equals(word, generatedWord))
            {
                return true;
            }

            return false;
        }



        private static bool GenerateForwardDiagonalWordFromChar(int i, int j, string word)
        {
            string generatedWord = "";
            int wordLength = word.Length;

            char[] wordArray = word.ToCharArray();
            int count = 1;

            while (wordLength > 0 && i >= 0 && i < Grid.GetLength(0) && j >=0  && j < Grid.GetLength(1))
            {
                char[] currentCharArray = wordArray.Take(count).ToArray();
                string currentWord = new string(currentCharArray);

                generatedWord += Grid[i, j];

                if(generatedWord != currentWord)
                {
                    return false;
                }

                i++;
                j--;
                wordLength--;
                count++;
            }

            if (String.Equals(word, generatedWord))
            {
                return true;
            }

            return false;
        }

        private static string GenerateOutputMessage(int startRow, int endRow, int startColumn, int endColumn, string word)
        {
            string startPosition = "(" + startRow + "," + startColumn + ")";
            string endPosition = "(" + endRow + "," + endColumn + ")";
            string outputMessage = word + " found at " + startPosition + " to " + endPosition;
            return outputMessage;
        }
    }
}
