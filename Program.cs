using Console = Colorful.Console;
using System;
using System.Collections.Generic;
using System.Drawing;
using Solun.Entities.Mobs;
using Solun.World;

namespace Solun
{
	class Program
	{
		static void Main()
		{	
			Console.WriteAscii("Solun", Color.Yellow);

			Room testRoom = new Room(
				"testRoom",
				"The room is colored red");

			Player player = new Player(testRoom);

			CommandHandler comHan = new CommandHandler(player);

			while(true)
			{
				Console.Write("\n> ");
				comHan.HandleInput(Console.ReadLine());
			}
		}
	}
}
