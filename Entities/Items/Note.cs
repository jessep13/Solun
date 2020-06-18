using Console = Colorful.Console;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Solun.Entities.Items
{
	class Note : Item
	{
		string message;

		public Note(NameHolder names, string description, string message)
		{
			allNames = names;
			this.description = description;
			
			weight = 0;

			this.message = message;
		}

		public override void Interact(Entity entity)
		{
			Console.WriteLine(message, Color.White);
		}
	}
}
