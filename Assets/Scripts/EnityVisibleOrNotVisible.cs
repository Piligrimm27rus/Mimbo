using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnityVisibleOrNotVisible : MonoBehaviour
{
    public bool isVisible;

    private void OnBecameInvisible()
    {
        isVisible = false;
    }

    private void OnBecameVisible()
    {
        isVisible = true;
    }
}
