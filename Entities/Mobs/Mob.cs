using Solun.Entities.Items;
using Solun.World;
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

		protected Room currentRoom;

		public Room CurrentRoom => currentRoom;

		public Mob() { }
		
		public Mob(string name, string description, int maxHealth, Room currentRoom)
		{
			this.name = name;
			this.description = description;

			this.maxHealth = maxHealth;
			health = maxHealth;

			this.currentRoom = currentRoom;
			currentRoom.Entities.Add(this);
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

		public void MoveToRoom(Room room, Entity entity)
		{
			if(entity is Door)
			{
				currentRoom.Entities.Remove(this);
				currentRoom = room;
				currentRoom.Entities.Add(this);
			}
		}
	}
}
