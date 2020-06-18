using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solun
{
	class NameHolder
	{
		List<string> names = new List<string>();

		public List<string> Names => names;
		public string Name => names.First();

		public NameHolder() => names = new List<string>();

		public NameHolder(string name) => names.Add(name);

		public NameHolder(string[] names) => this.names = names.ToList();

		public NameHolder(List<string> names) => this.names = names;

		public bool IsNamed(string name) => names.Contains(names.Find(nm => nm.ToLower() == name.ToLower()));
	}
}
