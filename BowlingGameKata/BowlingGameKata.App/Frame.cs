namespace BowlingGameKata.App
{
    public class Frame : IFrame
    {
        private const int MaxRollScore = 10;

        private readonly int? _secondRoll;

        public int FirstRoll { get; private set; }
        public int SecondRoll => _secondRoll ?? 0;

        public Frame(int firstRoll, int? secondRoll)
        {
            FirstRoll = firstRoll;
            _secondRoll = secondRoll;
        }
        
        public bool IsStrike() => FirstRoll == MaxRollScore;
        public bool IsSpare() => !IsStrike() && FirstRoll + SecondRoll == 10;

        public int GetScore() => FirstRoll + SecondRoll;
        public int GetScoreForSpare()=> FirstRoll;
        public int GetScoreForStrike()=> GetScore();
        public FrameTypeEnum GetFrameType() => FrameTypeEnum.Normal;
    }
}
