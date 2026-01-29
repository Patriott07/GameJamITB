using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public Slider sliderMasterAudio, sliderBGM, sliderSfx;

    public enum TypeSlider { Master, BGM, SFX }
    public AudioMixer audioMixer;

    void Start()
    {
        sliderMasterAudio.value = PlayerPrefs.GetFloat("master_audio", 0);
        sliderBGM.value = PlayerPrefs.GetFloat("bgm_audio", 0);
        sliderSfx.value = PlayerPrefs.GetFloat("sfx_audio", 0);
    }

    public void OnSliderAudioChange(string typeSlider)
    {
        switch (typeSlider)
        {
            case "Master":
                float v = sliderMasterAudio.value;
                if (v <= -6) v = -99;

                PlayerPrefs.SetFloat("master_audio", v);
                audioMixer.SetFloat("volume_master", v);
                break;
            case "Bg":
                v = sliderBGM.value;
                if (v <= -6) v = -99;

                PlayerPrefs.SetFloat("bgm_audio", v);
                audioMixer.SetFloat("volume_bg", v);
                break;
            case "Sfx":
                v = sliderSfx.value;
                if (v <= -6) v = -99;

                PlayerPrefs.SetFloat("sfx_audio", v);
                audioMixer.SetFloat("volume_sfx", v);
                break;
        }

    }
}
