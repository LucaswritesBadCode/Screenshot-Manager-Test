using NUnit.Framework;
using UnityEngine;
using LucasWritesBadCode.BowlingScoreCounter.Runtime;
using System;

namespace LucasWritesBadCode.BowlingScoreCounter.Tests.Editor
{
    public class BowlingScoreCounterTests : MonoBehaviour
    {
        [Test]
        public void When_FirstRound5_Expect_5()
        {
            string[] pins = { "5 0" };
            ScoreResult result = ScoreHandler.GetScoreResult(pins);

            Assert.That(result.TotalScore, Is.EqualTo(5));
        }

        [Test]
        public void When_Strike_Expect_10()
        {
            string[] pins = { "X" };
            ScoreResult result = ScoreHandler.GetScoreResult(pins);

            Assert.That(result.TotalScore, Is.EqualTo(10));
        }

        [Test]
        public void When_Spare_Expect_10()
        {
            string[] pins = { "3 /" };
            ScoreResult result = ScoreHandler.GetScoreResult(pins);

            Assert.That(result.TotalScore, Is.EqualTo(10));
        }

        [Test]
        public void When_StrikeThen2And3_Expect_20()
        {
            string[] pins = { "X", "2 3" };
            ScoreResult result = ScoreHandler.GetScoreResult(pins);

            Assert.That(result.TotalScore, Is.EqualTo(20));
        }

        [Test]
        public void When_SpareThen2And3_Expect_17()
        {
            string[] pins = { "2 /", "2 3" };
            ScoreResult result = ScoreHandler.GetScoreResult(pins);

            Assert.That(result.TotalScore, Is.EqualTo(17));
        }

        [Test]
        public void When_Double_Expect_30()
        {
            string[] pins = { "X", "X" };
            ScoreResult result = ScoreHandler.GetScoreResult(pins);

            Assert.That(result.TotalScore, Is.EqualTo(30));
        }

        [Test]
        public void When_Turkey_Expect_60()
        {
            string[] pins = { "X", "X", "X" };
            ScoreResult result = ScoreHandler.GetScoreResult(pins);

            Assert.That(result.TotalScore, Is.EqualTo(60));
        }

        [Test]
        public void When_Cummulative20_Expect_20()
        {
            string[] pins = { "2 2", "6 0", "2 4", "0 3", "1 0" };
            ScoreResult result = ScoreHandler.GetScoreResult(pins);

            Assert.That(result.TotalScore, Is.EqualTo(20));
        }

        [Test]
        public void When_HasNumberOfPinsOutsideOfMinBound_Expect_Error()
        {
            string[] pins = { "5 3", "-1" };
            Assert.Throws<Exception>(() => ScoreHandler.GetScoreResult(pins));
        }

        [Test]
        public void When_HasNumberOfPinsOutsideOfMaxBound_Expect_Error()
        {
            string[] pins = { "5 2", "12 4" };
            Assert.Throws<Exception>(() => ScoreHandler.GetScoreResult(pins));
        }

        [Test]
        public void When_MoreThanMaxRound_Expect_IgnoreTheRest()
        {
            string[] pins = { "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };
            ScoreResult result = ScoreHandler.GetScoreResult(pins);

            Assert.That(result.TotalScore, Is.EqualTo(10));
        }

        [Test]
        public void When_AllStrikes_Expect_300()
        {
            string[] pins = { "X", "X", "X", "X", "X", "X", "X", "X", "X", "XXX" };
            ScoreResult result = ScoreHandler.GetScoreResult(pins);

            Assert.That(result.TotalScore, Is.EqualTo(300));
        }

        [Test]
        public void When_NoPins_Expect_0()
        {
            string[] pins = { "0", "0", "0", "0", "0" };
            ScoreResult result = ScoreHandler.GetScoreResult(pins);

            Assert.That(result.TotalScore, Is.EqualTo(0));
        }

        [Test]
        public void When_EmptyInput_Expect_0()
        {
            string[] pins = { };
            ScoreResult result = ScoreHandler.GetScoreResult(pins);

            Assert.That(result.TotalScore, Is.EqualTo(0));
        }

        [Test]
        public void When_SpareOnFirstAttempt_Expect_Error()
        {
            string[] pins = { "/ 2" };
            Assert.Throws<Exception>(() => ScoreHandler.GetScoreResult(pins));
        }

        [Test]
        public void When_StrikeOnSecondAttempt_Expect_Error()
        {
            string[] pins = { "5 X" };
            Assert.Throws<Exception>(() => ScoreHandler.GetScoreResult(pins));
        }

        [Test]
        public void When_HaveAttemptAfterStrike_Expect_Error()
        {
            string[] pins = { "X 5" };
            Assert.Throws<Exception>(() => ScoreHandler.GetScoreResult(pins));
        }

        [Test]
        public void When_InvalidSymbol_Expect_Error()
        {
            string[] pins = { "S" };
            Assert.Throws<Exception>(() => ScoreHandler.GetScoreResult(pins));
        }

        [Test]
        public void When_NullInput_Expect_NullException()
        {
            Assert.Throws<ArgumentNullException>(() => ScoreHandler.GetScoreResult(null));
        }

        [Test]
        public void When_FinalFrameAllStrikes_Expect_30()
        {
            string[] pins = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "XXX" };
            ScoreResult result = ScoreHandler.GetScoreResult(pins);

            Assert.That(result.TotalScore, Is.EqualTo(30));
        }

        [Test]
        public void When_FinalFrameHas3AttemptsWithoutStrikeOrSpare_Expect_Error()
        {
            string[] pins = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "324" };
            Assert.Throws<Exception>(() => ScoreHandler.GetScoreResult(pins));
        }
    }
}
