# Wild Arms - Belselk Bash Randomizer

This is the code repository for the Wild Arms Randomizer! written in C# using .Net Standard and .Net Core.

This randomizer modifies Wild Arms to have various random elements, as well as making it an open world where any area can be accessed immediately. The full list of features (and future planned ones) are listed in this google doc:

https://docs.google.com/spreadsheets/d/1aj5VkF7lpo7cF2HtTAD6kkeovJQ-Mk3RNGlLnKt23No/edit?usp=sharing

The project is currently in a Alpha state (it doesn't crash?), so any help testing for finding issues/balance concerns would be of great help. Better configuration and a UI are next on my list for features, so if you hate console commands don't worry.

Currently, you can either run the RandomizerConsole.exe directly from the build folder, or call it with command line arguments. In order to run the exe directly, a file named wild_arms.bin must be present in the same directory as the executable. The seed will be the current time of the computer. To run it from command line, use the following arguments:

RandomizerConsole.exe \[input_file\] \[output_file\] \[seed\]

Shout outs to Abyssonym for help with emulator choice, randomizer advice, and the python code that the Utilities project is based off of. Shout outs to Power etc. for Run Away code and MIPS/PS1 doc.
