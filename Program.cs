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
		static Player player;
		static Sector sector = new Sector();
		
		static void Main()
		{	
			Console.WriteAscii("Solun", Color.Yellow);

			CreateSector();
			SpawnPlayer("A");

			CommandHandler comHan = new CommandHandler(player);

			// Game loop
			while(true)
			{
				Console.Write("\n> ");
				comHan.HandleInput(Console.ReadLine());
			}
		}

		static void CreateSector()
		{
			Room tempRoom;
			
			sector.Rooms.Add(new Room("A", "The entire room is red"));
			sector.Rooms.Add(new Room("B", "The entire room is green"));
			sector.Rooms.Add(new Room("C", "The entire room is blue"));

			sector.LinkRooms("A", "B");
			sector.LinkRooms("C", "B");
		}

		static void SpawnPlayer(string roomName)
		{
			Room spawnRoom = sector.Rooms.Find(room => room.Name.ToLower() == "A".ToLower());
			if(!sector.Rooms.Contains(spawnRoom)) throw new Exception("Spawn room not found");
			else player = new Player(spawnRoom);
		}
	}
}
