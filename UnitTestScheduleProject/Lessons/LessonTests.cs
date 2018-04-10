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
    public class LessonTests
    {

        Lesson lsn1 = new Lesson("קומפילציה",   "הרצאה",    1,  "בוריס",    "ראשון",    10, 12, 12 - 10,    3,  "F111");
        Lesson lsn2 = new Lesson("קומפילציה",   "הרצאה",    2,  "בוריס",    "ראשון",     9, 11,  11 - 9,    3,  "F112");
        Lesson lsn3 = new Lesson("קומפילציה",   "הרצאה",    3,  "בוריס",    "ראשון",    11, 14, 14 - 11,    3,  "F113");
        Lesson lsn4 = new Lesson("קומפילציה",   "הרצאה",    4,  "בוריס",    "ראשון",    10, 12, 12-10,      3,  "F114");
        Lesson lsn5 = new Lesson("בדיקות"   ,   "תרגול",    1,  "בוריס",    "ב"     ,   15, 18, 18-15,      2, "F114");
        Lesson lsn6 = new Lesson("בדיקות"   ,   "מעבדה",    1,  "בוריס",    "שני"     ,   15, 18, 18 - 15, 2, "F114");
        Lesson lsn7 = new Lesson("בדיקות"   ,   "הרצאה",    1,  "בוריס",    "רבעי"     ,   10, 12, 12-10 ,     2, "F114");

        [TestMethod()]
        public void getStartHourTest()
        {

            Assert.IsTrue(lsn1.getStartHour().Equals("10:00"));
            Assert.IsTrue(lsn2.getStartHour().Equals("0" + lsn2.start.ToString() + ":00"));
        }
        
        [TestMethod()]
        public void getEndHourTest()
        {
            Assert.IsTrue(lsn1.getEndHour().Equals("12:00"));
            Assert.IsTrue(lsn2.getEndHour().Equals("11:00"));
        }

        [TestMethod()]
        public void getShortDayTest()
        {
            Assert.IsTrue(lsn1.getShortDay().Equals("א"));
            Assert.IsTrue(lsn5.getShortDay().Equals("ב"));
            Assert.IsTrue(lsn6.getShortDay().Equals("ב"));
            Assert.IsTrue(lsn7.getShortDay().Equals("ד"));
        }

        [TestMethod()]
        public void getFullDayTest()
        {
            Assert.IsTrue(lsn1.getFullDay().Equals("ראשון"));
            Assert.IsTrue(lsn5.getFullDay().Equals("שני"));
            Assert.IsTrue(lsn6.getFullDay().Equals("שני"));
            Assert.IsTrue(lsn7.getFullDay().Equals("רבעי"));
        }

        [TestMethod()]
        public void cloneTest()
        {
            Lesson lsn0 = new Lesson("קומפילציה", "הרצאה", 1, "בוריס", "ראשון", 10, 12, 12 - 10, 3, "F111");
            Assert.IsTrue(lsn1.clone().sameValue(lsn0));
            Assert.IsFalse(lsn1.clone() == lsn0);
            Assert.IsFalse(lsn1.clone().Equals(lsn0));
        }

        [TestMethod()]
        public void sameValueTest()
        {
            Lesson lsn0 = new Lesson("קומפילציה", "הרצאה", 1, "בוריס", "ראשון", 10, 12, 12 - 10, 3, "F111");
            Assert.IsTrue(lsn1.sameValue(lsn0));
        }

        [TestMethod()]
        public void EqualsTest()
        {
            Lesson lsn0 = new Lesson("קומפילציה", "הרצאה", 1, "בוריס", "ראשון", 10, 12, 12 - 10, 3, "F111");
            Lesson lsn11 = lsn1;
            Assert.IsTrue(lsn1.Equals(lsn11));
            Assert.IsTrue(lsn11 == lsn1);
            Assert.IsTrue(lsn11.Equals(lsn1));
        }

        [TestMethod()]
        public void BeforeTest()
        {
            Lesson bl1214 = new Lesson(" ", "ה", 1, " ", "ראשון", 12, 14, 2, 1, " ");
            Lesson bl1012 = new Lesson(" ", "ה", 1, " ", "ראשון", 10, 12, 2, 1, " ");
            Lesson bl1416 = new Lesson(" ", "ה", 1, " ", "ראשון", 14, 16, 2, 1, " ");
            Lesson bl1113 = new Lesson(" ", "ה", 1, " ", "ראשון", 11, 13, 2, 1, " ");
            Lesson bl1315 = new Lesson(" ", "ה", 1, " ", "ראשון", 13, 15, 2, 1, " ");

            Assert.IsTrue(bl1012.Before(bl1214));
            Assert.IsFalse(bl1214.Before(bl1113));
            Assert.IsFalse(bl1113.Before(bl1214));
            Assert.IsFalse(bl1315.Before(bl1012));
        }
        [TestMethod()]
        public void AfterTest()
        {
            Lesson bl1214 = new Lesson(" ", "ה", 1, " ", "ראשון", 12, 14, 2, 1, " ");
            Lesson bl1012 = new Lesson(" ", "ה", 1, " ", "ראשון", 10, 12, 2, 1, " ");
            Lesson bl1416 = new Lesson(" ", "ה", 1, " ", "ראשון", 14, 16, 2, 1, " ");
            Lesson bl1113 = new Lesson(" ", "ה", 1, " ", "ראשון", 11, 13, 2, 1, " ");
            Lesson bl1315 = new Lesson(" ", "ה", 1, " ", "ראשון", 13, 15, 2, 1, " ");

            Assert.IsFalse(bl1012.After(bl1214));
            Assert.IsFalse(bl1214.After(bl1113));
            Assert.IsTrue(bl1416.After(bl1214));
            Assert.IsTrue(bl1416.After(bl1012));
        }
        [TestMethod()]
        public void collisionBeforeTest()
        {
            Lesson bl1214 = new Lesson(" ", "ה", 1, " ", "ראשון", 12, 14, 2, 1, " ");
            Lesson bl1012 = new Lesson(" ", "ה", 1, " ", "ראשון", 10, 12, 2, 1, " ");
            Lesson bl1416 = new Lesson(" ", "ה", 1, " ", "ראשון", 14, 16, 2, 1, " ");
            Lesson bl1113 = new Lesson(" ", "ה", 1, " ", "ראשון", 11, 13, 2, 1, " ");
            Lesson bl1315 = new Lesson(" ", "ה", 1, " ", "ראשון", 13, 15, 2, 1, " ");

            Assert.IsFalse(bl1012.collisionBefore(bl1214));
            Assert.IsFalse(bl1214.collisionBefore(bl1113));
            Assert.IsTrue(bl1113.collisionBefore(bl1214));
            Assert.IsTrue(bl1012.collisionBefore(bl1113));
        }
        [TestMethod()]
        public void collisionAfterTest()
        {
            Lesson bl1214 = new Lesson(" ", "ה", 1, " ", "ראשון", 12, 14, 2, 1, " ");
            Lesson bl1012 = new Lesson(" ", "ה", 1, " ", "ראשון", 10, 12, 2, 1, " ");
            Lesson bl1416 = new Lesson(" ", "ה", 1, " ", "ראשון", 14, 16, 2, 1, " ");
            Lesson bl1113 = new Lesson(" ", "ה", 1, " ", "ראשון", 11, 13, 2, 1, " ");
            Lesson bl1315 = new Lesson(" ", "ה", 1, " ", "ראשון", 13, 15, 2, 1, " ");

            Assert.IsFalse(bl1012.collisionAfter(bl1214));
            Assert.IsTrue(bl1214.collisionAfter(bl1113));
            Assert.IsFalse(bl1416.collisionAfter(bl1214));
            Assert.IsTrue(bl1315.collisionAfter(bl1214));
        }

        [TestMethod()]
        public void isCollisionTest()
        {
            Lesson bl1214 = new Lesson(" ", "ה", 1, " ", "ראשון", 12, 14, 2, 1, " ");
            Lesson bl1012 = new Lesson(" ", "ה", 1, " ", "ראשון", 10, 12, 2, 1, " ");
            Lesson bl1416 = new Lesson(" ", "ה", 1, " ", "ראשון", 14, 16, 2, 1, " ");
            Lesson bl1113 = new Lesson(" ", "ה", 1, " ", "ראשון", 11, 13, 2, 1, " ");
            Lesson bl1315 = new Lesson(" ", "ה", 1, " ", "ראשון", 13, 15, 2, 1, " ");

            Assert.IsFalse(bl1012.isCollision(bl1214));
            Assert.IsTrue(bl1214.isCollision(bl1113));
            Assert.IsFalse(bl1416.isCollision(bl1214));
            Assert.IsTrue(bl1315.isCollision(bl1315));
        }

        [TestMethod()]
        public void isntCollisionTest()
        {
            Lesson bl1214 = new Lesson(" ", "ה", 1, " ", "ראשון", 12, 14, 2, 1, " ");
            Lesson bl1012 = new Lesson(" ", "ה", 1, " ", "ראשון", 10, 12, 2, 1, " ");
            Lesson bl1416 = new Lesson(" ", "ה", 1, " ", "ראשון", 14, 16, 2, 1, " ");
            Lesson bl1113 = new Lesson(" ", "ה", 1, " ", "ראשון", 11, 13, 2, 1, " ");
            Lesson bl1315 = new Lesson(" ", "ה", 1, " ", "ראשון", 13, 15, 2, 1, " ");

            Assert.IsTrue(bl1012.isntCollision(bl1214));
            Assert.IsFalse(bl1214.isntCollision(bl1113));
            Assert.IsTrue(bl1416.isntCollision(bl1214));
            Assert.IsFalse(bl1315.isntCollision(bl1315));
        }
    }
}