using UnityEngine;
using System.Collections;

public class MushroomFreezeEffect : MonoBehaviour
{
    public float timeEffect { get; set; }
    public PlayerMovement player { get; set; }

    private float speed;

    private void Awake()
    {
        player = GetComponent<PlayerMovement>();

        speed = player.speed;
        timeEffect = Time.time + 5f;

        StartCoroutine("Effect", this);
    }

    IEnumerator Effect()
    {
        while (timeEffect >= Time.time)
        {
            player.speed = 0;
            yield return null;
        }

        player.speed = speed;
        StopCoroutine("Effect");
        Destroy(GetComponent<MushroomFreezeEffect>());
    }
}