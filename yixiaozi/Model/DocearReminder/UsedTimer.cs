using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yixiaozi.Model.DocearReminder
{
    public class UsedTimer
    {
        public UsedTimer()
        {
            TimeLog = new List<OneTime>();
        }
        public TimeSpan AllTime {
            get {
                TimeSpan dts = new TimeSpan();
                if (TimeLog==null)
                {
                    return dts;
                }
                foreach (OneTime item in TimeLog)
                {
                    try//避免结束时间为空的问题
                    {
                        if (item.endDate!=null)
                        {
                            TimeSpan newdts = item.endDate - item.startDate;

                            dts =dts.Add(newdts);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                return dts;
            }
        }
        public TimeSpan todayTime { get {
                TimeSpan _todayTime = new TimeSpan();
                if (TimeLog == null)
                {
                    return _todayTime;
                }
                foreach (OneTime item in TimeLog)
                {
                    try//避免结束时间为空的问题
                    {
                        if (item.endDate != null)
                        {
                            TimeSpan newdts = item.endDate - item.startDate;
                            if (item.startDate >= DateTime.Today.AddHours(-8))
                            {
                                _todayTime = _todayTime.Add(newdts);
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                return _todayTime;
            } }
        public List<OneTime> TimeLog { get; set; }
        public Int64 Count
        {
            get
            {
                if (TimeLog==null)
                {
                    return 0;
                }
                return TimeLog.Count;
            }
        }
        public void SetEndDate(Guid id) {
            if (TimeLog==null||TimeLog.Count==0)
            {
                return;
            }
            OneTime log= TimeLog.FirstOrDefault(m => m.ID == id);
            if (log!=null)
            {
                log.endDate = DateTime.Now;
            }
        }
        public void NewOneTime(Guid id) {
            if (TimeLog.Count(m=>m.ID==id)==0)
            {
                TimeLog.Add(new OneTime { ID = id, startDate = DateTime.Now ,endDate= DateTime.Now, Log=""});
            }
        }
    }
    public class OneTime
    {
        public Guid ID { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set;}
        public string Log { get; set; }
    }
}
