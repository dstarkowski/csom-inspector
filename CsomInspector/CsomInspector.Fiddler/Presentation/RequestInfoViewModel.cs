using CsomInspector.Core;
using System;
using System.Windows;

namespace CsomInspector.Fiddler.Presentation
{
	public class RequestInfoViewModel : ViewModelBase
	{
		public String ApplicationName { get; private set; }

		public Version ClientLibraryVersion { get; private set; }

		public String CorrelationId { get; private set; }

		public ErrorInfo ErrorInfo { get; private set; }

		public Visibility ErrorInfoVisibility => ErrorInfo == null ? Visibility.Collapsed : Visibility.Visible;

		public Version ServerLibraryVersion { get; private set; }

		public void SetSessionData(Request request, Response response)
		{
			ApplicationName = request.ApplicationName;
			ClientLibraryVersion = request.LibraryVersion;
			CorrelationId = response.TraceCorrelationId;
			ErrorInfo = response.ErrorInfo;
			ServerLibraryVersion = new Version(response.LibraryVersion);

			RaisePropertyChanged(
				nameof(ApplicationName),
				nameof(ClientLibraryVersion),
				nameof(CorrelationId),
				nameof(ErrorInfo),
				nameof(ServerLibraryVersion));
		}
	}
}