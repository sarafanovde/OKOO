using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class MinWordSolution
    {
        private Incident im;
        private int minValue = 10;

        // загружаем инцидент
        public void SetDataIncident (Incident im)
        {
            this.im = im;
        }
        // сделать также для ЗНО потом

        // проверка чтобы слов было больше 10
        public bool GetResult()
        {
            return MoreLess.CheckMore(im.Solution, minValue);
        }

        public bool GetResultForProtocol()
        {
            if (im.protocol != null)
                foreach (var x in im.protocol.Protocol)
            {
                if (MoreLess.CheckLess(x.Comments, 3) && x.Type != "Сообщение инженеру")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
