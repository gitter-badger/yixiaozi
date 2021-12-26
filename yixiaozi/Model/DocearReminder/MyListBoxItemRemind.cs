using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yixiaozi.Model.DocearReminder
{
    public class MyListBoxItemRemind
    {
        public MyListBoxItemRemind()
        {
            isTask = true;
        }
        public string Text { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public int editTime { get; set; }
        public string Value { get; set; }
        public bool IsShow { get; set; }
        public string remindertype;
        public int rhours;
        public int rdays;
        public int rWeek;
        public char[] rweeks;
        public int rMonth;
        public int ryear;
        public int level;
        public int rtaskTime;
        public string IsDaka { get; set; }
        public string IsView { get; set; }
        public int DakaDay;
        public int[] DakaDays;
        public int LeftDakaDays;
        public bool isEncrypted;
        public bool isImportant;
        public string IsJinian { get; set; }
        public DateTime jinianDatetime { get; set; }
        public DateTime EndDate { get; set; }
        public string IDinXML;
        public string rootPath { get; set; }
        public int ebstring { get; set; }
        public bool ISReminderOnly { get; set; }
        public bool isTask { get; set; }
        public string link { get; set; }
    }
    public class mindmapfile
    {
        public string name { get; set; }
        public string filePath { get; set; }
    }
    public class MyListBoxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public bool IsSpecial { get; set; }
    }
    public class Reminder
    {
        public List<ReminderItem> reminders = new List<ReminderItem>();
        public List<Fenlei> Fenleis = new List<Fenlei>();
        public int editCount;
        public int reminderCount;
        public List<string> NoFenleiMindmaps
        {
            get
            {
                List<string> nofenleimindmaps = new List<string>();
                List<string> hasfenleimindmaps = new List<string>();
                foreach (Fenlei item in Fenleis)
                {
                    hasfenleimindmaps.AddRange(item.MindMaps);
                }
                foreach (ReminderItem item in reminders)
                {
                    if (!hasfenleimindmaps.Contains(item.mindmap) && !nofenleimindmaps.Contains(item.mindmap))
                    {
                        nofenleimindmaps.Add(item.mindmap);
                    }
                }
                return nofenleimindmaps;
            }
        }
        public string mindmaps
        {
            get
            {
                string mindmaps = "";
                foreach (Fenlei item in Fenleis)
                {
                    foreach (string mindmap in item.MindMaps)
                    {
                        mindmaps += mindmap + ";";
                    }
                }
                return mindmaps;
            }
        }
    }

    public class ReminderItem
    {
        public string name;
        public string nameFull;
        public DateTime time;
        public string mindmap;
        public string mindmapPath;
        public int editCount;
        public bool isCompleted = false;
        public bool isCurrect = false;
        public bool isNew = true;
        public DateTime? comleteTime = null;
        public List<DateTime> editTime;
        public string remindertype;
        public int rhours;
        public int rdays;
        public int rWeek;
        public char[] rweeks;
        public int rMonth;
        public int ryear;
        public double tasktime;
        public int tasklevel;
        public string ID;
        public int ebstring;
        public bool isview = false;
    }
    public class Fenlei
    {
        public string Name;
        public List<string> MindMaps;
    }
    public class node
    {
        public string mindmapName { get; set; }
        public string mindmapPath { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public DateTime editDateTime { get; set; }
        public string IDinXML;
    }
}
