﻿using System;
using System.Windows.Navigation;
using Windows.Phone.Storage.SharedAccess;

namespace Spritz_Rider_WP8
{
	class CustomURIMapper : UriMapperBase
	{
		private string tempUri;

		public override Uri MapUri(Uri uri)
		{
			tempUri = uri.ToString();

			// File association launch
			// Example launch URI: /FileTypeAssociation?fileToken=89819279-4fe0-4531-9f57-d633f0949a19
			if (tempUri.Contains("/FileTypeAssociation"))
			{
				// Get the file ID (after "fileToken=").
				int fileIDIndex = tempUri.IndexOf("fileToken=") + 10;
				string fileID = tempUri.Substring(fileIDIndex);

				// Map the file association launch to route page.
				return new Uri("/MainPage.xaml?fileToken=" + fileID, UriKind.Relative);
			}

			// Otherwise perform normal launch.
			return uri;
		}
	}
}