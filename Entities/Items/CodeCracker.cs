using Console = Colorful.Console;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Solun.Entities.Items
{
	class CodeCracker : Item
	{
		public static readonly int maxGuesses = 10;
		int guesses = 0;
		
		public CodeCracker()
		{
			allNames = new NameHolder(new string[] 
			{
				"Nuts&Bolts-Cracker",
				"Nuts&Bolts",
				"Code Cracker",
				"Nut Cracker",
				"Cracker",
			});
			this.description = "A device used to crack Lock Terminal codes. The letters SQ are embossed on the side.";

			weight = 1;
		}

		public override void Interact(Entity entity)
		{
			throw new NotImplementedException();
		}

		public int[] Split(int num)
		{
			if(num < 0 || num > 9999) throw new Exception("Int is invalid");
			int[] split = new int[4];

			int parse = num;

			for(int i = 0; i < 4; i++)
			{
				split[3 - i] = parse % 10;
				
				parse -= split[i];
				parse /= 10;
			}

			return split;
		}

		public (int, int) CheckGuess(int guess, int code)
		{
			if(guesses >= maxGuesses) return (nuts: -1, bolts: -1);

			int[] splitGuess = Split(guess);
			int[] splitCode = Split(code);

#if DEBUG
			Console.Write("GUESS: |", Color.HotPink);
			foreach(int d in splitGuess) Console.Write(d, Color.HotPink);
			Console.WriteLine("|");
			Console.Write(" CODE: |", Color.HotPink);
			foreach(int d in splitCode) Console.Write(d, Color.HotPink);
			Console.WriteLine("|");
#endif

			int nuts = 0, bolts = 0;

			int[] matchedNumbers = { -1, -1, -1, -1 };

			// Count nuts
			for(int i = 0; i < 4; i++)
			{
				if(splitGuess[i] == splitCode[i])
				{
					matchedNumbers[i] = splitGuess[i];
					nuts++;
				}
			}

			// Count bolts

			// For each digit:
			// Count how many digits there are in guess and code that match specified digit
			// Subtract by the amount that are nuts for said digit in each count
			// Bolts += min(guess count, code count)

			for(int d = 0; d < 10; d++)
			{
				int numGuess = 0, numCode = 0;
				
				for(int i = 0; i < 4; i++)
				{
					// Count how many digits there are in guess and code that match specified digit
					if(splitGuess[i] == d) numGuess++;
					if(splitCode[i] == d) numCode++;

					// Subtract by the amount that are nuts for said digit in each count
					if(splitGuess[i] == splitCode[i])
					{
						numGuess--;
						numCode--;
					}
				}

				bolts += Math.Min(numGuess, numCode);
			}

			guesses++;
			return (nuts, bolts);
		}
	}
}
