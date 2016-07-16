using CsomInspector.Core;
using System;

namespace CsomInspector.Fiddler.Presentation
{
	public class RequestInfoViewModel : ViewModelBase
	{
		public String ApplicationName { get; private set; }

		public Version ClientLibraryVersion { get; private set; }

		public String ClientTag { get; set; }

		public String CorrelationId { get; private set; }

		public Version ServerLibraryVersion { get; private set; }

		public void SetSessionData(Request request, Response response)
		{
			ApplicationName = request?.ApplicationName;
			ClientLibraryVersion = request?.LibraryVersion;
			CorrelationId = response?.TraceCorrelationId;
			ServerLibraryVersion = response != null ? new Version(response.LibraryVersion) : null;
			ClientTag = request?.ClientTag;

			RaisePropertyChanged(
				nameof(ApplicationName),
				nameof(ClientLibraryVersion),
				nameof(ClientTag),
				nameof(CorrelationId),
				nameof(ServerLibraryVersion));
		}
	}
}