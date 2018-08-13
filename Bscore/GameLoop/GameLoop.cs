using Bscore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bscore.GameLoop
{
    public class GameLoop
    {
        public void StartGame() {

            int Frame = 1;
            int MaxFrames = 10;
            Game game = new Game();
            Messages.Messages messages = new Messages.Messages();


            while (Frame <= MaxFrames)
            {
                Frame frameDetails = new Frame(Frame);
                messages.FrameMessage(Frame);


                bool inFrame = true;
                while (inFrame)
                {
                    string roll = Console.ReadLine();

                    if (Int32.TryParse(roll, out int value) && ValidateRoll(frameDetails, value))
                    {

                        Roll rollPins = new Roll();
                        rollPins.Pins = value;
                        frameDetails.Rolls.Add(rollPins);

                        if (rollPins.Pins == 10)
                        {
                            messages.StrikeMessage();
                            Frame += 1;
                            inFrame = false;
                            continue;
                        }

                        if (frameDetails.Rolls.Count == 2)
                        {
                            if (DetermineSpare(frameDetails))
                                messages.SpareMessage();

                            if (Frame == 10 && (DetermineSpare(frameDetails) || DetermineStrike(frameDetails)))
                                continue;

                            Frame += 1;
                            inFrame = false;
                        }

                        if (frameDetails.Rolls.Count == 3)
                        {
                            Frame += 1;
                            inFrame = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please make sure your enter a valid score, try entering again.");
                    }
                }

                game.Frames.Add(frameDetails);
                Console.WriteLine("Game Summary: " + CalculateScore(game));
            }


            Console.WriteLine("Game over, thanks for playing!");
            Console.ReadLine();

        }

        private bool ValidateRoll(Frame frameDetails, int value)
        {

            if (frameDetails.Rolls.Count == 1)
            {
                int totalPins = frameDetails.Rolls[0].Pins + value;
                return totalPins >= 0 && totalPins <= 10;

            }

            return value >= 0 && value <= 10;
        }

        private string CalculateScore(Game game)
        {
            int totalScore = 0;
            for (int i = 0; i < game.Frames.Count; i++)
            {
                Frame frame = game.Frames[i];
                bool isStrike = DetermineStrike(frame);
                bool isSpare = DetermineSpare(frame);
                int additionalPoints = 0;

                if (isStrike)
                {
                    if (game.Frames.Count >= i + 2)
                    {
                        
                        if (game.Frames[i + 1].Rolls.Count >= 1)
                        {
                            additionalPoints += game.Frames[i + 1].Rolls[0].Pins;
                        }

                        if (game.Frames[i + 1].Rolls.Count >= 2)
                        {
                            additionalPoints += game.Frames[i + 1].Rolls[1].Pins;
                        }
                    }
                }
                else if (isSpare)
                {
                    if (game.Frames.Count >= i + 2)
                    {
                        additionalPoints += game.Frames[i + 1].Rolls[0].Pins;
                    }
                }

                frame.Rolls.ForEach(roll => {
                    totalScore += roll.Pins;
                });

                totalScore += additionalPoints;
            }

            return totalScore.ToString() + " points";
        }

        private bool DetermineSpare(Frame frame)
        {
            if (frame.Rolls[0].Pins == 10)
                return false;

            int totalScore = 0;
            
            frame.Rolls.ForEach(roll => {
                totalScore += roll.Pins;
            });

            return totalScore == 10;
        }

        private bool DetermineStrike(Frame frame)
        {
            return frame.Rolls[0].Pins == 10;
        }
    }
}
