using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class MoreLess 
    {
        
        // Метод вернет true если число слов больше числа num
        public static bool CheckMore(string text, int num)
        {
            var result = text.Split(new[] { ' ', '\n', '!', '?', '.', ',', ':' }, StringSplitOptions.RemoveEmptyEntries);
            if (result.Count() >= num)
                return true;
            else
                return false;
        }
        // Метод вернет true если число слов менее числа num
        public static bool CheckLess(string text, int num)
        {
            var result = text.Split(new[] { ' ', '\n', '!', '?', '.', ',', ':' }, StringSplitOptions.RemoveEmptyEntries);
            if (result.Count() <= num)
                return true;
            else
                return false;
        }
        //TODO Описание метода поиска "!!!" и "???"

    }
}
