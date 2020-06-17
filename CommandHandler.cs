using Console = Colorful.Console;
using Solun.Entities;
using Solun.Entities.Items;
using Solun.Entities.Mobs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Solun
{
	class CommandHandler
	{
		delegate void Command(string[] args);

		Player player;

		static List<Command> Commands = new List<Command>();

		public CommandHandler(Player player)
		{
			this.player = player;
			
			// List them all here
			Commands.Add(Help);
			Commands.Add(Exit);
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
				args[i].Replace('_', ' ');

				// Print each argument if DEBUG
#if DEBUG
				Console.WriteLine($"[{i}]: |{args[i]}|", Color.HotPink);
#endif
			}

			// Find which command to use
			foreach(Command com in Commands)
			{
#if DEBUG
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
		1. Make a "private static void {command}(string[] args)" method
		2. Update DefineCommands by adding "Commands.Add({command});" line
		3. Add documentation to Help command with a new case and by adding a line in the default case
		 
		 */

		private static void Help(string[] args)
		{
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
				default:
					Console.WriteLine("exit: Exits the application");
					Console.WriteLine("help: Gives info on commands");
					break;
			}
		}

		private static void Exit(string[] args)
		{
			System.Environment.Exit(0);
		}

		#endregion
	}
}
