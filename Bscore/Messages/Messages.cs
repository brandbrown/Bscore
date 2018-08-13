using System;

namespace Bscore.Messages
{
    public class Messages
    {
        public Messages()
        {
        }

        public void IntroMessage() {
            Write("Welcome to not really bowling but sort of kind of a way to keep score of bowling!");

            Write("It's the first frame, enter the first roll when you are ready:");
        }

        public void FrameMessage(int frame)
        {
            Write("It's frame " + frame.ToString() + ", enter your roll:");
        }

        public void StrikeMessage()
        {
            Write("Strike!");
        }

        public void SpareMessage()
        {
            Write("Spare!");
        }

        private void Write(string message) {
            Console.WriteLine(message);
        }
    }
}