using System;
using System.Collections.Generic;
using System.Text;

namespace Solun.Entities
{
	abstract class Entity
	{
		protected NameHolder allNames = new NameHolder();

		protected string description;

		public NameHolder AllNames => allNames;
		public string Name => allNames.Name;
		public string Description => description;

		public Entity() { }

		public Entity(NameHolder names, string description)
		{
			allNames = names;
			this.description = description;
		}

		public abstract void Interact(Entity entity);
		
		public override string ToString() => Name; // Only if entity is not implied

		public bool IsNamed(string name) => allNames.IsNamed(name);
	}
}
