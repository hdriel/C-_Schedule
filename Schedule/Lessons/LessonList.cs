using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Schedule.Lessons
{
    public class LessonList
    {
        List<Lesson> lesson;

        public LessonList()
        {
            lesson = new List<Lesson>();
        }
        public LessonList(int n)
        {
            lesson = new List<Lesson>(n);
        }
        public LessonList(Lesson[] l)
        {
            lesson = new List<Lesson>();
            for (int i = 0; i < l.Length; i++)
            {
                lesson.Add(l[i]);
            }
        }

        public LessonList add(Lesson l)
        {
            try
            {
                Lesson.checkNullArgumant(l, "השיעור שאותו אתה מנסה להוסיף לרשימה, לא הוגדר");
            }
            catch (ArgumentNullException)
            {
                return this;
            }

            if (lesson == null)
            {
                lesson = new List<Lesson>();
                lesson[0] = l;
                return this;
            }
            else if (exist(l))
            {
                return this;
            }
            lesson.Add(l);
            return this;
        }
        public LessonList remove(Lesson l)
        {
            try {
                Lesson.checkNullArgumant(l, "השיעור שאותו אתה מנסה להוסיף לרשימה, לא הוגדר");
            }
            catch (ArgumentNullException) {
                return this;
            }

            if (lesson == null || !exist(l)) {
                return this;
            }
            
            for (int i = 0; lesson != null && i < lesson.Count; i++)
            {
                if (lesson[i].sameValue(l)){
                    lesson.Remove(lesson[i]);
                    break;
                }
            }
            return this;
        }
        public bool exist(Lesson l)
        {
            if (lesson == null)
            {
                return false;
            }
            for (int i = 0; i < lesson.Count; i++)
            {
                if (lesson[i].sameValue(l))
                {
                    return true;
                }
            }
            return false;
        }
        public int amount()
        {
            if (lesson != null)
                return lesson.Count;
            return 0;
        }
        public Lesson[] getLessons()
        {
            return lesson.ToArray();
        }
        public List<Lesson> getList()
        {
            return lesson.ToList();
        }
        // override get item operator lesson[i] (the operator [])
        public Lesson this[int i]
        {
            get
            {
                if (i >= 0 && i < lesson.Count)
                    return lesson[i];
                return null;
            }
            set { lesson.Add(value); }
        }
        // Overload + operator to Move the lesson h hour ahead.
        public static LessonList operator +(LessonList list, Lesson l)
        {
            return list.add(l);
        }
        // Overload + operator to Move the lesson h hour ahead.
        public static LessonList operator -(LessonList list, Lesson l)
        {
            return list.remove(l);
        }

        public void sortBy(string by, bool MinToMax)
        {
            SortQuick(by, lesson, 0, amount() - 1, MinToMax);
        }
        private static int Partition(string sortBy, List<Lesson> arr, int left, int right, bool MinToMax)
        {
            Lesson pivot = arr[right];
            Lesson temp;

            int i = left;
            for (int j = left; j < right; j++)
            {
                switch (sortBy.ToLower())
                {
                    case "year":
                        if ((MinToMax && arr[j].year <= pivot.year) || (!MinToMax && arr[j].year > pivot.year))
                        {
                            temp = arr[j];
                            arr[j] = arr[i];
                            arr[i] = temp;
                            i++;
                        }
                        break;

                    case "type":
                        if ((MinToMax && arr[j].type.CompareTo(pivot.type) <= 0) || (!MinToMax && arr[j].type.CompareTo(pivot.type) > 0))
                        {
                            temp = arr[j];
                            arr[j] = arr[i];
                            arr[i] = temp;
                            i++;
                        }
                        break;

                    case "course":
                        if ((MinToMax && arr[j].courseName.CompareTo(pivot.courseName) <= 0) || (!MinToMax && arr[j].courseName.CompareTo(pivot.courseName) > 0))
                        {
                            temp = arr[j];
                            arr[j] = arr[i];
                            arr[i] = temp;
                            i++;
                        }
                        break;

                    case "lecturer":
                        if ((MinToMax && arr[j].lecturer.CompareTo(pivot.lecturer) <= 0) || (!MinToMax && arr[j].lecturer.CompareTo(pivot.lecturer) > 0))
                        {
                            temp = arr[j];
                            arr[j] = arr[i];
                            arr[i] = temp;
                            i++;
                        }
                        break;

                    case "day":
                        if ((MinToMax && arr[j].getShortDay().CompareTo(pivot.getShortDay()) <= 0) || (!MinToMax && arr[j].getShortDay().CompareTo(pivot.getShortDay()) > 0))
                        {
                            temp = arr[j];
                            arr[j] = arr[i];
                            arr[i] = temp;
                            i++;
                        }
                        break;
                    case "start":
                        if ((MinToMax && arr[j].start <= pivot.start) || (!MinToMax && arr[j].start > pivot.start))
                        {
                            temp = arr[j];
                            arr[j] = arr[i];
                            arr[i] = temp;
                            i++;
                        }
                        break;
                    case "end":
                        if ((MinToMax && arr[j].end <= pivot.end) || (!MinToMax && arr[j].end > pivot.end))
                        {
                            temp = arr[j];
                            arr[j] = arr[i];
                            arr[i] = temp;
                            i++;
                        }
                        break;
                    case "hours":
                        if ((MinToMax && arr[j].weekHour <= pivot.weekHour) || (!MinToMax && arr[j].weekHour > pivot.weekHour))
                        {
                            temp = arr[j];
                            arr[j] = arr[i];
                            arr[i] = temp;
                            i++;
                        }
                        break;
                    case "class":
                        if ((MinToMax && arr[j].className.CompareTo(pivot.className) <= 0) || (!MinToMax && arr[j].className.CompareTo(pivot.className) > 0))
                        {
                            temp = arr[j];
                            arr[j] = arr[i];
                            arr[i] = temp;
                            i++;
                        }
                        break;
                    default:
                        if ((MinToMax && arr[j].year <= pivot.year) || (!MinToMax && arr[j].year > pivot.year))
                        {
                            temp = arr[j];
                            arr[j] = arr[i];
                            arr[i] = temp;
                            i++;
                        }
                        break;
                }

            }

            arr[right] = arr[i];
            arr[i] = pivot;

            return i;
        }
        private static void SortQuick(string sortBy, List<Lesson> arr, int left, int right, bool MinToMax)
        {
            // For Recusrion  
            if (left < right)
            {
                int pivot = Partition(sortBy, arr, left, right, MinToMax);
                SortQuick(sortBy, arr, left, pivot - 1, MinToMax);
                SortQuick(sortBy, arr, pivot + 1, right, MinToMax);
            }
        }

        public bool hasCollision(ref List<Lesson[]> collisions)
        {
            bool flag = false;
            if (collisions == null) collisions = new List<Lesson[]>();
            for (int i = 0; lesson != null && i < lesson.Count; i++)
            {
                for (int j = i+1; j < lesson.Count; j++)
                {
                    if (i != j && lesson[i] <= lesson[j]) {
                        Lesson[] col = new Lesson[2];
                        col[0] = lesson[i];
                        col[1] = lesson[j];
                        collisions.Add(col);
                        flag = true;
                    }
                }
            }
            return flag;
        }

        public bool isSameLessonList(LessonList list)
        {
            if (this.amount() != list.amount())
                return false;
            for (int i = 0; i < this.amount(); i++)
            {
                if (!this[i].sameValue(list[i]))
                    return false;
            }
            return true;
        }

        public Lesson[] findLessonsAtTime(int start, int end, int day)
        {
            List<Lesson> lessons = new List<Lesson>();
            for (int i = 0; i < this.lesson.Count; i++)
            {
                if(this.lesson[i].start >= start && this.lesson[i].end <= end)
                {
                    lessons.Add(this.lesson[i]);
                }
            }
            return lessons.ToArray();
        }
    }
}
