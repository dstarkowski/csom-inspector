using System;
using System.Collections.Generic;

namespace CsomInspector.Core
{
	public class TypeMappings
	{
		public TypeMappings()
		{
			_mappings = new Dictionary<Guid, string>();
		}

		private static TypeMappings _current;
		private Dictionary<Guid, String> _mappings;
		public static TypeMappings Current
		{
			get
			{
				if (_current == null)
				{
					_current = CreateMappings();
				}

				return _current;
			}
		}

		public static TypeMappings CreateMappings()
		{
			var mappings = new TypeMappings();
			mappings
				.Add("{3747adcd-a3c3-41b9-bfab-4a64dd2f1e0a}", "Microsoft.SharePoint.SPContext")
				.Add("{981cbc68-9edc-4f8d-872f-71146fcbb84f}", "Microsoft.SharePoint.Taxonomy.TaxonomySession")
				.Add("{3d248d7b-fc86-40a3-aa97-02a75d69fb8a}", "Microsoft.SharePoint.SPQuery")
				.Add("{cf560d69-0fdb-4489-a216-b6b47adf8ef8}", "Microsoft.Office.Server.UserProfiles.PeopleManager")
				.Add("{8d2ac302-db2f-46fe-9015-872b35f15098}", "Microsoft.SharePoint.Search.Query.SearchExecutor")
				.Add("{80173281-fffd-47b6-9a49-312e06ff8428}", "Microsoft.SharePoint.Search.Query.KeywordQuery")
				.Add("{4e46b28c-e27f-4964-a8d4-fc25658d86d1}", "Microsoft.SharePoint.SPFeature")
				.Add("{8b9c0015-193d-4062-8e98-8d23c303eedd}", "Microsoft.SharePoint.SPFeatureCollection")
				.Add("{c4121b04-0f57-4b1d-a145-d25426b16480}", "Microsoft.SharePoint.SPField")
				.Add("{f9ee4627-2914-46cd-806a-4921f96a0c72}", "Microsoft.SharePoint.SPFieldCalculated")
				.Add("{a0b73943-fabc-47d3-b2c8-ec41b1216b1d}", "Microsoft.SharePoint.SPFieldChoice")
				.Add("{d449d756-e113-4d27-a5e7-609cbc3eba7e}", "Microsoft.SharePoint.SPFieldCollection")
				.Add("{379b8994-f0e7-4c74-a1e6-185eb8600c3c}", "Microsoft.SharePoint.SPFieldComputed")
				.Add("{e03ca5f6-5f18-47f3-8ab4-74caba56ee1e}", "Microsoft.SharePoint.SPFieldCurrency")
				.Add("{4f9dc9b4-d900-40eb-94b9-008abbfb22e1}", "Microsoft.SharePoint.SPFieldDateTime")
				.Add("{d5a367f2-3b74-4984-ab50-9a700883c90b}", "Microsoft.SharePoint.SPFieldGeolocation")
				.Add("{97650aff-7e7b-44be-ac6e-d559f7f897a2}", "Microsoft.SharePoint.SPFieldGeolocationValue")
				.Add("{768b27aa-a4e0-4cfb-9956-1f7f0e93fb42}", "Microsoft.SharePoint.SPFieldGuid")
				.Add("{e2d99203-868f-4211-ac76-f6bca0ff94ee}", "Microsoft.SharePoint.SPFieldLink")
				.Add("{6d87e76a-b8a8-4634-8170-082b1d454bfd}", "Microsoft.SharePoint.SPFieldLinkCollection")
				.Add("{63fb2c92-8f65-4bbb-a658-b6cd294403f4}", "Microsoft.SharePoint.SPFieldLinkCreationInformation")
				.Add("{ee47ff61-3260-43a9-be22-829a1fa85b44}", "Microsoft.SharePoint.SPFieldLookup")
				.Add("{f1d34cc0-9b50-4a78-be78-d5facfcccfb7}", "Microsoft.SharePoint.SPFieldLookupValue")
				.Add("{284c160f-3783-4344-a471-80e032719f26}", "Microsoft.SharePoint.SPFieldMultiChoice")
				.Add("{9ae17ecc-11a6-4433-8b51-7ecb865ffe05}", "Microsoft.SharePoint.SPFieldMultiLineText")
				.Add("{e32d2a19-d2a8-428c-b056-ea71ede547ce}", "Microsoft.SharePoint.SPFieldNumber")
				.Add("{47cba3a7-3327-4fa1-ac10-73a7a182fe05}", "Microsoft.SharePoint.SPFieldText")
				.Add("{b1ae9217-e0b0-4e34-9604-bd2462647ee9}", "Microsoft.SharePoint.SPFieldUrl")
				.Add("{fa8b44af-7b43-43f2-904a-bd319497011e}", "Microsoft.SharePoint.SPFieldUrlValue")
				.Add("{ebd2fb89-e8a2-46c4-b317-9b2347121765}", "Microsoft.SharePoint.SPFieldUser")
				.Add("{c956ab54-16bd-4c18-89d2-996f57282a6f}", "Microsoft.SharePoint.SPFieldUserValue")
				.Add("{df28be1e-74b5-4b21-b73a-2bbac0a23d8a}", "Microsoft.SharePoint.SPFile")
				.Add("{d367b17c-170b-4691-a1e3-8bccf7686ce4}", "Microsoft.SharePoint.SPFileCollection")
				.Add("{f5c8173c-cae6-4469-a7af-3879ca3c617c}", "Microsoft.SharePoint.SPFileCreationInformation")
				.Add("{96e4bc1b-e67f-4967-9327-36b79e20aebc}", "Microsoft.SharePoint.SPFileVersion")
				.Add("{dbe8175a-505d-4eff-bec4-6c809709808b}", "Microsoft.SharePoint.SPFolder")
				.Add("{b6b425aa-9e17-4205-a4aa-b82c2c3f884d}", "Microsoft.SharePoint.SPFolderCollection")
				.Add("{50aaca3c-fa54-47d2-b946-a2839ee956a9}", "Microsoft.SharePoint.SPForm")
				.Add("{078611ea-ce4d-45c0-9b7a-d4b1b46cc327}", "Microsoft.SharePoint.SPFormCollection")
				.Add("{e54ad5f1-ce4e-453b-b7f7-aea6556c9c40}", "Microsoft.SharePoint.SPGroup")
				.Add("{0b9f0e6c-2c15-425e-b0b2-961f78bf1ecf}", "Microsoft.SharePoint.SPGroupCollection")
				.Add("{9fd1540e-59e6-47fa-9a00-5173c9c35785}", "Microsoft.SharePoint.SPGroupCreationInformation")
				.Add("{d89f0b18-614e-4b4a-bac0-fd6142b55448}", "Microsoft.SharePoint.SPList")
				.Add("{1e78b736-61f0-441c-a785-10fc25387c8d}", "Microsoft.SharePoint.SPListCollection")
				.Add("{e247b7fc-095e-4ea4-a4c9-c5d373723d8c}", "Microsoft.SharePoint.SPListCreationInformation")
				.Add("{53cc48c0-1777-47b7-99ca-729390f06602}", "Microsoft.SharePoint.SPListItem")
				.Add("{1722df25-a4d3-44bb-a1c6-04dbb90e9d91}", "Microsoft.SharePoint.SPListItemCollection")
				.Add("{922354eb-c56a-4d88-ad59-67496854efe1}", "Microsoft.SharePoint.SPListItemCollectionPosition")
				.Add("{54cdbee5-0897-44ac-829f-411557fa11be}", "Microsoft.SharePoint.SPListItemCreationInformation")
				.Add("{d772ecd1-daa3-4cb1-9ea1-feea1e383fb2}", "Microsoft.SharePoint.SPListTemplate")
				.Add("{23748d10-16a1-4946-a38b-98fdec0e0ec8}", "Microsoft.SharePoint.SPListTemplateCollection")
				.Add("{d6aa87a7-71b3-4bbe-bca7-00ab1bd7d37f}", "Microsoft.SharePoint.SPNavigation")
				.Add("{cd5d6053-7ffd-41ac-bf36-7b856320a122}", "Microsoft.SharePoint.SPNavigationNode")
				.Add("{2d818ed7-8fef-4a1c-bceb-a9404dfa482f}", "Microsoft.SharePoint.SPNavigationNodeCollection")
				.Add("{7aaaa605-79a9-4fda-ae1e-db952e5083e0}", "Microsoft.SharePoint.SPNavigationNodeCreationInformation")
				.Add("{8a76e712-17a1-4a40-b2df-cca7c060d78f}", "Microsoft.SharePoint.SPPrincipal")
				.Add("{07da03be-4d19-48f3-9c5f-7c67b134a93b}", "Microsoft.SharePoint.SPRoleAssignment")
				.Add("{2690207a-e174-4d49-b2ca-cff663225dc1}", "Microsoft.SharePoint.SPRoleAssignmentCollection")
				.Add("{aa7ecb4a-9c7e-4ad9-bd20-58a2775e5ad7}", "Microsoft.SharePoint.SPRoleDefinition")
				.Add("{07bf1941-6953-4761-b114-58374b4aaf57}", "Microsoft.SharePoint.SPRoleDefinitionBindingCollection")
				.Add("{964b9ab0-d026-4487-99d1-e06450963cc9}", "Microsoft.SharePoint.SPRoleDefinitionCollection")
				.Add("{1b1bf348-994e-44fd-823f-0748f5ad94c8}", "Microsoft.SharePoint.SPSecurableObject")
				.Add("{e1bb82e8-0d1e-4e52-b90c-684802ab4ef6}", "Microsoft.SharePoint.SPSite")
				.Add("{ae70d2a4-ec46-4ed9-9b1e-9d0245754463}", "Microsoft.SharePoint.SPUser")
				.Add("{e090781e-8899-4672-9b3d-a78f49fad19d}", "Microsoft.SharePoint.SPUserCollection")
				.Add("{6ecd8af6-bed3-4a74-be76-1ec981b350e1}", "Microsoft.SharePoint.SPUserCreationInformation")
				.Add("{232f8709-5dfd-44cf-a35b-7d8538c9336e}", "Microsoft.SharePoint.SPUserCustomAction")
				.Add("{70d1cb2d-d304-4d96-9b54-74b3f400fa28}", "Microsoft.SharePoint.SPUserCustomActionCollection")
				.Add("{2399f45d-1784-4965-9a5f-a3415790a0d0}", "Microsoft.SharePoint.SPView")
				.Add("{03c5d7a9-9541-4482-9919-ca0cccf565a0}", "Microsoft.SharePoint.SPViewCollection")
				.Add("{a3547807-7266-42f3-b055-afa6e840e458}", "Microsoft.SharePoint.SPViewCreationInformation")
				.Add("{af975f76-8a94-4e6d-8325-bd1e20b7c301}", "Microsoft.SharePoint.SPViewFieldCollection")
				.Add("{a489add2-5d3a-4de8-9445-49259462dceb}", "Microsoft.SharePoint.SPWeb")
				.Add("{c197d59e-d070-43bf-ad5e-10d6152e38a6}", "Microsoft.SharePoint.SPWebCollection")
				.Add("{8f9e9fbe-189e-492f-884f-98f9ef9cc4d6}", "Microsoft.SharePoint.SPWebCreationInformation");

			return mappings;
		}

		public String Get(Guid guid)
		{
			return _mappings[guid];
		}

		private TypeMappings Add(Guid guid, String name)
		{
			_mappings.Add(guid, name);

			return this;
		}

		private TypeMappings Add(String guid, String name)
		{
			_mappings.Add(Guid.Parse(guid), name);

			return this;
		}
	}
}