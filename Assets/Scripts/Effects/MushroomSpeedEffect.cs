using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomSpeedEffect : MonoBehaviour
{
    public float timeEffect { get; set; }
    public PlayerMovement player { get; set; }

    private float playerSpeed;

    private void Awake()
    {
        player = GetComponent<PlayerMovement>();

        playerSpeed = player.speed;
        timeEffect = Time.time + 5f;

        StartCoroutine("Effect");
    }

    IEnumerator Effect()
    {
        while (timeEffect >= Time.time)
        {
            player.speed = playerSpeed * 1.5f;
            yield return null;
        }

        player.speed = playerSpeed;
        StopCoroutine("Effect");
        Destroy(GetComponent<MushroomSpeedEffect>());
    }
}
