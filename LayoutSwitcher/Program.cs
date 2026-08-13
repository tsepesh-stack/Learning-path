using System.Windows.Forms;
using System.Text.Json;
namespace SL2{
class Program
{
    [STAThread] // обязательная атрибут для работы с буфером
    static void Main()
    {
        Dictionary<char, char> engRus = new Dictionary<char, char>()
        {
            {'q','й'}, {'w','ц'}, {'e','у'}, {'r','к'}, {'t','е'},
            {'y','н'}, {'u','г'}, {'i','ш'}, {'o','щ'}, {'p','з'},
            {'a','ф'}, {'s','ы'}, {'d','в'}, {'f','а'}, {'g','п'},
            {'h','р'}, {'j','о'}, {'k','л'}, {'l','д'},
            {'z','я'}, {'x','ч'}, {'c','с'}, {'v','м'}, {'b','и'},
            {'n','т'}, {'m','ь'}
        };

        Dictionary<char, char> rusEng = engRus.ToDictionary(x => x.Value, x => x.Key);

        // Берём текст из буфера
        if (!Clipboard.ContainsText())
        {
            Console.WriteLine("В буфере нет текста!");
            return;
        }

        string text = Clipboard.GetText();
        string result = "";
        
        foreach (char c in text)
        {
            if (engRus.ContainsKey(c)) result += engRus[c];
            else if (rusEng.ContainsKey(c)) result += rusEng[c];
            else result += c;
        }

        // Кладём результат обратно в буфер
        Clipboard.SetText(result);
        Console.WriteLine($"Было: {text}");
        Console.WriteLine($"Стало: {result}");
        Console.WriteLine("Результат скопирован в буфер. Нажми Ctrl+V чтобы вставить.");
    }
}
}