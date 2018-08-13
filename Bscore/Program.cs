namespace Bscore
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Messages.Messages messages = new Messages.Messages();
            GameLoop.GameLoop Game = new GameLoop.GameLoop();

            messages.IntroMessage();
            Game.StartGame();
        }
    }
}