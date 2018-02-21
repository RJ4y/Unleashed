# Unleashed 2 Front-end

This is the front-end application for Unleashed made with the Xamarin framework

## Setup

Before either releasing the app in a package or launching it with an emulator:

Start in the root of the package and go to UnleashedApp. This folder can be opened in Visual Studio and Visual Studio for macOS.

Open the ```constants.cs``` file and look for

	public const string BaseApiUrl = "http://127.0.0.1:8000/";

Replace the given IP address with the IP address of the server running the back end.

## Distribution
	
Once that is completed the files can be either packaged into a .apk for distribution or deployed to a test device or emulator.
