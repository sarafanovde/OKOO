using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            
            List<Incident> ims = new List<Incident> ();

            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = true;
            excelApp.Workbooks.Open(@"C:\incident.xlsx");
          
            Excel.Worksheet currentSheet = (Excel.Worksheet)excelApp.Workbooks[1].Worksheets[1];
            int iLastRow = currentSheet.Cells[currentSheet.Rows.Count, "A"].End[Excel.XlDirection.xlUp].Row;  //последняя заполненная строка в столбце А
            var arrData = (object[,])currentSheet.Range["A1:D" + iLastRow].Value;
            

            for (int i = 2; i<=iLastRow; i++)
            {
                ims.Add(new Incident(arrData[i, 1].ToString(), arrData[i, 4].ToString(), false, false));
            }
            

            excelApp.Workbooks.Open(@"C:\audit.xlsx");
            currentSheet = (Excel.Worksheet)excelApp.Workbooks[2].Worksheets[1];
            iLastRow = currentSheet.Cells[currentSheet.Rows.Count, "A"].End[Excel.XlDirection.xlUp].Row;  //последняя заполненная строка в столбце А
            arrData = (object[,])currentSheet.Range["A1:O" + iLastRow].Value;
            string last = arrData[2, 2].ToString();
            AuditIncident temp = new AuditIncident();
            Incident im;
            for (int i = 2; i <=iLastRow; i++)
            {
                if (arrData[i,2].ToString() != last)
                {
                    im = ims.Find(x => x.Number == last);
                    if (im != null)
                    im.audit = temp;
                    temp = new AuditIncident();
                    last = arrData[i, 2].ToString();
                }
                var ID = arrData[i, 2].ToString();
                var OLD_VAL = arrData[i, 3] == null ? "":arrData[i,3].ToString();
                var NEW_VAL = arrData[i, 4] == null ? "" : arrData[i, 4].ToString();
                var Person = arrData[i, 8] == null ? "" : arrData[i, 8].ToString();
                var Time = (DateTime)arrData[i, 9];
                var Field = arrData[i, 15] == null ? "" : arrData[i, 15].ToString();
                temp.Audit.Add(new AuditOne() { ID = ID, OLD_VAL = OLD_VAL, NEW_VAL = NEW_VAL, Person = Person, Time = Time, Field = Field });

            }
            im = ims.Find(x => x.Number == last);
            if (im != null)
                im.audit = temp;

            excelApp.Workbooks.Open(@"C:\protocol.xlsx");
            currentSheet = (Excel.Worksheet)excelApp.Workbooks[3].Worksheets[1];
            iLastRow = currentSheet.Cells[currentSheet.Rows.Count, "A"].End[Excel.XlDirection.xlUp].Row;  //последняя заполненная строка в столбце А
            arrData = (object[,])currentSheet.Range["A1:O" + iLastRow].Value;
            last = arrData[2, 1].ToString();
            ProtocolIncident tempprotocol = new ProtocolIncident();
            for (int i = 2; i <= iLastRow; i++)
            {
                if (arrData[i, 1].ToString() != last)
                {
                    im = ims.Find(x => x.Number == last);
                    if (im != null)
                        im.protocol = tempprotocol;
                    tempprotocol = new ProtocolIncident();
                    last = arrData[i, 1].ToString();
                }
                var ID = arrData[i, 1].ToString();
                var Added_By = arrData[i, 2] == null ? "" : arrData[i, 2].ToString();
                var Added_time = (DateTime)arrData[i, 3];
                var Comments = arrData[i, 4] == null ? "" : arrData[i, 4].ToString();
                var Type = arrData[i, 5] == null ? "" : arrData[i, 5].ToString();
                tempprotocol.Protocol.Add(new ProtocolOne() { ID = ID, Added_by = Added_By, Added_Time = Added_time, Comments = Comments, Type = Type });

            }
            im = ims.Find(x => x.Number == last);
            if (im != null)
                im.protocol = tempprotocol;

            excelApp.Workbooks.Open(@"C:\CAPS.xlsx");
            currentSheet = (Excel.Worksheet)excelApp.Workbooks[4].Worksheets[1];
            iLastRow = currentSheet.Cells[currentSheet.Rows.Count, "A"].End[Excel.XlDirection.xlUp].Row;  //последняя заполненная строка в столбце А
            arrData = (object[,])currentSheet.Range["A1:O" + iLastRow].Value;
            List<string> CapsException = new List<string>();

            for (int i = 1; i <= iLastRow; i++)
            {
                CapsException.Add(arrData[i, 1].ToString());
            }

            excelApp.Workbooks.Open(@"C:\result.xlsx");

            currentSheet = (Excel.Worksheet)excelApp.Workbooks[5].Worksheets[1];

            int k = 1;
            
            CheckCasp caps = new CheckCasp(CapsException);
            CheckForCorrectWords checher = new CheckForCorrectWords();
            MinWordSolution min = new MinWordSolution();

            Stopwatch SW = new Stopwatch(); // Создаем объект
            SW.Start(); // Запускаем
            for (int i = 0; i < ims.Count(); i++)
            {
                Incident inc = ims[i];
                checher.SetDataIncident(ims[i]);
                min.SetDataIncident(ims[i]);
                var cps = caps.GetResult(ims[i]);
                var differences = ims[i].audit.CheckFirstRequest();
                var capsProt = caps.GetResultProtocol(ims[i]);
                var checkCorrectWordsProt = checher.GetResultProtocol();
                string result = "";
                foreach (var x in differences)
                {
                    result += x.ToString();
                }
                currentSheet.Cells[k, 1] = ims[i].Number; 
                currentSheet.Cells[k, 2] = ims[i].Solution;
                currentSheet.Cells[k, 3] = ims[i].BadProtocol;
                currentSheet.Cells[k, 4] = checher.GetResult();
                currentSheet.Cells[k, 5] = "больше 10 слов " + min.GetResult();
                currentSheet.Cells[k, 6] = "слова капсом " + cps;
                currentSheet.Cells[k, 7] = "Запрос информации по аудиту" + result;
                currentSheet.Cells[k, 8] = checkCorrectWordsProt;
                currentSheet.Cells[k, 9] = "меньше трех слов" + min.GetResultForProtocol();
                currentSheet.Cells[k, 10] = "слова капсом в протоколе" + capsProt;

                k++;
            }
            SW.Stop(); //Останавливаем
            
            Console.WriteLine("Время исполнения " + Convert.ToString(SW.Elapsed.Seconds));
            excelApp.Quit();

            Console.ReadKey();

        }
    }
}
