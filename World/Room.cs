using Solun.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

		Sector sector;

		public List<Entity> Entities => entities;

		public Room(string name, string description, Sector sector)
		{
			this.name = name;
			this.description = description;

			this.sector = sector;
		}

		public void AddEntity(Entity entity) => entities.Add(entity);

		public Entity FindEntity(string name) => entities.Find(entity => entity.Name.ToLower() == name.ToLower());

		public List<Type> FindAll<Type>() => entities.OfType<Type>().ToList();

		public EntityType FindEntityType<EntityType>(string name)
			=> FindAll<EntityType>().Find(entity => entity.ToString().ToLower() == name.ToLower());

		public Door FindDoorTo(Room room) => FindDoorTo(room.Name);

		public Door FindDoorTo(string name) => FindAll<Door>().Find(door => door.EndRoom.Name.ToLower() == name.ToLower());
	}
}
