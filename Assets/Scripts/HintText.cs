using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintText : MonoBehaviour
{
    public Text Hint;
    void Start()
    {
        this.Hint.text = "";
    }
    void Update()
    {
        
    }

    public void SetHintText(string hint)
    {
        this.Hint.text = hint;
    }

    public void Dispose()
    {
        this.Hint.text = "";
    }
}
