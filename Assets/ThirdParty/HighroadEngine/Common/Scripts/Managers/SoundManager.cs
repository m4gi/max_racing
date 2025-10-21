using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoreMountains.Tools;

namespace MoreMountains.HighroadEngine
{	
	/// <summary>
	/// This persistent singleton handles sound playing
	/// </summary>
	public class SoundManager : MMPersistentSingleton<SoundManager>
	{	
		/// true if the music is enabled	
		public bool MusicOn=true;
		/// true if the sound fx are enabled
		public bool SfxOn=true;
		/// the music volume
		[Range(0,1)]
		public float MusicVolume=0.3f;
		/// the sound fx volume
		[Range(0,1)]
		public float SfxVolume=1f;
		
		private List<AudioSource> soundGames = new();

	    protected AudioSource _backgroundMusic;

	    private bool localMusicOn;
	    private bool localSfxOn;

	    private void Start()
	    {
		    LocalDataPlayer.Instance.OnSoundChanged += UpdateSound;
		    UpdateSound(LocalDataPlayer.Instance.GetSound, LocalDataPlayer.Instance.GetMusic);
	    }

	    private void OnDestroy()
	    {
		    LocalDataPlayer.Instance.OnSoundChanged -= UpdateSound;
	    }

	    private void UpdateSound(bool sfxOn, bool musicOn)
	    {
		    localMusicOn = musicOn;
		    localSfxOn = sfxOn;
		    foreach (AudioSource audioSource in soundGames.ToList())
		    {
			    if (audioSource == null)
			    {
				    soundGames.Remove(audioSource);
				    continue;
			    }
			    audioSource.mute = !sfxOn;
		    }

		    if (_backgroundMusic != null)
				_backgroundMusic.mute = !musicOn;
	    }

	    /// <summary>
		/// Plays a background music.
		/// Only one background music can be active at a time.
		/// </summary>
		/// <param name="Clip">Your audio clip.</param>
		public virtual void PlayBackgroundMusic(AudioSource Music)
		{
			// if the music's been turned off, we do nothing and exit
			if (!MusicOn)
				return;
			// if we already had a background music playing, we stop it
			if (_backgroundMusic!=null)
			{
				StopBackGroundMusic();
			}
			// we set the background music clip
			_backgroundMusic=Music;
			// we set the music's volume
			_backgroundMusic.volume=MusicVolume;
			// we set the loop setting to true, the music will loop forever
			_backgroundMusic.loop=true;
			// we start playing the background music
			_backgroundMusic.Play();		
			
			_backgroundMusic.mute = !localMusicOn;
		}	

		/// <summary>
		/// Stops the background music.
		/// </summary>
		public virtual void StopBackGroundMusic()
		{
			if (_backgroundMusic != null)
			{
				_backgroundMusic.Stop();
				Destroy(_backgroundMusic);
			}
		}
		
		/// <summary>
		/// Plays a sound
		/// </summary>
		/// <returns>An audiosource</returns>
		/// <param name="Sfx">The sound clip you want to play.</param>
		/// <param name="Location">The location of the sound.</param>
		/// <param name="Volume">The volume of the sound.</param>
		public virtual AudioSource PlaySound(AudioClip sfx, Vector3 location, bool shouldDestroyAfterPlay=true)
		{
			if (!SfxOn)
				return null;
			// we create a temporary game object to host our audio source
			GameObject temporaryAudioHost = new GameObject("TempAudio");
			// we set the temp audio's position
			temporaryAudioHost.transform.position = location;
			// we add an audio source to that host
			AudioSource audioSource = temporaryAudioHost.AddComponent<AudioSource>() as AudioSource; 
			// we set that audio source clip to the one in paramaters
			audioSource.clip = sfx; 
			// we set the audio source volume to the one in parameters
			audioSource.volume = SfxVolume;
			// we start playing the sound
			audioSource.Play(); 
			// we destroy the host after the clip has played
			audioSource.mute = !localMusicOn;
			if (shouldDestroyAfterPlay)
			{
				Destroy(temporaryAudioHost, sfx.length);
			}
			// we return the audiosource reference
			return audioSource;
		}
		
		/// <summary>
		/// Plays a sound
		/// </summary>
		/// <returns>An audiosource</returns>
		/// <param name="Sfx">The sound clip you want to play.</param>
		/// <param name="Location">The location of the sound.</param>
		/// <param name="Volume">The volume of the sound.</param>
		public virtual AudioSource PlayLoop(AudioClip Sfx, Vector3 Location)
		{
			if (!SfxOn)
				return null;
			// we create a temporary game object to host our audio source
			GameObject temporaryAudioHost = new GameObject("TempAudio");
			// we set the temp audio's position
			temporaryAudioHost.transform.position = Location;
			// we add an audio source to that host
			AudioSource audioSource = temporaryAudioHost.AddComponent<AudioSource>() as AudioSource; 
			// we set that audio source clip to the one in paramaters
			audioSource.clip = Sfx; 
			// we set the audio source volume to the one in parameters
			audioSource.volume = SfxVolume;
			// we set it to loop
			audioSource.loop=true;
			// we start playing the sound
			audioSource.Play(); 
			
			audioSource.mute = !localSfxOn;
			
			soundGames.Add(audioSource);
			// we return the audiosource reference
			return audioSource;
		}
	}
}