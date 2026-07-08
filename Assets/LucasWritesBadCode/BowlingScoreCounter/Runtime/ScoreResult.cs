namespace LucasWritesBadCode.BowlingScoreCounter.Runtime
{
    public struct ScoreResult
    {
        public int TotalScore { get; set; }
        public int NumberOfFrames { get; set; }
    }

    public enum FrameResultType
    {
        None = 0,
        Number = 1,
        Spare = 2,
        Strike = 3
    }


    public struct FrameResult
    {
        public int[] FrameScore { get; set; }
        public FrameResultType FrameType { get; set; }
    }
}