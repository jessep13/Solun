using Console = Colorful.Console;
using System;
using System.Collections.Generic;
using System.Drawing;
using Solun.Entities.Mobs;

namespace Solun
{
	class Program
	{
		static void Main()
		{	
			Console.WriteAscii("Solun", Color.Yellow);

			Player player = new Player();

			CommandHandler comHan = new CommandHandler(player);

			while(true)
			{
				Console.Write("\n> ");
				comHan.HandleInput(Console.ReadLine());
			}
		}
	}
}
