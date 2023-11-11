using System;
using System.Net.Http.Headers;

class Programm
{
    public static char[] charArayOfOperations = { '+', '-', '*', '/' };
    public static string[] stringArrayOfOperations = { "+", "-", "*", "/" };
    public static char[] charArrayOfNumbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    static void Main()
    {
        string userInput = Console.ReadLine();

        List<string> nums = GetListOfNumbers(userInput);
        List<string> operations = GetListOfOperations(userInput);

        double result = GetResult(nums, operations);
        string[,] oper = GetIndexArray(operations);

        string[,] sortOper = GetSortedArrayOfOperations(operations, oper);
        Console.WriteLine("Ответ: " + result);
    }

    public static double GetResult(List<string> numbers, List<string> operations)
    {
        string[,] indexes = GetIndexArray(operations);
        string[,] sortedIndexes = GetSortedArrayOfOperations(operations, indexes);

        int indexOfFirstNum = Convert.ToInt32(sortedIndexes[2, 0]);
        double result = Convert.ToDouble(numbers[indexOfFirstNum]);

        int i = 0;
        while (i < operations.Count)
        {
            if (sortedIndexes[0, i] == "/")
            {
                int indexOfNum = Convert.ToInt32(sortedIndexes[2, i]);
                if (i != 0 && Convert.ToInt32(sortedIndexes[2, i]) < Convert.ToInt32(sortedIndexes[2, i - 1]))
                { 
                    result = GetDoubleNumber(numbers, (indexOfNum + 1)) / result;
                }
                else result = result / GetDoubleNumber(numbers, (indexOfNum + 1));
            }
            else if(sortedIndexes[0, i] == "*")
            {
                int indexOfNum = Convert.ToInt32(sortedIndexes[2, i]);
                if (i != 0 && Convert.ToInt32(sortedIndexes[2, i]) < Convert.ToInt32(sortedIndexes[2, i - 1]))
                {
                    result = GetDoubleNumber(numbers, (indexOfNum + 1)) * result;
                }
                else result = result * GetDoubleNumber(numbers, (indexOfNum + 1));     
            }
            else if (sortedIndexes[0, i] == "-")
            {
                int indexOfNum = Convert.ToInt32(sortedIndexes[2, i]);
                if (i != 0 && Convert.ToInt32(sortedIndexes[2, i]) < Convert.ToInt32(sortedIndexes[2, i - 1]))
                {
                    result = GetDoubleNumber(numbers, (indexOfNum)) - result;
                }
                else result = result - GetDoubleNumber(numbers, (indexOfNum + 1));   
            }
            else if (sortedIndexes[0, i] == "+")
            {
                int indexOfNum = Convert.ToInt32(sortedIndexes[2, i]);
                if (i != 0 && Convert.ToInt32(sortedIndexes[2, i]) < Convert.ToInt32(sortedIndexes[2, i - 1]))
                {
                    result = GetDoubleNumber(numbers, (indexOfNum)) + result;
                }
                else result = result + GetDoubleNumber(numbers, (indexOfNum + 1));
            }
            i++;
        }
        return result;
    } 

    public static string[,] GetSortedArrayOfOperations(List<string> operations, string[,] indexes)
    {
        string[,] sortedOperations = new string[3, operations.Count];

        int j = 0;
        while( j < operations.Count() )
        {
            int i = 0;
            while (i < operations.Count())
            {
                if (indexes[1, i] == "1")
                {
                    sortedOperations[0, j] = indexes[0, i];
                    sortedOperations[1, j] = indexes[1, i];
                    sortedOperations[2, j] = indexes[2, i];

                    j++;
                }
                i++;
            }
            int c = 0;
            while (c < operations.Count())
            {
                if (indexes[1, c] == "0")
                {
                    sortedOperations[0, j] = indexes[0, c];
                    sortedOperations[1, j] = indexes[1, c];
                    sortedOperations[2, j] = indexes[2, c];

                    j++;
                }
                c++;
            }
        }
        return sortedOperations;
    }

    /// <summary>
    /// Переводит элемент списка в число double(список, индекс элемента)
    /// </summary>
    /// <param name="numbers"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static double GetDoubleNumber(List<string> numbers, int index)
    {
        return Convert.ToDouble(numbers[index]);
    }

    /// <summary>
    /// Создаёт двухмерный массив(в первой строке операция, во второй индекс значимости, в третьей порядковый индекс)
    /// </summary>
    /// <param name="operations"></param>
    /// <returns></returns>
    public static string[,] GetIndexArray(List<string> operations)
    {
        string[,] indexOfOperations = new string[3, operations.Count];
        
        int i = 0;
        int index = -1;
        while(i <  operations.Count)
        {
            if (operations[i] == "/" || operations[i] == "*")
            {
                index += 2;
            }
            else if (operations[i] == "-" || operations[i] == "+")
            {
                index++;
            }

            indexOfOperations[0, i] = operations[i];
            indexOfOperations[1, i] = Convert.ToString(index);
            indexOfOperations[2, i] = Convert.ToString(i);
            i++;
            index = -1;
        }
        return indexOfOperations;
    }
    /// <summary>
    /// Создает из строки список с числами
    /// </summary>
    /// <param name="userInput"></param>
    /// <returns></returns>
    public static List<string> GetListOfNumbers(string userInput)
    {
        string inputWithoutSpace = userInput.Replace(" ", "");

        string strokeOfNums = ReplaceChar( inputWithoutSpace, charArayOfOperations, ' ');
        string[] nums = strokeOfNums.Trim().Split(' ');

        List<string> numbers = new List<string>();

        int i = 0;
        while (i < nums.Length)
        {
            numbers.Add(nums[i]);
            i++;
        }
        return numbers;
    }

    /// <summary>
    /// Создает из строки список с операциями
    /// </summary>
    /// <param name="userInput"></param>
    /// <returns></returns>
    public static List<string> GetListOfOperations(string userInput)
    {
        List<string> operations = new List<string>();
        
        string strokeOfOperations = ReplaceChar(userInput, charArrayOfNumbers, ' ');
        string[] arrayOfOperations = strokeOfOperations.Split(" ");
        
        int i = 0;
        int k = 0;
        while( i < arrayOfOperations.Length)
        {
            if (k < stringArrayOfOperations.Length && arrayOfOperations[i] == stringArrayOfOperations[k])
            {
                operations.Add(arrayOfOperations[i]);
                i++;
                k = 0;
            }
            else if(k != stringArrayOfOperations.Length && arrayOfOperations[i] != stringArrayOfOperations[k])
            {
                k++;
            }
            else if(k == stringArrayOfOperations.Length) 
            {
                i++;
                k = 0;
            }
        }
        return operations;    
    }

    /// <summary>
    /// Заменяет символы в строке на указанные (строка, массив символов на которые нужно заменить, символ на который заменяем)
    /// </summary>
    /// <param name="userInput"></param>
    /// <param name="objects"></param>
    /// <param name="changingChar"></param>
    /// <returns></returns>
    public static string ReplaceChar(string userInput, char[] objects, char changingChar)
    {
        int i = 0;
        while (i < objects.Length)
        {
            char replacementChar = objects[i];
            userInput = userInput.Replace(replacementChar, changingChar);
            i++;
        }
        return userInput;
    }
}