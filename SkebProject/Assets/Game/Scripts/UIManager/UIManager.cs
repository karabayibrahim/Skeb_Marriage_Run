using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Image Bar;
    public Image NegativeBar;
    public Text StatusText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StatusTextAdjust(string _text,Color _color)
    {
        StatusText.text = _text;
        StatusText.color = _color;
    }
}
