using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Lessons
{
    [Serializable()]
    public class Lesson
    {
        public static int countLessons = 0;
        private static string[] shortType = { "ה", "מ", "ת"};
        public static string[] fullType = { "הרצאה", "מעבדה", "תרגול" };

        private static string[] shortDay = { "א", "ב", "ג", "ד", "ה", "ו" };
        public static string[] fullDay = { "ראשון", "שני", "שלישי", "רבעי", "חמישי", "שישי" };//{ "ראשון", "שני", "שלישי", "רבעי", "חמישי", "שישי" };
        private static int minHour = 8;
        private static int maxHour = 22;

        // the name of the lesson course
        private string _courseName;
        public string courseName
        {
            get { return _courseName; }
            set { _courseName = value; }
        }

        // the type is ה|ת|מ = הרצאה, תרגול , מעבדה
        private string _type;
        public string type {
            get { return _type; }
            set {
                _type = value;
                bool found = false;
                foreach (var item in shortType)
                {
                    if (item.Equals(_type))
                        found = true;
                }
                foreach (var item in fullType)
                {
                    if (item.Equals(_type))
                        found = true;
                }
                if (!found)
                {
                    throw new ArgumentException("הסוג חייב להיות מ(עבדה)|ת(רגול)|ה(רצאה)");
                }
                else
                {
                    _type = _type.Substring(0, 1);
                }

            }
        }

        // the number is sequence number like 1,2,3,4,5
        private int _number;
        public int number {
            get { return _number; }
            set {
                _number = value;
                if (_number <= 0)
                {
                    throw new ArgumentException("מספר הקורס חייב להיות גדול מ-0");
                }
            }
        }

        // the lecturer that teach this
        private string _lecturer;
        public string lecturer
        {
            get { return _lecturer; }
            set { _lecturer = value; }
        }

        // the day of week : ראשון, שני , שלישי , רבעי, חמישי, שישי | א,ב,ג,ד,ה,ו
        private string _day;
        public string day
        {
            get { return _day; }
            set
            {
                _day = value;
                _day = getShortDay();
                if (_day.Length == 0 || ( !(_day.ToCharArray()[0] >= 'א' && _day.ToCharArray()[0] <= 'ו') ) )
                {
                    throw new ArgumentException("היום חייב להיות בפורמט של [א-ו] או [ראשון-שישי]");
                }
                else
                {
                    _day = getShortDay();
                }
            }
        }

        // the start hour of this lesson: 8 -> 08:00
        private int _start;
        public int start
        {
            get { return _start; }
            set {
                _start = value;
                if(_start < minHour)
                {
                    throw new ArgumentException("שעת ההתחלה חייבת להיות אחרי " + minHour.ToString());
                }
                if(end != 0 && _start > end)
                {
                    throw new ArgumentException("שעת התחלה חייבת להיות אחרי שעת סיום!");
                }
            }
        }
        // the end  hour of this lesson: 10 -> 10:00
        private int _end;
        public int end
        {
            get { return _end; }
            set
            {
                if (_end > maxHour)
                {
                    throw new ArgumentException("שעת סיום חייבת להיות לפני " + maxHour.ToString());
                }
                _end = value;
                if (start != 0 && start > _end)
                {
                    throw new ArgumentException("שעת סיום חייבת להיות אחרי שעת התחלה!");
                }
            }
        }
        // the year - must be 1-4
        private int _year;
        public int year
        {
            get { return _year; }
            set
            {
                _year = value;
                if (!(_year >= 1 && _year <= 4))
                {
                    throw new ArgumentException("שנת הקרוס אמור להיות מספר בין 1 ל4");
                }
            }
        }
        public string getYearLetter()
        {
            switch (year)
            {
                case 1: return "א";
                case 2: return "ב";
                case 3: return "ג";
                case 4: return "ד";
                default: return "";
            }
        }

        // the week hour of this lesson: end - start
        private int _weekHour;
        public int weekHour
        {
            get { return _weekHour; }
            set
            {
                _weekHour = value;
                if (_weekHour != (end - start))
                {
                    if (start + _weekHour <= maxHour)
                    {
                        end = start + _weekHour;
                    }
                    else if(end - _weekHour >= minHour)
                    {
                        start = end - _weekHour;
                    }
                    else
                    {
                        _weekHour = end - start;
                    }
                }
            }
        }

        // the class name of this lesson 
        private string _className;
        public string className
        {
            get { return _className; }
            set { _className = value; }
        }

        // CTORS
        public Lesson() { countLessons++; }
        public Lesson(Lesson l)
        {
            this.courseName = l.courseName;
            this.type = l.type;
            this.number = l.number;
            this.lecturer = l.lecturer;
            this.day = l.day;
            this.className = l.className;
            this.end = l.end;
            this.start = l.start;
            this.weekHour = l.weekHour;
            this.year = l.year;
            countLessons++;
        }
        public Lesson(string courseName, string type, int number, string lecturer, string day, int start, int end, int hours, int year, string className = "")
        {
            this.className = className;
            this.courseName = courseName;
            this.day = day;
            this.start = start;
            this.end = end;
            this.weekHour = hours;
            this.year = year;
            this.lecturer = lecturer;
            this.type = type;
            this.number = number;

            /*
            if (type.Length == 0 || 
                 (
                  !type.Equals("ה") && !type.Equals("ת") && !type.Equals("מ") && !type.Equals("הרצאה") && !type.Equals("תרגול") && !type.Equals("מעבדה") 
                 )
                )
            {
                throw new ArgumentException("הסוג חייב להיות מ(עבדה)|ת(רגול)|ה(רצאה)");
            }
            else
            {
                type = type.Substring(0, 1);
            }
            /*
            int i = Array.IndexOf(fullDay, day);
            if (day.Length == 2 && day[1] == '\'')
                day = day[0].ToString();
            int j = Array.IndexOf(shortDay, day);

            if (i < 0 && j < 0)
            {
                throw new ArgumentException("היום חייב להיות בפורמט של [א-ו] או [ראשון-שישי]");
            }
            
            this.day = day;

            if (start < minHour)
            {
                throw new ArgumentException("שעת ההתחלה חייבת להיות אחרי " + minHour.ToString());
            }
            this.start = start;
            if (end > maxHour)
            {
                throw new ArgumentException("שעת סיום חייבת להיות לפני " + maxHour.ToString());
            }
            this.end = end;

            if (start >= end)
            {
                throw new ArgumentException("שעת סיום חייבת להיות אחרי שעת התחלה");
            }

            this.weekHour = end - start;

            this.className = className;
            this.year = year;
            if (!(year >= 1 && year <= 4))
            {
                throw new ArgumentException("שנת הקרוס אמור להיות מספר בין 1 ל4");
            }
            */
            countLessons++;
        }

        // convert the time int to time string : 8 -> 08:00 , 20 -> 20:00
        public string getStartHour()
        {
            return getStrHour(this.start);
        }
        public string getEndHour()
        {
            return getStrHour(this.end);
        }
        private string getStrHour(int h)
        {
            string str = "";
            if (h < 10)
            {
                str += "0";
            }
            str += h.ToString() + ":00";
            return str;
        }

        // get day from full to shory , and opposite
        public string getShortDay()
        {
            int index = Array.IndexOf(fullDay, this.day);
            if (index >= 0 && index <= shortDay.Length)
                return shortDay[index];
            else
                return this.day;
        }
        public string getFullDay()
        {
            int index = Array.IndexOf(shortDay, this.day);
            if (index >= 0 && index <= fullDay.Length)
                return fullDay[index];
            else
                return this.day;
        }

        public string getFullType()
        {
            for (int i = 0; i < shortType.Length; i++)
            {
                if (type.Equals(shortType[i]))
                {
                    return fullType[i];
                }
            }
            return "";
        }

        public override int GetHashCode()
        {
            return countLessons;
        }

        // Overload + operator to Move the lesson h hour ahead.
        public static Lesson operator +(Lesson l, int h)
        {
            if (l.end + h < maxHour)
            {
                l.start += h;
                l.end += h;
            }
            return l;
        }
        // Overload + operator to Move the lesson h hour back
        public static Lesson operator -(Lesson l, int h)
        {
            if (l.start - h > minHour)
            {
                l.start -= h;
                l.end -= h;
            }
            return l;
        }

        public Lesson clone()
        {
            return new Lesson(this.courseName, this.type, this.number, this.lecturer, this.day, this.start, this.end , this.weekHour, this.year, this.className);
        }
        public bool sameValue(Lesson l)
        {
            checkNullArgumant(l, "השיעור שאיתו אתה מנסה לבדוק ערכים זהים , לא הוגדר");
            return courseName.Equals(l.courseName) &&
                   type.Equals(l.type) &&
                   number == l.number &&
                   lecturer.Equals(l.lecturer) &&
                   day.Equals(l.day) &&
                   start == l.start &&
                   end == l.end &&
                   year == l.year &&
                   className.Equals(l.className);
        }

        public override bool Equals(Object obj)
        {
            Lesson l = null;
            if(obj is Lesson)
                l = (Lesson)obj;
            else
                return false;
            checkNullArgumant(l, "האובייקט (שיעור) שאיתו אתה מנסה לבדוק השוואת אובייקט , לא הוגדר");

            return object.ReferenceEquals(this, l);
        }
        // this is the same course and the same type
        public static bool operator ==(Lesson l1, Lesson l2)
        {
            if (object.ReferenceEquals(l1, null))
            {
                return object.ReferenceEquals(l2, null);
            }
            return l1.Equals(l2);
        }
        // this isn't the same course and the same type
        public static bool operator !=(Lesson l1, Lesson l2)
        {
            checkNullArgumant(l2, "השיעור שאיתו אתה מנסה לבדוק אי-שיוון , לא הוגדר");
            return !(l1 == l2);
        }

        // the lesson 1 is before lesson 2 and doesn't collision
        public static bool operator <(Lesson l1, Lesson l2)
        {
            checkNullArgumant(l2, "השיעור השני שאיתו אתה מנסה לבדוק שהוא לפני השיעור שלך , לא הוגדר");
            return l1.Before(l2);
        }
        public bool Before(Lesson lsn)
        {
            return (this.getShortDay().CompareTo(lsn.getShortDay()) < 0) ||
                    (this.getShortDay().CompareTo(lsn.getShortDay()) == 0 &&
                     this.end <= lsn.start);
        }
        
        // the lesson 1 is after lesson 2 and doesn't collision
        public static bool operator >(Lesson l1, Lesson l2)
        {
            checkNullArgumant(l2, "השיעור השני שאיתו אתה מנסה לבדוק שהוא אחרי השיעור שלך , לא הוגדר");
            return l1.After(l2);
        }
        public bool After(Lesson lsn)
        {
            return (this.getShortDay().CompareTo(lsn.getShortDay()) > 0) ||
                    (this.getShortDay().CompareTo(lsn.getShortDay()) == 0 &&
                     this.start >= lsn.end);
        }
        
        // the lesson 1 is collision from start in lesson 2
        public static bool operator <=(Lesson l1, Lesson l2)
        {
            checkNullArgumant(l2, "השיעור השני שאיתו אתה מנסה לבדוק שהוא מתנגש מתחילת השיעור שלך , לא הוגדר");
            return l1.collisionBefore(l2);
        }
        public bool collisionBefore(Lesson lsn)
        {
            return (this.getShortDay().CompareTo(lsn.getShortDay()) == 0 &&
                     this.start <= lsn.start && this.end > lsn.start);
        }
        
        // the lesson 1 is collision from end in lesson 2
        public static bool operator >=(Lesson l1, Lesson l2)
        {
            checkNullArgumant(l2, "השיעור השני שאיתו אתה מנסה לבדוק שהוא מתנגש מסוף השיעור שלך , לא הוגדר");
            return l1.collisionAfter(l2);
        }
        public bool collisionAfter(Lesson lsn)
        {
            return (this.getShortDay().CompareTo(lsn.getShortDay()) == 0 &&
                     this.start >= lsn.start && this.start < lsn.end);
        }

        // print the lesson data for the schedule
        public override string ToString()
        {
            /*
            string result = "";
            result += "קורס: " + courseName + "\n";
            result += "סוג השיעור: " + getFullType() + "\n";
            if (!lecturer.Equals(""))
                result += "מרצה: " + lecturer + "\n";
            if (!className.Equals(""))
                result += "כיתה: " + className + "\n";
            result += "שעות: " + getShortDay() + "-" + getEndHour() + "\n";
            result += "יום: " + getFullDay() + "\n";
            return result;
            */
            string result2 = "";
            result2 += courseName + "\n";
            result2 += getFullType() + "\n";
            if (!lecturer.Equals(""))
                result2 += lecturer + "\n";
            if (!className.Equals(""))
                result2 += className + "\n";
            result2 +=  getStartHour() + "-" + getEndHour() + "\n";
            result2 +=  getFullDay() + "\n";
            return result2;
            //return String.Format("{0}({1}-סוג): מרצה - {2}, כיתה - {3} \n({4}-{5})\n", this.courseName, this.getFullType(), this.lecturer, this.className, this.getStartHour(), this.getEndHour());
        }

        // the lesson this is collision with the lesson 2 before or after
        public bool isCollision(Lesson l2)
        {
            checkNullArgumant(l2, "השיעור השני שאיתו אתה מנסה לבדוק שהוא מתנגש עם שיעור שלך , לא הוגדר");
            return this >= l2 || this <= l2;
        }
        // the lesson this is not collision with the lesson 2 before or after
        public bool isntCollision(Lesson l2)
        {
            checkNullArgumant(l2, "השיעור השני שאיתו אתה מנסה לבדוק שהוא לא מתנגש עם שיעור שלך , לא הוגדר");
            return !isCollision(l2);
        }

        public static void checkNullArgumant(Lesson l ,string message)
        {
            try
            {
                if (l == null) ;
                    //throw new ArgumentNullException("");
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("");
            }
        }
    }
}
