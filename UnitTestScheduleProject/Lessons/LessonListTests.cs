using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schedule.Lessons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Lessons.Tests
{
    [TestClass()]
    public class LessonListTests
    {
        static Lesson[] lsns1 = new Lesson[]
        {
            new Lesson("קומפילציה", "הרצאה", 1, "בוריס", "ראשון", 10, 12, 2, 3, "F111"),
            new Lesson("קומפילציה", "הרצאה", 2, "בוריס", "ראשון", 9, 11, 2, 3, "F112"),
            new Lesson("קומפילציה", "הרצאה", 3, "בוריס", "ראשון", 11, 14, 3, 3, "F113"),
            new Lesson("קומפילציה", "הרצאה", 4, "בוריס", "ראשון", 10, 12, 2, 3, "F114"),
            new Lesson("בדיקות", "תרגול", 1, "בוריס", "ב", 15, 18, 3, 2, "F114"),
            new Lesson("בדיקות", "מעבדה", 1, "בוריס", "שני", 15, 18, 3, 2, "F114"),
            new Lesson("בדיקות", "מעבדה", 1, "בוריס", "רבעי", 15, 18, 3, 2, "F114")
        };

        static Lesson[] lsns2 = new Lesson[]
        {
            new Lesson("קומפילציה", "הרצאה", 1, "בוריס", "ראשון", 10, 12, 2, 3, "F111"),
            new Lesson("קומפילציה", "הרצאה", 2, "בוריס", "שני", 9, 11, 2, 3, "F112"),
            new Lesson("קומפילציה", "הרצאה", 3, "בוריס", "שלישי", 11, 14, 3, 3, "F113"),
            new Lesson("קומפילציה", "הרצאה", 4, "בוריס", "רבעי", 10, 12, 2, 3, "F114"),
            new Lesson("בדיקות", "תרגול", 1, "בוריס", "ה", 15, 18, 3, 2, "F114"),
            new Lesson("בדיקות", "מעבדה", 1, "בוריס", "ו", 15, 18, 3, 2, "F114"),
        };

        Lesson l = new Lesson("בדיקות", "מעבדה", 1, "בוריס", "רבעי", 15, 18, 3, 2, "F114");

        LessonList list1 = new LessonList(lsns1);
        LessonList list2 = new LessonList(lsns2);
        LessonList list3 = new LessonList(lsns2);

        List<LessonList[]> set;

        [TestMethod()]
        public void isSameLessonListTest()
        {
            Assert.IsTrue(list2.isSameLessonList(list3));
            Assert.IsFalse(list2.isSameLessonList(list1));
        }


        [TestMethod()]
        public void addTest()
        {
            Assert.IsTrue(list2.isSameLessonList(list2.add(l)));
        }

        [TestMethod()]
        public void removeTest()
        {
            Assert.IsTrue(list1.remove(l).amount() != 7);
        }

        [TestMethod()]
        public void existTest()
        {
            Assert.IsTrue(list1.exist(l));
            Assert.IsFalse(list2.exist(l));
        }

        [TestMethod()]
        public void amountTest()
        {
            Assert.IsTrue(list1.amount() == 7);
            Assert.IsTrue(list2.amount() == 6);
        }

        [TestMethod()]
        public void getLessonsTest()
        {
            Assert.IsTrue(new LessonList(list2.getLessons()).isSameLessonList(list3));
            Assert.IsTrue(list2.getLessons().Length == 6);
        }

        [TestMethod()]
        public void sortByTest()
        {
            LessonList list_course = new LessonList(new Lesson[]
            {
                new Lesson("אמינות", "תרגול", 1, "בוריס", "ב", 15, 18, 3, 2, "F114"),
                new Lesson("בדיקות", "מעבדה", 1, "בוריס", "שני", 15, 18, 3, 2, "F114"),
                new Lesson("קומפילציה", "הרצאה", 1, "בוריס", "ראשון", 10, 12, 2, 3, "F111"),
            });
            LessonList list_type = new LessonList(new Lesson[]
            {
                new Lesson("קומפילציה", "הרצאה", 1, "בוריס", "ראשון", 10, 12, 2, 3, "F111"),
                new Lesson("בדיקות", "מעבדה", 1, "בוריס", "שני", 15, 18, 3, 2, "F114"),
                new Lesson("אמינות", "תרגול", 1, "בוריס", "ב", 15, 18, 3, 2, "F114"),
            });

            LessonList list_to_sort_course = new LessonList(new Lesson[]
            {
                new Lesson("קומפילציה", "הרצאה", 1, "בוריס", "ראשון", 10, 12, 2, 3, "F111"),
                new Lesson("אמינות", "תרגול", 1, "בוריס", "ב", 15, 18, 3, 2, "F114"),
                new Lesson("בדיקות", "מעבדה", 1, "בוריס", "שני", 15, 18, 3, 2, "F114"),
            });
            LessonList list_to_sort_type = new LessonList(new Lesson[]
            {
                new Lesson("אמינות", "תרגול", 1, "בוריס", "ב", 15, 18, 3, 2, "F114"),
                new Lesson("קומפילציה", "הרצאה", 1, "בוריס", "ראשון", 10, 12, 2, 3, "F111"),
                new Lesson("בדיקות", "מעבדה", 1, "בוריס", "שני", 15, 18, 3, 2, "F114"),
            });
            list_to_sort_course.sortBy("course", true);
            list_to_sort_type.sortBy("type", true);
            Assert.IsTrue(list_to_sort_course.isSameLessonList(list_course));
            Assert.IsTrue(list_to_sort_type.isSameLessonList(list_type));
        }

        [TestMethod()]
        public void hasCollisionTest()
        {
            List<Lesson[]> set1 = new List<Lesson[]>();
            set1.Add(lsns1);
            List<Lesson[]> set2 = new List<Lesson[]>();
            set2.Add(lsns2);

            Assert.IsTrue(list1.hasCollision(ref set2));
            Assert.IsFalse(list2.hasCollision(ref set1));
        }
    }
}