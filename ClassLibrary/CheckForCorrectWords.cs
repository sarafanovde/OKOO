using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class CheckForCorrectWords
    {
        private Incident im;
        private KMP kmp = new KMP();
        // загружаем инцидент
        public void SetDataIncident(Incident im)
        {
            this.im = im;
        }
        // сделать также для ЗНО потом

        List<string> badWords = new List<string> { "делай", "сделай", "удали", "послушай", "говори", "пиши", "звони", "вводи", "закрой", "выполни", "создай", "посмотри", "смотри"};
        // проверка чтобы не было слов из списка
        // список пока сделан статичесикй
        public string GetResult()
        {
            var result = "";
            var wors = im.Solution.Split(' ');
            foreach (string x in badWords)
            {
                foreach (string k in wors)
                    if (k.ToLower() == x.ToLower())
                        result = result + "" + x;
                
            }
            List<string> badSign = new List<string> { "!!", "??", "!?", "?!", "..." };
            foreach (var x in badSign)
            {
                if (kmp.KMP_algo(x, im.Solution))
                {
                    result = result + " " + x;
                }
            }
            
            return result;
        }
        public string GetResultProtocol()
        {
            var result = "";
            
            foreach (var protocol in im.protocol.Protocol)
            {
                var wors = protocol.Comments.Split(' ');
            
                foreach (string x in badWords)
                {
                    foreach (string k in wors)
                        if (k.ToLower() == x.ToLower())
                            result = result + "" + x;

                }
            }
            List<string> badSign = new List<string> { "!!", "??", "!?", "?!", "..." };
            foreach (var protocol in im.protocol.Protocol)
            {
                var wors = protocol.Comments;
                foreach (var x in badSign)
                {
                    if (kmp.KMP_algo(x, wors))
                    {
                        result = result + " " + x;
                    }
                }
            }
            return result;
        }

    }
}
