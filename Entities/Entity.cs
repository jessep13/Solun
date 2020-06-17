using System;
using System.Collections.Generic;
using System.Text;

namespace Solun.Entities
{
	abstract class Entity
	{
		protected string name;
		protected string description;

		public Entity() { }

		public Entity(string name, string description)
		{
			this.name = name;
			this.description = description;
		}

		public abstract void Interact(Entity entity);
	}
}
