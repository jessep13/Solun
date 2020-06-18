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
		List<string> names = new List<string>();
		//string name;
		string description;

		public List<string> Names => names;
		public string Name => Names.First();
		public string Description => description;

		List<Entity> entities = new List<Entity>();

		Sector sector;

		public List<Entity> Entities => entities;

		public Room(string name, string description, Sector sector)
		{
			names.Add(name);
			this.description = description;

			this.sector = sector;
		}

		public Room(List<string> names, string description, Sector sector)
		{
			this.names = names;
			this.description = description;

			this.sector = sector;
		}

		public Room(string[] names, string description, Sector sector)
		{
			this.names = names.ToList();
			this.description = description;

			this.sector = sector;
		}

		public bool IsNamed(string name) => names.Contains(names.Find(nm => nm.ToLower() == name.ToLower()));

		public void AddEntity(Entity entity) => entities.Add(entity);

		public Entity FindEntity(string name) => entities.Find(entity => entity.Name.ToLower() == name.ToLower());

		public List<Type> FindAll<Type>() => entities.OfType<Type>().ToList();

		public EntityType FindEntityType<EntityType>(string name)
			=> FindAll<EntityType>().Find(entity => entity.ToString().ToLower() == name.ToLower());

		public Door FindDoorTo(Room room) => FindDoorTo(room.Name);

		public Door FindDoorTo(string name) //=> FindAll<Door>().Find(door => door.EndRoom.IsNamed(name));
		{
			List<Door> validDoors = FindAll<Door>().FindAll(door => door.EndRoom.IsNamed(name));
			switch(validDoors.Count)
			{
				case 0:
					return null;
				case 1:
					return validDoors.First();
				default:
					throw new Exception("Multiple Objects Found");
			}
		}
	}
}
