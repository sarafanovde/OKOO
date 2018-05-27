using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class CheckCasp
    {
        public string GetResult(Incident im)
        {
            //&& Char.IsDigit(x.Last())!= true && (((Convert.ToInt32(x.Last())>=97 && Convert.ToInt32(x.Last()) <= 113) || (Convert.ToInt32(x.Last()) >= 65 && Convert.ToInt32(x.Last()) >= 91)))
            var str = im.Solution.Split(' ', '\n', '!', '?', '.', ',', ':').Where(x => x == x.ToUpper());
            var result = "";
            if (str.Count() != 0)
            {
                foreach (var x in str)
                {
                    if (x != "")
                    {
                        var k = x.Last();
                        int code = (int)k;

                        if ((!Char.IsDigit(x.Last())) && (code >= 1040 && code <= 1103))
                        {
                            result = result + " " + x;
                        }
                    }
                }
                return result;
            }
            else
                return "";
        }
    }
}
