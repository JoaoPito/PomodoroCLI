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
- [ ] Session Settings/Default Settings customization
- [ ] Pomogotchi companion

## :tomato: Installation

Binaries can be found in the folder "Binaries", choose your OS (if available), download it, and it's ready to go!

**Linux and Mac users** can compile the project using **Visual Studio** or using **.NET 7.0** (https://dotnet.microsoft.com/en-us/download).

### Windows

[Windows - 64 bit](Binaries/Windows/pomogotchi_x64win.zip)

1. Download the .zip file corresponding to your system's hardware (x64 ou x86) by clickng in the github download icon on the right side of the page
2. Extract it anywhere you like
3. Run PomoGUI.exe

### Linux
*Coming soon!*

### macOS
*Coming soon!*

## :onion: Customization

###  Ding sound
**For now it's not possible to change the sound file name. This means that only .wav files are supported**

This is the sound that is played when a session ends. It can be changed by replacing the **ding.wav** file with your custom audio file.
Supported audio formats:
- WAV
- AIFF
- MP3 (using ACM, DMO or MFT)
- G.711 mu-law and a-law
- ADPCM, G.722, Speex (using NSpeex)
- WMA, AAC, MP4 and more others with Media Foundation

Pomogotchi uses the [NAudio](https://github.com/naudio/NAudio) C# audio library.

