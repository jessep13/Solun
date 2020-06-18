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

		public Item(NameHolder names, string description, int weight)
		{
			allNames = names;
			this.description = description;

			this.weight = weight;
		}
	}
}
