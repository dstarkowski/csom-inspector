using System;

namespace CsomInspector.Core
{
	public static class TypeMappingHelper
	{
		public static String GetTypeName(Guid typeId)
		{
			var type = typeId.ToString("D").ToLower();

			switch (type)
			{
				case "3747adcd-a3c3-41b9-bfab-4a64dd2f1e0a":
					return "Microsoft.SharePoint.SPContext";

				case "981cbc68-9edc-4f8d-872f-71146fcbb84f":
					return "Microsoft.SharePoint.Taxonomy.TaxonomySession";

				case "3d248d7b-fc86-40a3-aa97-02a75d69fb8a":
					return "Microsoft.SharePoint.SPQuery";

				case "cf560d69-0fdb-4489-a216-b6b47adf8ef8":
					return "Microsoft.Office.Server.UserProfiles.PeopleManager";

				case "8d2ac302-db2f-46fe-9015-872b35f15098":
					return "Microsoft.SharePoint.Search.Query.SearchExecutor";

				case "80173281-fffd-47b6-9a49-312e06ff8428":
					return "Microsoft.SharePoint.Search.Query.KeywordQuery";

				default:
					return typeId.ToString();
			}
		}
	}
}