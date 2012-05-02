using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell.Interop;
using Microsoft.Phone.TaskModel.Interop;

namespace Microsoft.Phone.Tasks1
{
	internal enum MarketplaceContent
	{
		Applications = 1,
		Music
	}

	/// <summary>Defines the list of content types that can be shown by the Windows Phone Marketplace client application. This enumeration is used by <see cref="T:Microsoft.Phone.Tasks.MarketplaceDetailTask" />, <see cref="T:Microsoft.Phone.Tasks.MarketplaceHubTask" />, and <see cref="T:Microsoft.Phone.Tasks.MarketplaceSearchTask" />.</summary>
	public enum MarketplaceContentType
	{
		/// <summary>Application content.</summary>
		Applications = 1,
		/// <summary>Music content.</summary>
		Music
	}

	/// <summary>Allows an application to launch the Windows Phone Marketplace client application and display the details page for the specified product.</summary>
	public sealed class MarketplaceDetailTask
	{
		private MarketplaceContentType _marketPlaceContentType;
		private string _contentIdentifier;
		/// <summary>Gets or sets the type of content displayed in the Windows Phone Marketplace client application.</summary>
		/// <returns>Type: 
		/// <see cref="T:Microsoft.Phone.Tasks.MarketplaceContentType" />.</returns>
		public MarketplaceContentType ContentType
		{
			get
			{
				return this._marketPlaceContentType;
			}
			set
			{
				this._marketPlaceContentType = value;
			}
		}
		/// <summary>Gets or sets the unique identifier for the product to be displayed.</summary>
		/// <returns>Type: 
		/// <see cref="T:System.String" />.</returns>
		public string ContentIdentifier
		{
			get
			{
				return this._contentIdentifier;
			}
			set
			{
				this._contentIdentifier = value;
			}
		}
		/// <summary>Creates a new instance of the <see cref="T:Microsoft.Phone.Tasks.MarketplaceDetailTask" /> class.</summary>
		public MarketplaceDetailTask()
		{
			this._marketPlaceContentType = MarketplaceContentType.Applications;
			this._contentIdentifier = null;
		}
		/// <summary>Shows the Windows Phone Marketplace client application and displays the details page for the specified product.</summary>
		public void Show()
		{
			if (!ChooserHelper.NavigationInProgressGuard(delegate
			{
				this.Show();
			}
			))
			{
				return;
			}
			MarketplaceLauncher.Show((MarketplaceContent)this._marketPlaceContentType, MarketplaceOperation.ViewDetails, this._contentIdentifier);
		}
	}

	/// <summary>Allows an application to launch the Windows Phone Marketplace client application.</summary>
	public sealed class MarketplaceHubTask
	{
		private MarketplaceContentType _marketPlaceContentType;
		/// <summary>Gets or sets the type of content displayed in the Windows Phone Marketplace client application.
		/// </summary>
		/// <returns>Type: 
		/// <see cref="T:Microsoft.Phone.Tasks.MarketplaceContentType" />.</returns>
		public MarketplaceContentType ContentType
		{
			get
			{
				return this._marketPlaceContentType;
			}
			set
			{
				this._marketPlaceContentType = value;
			}
		}
		/// <summary>Creates a new instance of the <see cref="T:Microsoft.Phone.Tasks.MarketplaceHubTask" /> class.</summary>
		public MarketplaceHubTask()
		{
			this._marketPlaceContentType = MarketplaceContentType.Applications;
		}
		/// <summary>Shows the Windows Phone Marketplace client application.</summary>
		public void Show()
		{
			if (!ChooserHelper.NavigationInProgressGuard(delegate
			{
				this.Show();
			}
			))
			{
				return;
			}
			MarketplaceLauncher.Show((MarketplaceContent)this._marketPlaceContentType, MarketplaceOperation.Open, null);
		}
	}

	internal static class MarketplaceLauncher
	{
		public static void Show(MarketplaceContent content, MarketplaceOperation operation)
		{
			MarketplaceLauncher.Show(content, operation, null);
		}
		public static void Show(MarketplaceContent content, MarketplaceOperation operation, string context)
		{
			if (!ChooserHelper.NavigationInProgressGuard(delegate
			{
				MarketplaceLauncher.Show(content, operation, context);
			}
			))
			{
				return;
			}
			if (operation == MarketplaceOperation.Open)
			{
				if (!string.IsNullOrEmpty(context))
				{
					throw new ArgumentException("context");
				}
				MarketplaceLauncher.DispatchBrowse(content);
				return;
			}
			else
			{
				if (operation == MarketplaceOperation.Search)
				{
					if (context == null)
					{
						throw new ArgumentNullException("context");
					}
					if (context.Length == 0)
					{
						throw new ArgumentException("context");
					}
					MarketplaceLauncher.DispatchSearch(content, context);
					return;
				}
				else
				{
					if (operation != MarketplaceOperation.ReviewItem && operation != MarketplaceOperation.ViewDetails)
					{
						throw new ArgumentOutOfRangeException("operation");
					}
					if (content != MarketplaceContent.Applications)
					{
						throw new ArgumentException("content");
					}
					string relative;
					if (operation == MarketplaceOperation.ReviewItem)
					{
						if (context != null)
						{
							throw new ArgumentException("context");
						}
						context = new HostInfo().ProductId;
						relative = "AppReviews";
					}
					else
					{
						if (context == null)
						{
							context = new HostInfo().ProductId;
						}
						else
						{
							context = new Guid(context).ToString();
						}
						relative = "AppDetails";
					}
					ParameterPropertyBag parameterPropertyBag = new ParameterPropertyBag();
					parameterPropertyBag.CreateProperty("id").StringValue = context;
					ChooserHelper.Navigate(MeuxHelper.MakeUri(relative), parameterPropertyBag);
					return;
				}
			}
		}
		private static void DispatchBrowse(MarketplaceContent content)
		{
			string relative;
			switch (content)
			{
				case MarketplaceContent.Applications:
				{
					relative = "AppMarketplaceHub";
					break;
				}
				case MarketplaceContent.Music:
				{
					relative = "MusicMarketplaceHub";
					break;
				}
				default:
				{
					throw new ArgumentOutOfRangeException("content");
				}
			}
			ChooserHelper.Navigate(MeuxHelper.MakeUri(relative), new ParameterPropertyBag());
		}
		private static void DispatchSearch(MarketplaceContent content, string context)
		{
			string stringValue;
			switch (content)
			{
				case MarketplaceContent.Applications:
				{
					stringValue = "application";
					break;
				}
				case MarketplaceContent.Music:
				{
					stringValue = "music";
					break;
				}
				default:
				{
					throw new ArgumentOutOfRangeException("content");
				}
			}
			ParameterPropertyBag parameterPropertyBag = new ParameterPropertyBag();
			parameterPropertyBag.CreateProperty("SearchHint").StringValue = stringValue;
			parameterPropertyBag.CreateProperty("SearchTerm").StringValue = context;
			ChooserHelper.Navigate(MeuxHelper.MakeUri("MarketplaceSearch"), parameterPropertyBag);
		}
	}

	internal enum MarketplaceOperation
	{
		Open = 1,
		Search,
		ViewDetails,
		ReviewItem
	}
}


namespace Microsoft.Phone.Tasks
{
	/// <summary>Allows an application to launch the Windows Phone Marketplace client application and display the review page for the specified product.</summary>
	public sealed class MarketplaceReviewTask
	{
		/// <summary>Shows the Windows Phone Marketplace client application and displays the review page for the specified product.</summary>
		public void Show()
		{
			if (!ChooserHelper.NavigationInProgressGuard(delegate
			{
				this.Show();
			}
			))
			{
				return;
			}
			MarketplaceLauncher.Show(MarketplaceContent.Applications, MarketplaceOperation.ReviewItem, null);
		}
	}

	/// <summary>Allows an application to launch the Windows Phone Marketplace client application and display the search results from the specified search terms.</summary>
	public sealed class MarketplaceSearchTask
	{
		private MarketplaceContentType _marketPlaceContentType;
		private string _searchTerms;
		/// <summary>Gets or sets the type of content displayed in the Windows Phone Marketplace client application.</summary>
		/// <returns>Type: 
		/// <see cref="T:Microsoft.Phone.Tasks.MarketplaceContentType" />.</returns>
		public MarketplaceContentType ContentType
		{
			get
			{
				return this._marketPlaceContentType;
			}
			set
			{
				this._marketPlaceContentType = value;
			}
		}
		/// <summary>Gets or sets the search terms.</summary>
		/// <returns>Type: 
		/// <see cref="T:System.String" />.</returns>
		public string SearchTerms
		{
			get
			{
				return this._searchTerms;
			}
			set
			{
				this._searchTerms = value;
			}
		}
		/// <summary>Creates a new instance of the <see cref="T:Microsoft.Phone.Tasks.MarketplaceSearchTask" /> class.</summary>
		public MarketplaceSearchTask()
		{
			this._marketPlaceContentType = MarketplaceContentType.Applications;
			this._searchTerms = null;
		}
		/// <summary>Shows the Windows Phone Marketplace client application and displays the search results from the specified search terms.</summary>
		public void Show()
		{
			if (!ChooserHelper.NavigationInProgressGuard(delegate
			{
				this.Show();
			}
			))
			{
				return;
			}
			MarketplaceLauncher.Show((MarketplaceContent)this._marketPlaceContentType, MarketplaceOperation.Search, this._searchTerms);
		}
	}

	/// <summary>Lists the data stores in which a media file may be stored. Used by <see cref="T:Microsoft.Phone.Tasks.MediaPlayerLauncher" />.</summary>
	public enum MediaLocationType
	{
		/// <summary>The media item is in neither data store. The Show method will throw a FileNotFoundException if this value is used.</summary>
		None,
		/// <summary>The media file is in the application’s installation directory.</summary>
		Install,
		/// <summary>The media file is in isolated storage.</summary>
		Data
	}

	/// <summary>An enumeration defining the bitwise flags that are used with the Controls property of the <see cref="T:Microsoft.Phone.Tasks.MediaPlayerLauncher" /> object to specify which controls should be displayed by the media player application.</summary>
	[Flags]
	public enum MediaPlaybackControls
	{
		/// <summary>No controls.</summary>
		None = 0,
		/// <summary>The pause control.</summary>
		Pause = 1,
		/// <summary>The stop control.</summary>
		Stop = 2,
		/// <summary>The fast forward control.</summary>
		FastForward = 4,
		/// <summary>The rewind control.</summary>
		Rewind = 8,
		/// <summary>The skip control.</summary>
		Skip = 16,
		/// <summary>All controls. The equivalent of using OR to combine all of the other members of the enumeration.</summary>
		All = 31
	}

	/// <summary>Allows an application to launch the media player.</summary>
	public class MediaPlayerLauncher
	{
		private MediaPlaybackControls _controls;
		private Uri _media;
		private MediaLocationType _locationHint;
		/// <summary>Gets or sets the media played with the media player application.</summary>
		/// <returns>Type: 
		/// <see cref="T:System.Uri" />. The URI of a media file.</returns>
		public Uri Media
		{
			get
			{
				return this._media;
			}
			set
			{
				if (!this.ValidateUri(value))
				{
					throw new ArgumentException("value");
				}
				this._media = value;
			}
		}
		/// <summary>Gets or sets the flags that determine which controls are displayed in the media player application.</summary>
		/// <returns>Type 
		/// <see cref="T:Microsoft.Phone.Tasks.MediaPlaybackControls" />. The bitwise combination of <see cref="T:Microsoft.Phone.Tasks.MediaPlaybackControls" /> members that determine which controls are displayed in the media player application.</returns>
		public MediaPlaybackControls Controls
		{
			get
			{
				return this._controls;
			}
			set
			{
				if ((value & ~(MediaPlaybackControls.Pause | MediaPlaybackControls.Stop | MediaPlaybackControls.FastForward | MediaPlaybackControls.Rewind | MediaPlaybackControls.Skip)) != MediaPlaybackControls.None)
				{
					throw new ArgumentException();
				}
				this._controls = value;
			}
		}
		/// <summary>Sets the location of the media file to be played. The <see cref="T:Microsoft.Phone.Tasks.MediaLocationType" /> enumeration is used to specify either isolated storage or the application’s installation folder.</summary>
		/// <returns>Type: 
		/// <see cref="T:Microsoft.Phone.Tasks.MediaLocationType" />.</returns>
		public MediaLocationType Location
		{
			set
			{
				this._locationHint = value;
			}
		}
		/// <summary>Initializes a new instance of the media player application.</summary>
		public MediaPlayerLauncher()
		{
			this.Controls = MediaPlaybackControls.All;
			this._locationHint = MediaLocationType.Data;
		}
		/// <summary>Shows the media player application.</summary>
		public void Show()
		{
			if (!ChooserHelper.NavigationInProgressGuard(delegate
			{
				this.Show();
			}
			))
			{
				return;
			}
			if (this.Media == null)
			{
				throw new InvalidOperationException();
			}
			ParameterPropertyBag parameterPropertyBag = new ParameterPropertyBag();
			parameterPropertyBag.CreateProperty("url").StringValue = this.AdjustUri(this.Media);
			parameterPropertyBag.CreateProperty("controls").Int32Value = (int)this.Controls;
			ChooserHelper.Navigate(MeuxHelper.MakeUri("_PlayUrl"), parameterPropertyBag);
		}
		private string AdjustUri(Uri u)
		{
			if (u.IsAbsoluteUri)
			{
				return u.AbsoluteUri;
			}
			string text = this.ResolveToAbsolutePath(u);
			Console.WriteLine("MediaPlayerLauncher: passing path to meux = " + text);
			return text;
		}
		private bool ValidateUri(Uri uri)
		{
			if (uri.IsAbsoluteUri)
			{
				string scheme = uri.Scheme;
				return string.Compare(scheme, "https", StringComparison.InvariantCultureIgnoreCase) == 0 || string.Compare(scheme, "http", StringComparison.InvariantCultureIgnoreCase) == 0;
			}
			return true;
		}
		private string GetFullyQualifiedPath(Uri relativeUri, string folder)
		{
			string text = relativeUri.OriginalString.Replace('/', '\\');
			text = Path.Combine(folder, text);
			Console.WriteLine("GetFullyQualifiedPath fullFilePath = " + text);
			return text;
		}
		[SecuritySafeCritical]
		private string ResolveToAbsolutePath(Uri relativeUri)
		{
			HostInfo hostInfo = new HostInfo();
			Console.WriteLine("ResolveToAbsolutePath AppDataFolder = " + hostInfo.AppDataFolder);
			Console.WriteLine("ResolveToAbsolutePath AppInstallFolder = " + hostInfo.AppInstallFolder);
			Console.WriteLine("ResolveToAbsolutePath AppIsolatedStorePath = " + hostInfo.AppIsolatedStorePath);
			string fullyQualifiedPath;
			if (this._locationHint == MediaLocationType.Install)
			{
				fullyQualifiedPath = this.GetFullyQualifiedPath(relativeUri, hostInfo.AppInstallFolder);
			}
			else
			{
				if (this._locationHint != MediaLocationType.Data)
				{
					throw new FileNotFoundException(relativeUri.OriginalString);
				}
				fullyQualifiedPath = this.GetFullyQualifiedPath(relativeUri, hostInfo.AppIsolatedStorePath);
			}
			if (!File.Exists(fullyQualifiedPath))
			{
				Console.WriteLine("MediaPlayerLauncher: ResolveToAbsolutePath failed path validation = " + fullyQualifiedPath);
				throw new FileNotFoundException(fullyQualifiedPath);
			}
			return fullyQualifiedPath;
		}
	}

	internal class MeuxHelper
	{
		public static Uri MakeUri(string relative)
		{
			return new Uri("app://5B04B775-356B-4AA0-AAF8-6491FFEA5630/" + relative, UriKind.Absolute);
		}
	}

	/// <summary>Allows an application to launch the Phone application. Use this to allow users to make a phone call from your application.</summary>
	public sealed class PhoneCallTask
	{
		/// <summary>Gets or sets the name that is displayed when the Phone application is launched.</summary>
		/// <returns>Type: 
		/// <see cref="T:System.String" />. The name that is displayed when the Phone application is launched.</returns>
		public string DisplayName
		{
			get;
			set;
		}
		/// <summary>Gets or sets the phone number that is dialed when the Phone application is launched.</summary>
		/// <returns>Type: 
		/// <see cref="T:System.String" />. The phone number that is dialed when the Phone application is launched.</returns>
		public string PhoneNumber
		{
			get;
			set;
		}
		static PhoneCallTask()
		{
		}
		/// <summary>Shows the Phone application.</summary>
		[SecuritySafeCritical]
		public void Show()
		{
			if (!ChooserHelper.NavigationInProgressGuard(delegate
			{
				this.Show();
			}
			))
			{
				return;
			}
			ThreadPool.QueueUserWorkItem(new WaitCallback(PhoneCallTask.PhoneDial), this);
		}
		[SecuritySafeCritical]
		private static void PhoneDial(object phoneCallTask)
		{
			PhoneCallTask phoneCallTask2 = phoneCallTask as PhoneCallTask;
			string phoneNumber = phoneCallTask2.PhoneNumber;
			string arg_14_0 = phoneCallTask2.DisplayName;
			string.IsNullOrEmpty(phoneNumber);
		}
	}

	/// <summary>Allows an application to launch the Contacts application. Use this to obtain the phone number of a contact selected by the user.</summary>
	public sealed class PhoneNumberChooserTask : ChooserBase<PhoneNumberResult>
	{
		/// <summary>Shows the Contacts application.</summary>
		public override void Show()
		{
			if (!ChooserHelper.NavigationInProgressGuard(delegate
			{
				this.Show();
			}
			))
			{
				return;
			}
			ParameterPropertyBag ppb = new ParameterPropertyBag();
			byte[] buffer = ChooserHelper.Serialize(ppb);
			Uri appUri = new Uri("app://5B04B775-356B-4AA0-AAF8-6491FFEA5615/ChoosePhonePropertyOfExistingPerson", UriKind.Absolute);
			base.Show();
			ChooserHelper.Invoke(appUri, buffer, this._genericChooser);
		}
		internal override void OnInvokeReturned(byte[] outputBuffer, Delegate fireThisHandlerOnly)
		{
			bool flag = false;
			if (outputBuffer.Length > 0)
			{
				ParameterPropertyBag parameterPropertyBag = new ParameterPropertyBag(outputBuffer, (uint)outputBuffer.Length);
				ParameterProperty property = parameterPropertyBag.GetProperty("PickerPropertyValue");
				if (property.ValueType == ParameterPropertyValueType.ValueType_String && !string.IsNullOrEmpty(property.StringValue))
				{
					flag = true;
					base.FireCompleted(this, new PhoneNumberResult(TaskResult.OK)
					{
						PhoneNumber = property.StringValue
					}, fireThisHandlerOnly);
				}
			}
			if (!flag)
			{
				base.FireCompleted(this, new PhoneNumberResult(TaskResult.Cancel), fireThisHandlerOnly);
			}
		}
	}

	/// <summary>Represents a phone number returned from a call to the Show method of a <see cref="T:Microsoft.Phone.Tasks.PhoneNumberChooserTask" /> object.</summary>
	public class PhoneNumberResult : TaskEventArgs
	{
		/// <summary>The phone number contained in the result.</summary>
		/// <returns>Type: 
		/// <see cref="T:System.String" />. The phone number contained in the result.</returns>
		public string PhoneNumber
		{
			get;
			internal set;
		}
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Phone.Tasks.PhoneNumberResult" /> class.</summary>
		public PhoneNumberResult()
		{
		}
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Phone.Tasks.PhoneNumberResult" /> class with the specified <see cref="T:Microsoft.Phone.Tasks.TaskResult" />.</summary>
		/// <param name="taskResult">The <see cref="T:Microsoft.Phone.Tasks.TaskResult" /> associated with the new <see cref="T:Microsoft.Phone.Tasks.PhoneNumberResult" />.</param>
		public PhoneNumberResult(TaskResult taskResult) : base(taskResult)
		{
		}
	}

	/// <summary>Allows an application to launch the Photo Chooser application. Use this to allow users to select a photo.</summary>
	public sealed class PhotoChooserTask : ChooserBase<PhotoResult>
	{
		private const string photoChooserPrefix = "PhotoChooser-";
		/// <summary>Gets or sets the maximum height and the height component of the aspect ratio for a cropping region set by the user during the photo choosing process.</summary>
		/// <returns>Type: 
		/// <see cref="T:System.Int32" />. The height at which the image will be cropped.</returns>
		public int PixelHeight
		{
			get;
			set;
		}
		/// <summary>Gets or sets the maximum height and the height component of the aspect ratio for a cropping region set by the user during the photo choosing process.</summary>
		/// <returns>Type: 
		/// <see cref="T:System.Int32" />. The width at which the image will be cropped.</returns>
		public int PixelWidth
		{
			get;
			set;
		}
		/// <summary>Gets or sets whether the user is presented with a button for launching the camera during the photo choosing process.</summary>
		/// <returns>Type <see cref="T:System.Boolean" />.</returns>
		public bool ShowCamera
		{
			get;
			set;
		}
		/// <summary>Shows the Photo Chooser application.</summary>
		[SecuritySafeCritical]
		public override void Show()
		{
			if (!ChooserHelper.NavigationInProgressGuard(delegate
			{
				this.Show();
			}
			))
			{
				return;
			}
			ParameterPropertyBag parameterPropertyBag = new ParameterPropertyBag();
			ChooserHelper.FillCommonPhotoProperties(parameterPropertyBag, "PhotoChooser-");
			int pixelWidth = this.PixelWidth;
			int pixelHeight = this.PixelHeight;
			parameterPropertyBag.CreateProperty("CropWidthPixels").Int32Value = pixelWidth;
			parameterPropertyBag.CreateProperty("CropHeightPixels").Int32Value = pixelHeight;
			parameterPropertyBag.CreateProperty("ShowCamera").BoolValue = this.ShowCamera;
			byte[] buffer = ChooserHelper.Serialize(parameterPropertyBag);
			Uri appUri = new Uri("app://5B04B775-356B-4AA0-AAF8-6491FFEA5632/PhotoPicker", UriKind.Absolute);
			base.Show();
			ChooserHelper.Invoke(appUri, buffer, this._genericChooser);
		}
		internal override void OnInvokeReturned(byte[] outputBuffer, Delegate fireThisHandlerOnly)
		{
			bool flag = false;
			if (outputBuffer.Length > 0)
			{
				ParameterPropertyBag parameterPropertyBag = new ParameterPropertyBag(outputBuffer, (uint)outputBuffer.Length);
				ParameterProperty property = parameterPropertyBag.GetProperty("SelectedFilePath");
				if (property.ValueType == ParameterPropertyValueType.ValueType_String && !string.IsNullOrEmpty(property.StringValue))
				{
					flag = true;
					PhotoResult photoResult = ChooserHelper.PathParameterToPhotoResult(property);
					photoResult.TaskResult = TaskResult.OK;
					base.FireCompleted(this, photoResult, fireThisHandlerOnly);
				}
			}
			if (!flag)
			{
				base.FireCompleted(this, new PhotoResult(TaskResult.Cancel), fireThisHandlerOnly);
			}
		}
	}

	/// <summary>Represents a photo returned from a call to the Show method of a <see cref="T:Microsoft.Phone.Tasks.PhotoChooserTask" /> object or a <see cref="T:Microsoft.Phone.Tasks.CameraCaptureTask" /> object.</summary>
	public class PhotoResult : TaskEventArgs
	{
		/// <summary>Gets the file name of the photo.</summary>
		/// <returns>Type 
		/// <see cref="T:System.String" />. The file name of the photo.</returns>
		public string OriginalFileName
		{
			get;
			internal set;
		}
		/// <summary>Gets the stream containing the data for the photo.</summary>
		/// <returns>Type: 
		/// <see cref="T:System.IO.Stream" />. The data for the photo.</returns>
		public Stream ChosenPhoto
		{
			get;
			internal set;
		}
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Phone.Tasks.PhotoResult" /> class.</summary>
		public PhotoResult()
		{
		}
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Phone.Tasks.PhotoResult" /> class with the specified <see cref="T:Microsoft.Phone.Tasks.TaskResult" />.</summary>
		/// <param name="taskResult">The <see cref="T:Microsoft.Phone.Tasks.TaskResult" /> associated with the new <see cref="T:Microsoft.Phone.Tasks.PhotoResult" />.</param>
		public PhotoResult(TaskResult taskResult) : base(taskResult)
		{
		}
	}

	[SecuritySafeCritical]
	internal class PhotoStream : FileStream
	{
		public PhotoStream(string localFileName) : base(localFileName, FileMode.Open, FileAccess.Read, FileShare.Read)
		{
		}
	}

	/// <summary>Allows an application to launch the contacts application. Use this to allow users to save an email address from your application to a new or existing contact.</summary>
	public sealed class SaveEmailAddressTask : ChooserBase<TaskEventArgs>
	{
		/// <summary>Gets or sets the email address that can be saved to a contact.</summary>
		/// <returns>Type 
		/// <see cref="T:System.String" />. The email address that can be saved to a contact.</returns>
		public string Email
		{
			get;
			set;
		}
		/// <summary>Shows the contacts application.</summary>
		public override void Show()
		{
			if (!ChooserHelper.NavigationInProgressGuard(delegate
			{
				this.Show();
			}
			))
			{
				return;
			}
			ParameterPropertyBag parameterPropertyBag = new ParameterPropertyBag();
			ParameterProperty parameterProperty = parameterPropertyBag.CreateProperty("EmailAddressToSave");
			parameterProperty.StringValue = this.Email;
			byte[] buffer = ChooserHelper.Serialize(parameterPropertyBag);
			Uri appUri = new Uri("app://5B04B775-356B-4AA0-AAF8-6491FFEA5615/SaveAnEmailToAddressBook", UriKind.Absolute);
			base.Show();
			ChooserHelper.Invoke(appUri, buffer, this._genericChooser);
		}
		internal override void OnInvokeReturned(byte[] outputBuffer, Delegate fireThisHandlerOnly)
		{
			if (outputBuffer != null && outputBuffer.Length > 0)
			{
				base.FireCompleted(this, new TaskEventArgs(TaskResult.OK), fireThisHandlerOnly);
				return;
			}
			base.FireCompleted(this, new TaskEventArgs(TaskResult.Cancel), fireThisHandlerOnly);
		}
	}

	/// <summary>Allows an application to launch the contacts application. Use this to allow users to save a phone number from your application to a new or existing contact.</summary>
	public sealed class SavePhoneNumberTask : ChooserBase<TaskEventArgs>
	{
		/// <summary>Gets or sets the phone number that can be saved to a contact.</summary>
		/// <returns>Type: 
		/// <see cref="T:System.String" />. The phone number that can be saved to a contact.</returns>
		public string PhoneNumber
		{
			get;
			set;
		}
		/// <summary>Shows the contacts application.</summary>
		public override void Show()
		{
			if (!ChooserHelper.NavigationInProgressGuard(delegate
			{
				this.Show();
			}
			))
			{
				return;
			}
			ParameterPropertyBag parameterPropertyBag = new ParameterPropertyBag();
			ParameterProperty parameterProperty = parameterPropertyBag.CreateProperty("PhoneNumberToSave");
			parameterProperty.StringValue = this.PhoneNumber;
			byte[] buffer = ChooserHelper.Serialize(parameterPropertyBag);
			Uri appUri = new Uri("app://5B04B775-356B-4AA0-AAF8-6491FFEA5615/SaveANumberToAddressBook", UriKind.Absolute);
			base.Show();
			ChooserHelper.Invoke(appUri, buffer, this._genericChooser);
		}
		internal override void OnInvokeReturned(byte[] outputBuffer, Delegate fireThisHandlerOnly)
		{
			if (outputBuffer != null && outputBuffer.Length > 0)
			{
				base.FireCompleted(this, new TaskEventArgs(TaskResult.OK), fireThisHandlerOnly);
				return;
			}
			base.FireCompleted(this, new TaskEventArgs(TaskResult.Cancel), fireThisHandlerOnly);
		}
	}

	/// <summary>Allows an application to launch the Web Search application.</summary>
	public sealed class SearchTask
	{
		/// <summary>Gets or sets the search query that the Web Search application will use when it is launched.</summary>
		/// <returns>Type: 
		/// <see cref="T:System.String" />. The search query that the Web Search application will use when it is launched.</returns>
		public string SearchQuery
		{
			get;
			set;
		}
		/// <summary>Shows the Web Search application.</summary>
		public void Show()
		{
			if (!ChooserHelper.NavigationInProgressGuard(delegate
			{
				this.Show();
			}
			))
			{
				return;
			}
			ParameterPropertyBag parameterPropertyBag = new ParameterPropertyBag();
			ParameterProperty parameterProperty = parameterPropertyBag.CreateProperty("QueryString");
			parameterProperty.StringValue = this.SearchQuery;
			Uri appUri = new Uri("app://5B04B775-356B-4AA0-AAF8-6491FFEA5661/SearchResults", UriKind.Absolute);
			ChooserHelper.Navigate(appUri, parameterPropertyBag);
		}
	}

	/// <summary>Launches the Messaging application with a new SMS message displayed.</summary>
	public sealed class SmsComposeTask
	{
		/// <summary>Gets or sets the body text of the new SMS message.</summary>
		/// <returns>Type: 
		/// <see cref="T:System.String" />.</returns>
		public string Body
		{
			get;
			set;
		}
		/// <summary>Gets or sets the recipient list for the new SMS message.</summary>
		/// <returns>Type: 
		/// <see cref="T:System.String" />.</returns>
		public string To
		{
			get;
			set;
		}
		/// <summary>Shows the Messaging application.</summary>
		public void Show()
		{
			if (!ChooserHelper.NavigationInProgressGuard(delegate
			{
				this.Show();
			}
			))
			{
				return;
			}
			ParameterPropertyBag parameterPropertyBag = new ParameterPropertyBag();
			string to = this.To;
			if (!string.IsNullOrEmpty(to))
			{
				ParameterProperty parameterProperty = parameterPropertyBag.CreateProperty("To");
				parameterProperty.StringValue = to;
			}
			string body = this.Body;
			if (!string.IsNullOrEmpty(body))
			{
				ParameterProperty parameterProperty2 = parameterPropertyBag.CreateProperty("Body");
				parameterProperty2.StringValue = body;
			}
			ParameterProperty parameterProperty3 = parameterPropertyBag.CreateProperty("Account");
			parameterProperty3.StringValue = "{FD39DA85-C18F-4c0c-AEAC-75867CEA7876}";
			Uri appUri = new Uri("app://5B04B775-356B-4AA0-AAF8-6491FFEA5614/ShareContent", UriKind.Absolute);
			ChooserHelper.Navigate(appUri, parameterPropertyBag);
		}
	}

	/// <summary>The EventArgs used by the <see cref="E:Microsoft.Phone.Tasks.ChooserBase`1.Completed" /> event for all Choosers.</summary>
	public class TaskEventArgs : EventArgs
	{
		/// <summary>The exception associated with the <see cref="T:Microsoft.Phone.Tasks.TaskEventArgs" />.</summary>
		/// <returns>Type: 
		/// <see cref="T:System.Exception" />.</returns>
		public virtual Exception Error
		{
			get;
			internal set;
		}
		/// <summary>The <see cref="T:Microsoft.Phone.Tasks.TaskResult" /> associated with the <see cref="T:Microsoft.Phone.Tasks.TaskEventArgs" />. This indicates whether the task was completed successfully, if the user cancelled the task, or if no result information is available.
		/// </summary>
		/// <returns>Type: 
		/// <see cref="T:Microsoft.Phone.Tasks.TaskResult" />.</returns>
		public virtual TaskResult TaskResult
		{
			get;
			internal set;
		}
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Phone.Tasks.TaskEventArgs" /> class.</summary>
		public TaskEventArgs()
		{
		}
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Phone.Tasks.TaskEventArgs" /> class with the specified <see cref="T:Microsoft.Phone.Tasks.TaskResult" />.</summary>
		/// <param name="taskResult">The <see cref="T:Microsoft.Phone.Tasks.TaskResult" /> associated with the new <see cref="T:Microsoft.Phone.Tasks.TaskEventArgs" />.</param>
		public TaskEventArgs(TaskResult taskResult)
		{
			this.TaskResult = taskResult;
		}
	}

	/// <summary>Describes the success status of a chooser operation. </summary>
	public enum TaskResult
	{
		/// <summary>No success status was returned from the chooser operation.</summary>
		None,
		/// <summary>The chooser operation was successful.</summary>
		OK,
		/// <summary>The chooser operation was cancelled by the user.</summary>
		Cancel
	}

	/// <summary>Allows an application to launch the Web browser application.</summary>
	public sealed class WebBrowserTask
	{
		/// <summary>Gets or sets the URL to which the Web browser application will navigate when it is launched.</summary>
		/// <returns>Type: 
		/// <see cref="T:System.String" />. The URL to which the Web browser application will navigate when it is launched.</returns>
		public string URL
		{
			get;
			set;
		}
		/// <summary>Shows the Web browser application.</summary>
		public void Show()
		{
			if (!ChooserHelper.NavigationInProgressGuard(delegate
			{
				this.Show();
			}
			))
			{
				return;
			}
			string appUri = "app://5B04B775-356B-4AA0-AAF8-6491FFEA5660/_default?StartURL=" + this.URL;
			ChooserHelper.LaunchSession(appUri);
		}
	}
}
