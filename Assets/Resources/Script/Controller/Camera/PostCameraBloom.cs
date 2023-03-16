using Path;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostCameraBloom : MonoBehaviour        // PostProcessing (Bloom)전용 카메라 스크립트
{
    float time = 0;
    bool isBool = false;
    PostProcessVolume b_Volume;

    private void Awake()
    {
        b_Volume = GetComponent<PostProcessVolume>();
    }

    private void Update()
    {
        BloomUpdate();
    }

    #region FlashBang 전용
    private void BloomUpdate() 
    {
        if(FlashBangC.FlashBangStart == true)
        {
            if (isBool == false)
            {
                isBool = true;
                b_Volume.isGlobal = true;
                GameManager.Sound.Play(ObjSound_P.FlashBangRinging);     // 섬광 이명 사운드
            }

            time += Time.deltaTime;
            b_Volume.weight = Mathf.Lerp(b_Volume.weight, 0f, Time.deltaTime / 2.5f);

            if (time > 7f)
            {
                isBool = false;
                b_Volume.isGlobal = false;
                b_Volume.weight = 1;
                time = 0;
                Boss1Controller.FlashBangBool = false;
                FlashBangC.FlashBangStart = false;
            }
        }
    }
    #endregion
}
