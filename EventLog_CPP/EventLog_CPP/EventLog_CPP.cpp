// EventLog_CPP.cpp: 主项目文件。

#include "stdafx.h"

using namespace System;
using namespace System::IO;
using namespace System::Security::Permissions;

public ref class Watcher
{
private:
	// Define the event handlers.
	static void OnChanged( Object^ /*source*/, FileSystemEventArgs^ e )
	{
		// Specify what is done when a file is changed, created, or deleted.
		Console::WriteLine( "File: {0} {1}", e->FullPath, e->ChangeType );
	}

	static void OnRenamed( Object^ /*source*/, RenamedEventArgs^ e )
	{
		// Specify what is done when a file is renamed.
		Console::WriteLine( "File: {0} renamed to {1}", e->OldFullPath, e->FullPath );
	}

public:
	[PermissionSet(SecurityAction::Demand, Name="FullTrust")]
	int static run()
	{
		array<String^>^args = System::Environment::GetCommandLineArgs();

		// If a directory is not specified, exit program.
		if ( args->Length != 2 )
		{
			// Display the proper way to call the program.
			Console::WriteLine( "Usage: Watcher.exe (directory)" );
			return 0;
		}

		// Create a new FileSystemWatcher and set its properties.
		FileSystemWatcher^ watcher = gcnew FileSystemWatcher;
		watcher->Path = "D:\\";// args[ 1 ];

		/* Watch for changes in LastAccess and LastWrite times, and 
		the renaming of files or directories. */
		watcher->NotifyFilter = static_cast<NotifyFilters>(NotifyFilters::LastAccess |
			NotifyFilters::LastWrite | NotifyFilters::FileName | NotifyFilters::DirectoryName);

		// Only watch text files.
		watcher->Filter = "*.avi";

		// Add event handlers.
		watcher->Changed += gcnew FileSystemEventHandler( Watcher::OnChanged );
		watcher->Created += gcnew FileSystemEventHandler( Watcher::OnChanged );
		watcher->Deleted += gcnew FileSystemEventHandler( Watcher::OnChanged );
		watcher->Renamed += gcnew RenamedEventHandler( Watcher::OnRenamed );

		// Begin watching.
		watcher->EnableRaisingEvents = true;

		// Wait for the user to quit the program.
		Console::WriteLine( "Press \'q\' to quit the sample." );
		while ( Console::Read() != 'q' )
			;
	}
};


int main(array<System::String ^> ^args)
{
	Watcher::run();

	return 0;
}