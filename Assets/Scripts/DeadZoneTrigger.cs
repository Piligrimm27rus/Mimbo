using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneTrigger : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                //GameOver
                break;
            case "Entity":
            case "Coin":
            case "Mushroom":
            case "Portal":
                EnityManager.instance.RemoveAndDestroyObjectInEnityList(collision.gameObject);
                break;
        }
    }
}
