using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntityAnimation
{
    public Animator moveAnim { get; private set; }
    public Animator bodyAnim { get; private set; }


    public AntityAnimation() { }

    public AntityAnimation(Transform entity)
    {
        if (entity.GetComponent<Animator>() != null)
        {
            bodyAnim = entity.GetComponent<Animator>();

        }

        foreach (Transform item in entity.GetComponentInChildren<Transform>())
        {
            if (item.GetComponent<Animator>() != null)
            {
                moveAnim = item.GetComponentInChildren<Animator>();
            }
        }
    }
}
