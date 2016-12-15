Audio Manager Readme
Created by Alex Luthe

To add an audio clip, drag the clip into the Audio Manager inspector into the 'Audio ____' variables.

To play an audio clip, call the PlayAudioClip() function. It takes the paramaters:
AudioSource	AudioClip	NumberOfLoops (inclusive)	DoesDopler (bool)	Volume (Between 0 and 1)

To play a specific background clip, use this function. If a random background clip is not needed, uncheck the PlaysBackground bool in the inspector.