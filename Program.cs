using Console = Colorful.Console;
using System;
using System.Collections.Generic;
using System.Drawing;
using Solun.Entities.Mobs;
using Solun.World;
using Solun.Entities.Items;
using Solun.Entities.Terminals;
using System.Linq;

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
				new NameHolder("Cell"),
				"The room is surrounded in a white metal material with a terminal next to the door to the main lab.");
			sector.AddRoom(
				new NameHolder(new string[]
				{
					"Main Lab",
					"Main",
					"Lab"
				}),
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

			// Add note in Cell
			sector.FindRoom("Cell").AddEntity(new Note(
				"Cell Note",
				"The note appears to be typed, with a signature on the bottem.",
				"Hello. If you are reading this, it appears that you have learned to use your brain to pick up this note and to read it. I will explain later where you are, but for now you shouldn't be too concerned about that since you will be out of here in less than an hour. To open the door in front of you, there is a Lock Terminal awaiting for a code to open the door. You must enter \"1492\" into it in order to be granted access. The next challenge will lie in the next room. \nGood Luck. \n\t - F. E."));

			// Add bio lab
			sector.AddRoom(
				new NameHolder(new string[] 
				{
					"Bio Lab",
					"Bio",
					"Lab"
				}),
				"Plants are scatters all over the place. The room seems to suit their enviorment very well.");
			sector.LinkRooms("Bio", "Main");

			// Add chem lab
			sector.AddRoom(
				new NameHolder(new string[]
				{
					"Chemical Lab",
					"Chem Lab",
					"Chemical",
					"Chem",
					"Lab"
				}),
				"The room is suprisingly devoid of any visable chemicals. Perhaps they are in the cabinets for safety concerns.");
			sector.LinkRooms("Chem", "Main");
		}

		static void SpawnPlayer(string roomName)
		{
			Room spawnRoom = sector.FindRoom(roomName);
			if(!sector.Rooms.Contains(spawnRoom)) throw new Exception("Spawn room not found");
			else player = new Player(spawnRoom);
		}
	}
}
