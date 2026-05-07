using System;
namespace LS2
{
    class Program
    {
        static void Main()
        {
            System.Console.Write("Введите текст: ");
            string text = Console.ReadLine();
            Dictionary<char, char> engRus = new Dictionary<char, char>(){
                {'q', 'й'}, {'w', 'ц'}, {'e', 'у'}, {'r', 'к'}, {'t', 'е'},
                {'y', 'н'}, {'u', 'г'}, {'i', 'ш'}, {'o', 'щ'}, {'p', 'з'},
                {'a', 'ф'}, {'s', 'ы'}, {'d', 'в'}, {'f', 'а'}, {'g', 'п'},
                {'h', 'р'}, {'j', 'о'}, {'k', 'л'}, {'l', 'д'},
                {'z', 'я'}, {'x', 'ч'}, {'c', 'с'}, {'v', 'м'}, {'b', 'и'},
                {'n', 'т'}, {'m', 'ь'}
            };
            Dictionary<char, char> rusEng  = engRus.ToDictionary(x => x.Value, x => x.Key);
            string result = "";
            foreach (char c in text)
            {
                if (engRus.ContainsKey(c))
                    result += engRus[c];
                else if (rusEng.ContainsKey(c)) 
                    result += rusEng[c];
                else
                    result += c; 
            }
            Console.WriteLine(result);
        }
    }
}
