using Console = Colorful.Console;
using System;
using System.Collections.Generic;
using System.Drawing;
using Solun.Entities.Mobs;
using Solun.World;
using Solun.Entities.Items;
using Solun.Entities.Terminals;

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
			SpawnPlayer("Cell");

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
			// Create Cell and Main Lab rooms
			sector.AddRoom(
				"Cell",
				"The room is surrounded in a white metal material with a terminal next to the door to the main lab.");
			sector.AddRoom(
				"Main Lab",
				"The walls are the same as the last room. Another door is next to a terminal.");

			// Create Lock Terminal in Cell
			sector.FindRoom("Cell").AddEntity(new LockTerminal(
				1001,
				1492));

			// Link Cell and Main Lab
			sector.LinkRooms(
				"Cell",
				"Main Lab",
				sector.FindRoom("Cell").FindEntity("T#1001"),
				true);

			// Set lock for Lock Terminal
			sector.FindRoom("Cell").FindEntityType<LockTerminal>("T#1001").lockEntity
				= sector.FindRoom("Cell").FindDoorTo("Main Lab").DoorLock;
		}

		static void SpawnPlayer(string roomName)
		{
			Room spawnRoom = sector.FindRoom(roomName);
			if(!sector.Rooms.Contains(spawnRoom)) throw new Exception("Spawn room not found");
			else player = new Player(spawnRoom);
		}
	}
}
