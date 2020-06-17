using System;
using System.Collections.Generic;
using System.Text;

namespace Solun.Entities.Terminals
{
	class LockTerminal : Terminal
	{
		int code;
		Lock lockEntity;
		
		public LockTerminal(int id, Lock lockEntity, int code)
		{
			name = $"T#{id}";
			description = $"A standard terminal with the numbers {id} etched on the side. The monitor awaits an input";

			this.id = id;
			if(id > 9999 || id < 0) throw new Exception("Int is invalid");

			this.lockEntity = lockEntity;
			this.code = code; // Must be 4 digits long
			if(code > 9999 || code < 0) throw new Exception("Int is invalid");
		}

		protected override void Execute()
		{
			Console.WriteLine("Enter 4-digit Code: ");

			int passCode;

			while(true)
			{
				try
				{
					passCode = int.Parse(Console.ReadLine());
					if(passCode > 9999 || passCode < 0) throw new Exception("Int is invalid");
					break;
				}
				catch
				{
					Console.WriteLine("Input was not a digit code.");
				}
			}

			if(code == passCode)
			{
				Console.Write("Access Granted. Lock ");
				lockEntity.Interact(this);
				Console.WriteLine(lockEntity.IsLocked ? "locked." : "unlocked.");
			}
			else
			{
				Console.WriteLine("Access Denied.");
			}
		}
	}
}
