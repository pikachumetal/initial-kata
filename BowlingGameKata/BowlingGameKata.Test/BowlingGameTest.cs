using BowlingGameKata.App;
using Xunit;

namespace BowlingGameKata.Test
{
    public class BowlingGameTest
    {
        private readonly Game _game = new Game();

        private void RollSomePins(int rolls, IFrame frame)
        {
            for (var i = 0; i < rolls; i++)
            {
                _game.SetRoll(frame);
            }
        }

        [Fact]
        public void When_GutterGame__Then_ScoreIsZero()
        {
            RollSomePins(10, new Frame(0, 0));

            Assert.Equal(0, _game.GetScore());
        }

        [Fact]
        public void When_RollOneGame__Then_ScoreIs20()
        {
            RollSomePins(10, new Frame(1, 1));
            Assert.Equal(20, _game.GetScore());
        }

        [Fact]
        public void When_OneSpare__Then_ScoreIsOk()
        {
            _game.SetRoll(SetSpareFrame());
            _game.SetRoll(new Frame(3, 0));
            RollSomePins(8, new Frame(0, 0));
            Assert.Equal(16, _game.GetScore());
        }

        private static Frame SetSpareFrame() => new Frame(5, 5);

        [Fact]
        public void When_OneStrike__Then_ScoreIsOk()
        {
            _game.SetRoll(SetStrikeFrame());
            _game.SetRoll(new Frame(3, 0));
            RollSomePins(8, new Frame(0, 0));
            Assert.Equal(16, _game.GetScore());
        }

        private static Frame SetStrikeFrame() => new Frame(10, null);


        [Fact]
        public void When_PerfectGame__Then_ScoreIsOk()
        {
            RollSomePins(9, SetStrikeFrame());
            _game.SetRoll(new TenthFrame(10, 10, 10));
            Assert.Equal(300, _game.GetScore());
        }
    }
}
