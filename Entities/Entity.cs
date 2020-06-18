using System;
using System.Collections.Generic;
using System.Text;

namespace Solun.Entities
{
	abstract class Entity
	{
		protected string name;
		protected string description;

		public string Name => name;
		public string Description => description;

		public Entity() { }

		public Entity(string name, string description)
		{
			this.name = name;
			this.description = description;
		}

		public abstract void Interact(Entity entity);
		
		public override string ToString() => name; // Only if entity is not implied
	}
}
