﻿using BLL.InternationalCollaboration.AcademicActivity;
using ENTITIES;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.InternationalCollaboration.AcademicActivity
{
    [TestFixture]
    public class DeleteAcademicActivityUT
    {
        ScienceAndInternationalAffairsEntities db = new ScienceAndInternationalAffairsEntities();
        [TestCase]
        public void DeleteAcademicActivityUT_1()
        {
            AcademicActivityRepo activityRepo = new AcademicActivityRepo();
            new AddAcademicActivityUT().TestAddAcademicActivity_1();
            ENTITIES.AcademicActivity academicActivity = db.AcademicActivities.Last();
            bool res = activityRepo.deleteAA(academicActivity.activity_id);
            if (res)
                Assert.Pass();
        }
        [TestCase]
        public void DeleteAcademicActivityUT_2()
        {
            AcademicActivityRepo activityRepo = new AcademicActivityRepo();
            new AddAcademicActivityUT().TestAddAcademicActivity_1();
            ENTITIES.AcademicActivity academicActivity = db.AcademicActivities.Last();
            bool res = activityRepo.deleteAA(academicActivity.activity_id);
            if (res)
                Assert.Pass();
        }
        [TestCase]
        public void TestAddAcademicActivity_6()
        {
            AcademicActivityRepo activityRepo = new AcademicActivityRepo();
            new AddAcademicActivityUT().TestAddAcademicActivity_1();
            ENTITIES.AcademicActivity academicActivity = db.AcademicActivities.Last();
            bool res = activityRepo.deleteAA(academicActivity.activity_id);
            if (!res)
                Assert.Pass();
        }
    }
}