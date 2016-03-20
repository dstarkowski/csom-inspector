using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CsomInspector.Core.ObjectPaths
{
	public class IdentityParameter : IObjectTreeNode
	{
		public IdentityParameter(String objectName, String guid)
		{
			Name = objectName;
			_guid = guid;
		}

		private String _guid;

		public IEnumerable<IObjectTreeNode> Children => Enumerable.Empty<IObjectTreeNode>();

		public String Name { get; private set; }

		public static IEnumerable<IdentityParameter> FromIdentityString(String identityString)
		{
			var pattern = new Regex($@":([\w]+):([\w\-]*)", RegexOptions.IgnoreCase);
			var matches = pattern.Matches(identityString);

			foreach (Match match in matches)
			{
				var name = match.Groups[1].Value;
				var guid = match.Groups[2].Value;

				yield return new IdentityParameter(name, guid);
			}
		}

		public override String ToString() => $"{Name}: {_guid}";
	}
}