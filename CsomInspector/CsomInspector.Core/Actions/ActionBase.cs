using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;

namespace CsomInspector.Core.Actions
{
	public abstract class ActionBase : IObjectTreeNode, INotifyPropertyChanged
	{
		protected ActionBase()
		{
			MergedActions = new List<ActionBase>();
		}

		protected const String _elementNamespace = "http://schemas.microsoft.com/sharepoint/clientquery/2009";

		public event PropertyChangedEventHandler PropertyChanged;

		public virtual IEnumerable<IObjectTreeNode> Children => Enumerable.Empty<IObjectTreeNode>();

		public Int32 Id { get; protected set; }

		public Boolean IsHighlighted { get; private set; }

		public Int32 ObjectPathId { get; protected set; }

		public List<ActionBase> MergedActions { get; }
		public List<Result> Results { get; internal set; }

		public static ActionBase FromXml(XElement actionElement)
		{
			if (actionElement == null)
			{
				throw new ArgumentNullException(nameof(actionElement));
			}

			var name = actionElement.Name;
			if (actionElement.Name.NamespaceName != _elementNamespace)
			{
				throw new ArgumentException("Specified element does not match CSOM request namespace.", nameof(actionElement));
			}

			var idAttribute = actionElement.Attribute(XName.Get("Id"));
			var objectPathAttribute = actionElement.Attribute(XName.Get("ObjectPathId"));

			var action = CreateAction(actionElement);
			action.Id = Convert.ToInt32(idAttribute.Value);
			action.ObjectPathId = Convert.ToInt32(objectPathAttribute.Value);

			return action;
		}

		public void Highlight(Boolean isHighlighted)
		{
			IsHighlighted = isHighlighted;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsHighlighted)));
		}

		private static ActionBase CreateAction(XElement element)
		{
			var name = element.Name.LocalName;

			switch (name)
			{
				case "Query":
					return QueryAction.FromXml(element);
				case "Method":
					return MethodAction.FromXml(element);
				case "SetProperty":
					return SetPropertyAction.FromXml(element);
				default:
					return GenericAction.FromXml(element);
			}
		}
	}
}