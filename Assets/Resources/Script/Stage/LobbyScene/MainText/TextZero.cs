using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class TextZero : MonoBehaviour
{
    float time = 0;
    Text text;
    bool isBool = false;

    private void Awake()
    {
        text = GetComponent<Text>();
        this.UpdateAsObservable().Subscribe(_ => TextUpdate());
    }

    private void Start()
    {
        text.enabled = false;
    }

    private void OnEnable()
    {
        time = 0;
        isBool = false;
    }

    private void TextUpdate()
    {
        time += Time.deltaTime;

        if (time > 0.1f && isBool == false)
        {
            text.enabled = true;
            isBool = true;
        }
    }
}
