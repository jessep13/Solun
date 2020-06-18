#define LISTCOM
#undef LISTCOM

using Console = Colorful.Console;
using Solun.Entities;
using Solun.Entities.Items;
using Solun.Entities.Mobs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Solun.World;

namespace Solun
{
	class CommandHandler
	{
		delegate void Command(string[] args);

		private Player player;

		static List<Command> Commands = new List<Command>();

		public CommandHandler(Player player)
		{
			this.player = player;
			
			// List all commands here
			Commands.Add(Help);
			Commands.Add(Exit);
			Commands.Add(Look);
			Commands.Add(Use);
			Commands.Add(Move);
			Commands.Add(Take);
		}

		public void HandleInput(string input)
		{
			// Parse input into arguments
			string command = input;
			List<string> parameters = new List<string>();

			while(command.IndexOf(' ') != -1)
			{
				parameters.Add(command.Substring(0, command.IndexOf(' ')));
				command = command.Substring(command.IndexOf(' ') + 1);
			}

			parameters.Add(command);

			string[] args = parameters.ToArray();
			
			for(int i = 0; i < args.Length; i++)
			{
				// Turn '_' into ' '
				args[i] = args[i].Replace('_', ' ');

#if DEBUG
				// Print each argument				
				Console.WriteLine($"[{i}]: |{args[i]}|", Color.HotPink);
#endif
			}

			// Find which command to use
			foreach(Command com in Commands)
			{
#if DEBUG && LISTCOM
				Console.WriteLine($"COMMAND: {com.Method.Name}", Color.HotPink);
#endif
				if(com.Method.Name.ToLower() == args[0].ToLower())
				{
					com(args);
					return;
				}
			}

			// In event that the command is not recognised
			Console.WriteLine($"\"{args[0]}\" is not a valid command. For a list of all commands, type \"help\".");
		}

		#region Command Methods

		/*
		 
		How to add a command:
		--------------------
		1. Make a "private void {command}(string[] args)" method
		2. Update constructor by adding "Commands.Add({command});" line
		3. Add documentation to Help command with a new case and by adding a line in the default case
		 
		 */

		private void Help(string[] args)
		{
			// Get command to look up
			string command;
			if(args.Length == 1) command = null;
			else command = args[1].ToLower();

			switch(command)
			{
				case "exit":
					Console.WriteLine("exit: Exits the application");
					break;
				case "help":
					Console.WriteLine("help (command): Gives info on commands");
					Console.WriteLine("\t(command): The command to be detailed. if left blank, all commands will be listed");
					break;
				case "look":
					Console.WriteLine("look (entity) (\"-l\"): Gives description of room or entity");
					Console.WriteLine("\t(entity): The target entity to look at");
					Console.WriteLine("\t(\"-l\") list all entities");
					break;
				case "move":
					Console.WriteLine("move (room): Move to an adjacent room");
					Console.WriteLine("\t(room): The room to move to");
					break;
				case "use":
					Console.WriteLine("use (entity): Use the specified entity");
					Console.WriteLine("\t(entity): The target entity to use");
					break;
				case "take":
					Console.WriteLine("take (item): Take an item into your inventory");
					Console.WriteLine("\t(item): The target item to take");
					break;
				default:
					Console.WriteLine("exit: Exits the application");
					Console.WriteLine("help: Gives info on commands");
					Console.WriteLine("look: Gives description of room or entity");
					Console.WriteLine("move: Move to an adjacent room");
					Console.WriteLine("take: Take an item into your inventory");
					Console.WriteLine("use:  Use the specified entity");
					break;
			}
		}

		private void Exit(string[] args)
		{
			System.Environment.Exit(0);
		}

		private void Look(string[] args)
		{
			// Get name of entity to look at
			//string name;
			//if(args.Length == 1) name = null;
			//else name = args[1].ToLower();

			if(args.Length == 1)
			{
				Console.WriteLine(player.CurrentRoom.Description);
			}
			else
			{
				string name = args[1].ToLower();

				if(name == "-l")
				{
					// List inventory
					if(player.Inventory.Count != 0)
					{
						Console.WriteLine("Inventory:");

						foreach(Item item in player.Inventory)
						{
							Console.WriteLine($" - {item.Name}");
						}
					}

					// List in room
					if(player.CurrentRoom.Entities.Count != 0) 
					{
						Console.WriteLine("Room:");

						foreach(Entity entity in player.CurrentRoom.Entities)
						{
							Console.WriteLine($" - {entity.Name}");
						}
					}
					
				}
				else
				{
					// Check inventory
					if(player.Inventory.Count != 0)
					{
						Item item = player.Inventory.Find(item => item.Name.ToLower() == name.ToLower());
						if(player.Inventory.Contains(item))
						{
							Console.WriteLine(item.Description);
							return;
						}
					}

					// Check in room
					if(player.CurrentRoom.Entities.Count != 0)
					{
						Entity entity = player.CurrentRoom.FindEntity(name);
						if(player.CurrentRoom.Entities.Contains(entity))
						{
							Console.WriteLine(entity.Description);
							return;
						}
					}
				}
			}
		}

		private void Use(string[] args)
		{
			string name;
			if(args.Length == 1)
			{
				Console.WriteLine("What do you want to use?\n\t> ");
				name = Console.ReadLine();
			}
			else name = args[1];

			// Check inventory
			if(player.Inventory.Count != 0)
			{
				Item item = player.Inventory.Find(item => item.Name.ToLower() == name.ToLower());
				if(player.Inventory.Contains(item))
				{
					item.Interact(player);
					return;
				}
			}

			// Check in room
			if(player.CurrentRoom.Entities.Count != 0)
			{
				Entity entity = player.CurrentRoom.FindEntity(name);
				if(player.CurrentRoom.Entities.Contains(entity))
				{
					entity.Interact(player);
					return;
				}
			}

			Console.WriteLine($"Entity \"{name}\" not found. See all entities in room with look -l");
		}

		private void Move(string[] args)
		{
			// Get room name
			string roomName;
			if(args.Length == 1)
			{
				Console.WriteLine("What is the name of the room you want to move to?\n\t> ");
				roomName = Console.ReadLine();
			}
			else roomName = args[1];

			Room currentRoom = player.CurrentRoom;

			// Find and use correct door
			List<Door> doors = currentRoom.FindAll<Door>();
			Door enterDoor = currentRoom.FindDoorTo(roomName); 

			if(doors.Contains(enterDoor)) enterDoor.Interact(player);

			// Use as backup
			//Door door = (Door)currentRoom.Entities.Find(
			//	dr => dr is Door && dr.Name.ToLower() == $"{roomName} Door".ToLower());
			//if(currentRoom.Entities.Contains(door))
			//{
			//	door.Interact(player);
			//}

			// Check if player moved
			if(currentRoom != player.CurrentRoom)
			{
				Console.WriteLine($"You moved into the \"{player.CurrentRoom.Name}\" room");
			}
			else
			{
				Console.WriteLine($"You couldn't move into \"{roomName}\" room");
			}
		}

		private void Take(string[] args)
		{
			string name;
			if(args.Length == 1)
			{
				Console.WriteLine("What do you want to take?\n\t> ");
				name = Console.ReadLine();
			}
			else name = args[1];

			// Check in room
			if(player.CurrentRoom.Entities.Count != 0)
			{
				Entity entity = player.CurrentRoom.FindEntity(name);
				if(player.CurrentRoom.Entities.Contains(entity))
				{
					if(entity is Item && player.CanTakeItem((Item)entity))
					{
						player.AddItem((Item)entity);
						player.CurrentRoom.Entities.Remove(entity);
						Console.WriteLine($"You took the {entity.Name}");

						return;
					}
					else Console.WriteLine($"You cannot take {entity.Name}");
				}
			}

			Console.WriteLine($"Entity \"{name}\" not found. See all entities in room with look -l");
		}

		#endregion
	}
}
