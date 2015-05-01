// Created 2014 08 01 3:59 PM
// Changed 2014 08 04 9:18 PM
 
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
 
namespace LoyaltyGroup
{
    internal static class Program
    {
        private static void Main()
        {
            // замеряем скорость выполнения
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            // входной файл в массив символов, и сразу же чистим память
            var charArray = new StreamReader("input.txt").ReadToEnd().ToCharArray();
            GC.Collect();
 
            // выбираем оптимальное число потоков и создаем количество мапов равных числу потоков
            int processorCount = Environment.ProcessorCount;
            var arrayMapWordToRepeat = new Dictionary<string, int>[processorCount];
 
            // точки разделяющие массив между потоками
            var charPoint = new int[processorCount + 1];
            charPoint[0] = 0;
            charPoint[processorCount] = charArray.Length;
            for (int i = 1; i < processorCount; i++)
            {
                charPoint[i] = charArray.Length;
                for (var j = (int) ((charArray.LongLength*i)/processorCount); j < charArray.Length; j++)
                {
                    if (charArray[j] == ' ' && charPoint[i] != charPoint[i - 1])
                    {
                        charPoint[i] = j + 1;
                        break;
                    }
                }
            }
 
            // заполняем карту в параллельных потоках
            // ReSharper disable AccessToModifiedClosure
            Parallel.For(0, processorCount, thNumber =>
            {
                // инициализируем все мапы
                arrayMapWordToRepeat[thNumber] = new Dictionary<string, int>();
                // позиция с которой начинается новое слово
                int carretStartWord = charPoint[thNumber];
                // если первая литера в отрывке не является буквой перемещаем каретку
                for (int i = carretStartWord; i < charArray.Length; i++)
                {
                    char leter = charArray[i];
                    if ((leter >= 'A' && leter <= 'Z') || (leter >= 'a' && leter <= 'z'))
                    {
                        carretStartWord = i;
                        break;
                    }
                }
                // нахождение слов
                for (int i = carretStartWord; i < charPoint[thNumber + 1]; i++)
                {
                    // кешируем текущий символ
                    char leter = charArray[i];
                    if ((leter >= 'a' && leter <= 'z'))
                    {
                        // найдено начало слова епт
                        if (carretStartWord == -1)
                        {
                            carretStartWord = i;
                        }
                    }
                    else if ((leter >= 'A' && leter <= 'Z'))
                    {
                        // приводим к нижнему регистру
                        charArray[i] = (char) (charArray[i] + 32);
                        // найдено начало слова
                        if (carretStartWord == -1)
                        {
                            carretStartWord = i;
                        }
                    }
                    else
                    {
                        // если верно, то найден конец слова
                        // исправлен баг с нахождением слова в конце файла
                        if ((leter != '\'' && leter != '-' && carretStartWord != -1) ||
                            ((i == charArray.Length - 1) && (charArray[i] >= 'a' && charArray[i] <= 'z')))
                        {
                            var newWord = new string(charArray, carretStartWord, i - carretStartWord);
                            if (arrayMapWordToRepeat[thNumber].ContainsKey(newWord))
                            {
                                arrayMapWordToRepeat[thNumber][newWord]++;
                            }
                            else
                            {
                                arrayMapWordToRepeat[thNumber].Add(newWord, 1);
                            }
                            carretStartWord = -1;
                        }
                    }
                }
            });
            // ReSharper restore AccessToModifiedClosure
 
            // собираем результаты со всех мапов в один
            for (int i = 1; i < arrayMapWordToRepeat.Length; i++)
            {
                foreach (var word in arrayMapWordToRepeat[i].Keys)
                {
                    if (arrayMapWordToRepeat[0].ContainsKey(word))
                    {
                        arrayMapWordToRepeat[0][word] += arrayMapWordToRepeat[i][word];
                    }
                    else
                    {
                        arrayMapWordToRepeat[0].Add(word, arrayMapWordToRepeat[i][word]);
                    }
                }
            }
 
            // вывод результатов
            foreach (var source in arrayMapWordToRepeat[0].OrderByDescending(x => x.Value).ToArray().Take(20))
            {
                Console.WriteLine(source.Key + " " + source.Value);
            }
            stopwatch.Stop();
            Console.WriteLine("\nTime: " + stopwatch.ElapsedMilliseconds + "ms\n");
 
            // снова чистим память
            // ReSharper disable RedundantAssignment
            charArray = null;
            arrayMapWordToRepeat = null;
            // ReSharper restore RedundantAssignment
            GC.Collect();
            Console.ReadKey();
        }
    }
}