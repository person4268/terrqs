this literally just sets quickSplash to true in Terraia.Main to make the splash screen faster. You can also like, set the music volume to 0%, but I wanted music. why not use tModLoader? i never have tried and basic cursory info i found says that it loads too late for this to work. might be wrong tho

How to build/use (painful):

1. use dotpeek in wine to extract the embedded dlls into the game directory. name them from e.g `Terraria.Libraries.RailSDK.Linux.RailSDK.Net.dll` to `RailSDK.Net.dll`. maybe a better tool exists for this on linux but there really wasnt much.
2. copy fna.dll, relogic.dll, terraria.dll to dlls/
3. `dotnet build .`
4. grab a copy of monomod + patcher from [their CI](https://dev.azure.com/MonoMod/MonoMod/_build/results?buildId=782&view=artifacts&pathAsName=false&type=publishedArtifacts) for net35. extract into a directory. there wasnt net40 there and net452 didnt work iirc.
5. copy everything from terrQS/bin/Debug/net40 to your game directory.
6. `mono MonoMod.Patcher.exe path/to/Terraria.exe`
7. replace `Terraria.exe` with `MONOMODDED_Terraria.exe` or just kinda delete all the `System*.dll` files and use your own mono with `LD_LIBRARY_PATH=$(pwd)/lib64/ mono MONOMODDED_Terraria.exe`
8. if you want copy loader.png into game's `Content/` folder