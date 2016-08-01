using System;
using System.Collections.Generic;
using System.Linq;

namespace CsomInspector.Core.Actions
{
	public class ExceptionHandlingScope : ActionBase
	{
		protected ExceptionHandlingScope()
		{
		}

		public override IEnumerable<IObjectTreeNode> Children => new[] {
			new ObjectTreeNode("Exception hadnling scopes are not supported.", Enumerable.Empty<IObjectTreeNode>())
		};

		public static ExceptionHandlingScope FromXml() =>
			new ExceptionHandlingScope();

		public override String ToString() => "ExceptionHandlingScope";
	}
}