using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CsomInspector.Core.ObjectPaths
{
	public class IdentityParameter : IObjectTreeNode
	{
		static IdentityParameter()
		{
			_nameMappings = new Dictionary<String, String>(StringComparer.InvariantCultureIgnoreCase)
			{
				["site"] = "Site",
				["web"] = "Web",
				["list"] = "List",
				["item"] = "List item",
				["folder"] = "Folder",
				["file"] = "File",
				["field"] = "Field",
				["contenttype"] = "Content type",
				["view"] = "View",
				["u"] = "User",
				["g"] = "Group",
				["st"] = "Term store",
				["gr"] = "Term group",
				["se"] = "Term set",
				["rd"] = "Role definition",
				["ra"] = "Role assignment"
			};
		}

		private static Dictionary<String, String> _nameMappings;

		public IdentityParameter(String objectName, String guid)
		{
			Name = objectName;
			_guid = guid;
		}

		private String _guid;

		public IEnumerable<IObjectTreeNode> Children => Enumerable.Empty<IObjectTreeNode>();

		public String Name { get; }

		public static IEnumerable<IdentityParameter> FromIdentityString(String identityString)
		{
			var pattern = new Regex(@":([\w]+):([^:]*)", RegexOptions.IgnoreCase);
			var matches = pattern.Matches(identityString);

			foreach (Match match in matches)
			{
				var name = GetName(match.Groups[1].Value);
				var guid = match.Groups[2].Value;

				yield return new IdentityParameter(name, guid);
			}
		}

		private static String GetName(String key)
		{
			if (_nameMappings.ContainsKey(key))
			{
				return _nameMappings[key];
			}

			return $"{key} (?)";
		}

		public override String ToString() => $"{Name}: {_guid}";
	}
}