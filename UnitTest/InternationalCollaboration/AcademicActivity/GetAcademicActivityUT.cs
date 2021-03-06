﻿using BLL.InternationalCollaboration.AcademicActivity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BLL.InternationalCollaboration.AcademicActivity.AcademicActivityGuestRepo;

namespace UnitTest.InternationalCollaboration.AcademicActivity
{
    [TestFixture]
    class GetAcademicActivityUT
    {
        AcademicActivityGuestRepo guestRepo;
        [TestCase]
        public void GetAcademicActivityUT_1()
        {
            guestRepo = new AcademicActivityGuestRepo();
            List<int> type = new List<int> { 1, 2, 3, 4 };
            List<baseAA> alist = guestRepo.GetBaseAA(0, type, 1, null, null);
            Assert.Pass();
        }
        [TestCase]
        public void GetAcademicActivityUT_2()
        {
            guestRepo = new AcademicActivityGuestRepo();
            List<int> typeAll = new List<int> { 1, 2, 3, 4 };
            int listAll = guestRepo.GetBaseAA(0, typeAll, 2, null, null).Count;
            Assert.LessOrEqual(listAll, 6);
        }
        [TestCase]
        public void GetAcademicActivityUT_3()
        {
            guestRepo = new AcademicActivityGuestRepo();
            List<int> typeAll = new List<int> { 1, 2, 3, 4 };
            int subList = guestRepo.GetBaseAA(0, null, 2, null, null).Count;
            int listAll = guestRepo.GetBaseAA(0, typeAll, 2, null, null).Count;
            Assert.AreEqual(subList, listAll);
        }
        [TestCase]
        public void GetAcademicActivityUT_4()
        {
            guestRepo = new AcademicActivityGuestRepo();
            List<int> subType = new List<int> { 1, 2 };
            List<int> typeAll = new List<int> { 1, 2, 3, 4 };
            int subList = guestRepo.GetBaseAA(0, subType, 2, null, null).Count;
            int listAll = guestRepo.GetBaseAA(0, typeAll, 2, null, null).Count;
            Assert.LessOrEqual(subList, listAll);
        }
        public void GetAcademicActivityUT_5()
        {
            guestRepo = new AcademicActivityGuestRepo();
            List<int> subType = new List<int> { 1, 2, 3 };
            List<int> typeAll = new List<int> { 1, 2, 3, 4 };
            int subList = guestRepo.GetBaseAA(0, subType, 2, null, null).Count;
            int listAll = guestRepo.GetBaseAA(0, typeAll, 2, null, null).Count;
            Assert.LessOrEqual(subList, listAll);
        }
    }
}
