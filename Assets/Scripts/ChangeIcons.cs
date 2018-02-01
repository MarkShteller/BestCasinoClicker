using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeIcons : MonoBehaviour
{

    public void CallChangeIcons()
    {
        GameManager.Instance.ChangeOutIcons();
    }
}
