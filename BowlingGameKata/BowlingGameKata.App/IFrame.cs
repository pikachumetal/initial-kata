namespace BowlingGameKata.App
{
    public interface IFrame
    {
        int GetScore();
        int GetScoreForSpare();
        int GetScoreForStrike();
        bool IsStrike();
        bool IsSpare();
        FrameTypeEnum GetFrameType();
    }
}
