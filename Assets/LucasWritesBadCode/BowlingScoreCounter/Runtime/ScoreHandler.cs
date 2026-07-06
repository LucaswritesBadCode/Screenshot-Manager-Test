using System;
using System.Collections.Generic;
using UnityEngine;

namespace LucasWritesBadCode.BowlingScoreCounter.Runtime
{
    public static class ScoreHandler
    {
        public static void GetScore(int[] pinsKnockedOverPerRound)
        {
            ScoreResult scoreResult = CalculateScoreResult(pinsKnockedOverPerRound);
            Debug.Log($"Score = {scoreResult.TotalScore}, Number of Rounds = {scoreResult.NumberOfRounds}");
        }

        public static ScoreResult CalculateScoreResult(int[] pinsKnockedOverPerRound)
        {
            // I'm a bit unsure if this section should be separated into different functions
            if (pinsKnockedOverPerRound == null)
                throw new ArgumentNullException();

            List<int> calculatedScoreList = new List<int>();
            int numberOfRounds = 0;

            foreach (int round in pinsKnockedOverPerRound)
            {
                numberOfRounds++;

                int calculatedScore = CalculateScorePerRound(round);
                calculatedScoreList.Add(calculatedScore);

                if (!HasReachedMaxRounds(numberOfRounds)) continue;
                
                Debug.Log("Reached Maximum Number of Rounds");
                break;
            }

            int totalScore = CalculateTotalScore(calculatedScoreList);

            return new ScoreResult
            {
                TotalScore = totalScore,
                NumberOfRounds = numberOfRounds
            };
        }

        private static int CalculateScorePerRound(int pinsKnockedOver)
        {
            CheckIfValidNumber(pinsKnockedOver);

            int calculatedScore = pinsKnockedOver;

            // this technically doesn't factor in spares. could be something to add
            // though it will probably require using char or strings
            if (pinsKnockedOver == 10)
                calculatedScore = 30;

            return calculatedScore;
        }

        private static void CheckIfValidNumber(int pinsKnockedOver)
        {
            if (pinsKnockedOver < 0 || pinsKnockedOver > 10)
            {
                throw new Exception("Not a valid number of pins. Please input a number between 0 to 10.");
            }
        }

        private static bool HasReachedMaxRounds(int numberOfRounds)
        {
            if (numberOfRounds >= 10)
            {
                return true;
            }

            return false;
        }

        private static int CalculateTotalScore(List<int> roundScoresArray)
        {
            int totalScore = 0;

            foreach (int score in roundScoresArray)
            {
                totalScore += score;
            }

            return totalScore;
        }
    }
}