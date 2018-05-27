using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class KMP
    {
        //Префикс-функция для КМП
        private int[] PrefFunc(string x)
        {
            int[] res = new int[x.Length];
            int i = 0;
            int j = -1;
            res[0] = -1;
            while (i < x.Length - 1)
            {
                while ((j >= 0) && (x[j] != x[i]))
                    j = res[j];
                i++;
                j++;
                if (x[i] == x[j])
                    res[i] = res[j];
                else
                    res[i] = j;
            }
            return res;
        }
        //Функция поиска алгоритмом КМП. x - что ищем s - где ищем
        //Вернет true если найдено x в s
        public bool KMP_algo(string x, string s)
        {
            bool nom = false;
            s = s.ToLower();
            x = x.ToLower();
            if (x.Length > s.Length) return nom; 
            int[] d = PrefFunc(x);
            int i = 0, j;
            while (i < s.Length)
            {
                for (i = i, j = 0; (i < s.Length) && (j < x.Length); i++, j++) while ((j >= 0) && (x[j] != s[i]))
                        j = d[j];
                if (j == x.Length)
                    return true;
            }
            
            return false; //Возвращение результата поиска
        }
    }
}
