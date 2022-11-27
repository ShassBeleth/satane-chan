using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusButtonComponent : MonoBehaviour
{
    public Action OnClickHandler { set; get; }
    public void OnClick()
    {
        if( OnClickHandler != null)
        {
            OnClickHandler();
        }
    }
}
