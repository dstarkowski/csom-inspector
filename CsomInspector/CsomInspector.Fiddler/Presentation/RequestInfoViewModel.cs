using CsomInspector.Core;
using System;

namespace CsomInspector.Fiddler.Presentation
{
	public class RequestInfoViewModel : ViewModelBase
	{
		private Int64 _requestSize;

		private TimeSpan _sessionTime;

		private Int64 _responseSize;

		public String ApplicationName { get; private set; }

		public Version ClientLibraryVersion { get; private set; }

		public String ClientTag { get; set; }

		public String CorrelationId { get; private set; }

		public String RequestSize => $"{_requestSize / 1000d:0.00} KB";

		public String SessionTime => $"{_sessionTime.TotalSeconds:0.000} s";

		public String ResponseSize => $"{_responseSize / 1000.0d:0.00} KB";

		public Version ServerLibraryVersion { get; private set; }

		public void ClearSessionData()
		{
			ApplicationName = null;
			ClientLibraryVersion = null;
			CorrelationId = null;
			ServerLibraryVersion = null;
			ClientTag = null;
			_requestSize = 0;
			_sessionTime = TimeSpan.Zero;
			_responseSize = 0;

			RaiseAllPropertiesChanged();
		}

		public void SetSessionData(Request request, Response response, TimeSpan sessionTime, Int64 requestSize, Int64 responseSize)
		{
			ApplicationName = request.ApplicationName;
			ClientLibraryVersion = request.LibraryVersion;
			CorrelationId = response.TraceCorrelationId;
			ServerLibraryVersion = new Version(response.LibraryVersion);
			ClientTag = request.ClientTag;
			_requestSize = requestSize;
			_sessionTime = sessionTime;
			_responseSize = responseSize;

			RaiseAllPropertiesChanged();
		}

		private void RaiseAllPropertiesChanged() => RaisePropertyChanged(
			nameof(ApplicationName),
			nameof(ClientLibraryVersion),
			nameof(ClientTag),
			nameof(CorrelationId),
			nameof(ServerLibraryVersion),
			nameof(RequestSize),
			nameof(SessionTime),
			nameof(ResponseSize));
	}
}