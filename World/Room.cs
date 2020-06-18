using Solun.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solun.World
{
	class Room
	{
		string name;
		string description;

		public string Name => name;
		public string Description => description;

		List<Entity> entities = new List<Entity>();

		public List<Entity> Entities => entities;

		public Room(string name, string description)
		{
			this.name = name;
			this.description = description;
		}

		public Entity FindEntity(string name) => entities.Find(entity => entity.Name.ToLower() == name.ToLower());
	}
}
