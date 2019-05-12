using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGameKata.App
{
    public class TenthFrame :IFrame
    {
        private const int MaxRollScore = 10;

        private readonly int? _secondRoll;
        private readonly int? _thirdRoll;

        public int FirstRoll { get; private set; }
        public int SecondRoll => _secondRoll ?? 0;
        public int ThirdRoll => _thirdRoll ?? 0;
        
        public TenthFrame(int firstRoll, int? secondRoll, int? thirdRoll)
        {
            FirstRoll = firstRoll;
            _secondRoll = secondRoll ?? 0;
            _thirdRoll = thirdRoll ?? 0;
        }

        public bool IsStrike() => false;

        public bool IsSpare() => false;
        public FrameTypeEnum GetFrameType() => FrameTypeEnum.Tenth;

        public int GetScore() => FirstRoll + SecondRoll + ThirdRoll;
        public int GetScoreForSpare()=> FirstRoll;
        public int GetScoreForStrike()=> FirstRoll + SecondRoll;
    }
}
