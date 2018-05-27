using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class AuditIncident
    {
        public string ID;
        public List<AuditOne> Audit;

        public void AddInfo(AuditOne a)
        {
            Audit.Add(a);
        }

        public List<TimeSpan> CheckFirstRequest()
        {
            string person = "";
            string group = "";
            DateTime t = DateTime.Now;
            List<TimeSpan> differences = new List<TimeSpan>();
            TimeSpan difference;

            bool fl = false;
            foreach (var aud in Audit)
            {
                if (aud.Field == "Назначено Группе")
                {
                    fl = true;
                }
            }
            if (fl == false)
            {
                foreach (var x in Audit)
                {
                    if (x.Field == "Назначено сотруднику")
                    {
                        person = x.NEW_VAL;
                        t = x.Time;
                    }
                    if ((x.Field == "Счетчик запросов информации") && x.NEW_VAL == "1")
                    {
                        difference = x.Time - t;
                        differences.Add(difference);
                        return differences;
                    }
                }
            }
            if (fl == true)
            {
                foreach (var x in Audit)
                {
                    if (x.NEW_VAL == "МЦТП ИТ поддержка CRM-системы")
                    {
                        group = "МЦТП ИТ поддержка CRM-системы";
                        t = x.Time;
                    }
                    if (x.Field == "Статус" && x.NEW_VAL == "5 Выполнен")
                    {
                        difference = x.Time - t;
                        differences.Add(difference);
                        return differences;
                    }
                    if ((x.Field == "Счетчик запросов информации"))
                    {
                        difference = x.Time - t;
                        differences.Add(difference);
                        return differences;
                    }
                    if (x.OLD_VAL == "МЦТП ИТ поддержка CRM-системы")
                    {
                        difference = x.Time - t;
                        differences.Add(difference);
                    }
                    
                }
            }
            
            return differences;

        }
        public AuditIncident()
        {
            Audit = new List<AuditOne>();
        }
    }
}
