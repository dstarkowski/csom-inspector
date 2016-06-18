using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CsomInspector.Core.ObjectPaths
{
	public class Identity : ObjectPath
	{
		private Identity(String name, IEnumerable<IdentityParameter> parameters)
		{
			Parameters = parameters.ToList();
			Name = name;
		}

		public override IEnumerable<IObjectTreeNode> Children => Parameters;

		public String Name { get; private set; }

		public IEnumerable<IdentityParameter> Parameters { get; private set; }

		public override String Type => "Identity";

		public override String ToString() => $"{Name} by identity";

		//TODO: Whats the front part? Tenant? Realm?
		//15746a9d-205a-3000-9475-7593ef4cfb9e|740c6a0b-85e2-48a0-a494-e0f1759d4aa7:site:71e613d5-6a68-46eb-97f4-a73284fcaceb:web:6d055fe6-0dfe-4d3a-a0ad-74a9a07b878c

		internal static Identity FromXml(XElement element)
		{
			var nameAttribute = element.Attribute(XName.Get("Name"));
			var identityString = nameAttribute.Value;

			var parameters = IdentityParameter.FromIdentityString(identityString);
			var name = parameters
				.Select(p => p.Name)
				.Last();

			return new Identity(name, parameters);
		}
	}
}