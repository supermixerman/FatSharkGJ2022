using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textField;
    
    public void SetText(string a){
        textField.text = a;
    }

    public void SetTextColor(Color newColor){
        textField.color = newColor;
    }
}
