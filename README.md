# Pomogotchi
![PomodoroCLI](../main/img/to_ma_to.png)

Pomogotchi keeps you company for your study or work sessions. Your Pomogotchi gets really angry when it is hungry and the only way to feed them is to do **work sessions**. 
(be careful when feeding them after midnight though)

The name is based on the 90s Tamagotchi (in case you didn't notice), and the project itself is also based on it in hopes to help me (the developer) and hopefully you to spend more time studying and less time climbing mountains in Zelda.

**If you find any issues or have any feature suggestion**, please create a new issue here on GitHub (on the tab Issues) describing exactly what are your needs.

## :garlic: Warning!
***This project is still in early stages!***
Rotten tomatoes may appear.

There is still some work to be done:
- [x] User Interface
- [x] Sounds & Notifications
- [x] Session Settings/Default Settings customization
- [ ] Pomogotchi companion

## :tomato: Installation

Binaries can be found in the folder "Binaries", choose your OS (if available), download it, and it's ready to go!

### Dependencies
- Since Pomogotchi uses libVLCSharp for playing audio files, **VLC must be intalled on your machine**.

**Linux and Mac users** can compile the project using **Visual Studio** or using **.NET 7.0 CLI** (https://dotnet.microsoft.com/en-us/download).
This project is currently on pre-release since it doesn't have all the features yet. But can be used as a simple timer for now.

### Windows
Windows releases can be found on the GitHub's Releases page.

### Linux
Linux releases can be found on the GitHub's Releases page.
:warning: **Caution:** It has only been tested in Fedora 37, experience with other distros may vary.

### macOS
Since I don't have access to any macOS machine, so I have no idea if this app works on any mac. 
I would kindly appreciate any contributions from developers who happen to have one.

## :onion: Customization
The app settings can be found on the file **config.json** in the app's root folder.
In this file is specified the default work/break session durations as well as any extension configuration parameter (The app must be started at least once for settings to appear).

###  Sounds
The session end sound by default is located at the program folder and is called **sessionEnd.wav**, this file can be replaced or the file name can be changed on the **config.json** file.

Pomogotchi uses the [LibVLCSharp](https://github.com/videolan/libvlcsharp) C# audio library. 

