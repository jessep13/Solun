using Solun.Entities.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solun.Entities.Terminals
{
	class LockTerminal : Terminal
	{
		int code;
		public Lock lockEntity; // TODO: Reconfigure
		
		public LockTerminal(int id, int code, Lock lockEntity = null)
		{
			SetName(id);

			if(lockEntity != null) this.lockEntity = lockEntity;
			this.code = code; // Must be 4 digits long
			if(code > 9999 || code < 0) throw new Exception("Int is invalid");
		}

		public override bool IsAttachable(Item item) => item is CodeCracker;

		protected override void Execute()
		{
			Console.Write("Enter 4-digit Code [enter negative to exit]: ");

			int guess;

			while(true)
			{
				// Enter Guess
				try
				{
					guess = int.Parse(Console.ReadLine());
					if(guess > 9999) throw new Exception("Int is invalid");
					else if(guess < 0) break;
				}
				catch
				{
					Console.Write("Invalid Input. Enter again: ");
					continue;
				}

				// Check if code matches
				if(guess == code)
				{
					Console.Write("Access Granted. Lock ");
					lockEntity.Interact(this);
					Console.WriteLine(lockEntity.IsLocked ? "locked." : "unlocked.");
					attachment = null;
					break;
				}
				else
				{
					Console.Write("Access Denied. ");

					if(attachment != null)
					{
						CodeCracker cracker = (CodeCracker)attachment;
						(int, int) hint = cracker.CheckGuess(guess, code);

						if(hint.Item1 == -1 && hint.Item2 == -1) Console.WriteLine();
						else
						{
							Console.Write("[");

							for(int i = 0; i < hint.Item1; i++) Console.Write("N");
							for(int i = 0; i < hint.Item2; i++) Console.Write("B");

							Console.WriteLine("]");
						}
					}
					else Console.WriteLine();
				}
			}
		}
	}
}
