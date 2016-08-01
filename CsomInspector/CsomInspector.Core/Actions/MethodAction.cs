using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CsomInspector.Core.Actions
{
	public class MethodAction : ActionBase
	{
		private MethodAction(ObjectPaths.Method method)
		{
			_method = method;
		}

		private ObjectPaths.Method _method;

		public override IEnumerable<IObjectTreeNode> Children => _method.Children;

		public override String ToString() => _method.ToString();

		internal static new MethodAction FromXml(XElement actionElement)
		{
			var nameAttribute = actionElement.Attribute(XName.Get("Name"));
			var parametersElement = actionElement.Element(XName.Get("Parameters"));
			var method = ObjectPaths.Method.FromXml(actionElement);

			return new MethodAction(method);
		}
	}
}