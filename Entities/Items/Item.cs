using System;
using System.Collections.Generic;
using System.Text;

namespace Solun.Entities.Items
{
	abstract class Item : Entity
	{
		protected int weight;

		public int Weight => weight;

		public Item() { }

		public Item(string name, string description, int weight)
		{
			this.name = name;
			this.description = description;

			this.weight = weight;
		}
	}
}
