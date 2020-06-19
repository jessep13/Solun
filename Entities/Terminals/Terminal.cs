using Console = Colorful.Console;
using Solun.Entities.Items;
using Solun.Entities.Mobs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Solun.Entities.Terminals
{
	abstract class Terminal : Entity
	{
		protected int id; // 4 digits

		protected Item attachment = null;

		public Terminal() { }

		public Terminal(int id)
		{
			SetName(id);
		}

		protected void SetName(int id)
		{
			this.id = id;
			if(id > 9999 || id < 0) throw new Exception("Int is invalid");

			allNames = new NameHolder($"T#{id}");

			description = $"A standard terminal with the numbers {id} etched on the side. The monitor awaits an input";
		}

		public virtual bool IsAttachable(Item item) => false; 

		public override void Interact(Entity entity)
		{
			if(entity is Mob) PreExecute();
			else if(entity is Item && IsAttachable((Item)entity)) attachment = (Item)entity;
		}

		protected void PreExecute()
		{
			Color fg = Console.ForegroundColor;
			Console.ForegroundColor = Color.Lime;
			Execute();
			Console.ForegroundColor = fg;
		}

		protected abstract void Execute();
	}
}
