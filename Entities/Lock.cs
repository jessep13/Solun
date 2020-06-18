using System;
using System.Collections.Generic;
using System.Text;

namespace Solun.Entities
{
	class Lock : Entity
	{
		Entity unlockEntity;

		bool isLocked;

		public bool IsLocked => isLocked;

		public Lock(NameHolder names, string description, Entity unlockEntity, bool isLocked)
		{
			allNames = names;
			this.description = description;

			this.unlockEntity = unlockEntity;
			this.isLocked = isLocked;
		}

		public override void Interact(Entity entity)
		{
			if(entity == unlockEntity) isLocked = !isLocked;
		}
	}
}
