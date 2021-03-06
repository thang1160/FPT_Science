﻿using BLL.InternationalCollaboration.AcademicActivity;
using ENTITIES;
using ENTITIES.CustomModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.InternationalCollaboration.AcademicActivity
{
    [TestFixture]
    public class CheckinParticipant
    {
        ScienceAndInternationalAffairsEntities db = new ScienceAndInternationalAffairsEntities();
        [TestCase]
        public void CheckinParticipantUT_1()
        {
            new AddParticipantToPhase().AddParticipantToPhaseUT_1();
            Participant p = db.Participants.Last();
            CheckInRepo repo = new CheckInRepo();
            bool res = repo.Checkin(p.participant_id);
            if (res)
                Assert.Pass();
        }
        [TestCase]
        public void CheckinParticipantUT_2()
        {
            CheckInRepo repo = new CheckInRepo();
            bool res = repo.Checkin(0);
            if (res)
                Assert.Pass();
        }
    }
}