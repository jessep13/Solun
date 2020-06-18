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
		NameHolder allNames = new NameHolder();

		string description;

		public NameHolder AllNames => allNames;
		public string Name => allNames.Name;
		public string Description => description;

		List<Entity> entities = new List<Entity>();

		Sector sector;

		public List<Entity> Entities => entities;

		public Room(NameHolder names, string description, Sector sector)
		{
			allNames = names;
			this.description = description;

			this.sector = sector;
		}

		public bool IsNamed(string name) => allNames.IsNamed(name);

		public void AddEntity(Entity entity) => entities.Add(entity);

		public Entity FindEntity(string name) 
			=> Program.CheckMulti<Entity>(entities.FindAll(entity => entity.IsNamed(name)));
		//=> entities.Find(entity => entity.IsNamed(name));

		public List<Type> FindAll<Type>() => entities.OfType<Type>().ToList();

		public EntityType FindEntityType<EntityType>(string name)
			=> FindAll<EntityType>().Find(entity => entity.ToString().ToLower() == name.ToLower());

		public Door FindDoorTo(Room room) => FindDoorTo(room.Name);

		public Door FindDoorTo(string name) 
			=> Program.CheckMulti<Door>(FindAll<Door>().FindAll(door => door.EndRoom.IsNamed(name)));
	}
}
