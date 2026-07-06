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
            int[] pins = { 5 };
            ScoreResult result = ScoreHandler.CalculateScoreResult(pins);

            Assert.That(result.TotalScore, Is.EqualTo(5));
        }

        [Test]
        public void When_Strike_Expect_negative_1()
        {
            int[] pins = { 10 };
            ScoreResult result = ScoreHandler.CalculateScoreResult(pins);

            Assert.That(result.TotalScore, Is.EqualTo(-1));
        }

        [Test]
        public void When_Cummulative20_Expect_20()
        {
            int[] pins = { 4, 6, 5, 3, 2 };
            ScoreResult result = ScoreHandler.CalculateScoreResult(pins);

            Assert.That(result.TotalScore, Is.EqualTo(20));
        }

        [Test]
        public void When_HasNumberOfPinsOutsideOfMinBound_Expect_Error()
        {
            int[] pins = { 5, -1 };
            Assert.Throws<Exception>(() => ScoreHandler.CalculateScoreResult(pins));
        }

        [Test]
        public void When_HasNumberOfPinsOutsideOfMaxBound_Expect_Error()
        {
            int[] pins = { 5, 12 };
            Assert.Throws<Exception>(() => ScoreHandler.CalculateScoreResult(pins));
        }

        [Test]
        public void When_MoreThanMaxRound_Expect_IgnoreTheRest()
        {
            int[] pins = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            ScoreResult result = ScoreHandler.CalculateScoreResult(pins);

            Assert.That(result.TotalScore, Is.EqualTo(10));
        }

        [Test]
        public void When_AllStrikes_Expect_300()
        {
            int[] pins = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
            ScoreResult result = ScoreHandler.CalculateScoreResult(pins);

            Assert.That(result.TotalScore, Is.EqualTo(300));
        }

        [Test]
        public void When_NoPins_Expect_0()
        {
            int[] pins = { 0, 0, 0, 0, 0 };
            ScoreResult result = ScoreHandler.CalculateScoreResult(pins);

            Assert.That(result.TotalScore, Is.EqualTo(0));
        }

        [Test]
        public void When_EmptyInput_Expect_0()
        {
            int[] pins = { };
            ScoreResult result = ScoreHandler.CalculateScoreResult(pins);

            Assert.That(result.TotalScore, Is.EqualTo(0));
        }

        [Test]
        public void When_NullInput_Expect_NullException()
        {
            Assert.Throws<ArgumentNullException>(() => ScoreHandler.CalculateScoreResult(null));
        }
    }
}