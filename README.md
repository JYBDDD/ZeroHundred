# ZeroHundred
2. 두번째 프로젝트 (2.5D 탄막게임 - 서버연결<뒤끝> - 구글플레이 연동)


public void InitVolume()
    {
        var vol = PlayerPrefs.GetFloat("Volume_Master");
        Volume_Master = vol != 0 ? vol : 1f;
        sliderMaster.value = Volume_Master;
        //Debug.Log($"sliderMaster.value {sliderMaster.value}");
        SetMasterVal(Volume_Master);

        vol = PlayerPrefs.GetFloat("Volume_Narr");
        Volume_Narr = vol != 0 ? vol : 1f;
        sliderNarr.value = Volume_Narr;
        //Debug.Log($"sliderNarr.value {sliderNarr.value}");
        SetNarrVal(Volume_Narr);

        vol = PlayerPrefs.GetFloat("Volume_BGM");
        Volume_BGM = vol != 0 ? vol : 1f;
        sliderBGM.value = Volume_BGM;
        //Debug.Log($"sliderBGM.value {sliderBGM.value}");
        SetBGMVal(Volume_BGM);

        vol = PlayerPrefs.GetFloat("Volume_SFX");
        Volume_SFX = vol != 0 ? vol : 1f;
        sliderSFX.value = Volume_SFX;
        //Debug.Log($"sliderSFX.value {sliderSFX.value}");
        SetSFXVal(Volume_SFX);

        vol = PlayerPrefs.GetInt("Volume_Mute", 1);
        Volume_Mute = vol > 0 ? 1 : 0;
        SetMute(Volume_Mute > 0);

        _initSet = true;
    }
    public void SetMasterVal(float sliderVal)
    {
        //Debug.Log($"SetmasterVal");

        mixer.SetFloat("Volume_Master", Mathf.Log10(sliderVal) * 20);
        Volume_Master = sliderVal;
    }
    public void SetNarrVal(float sliderVal)
    {
        mixer.SetFloat("Volume_Narr", Mathf.Log10(sliderVal) * 20);
        Volume_Narr = sliderVal;
    }
