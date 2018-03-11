using System;
using System.Collections.Generic;
using System.Linq;

namespace CsomInspector.Core
{
	public class ErrorInfo : IObjectTreeNode
	{
		public IEnumerable<IObjectTreeNode> Children =>
			_childNodes ?? (_childNodes = CreateChildNodes());

		private IEnumerable<IObjectTreeNode> CreateChildNodes()
		{
			yield return new ObjectTreeNode($"{nameof(ErrorCode)}: {ErrorCode}", Enumerable.Empty<IObjectTreeNode>());
			yield return new ObjectTreeNode($"{nameof(ErrorMessage)}: {ErrorMessage}", Enumerable.Empty<IObjectTreeNode>());
			yield return new ObjectTreeNode($"{nameof(ErrorTypeName)}: {ErrorTypeName}", Enumerable.Empty<IObjectTreeNode>());
			yield return new ObjectTreeNode($"{nameof(ErrorValue)}: {ErrorValue}", Enumerable.Empty<IObjectTreeNode>());
			yield return new ObjectTreeNode($"{nameof(TraceCorrelationId)}: {TraceCorrelationId}", Enumerable.Empty<IObjectTreeNode>());
		}

		private IEnumerable<IObjectTreeNode> _childNodes;

		public String ErrorCode { get; set; }

		public String ErrorMessage { get; set; }

		public String ErrorTypeName { get; set; }

		public String ErrorValue { get; set; }

		public String TraceCorrelationId { get; set; }

		public override String ToString() =>
			"ErrorInfo";
	}
}