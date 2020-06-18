using Console = Colorful.Console;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Solun.Entities.Terminals
{
	abstract class Terminal : Entity
	{
		protected int id; // 4 digits

		public Terminal() { }

		public Terminal(int id)
		{
			SetName(id);
			description = $"A standard terminal with the numbers {id} etched on the side. The monitor awaits an input";
			
			this.id = id;
			if(id > 9999 || id < 0) throw new Exception("Int is invalid");
		}

		protected void SetName(int id) => allNames = new NameHolder($"T#{id}");

		public override void Interact(Entity entity)
		{
			Color fg = Console.ForegroundColor;
			Console.ForegroundColor = Color.Lime;
			Execute();
			Console.ForegroundColor = fg;
		}

		protected abstract void Execute();
	}
}
