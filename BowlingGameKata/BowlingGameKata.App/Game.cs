using System;
using System.Collections.Generic;

namespace BowlingGameKata.App
{
    public class Game
    {
        private const int MaxFrames = 10;

        private readonly IFrame[] _rolls = new IFrame[MaxFrames];
        private int _currentRoll;

        private readonly IDictionary<FrameTypeEnum, Func<int, IFrame, int>> _framesStrategies;

        public Game()
        {
            _framesStrategies = new Dictionary<FrameTypeEnum, Func<int, IFrame, int>>
            {
                {FrameTypeEnum.Normal, CalculateNormalFrame},
                {FrameTypeEnum.Tenth, CalculateTenthFrame}
            };
        }

        public void SetRoll(IFrame frame)
        {
            _rolls[_currentRoll++] = frame;
        }

        public int GetScore()
        {
            var score = 0;

            for (var frameIndex = 0; frameIndex < _rolls.Length; frameIndex++)
            {
                var frame = _rolls[frameIndex];
                var strategy = _framesStrategies[frame.GetFrameType()];
                score += strategy(frameIndex, frame);
            }

            return score;
        }

        private static int CalculateTenthFrame(int frameIndex, IFrame frame) => frame.GetScore();

        private int CalculateNormalFrame(int frameIndex, IFrame frame)
        {
            var totalFrameScore = frame.GetScore();

            if (frame.IsStrike())
            {
                totalFrameScore = CalculateStrikeScore(frameIndex, totalFrameScore);
            }
            else if (frame.IsSpare())
            {
                totalFrameScore = CalculateSpareScore(frameIndex, totalFrameScore);
            }

            return totalFrameScore;
        }

        private int CalculateStrikeScore(int frameIndex, int totalFrameScore)
        {
            var nextFrame = _rolls[frameIndex + 1];
            totalFrameScore += nextFrame.GetScoreForStrike();

            if (nextFrame.IsStrike())
            {
                totalFrameScore = CalculateSpareScore(frameIndex + 1, totalFrameScore);
            }

            return totalFrameScore;
        }

        private int CalculateSpareScore(int frameIndex, int totalFrameScore)
        {
            var nextFrame = _rolls[frameIndex + 1];
            totalFrameScore += nextFrame.GetScoreForSpare();
            return totalFrameScore;
        }
    }
}
