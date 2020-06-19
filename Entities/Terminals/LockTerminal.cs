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

		protected override void Execute()
		{
			Console.Write("Enter 4-digit Code: ");

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
					Console.Write("Invalid Input. Enter again: ");
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
