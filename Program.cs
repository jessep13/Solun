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

		public static Type CheckMulti<Type>(List<Type> typeList)
		{
			switch(typeList.Count)
			{
				case 0:
					return default(Type);
				case 1:
					return typeList.First();
				default:
					throw new Exception("Multiple Objects Found");
			}
		}

		static void CreateSector()
		{
			#region Cell
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
				new NameHolder(new string[]
				{
					"Cell Note",
					"Cell",
					"Note"
				}),
				"The note appears to be typed, with a signature on the bottem.",
				"Hello. If you are reading this, it appears that you have learned to use your brain to pick up this note and to read it. I will explain later where you are, but for now you shouldn't be too concerned about that since you will be out of here in less than an hour. To open the door in front of you, there is a Lock Terminal awaiting for a code to open the door. You must enter \"1492\" into it in order to be granted access. The next challenge will lie in the next room. \nGood Luck. \n\t - F. E."));

			#endregion

			#region Sub Labs

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

			// Add tech lab
			sector.AddRoom(
				new NameHolder(new string[]
				{
					"Tech Lab",
					"Tech",
					"Lab"
				}),
				"The room looks like a workspace for mechanical and electrical engineers. One side has mechanical parts scatters, while the other has PCBs and sodering kits on tabels.");
			sector.LinkRooms("Tech", "Main");

			sector.FindRoom("Tech").AddEntity(new MailTerminal(1002));
			sector.FindRoom("Tech").FindEntityType<MailTerminal>("T#1002").NewMessage(
				"Benny Konwell",
				"Shen Qu",
				"Help With Lab Code",
				"Hey Shen.\nI hear that you know a thing or two about how the lock terminal codes work. I was wondering if you knew anything regarding the code for the terminal in the main lab? I really want to feel some of the outside world for once, even if I die from being too exposed to the elements. I hope to hear back from you soon.\nThanks,\n\tBenny Konwell");
			sector.FindRoom("Tech").FindEntityType<MailTerminal>("T#1002").NewMessage(
				"Shen Qu",
				"Benny Konwell",
				"Re: Help With Lab Code",
				"Hello Benny.\nWhen they said that I knew \"a thing or two about how the lock terminal codes work,\" they didn't mean that I knew the code to open the lab terminal. All of the lock terminals have a 4 digit code that will toggle a designated lock that they are wired to. That means there are about 10000 code comboninations (from 0000 to 9999). Checking one code every secound (assuming you could type that fast) would take 2 hours, 46 minutes, and 40 secounds. By the time you found it, everyone would know what you just tried to do, and you would be removed from this facility.\nWith all that said, there is a way to get the code to that lock terminal that is significantly faster than brute force. I manufactured a device I'm calling the Nuts&Bolts-Cracker. This device allows you to enter a guess code, and it will tell you how many digits were correct either in the right place (called nuts), or in the wrong place (bolts). So let's say the code was 1234 (whoever thought this was a good code should be fired on the spot if this was real), and you entered 1524, then the cracker would display [NNB], meaning there were two nuts and one bolt. This is because the 1 and 4 are in the right place (hence two nuts), but while the 2 is in the code, it is in an incorrect spot (hence one bolt). A match would result in [NNNN], which is the goal you are reaching for when cracking the code. There is one major flaw with this device: each device can only be used on one terminal, and after 10 guesses, the device will cease to function to prevent causing the terminal to be overexposed to voltage, cause the system to at best be wipe or, at worst, blow up the entire terminal, killing the hacker in the process.\nIf you wish to use this device to try your crazy idea, you can grab five of them in my drawer in my workspace. When you get outside the facility, hide the device so they can't trace it back to me. I will cover my tracks on my end, but I hope you do the same, or I will deny any responsibility and call you a traitor to our cause.\nGood Luck.\n\tShen Qu\nP.S. Here's a tip for cracking code: Try using 0000 (or just 0) as your first code. You would never get any bolts since there are 0s in every place. After every guess you add one color at a time to make it manageable. I hope this tip helps.");
			sector.FindRoom("Tech").FindEntityType<MailTerminal>("T#1002").NewMessage(
				"Deshi Tollup",
				"SapphireLabs.All",
				"The Exposure Incident",
				"To all the staff at Sapphire Labs:\nAs you are all aware, three days ago Benny Konwell went on an absence without leave (or informally AWOL) as he managed to enter the code to the terminal and escaped our custody. While under normal circumstances we would not think too harshly of this incident, the fact that this laboratory and its staff agreed to work on Project Automaton in secrecy for a period of three years makes it my responsibility as Project Lead to enforce changes to the faucility to prevent this from occuring again and to locate Konwell before he has a chance to communicate with our opposition. We will now scramble the code every hour to prevent anyone from cracking the code. We will also launch an investigation to determine how Konwell accessed the code to the main doors. Anyone with information on this incident should mail me as soon as possible. I look forward to the finished results of this project.\n\t Deshi Tollup "
				);

			sector.FindRoom("Tech").AddEntity(new CodeCracker());

			#endregion

			// Creat Hallway
			sector.AddRoom(
				new NameHolder("Hallway"),
				"A long hallway that seems to lead outside.");

			// Create Lock Terminal in Main Lab
			sector.FindRoom("Main").AddEntity(new LockTerminal(
				1003,
				8525));

			// Link Main Lab and Hallway
			sector.LinkRooms(
				"Hallway",
				"Main",
				sector.FindRoom("Main").FindEntity("T#1003"),
				true);

			// Set lock for Lock Terminal
			sector.FindRoom("Main").FindEntityType<LockTerminal>("T#1003").lockEntity
				= sector.FindRoom("Main").FindDoorTo("Hallway").DoorLock;
		}

		static void SpawnPlayer(string roomName)
		{
			Room spawnRoom = sector.FindRoom(roomName);
			if(!sector.Rooms.Contains(spawnRoom)) throw new Exception("Spawn room not found");
			else player = new Player(spawnRoom);
		}
	}
}
