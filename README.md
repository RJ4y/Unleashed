# Unleashed 2 Front-end

This is the front-end application for Unleashed made with the Xamarin framework

## Setup

Before either releasing the app in a package or launching it with an emulator:

Start in the root of the package and go to UnleashedApp. Either open th solution here to use an IDE or continue through UnleashedApp/UnleashedApp.
Open the constants.cs file with a preferred editor or IDE. At the start of the file an IP-address is declared. This shows the loopback address on port #8000,
but it should be changed to the IP-address and port of the server running the backend.
	public const string BaseApiUrl = "http://127.0.0.1:8000/";
	
Once that is completed the files can be either packaged into a .apk for distribution or deployed to a test device/emulator.