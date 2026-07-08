using System;
using System.Collections.Generic;
using UnityEngine;

namespace LucasWritesBadCode.BowlingScoreCounter.Runtime
{
    // TODO: REFACTOR TIME!!!

    public static class ScoreHandler
    {
        const int MaxNumberOfFrames = 10;
        const int MaxNumberOfPins = 10;

        public static void CalculateScore(string[] pinsKnockedOver)
        {
            ScoreResult scoreResult = GetScoreResult(pinsKnockedOver);
            Debug.Log($"Score = {scoreResult.TotalScore}, Number of Frames = {scoreResult.NumberOfFrames}");
        }

        public static ScoreResult GetScoreResult(string[] pinsKnockedOver)
        {
            List<FrameResult> frameResults = new List<FrameResult>();
            int currentFrame = 0;


            foreach (string frame in pinsKnockedOver)
            {
                currentFrame++;

                if (CheckIfReachedMaximumFrames(currentFrame))
                {
                    foreach (FrameResult frameResult in HandleLastFrame(frame))
                    {
                        frameResults.Add(frameResult);
                    }

                    Debug.LogWarning("Reached Maximum Number of Frames");
                    break;
                }

                frameResults.Add(GetFrameResult(frame));

            }

            return new ScoreResult
            {
                TotalScore = CalculateTotalScore(frameResults),
                NumberOfFrames = currentFrame
            };
        }

        private static bool CheckIfReachedMaximumFrames(int numberOfFrames)
        {
            if (numberOfFrames >= MaxNumberOfFrames)
            {
                return true;
            }
            return false;
        }

        //im doing this the really bad way first so i can understand the logic
        private static List<FrameResult> HandleLastFrame(string frame)
        {
            List<FrameResult> lastFrameResults = new List<FrameResult>();

            List<char> attemptsSeperated = new List<char>();
            List<string> framesSeparated = new List<string>();
            string currentFrameString = string.Empty;
            bool finalAttempt = false;

            foreach (char attempt in frame)
            {
                if (char.IsWhiteSpace(attempt))
                { continue; }

                attemptsSeperated.Add(attempt);
            }

            for (int i = 0; i < attemptsSeperated.Count; i++)
            {
                if (i >= 3)
                {
                    throw new ArgumentOutOfRangeException("Too many attempts.");
                }

                if (finalAttempt)
                {
                    break;
                }

                if (i == attemptsSeperated.Count - 1)
                {
                    currentFrameString = string.Concat(attemptsSeperated[i]);
                    framesSeparated.Add(currentFrameString);
                    break;
                }

                switch (GetFrameType(attemptsSeperated[i])) // this violates DRY. need to figure out how to change this
                {
                    case FrameResultType.Number:
                        currentFrameString = string.Concat(attemptsSeperated[i], attemptsSeperated[i + 1]);
                        framesSeparated.Add(currentFrameString);

                        if (GetFrameType(attemptsSeperated[i + 1]) == FrameResultType.Number)
                        {
                            finalAttempt = true;
                        }

                        i++;
                        break;

                    case FrameResultType.Strike:
                        currentFrameString = attemptsSeperated[i].ToString();
                        framesSeparated.Add(currentFrameString);
                        break;
                }
            }

            foreach (string individualFrame in framesSeparated)
            {
                lastFrameResults.Add(GetFrameResult(individualFrame));
            }

            return lastFrameResults;
        }

        private static FrameResult GetFrameResult(string frame)
        {
            FrameResultType frameType = FrameResultType.None;
            int[] frameScores = new int[] { 0, 0 };
            int currentAttempt = 0;

            foreach (char attempt in frame)
            {
                if (currentAttempt > 1)
                {
                    throw new ArgumentOutOfRangeException("Too many attempts.");
                }

                if (frameType == FrameResultType.Strike)
                { break; }

                int attemptScore = 0;

                if (char.IsWhiteSpace(attempt))
                { continue; }

                frameType = GetFrameType(attempt);
                CheckForExceptions(frameType, currentAttempt);
                attemptScore = CalculateAttemptScore(attempt, frameType, frameScores[0]);

                frameScores[currentAttempt] = attemptScore;
                currentAttempt++;
            }

            return new FrameResult
            {
                FrameScore = frameScores,
                FrameType = frameType
            };
        }

        private static int CalculateAttemptScore(char numberOfPins, FrameResultType frameType, int prevPins)
        {
            switch (frameType)
            {
                case FrameResultType.Number:
                default:
                    if (char.IsDigit(numberOfPins))
                    {
                        return int.Parse(numberOfPins.ToString());
                    }
                    throw new ArgumentOutOfRangeException("Not a valid symbol. Please only use numbers, '/', 'X'");

                case FrameResultType.Spare:
                    return MaxNumberOfPins - prevPins;

                case FrameResultType.Strike:
                    return MaxNumberOfPins;
            }
        }

        private static FrameResultType GetFrameType(char numberOfPins)
        {
            switch (numberOfPins)
            {
                case '/':
                    return FrameResultType.Spare;

                case 'X':
                case 'x':
                    return FrameResultType.Strike;

                default:
                    return FrameResultType.Number;
            }
        }

        private static void CheckForExceptions(FrameResultType frameType, int currentAttempt)
        {
            if (frameType == FrameResultType.Spare && currentAttempt == 0)
            {
                throw new ArgumentOutOfRangeException("Spare can only be placed in the 2nd attempt.");
            }

            if (frameType == FrameResultType.Strike && currentAttempt == 1)
            {
                throw new ArgumentOutOfRangeException("Strike can only be placed in the 1st attempt.");
            }
        }


        private static int CalculateTotalScore(List<FrameResult> frameResults)
        {
            int totalScore = 0;

            for (int i = 0; i < frameResults.Count; i++)
            {
                Debug.Log(i + 1 + "=" + CalculateFrameScoreModifiers(frameResults, i));
                totalScore += CalculateFrameScoreModifiers(frameResults, i);
            }

            return totalScore;
        }

        private static int CalculateFrameScoreModifiers(List<FrameResult> frameResults, int i)
        {
            int attemptScore = 0;

            foreach (int attempt in frameResults[i].FrameScore)
            {
                attemptScore += attempt;
            }

            if (frameResults[i].FrameType == FrameResultType.Number)
                if (attemptScore >= MaxNumberOfPins)
                {
                    throw new ArgumentOutOfRangeException("Not a valid number of pins. If you hit 10 pins, use the spare '/' symbol.");
                }

            if (i >= 9)
            {
                return attemptScore;
            }

            switch (frameResults[i].FrameType)
            {
                case FrameResultType.Spare:
                    if (i + 1 >= frameResults.Count)
                    { break; }

                    attemptScore += frameResults[i + 1].FrameScore[0];
                    break;

                case FrameResultType.Strike:
                    if (i + 1 >= frameResults.Count)
                    { break; }

                    foreach (int attempt in frameResults[i + 1].FrameScore)
                    {
                        attemptScore += Mathf.Clamp(attempt, 0, MaxNumberOfPins);
                    }

                    if (i + 2 >= frameResults.Count)
                    { break; }

                    foreach (int attempt in frameResults[i + 2].FrameScore)
                    {
                        attemptScore += Mathf.Clamp(attempt, 0, MaxNumberOfPins);
                    }

                    break;
            }

            return attemptScore;
        }
    }
}
