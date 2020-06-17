using Solun.Entities.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solun.Entities.Mobs
{
	abstract class Mob : Entity
	{
		protected int health;
		protected int maxHealth;
		protected int maxWeight;

		public int Health => health;
		public int MaxHealth => maxHealth;
		public int MaxWeight => maxWeight;

		protected List<Item> inventory = new List<Item>();

		public List<Item> Inventory => inventory;

		// TODO: Implement armor and weapons

		public Mob() { }
		
		public Mob(string name, string description, int maxHealth)
		{
			this.name = name;
			this.description = description;

			this.maxHealth = maxHealth;
			health = maxHealth;
		}

		public int TotalWeight()
		{
			int total = 0;
			foreach(Item item in inventory) total += item.Weight;
			return total;
		}

		public bool CanTakeItem(Item item) => item.Weight + TotalWeight() <= maxWeight;

		public void AddItem(Item item)
		{
			if(CanTakeItem(item)) inventory.Add(item);
		}
	}
}
