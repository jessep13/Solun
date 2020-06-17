using Console = Colorful.Console;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Solun
{
	class Program
	{
		static void Main()
		{	
			Console.WriteAscii("Solun", Color.Yellow);

			CommandHandler comHan = new CommandHandler();

			while(true)
			{
				Console.Write("\n> ");
				comHan.HandleInput(Console.ReadLine());
			}
		}
	}
}
