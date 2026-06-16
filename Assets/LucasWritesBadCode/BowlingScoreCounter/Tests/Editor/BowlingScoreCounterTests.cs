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
            var (score, rounds) = ScoreHandler.CalculateScoreAndRounds(pins);

            Assert.That(score, Is.EqualTo(5));
        }

        [Test]
        public void When_Strike_Expect_30()
        {
            int[] pins = { 10 };
            var (score, rounds) = ScoreHandler.CalculateScoreAndRounds(pins);

            Assert.That(score, Is.EqualTo(30));
        }

        [Test]
        public void When_Cummulative20_Expect_20()
        {
            int[] pins = { 4, 6, 5, 3, 2 };
            var (score, rounds) = ScoreHandler.CalculateScoreAndRounds(pins);

            Assert.That(score, Is.EqualTo(20));
        }

        [Test]
        public void When_HasNumberOfPinsOutsideOfMinBound_Expect_Error()
        {
            int[] pins = { 5, -1 };
            Assert.Throws<System.Exception>(() => ScoreHandler.CalculateScoreAndRounds(pins));
        }

        [Test]
        public void When_HasNumberOfPinsOutsideOfMaxBound_Expect_Error()
        {
            int[] pins = { 5, 12 };
            Assert.Throws<System.Exception>(() => ScoreHandler.CalculateScoreAndRounds(pins));
        }

        [Test]
        public void When_MoreThanMaxRound_Expect_IgnoreTheRest()
        {
            int[] pins = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            var (score, rounds) = ScoreHandler.CalculateScoreAndRounds(pins);

            Assert.That(score, Is.EqualTo(10));
            //should probably test for the Debug.Log exception too.
        }

        [Test]
        public void When_AllStrikes_Expect_300()
        {
            int[] pins = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
            var (score, rounds) = ScoreHandler.CalculateScoreAndRounds(pins);

            Assert.That(score, Is.EqualTo(300));
        }

        [Test]
        public void When_NoPins_Expect_0()
        {
            int[] pins = { 0, 0, 0, 0, 0 };
            var (score, rounds) = ScoreHandler.CalculateScoreAndRounds(pins);

            Assert.That(score, Is.EqualTo(0));
        }

        [Test]
        public void When_EmptyInput_Expect_0()
        {
            int[] pins = { };
            var (score, rounds) = ScoreHandler.CalculateScoreAndRounds(pins);

            Assert.That(score, Is.EqualTo(0));
        }

        [Test]
        public void When_NullInput_Expect_NullException()
        {
            int[] pins = null;
            Assert.Throws<ArgumentNullException>(() => ScoreHandler.CalculateScoreAndRounds(pins));
        }
    }
}
