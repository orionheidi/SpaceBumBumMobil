using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{

	public Slider slider;
	public AudioSource myMusic;



	void Update ()
	{
        
		myMusic.volume = slider.value;
        
	}





	/*public Toggle fullScreenToggle;
        public Dropdown resolutionDropdown;
        public Dropdown textureDropdown;
        public Dropdown antialiasingDropdown;
        public Dropdown vSyncDropdown;
        public Slider musicSlider;

        public AudioSource musicSorce;
        public Resolution[] resolutions;
        public GameSettings gameSetings;

        void OnEnable()
        {
            gameSetings = new GameSettings();

            fullScreenToggle.onValueChanged.AddListener(delegate { OnFullScreenToggle(); });
            resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
            textureDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
            antialiasingDropdown.onValueChanged.AddListener(delegate { OnAntialiasingChange(); });
            vSyncDropdown.onValueChanged.AddListener(delegate { OnVSyncChange(); });
            musicSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });

            resolutions = Screen.resolutions;

            foreach(Resolution resolution in resolutions)
            {
                resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
            }
        }

        public void OnFullScreenToggle()
        {
           gameSetings.fullScreen = Screen.fullScreen = fullScreenToggle.isOn;
        }

        public void OnResolutionChange()
        {
            Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
        }

        public void OnTextureQualityChange()
        {
            QualitySettings.masterTextureLimit = gameSetings.textureQuality = textureDropdown.value;

        }

        public void OnAntialiasingChange()
        {
            QualitySettings.antiAliasing = gameSetings.antialising = (int) Mathf.Pow(2f, antialiasingDropdown.value);

        }

        public void OnVSyncChange()
        {
            QualitySettings.vSyncCount = gameSetings.vSync = vSyncDropdown.value;
        }

        public void OnMusicVolumeChange()
        {
            musicSorce.volume = gameSetings.musicVolume = musicSlider.value;

        }

      
}
