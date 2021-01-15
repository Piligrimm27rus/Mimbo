using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthEffect : MonoBehaviour
{
    float timeEffect;
    float playerScale;
    float time = 0;

    IEnumerator Growth()
    {
        MushroomGrowthEffect mushroomGrowthEffect = GetComponent<MushroomGrowthEffect>();
        if (mushroomGrowthEffect != null)
        {
            while (mushroomGrowthEffect.isGrowth)
            {
                yield return null;
            }
        }

        time = 0;
        playerScale = transform.localScale.y;
        timeEffect = Time.time + 10f;

        while (timeEffect >= Time.time)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(playerScale, playerScale, playerScale) * 2.5f, easeInOutCubic(time / 4));
            time += Time.deltaTime;
            yield return null;
        }

        time = 0;
        while (transform.localScale.y != playerScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(playerScale, playerScale, playerScale), easeInOutCubic(time / 4));
            time += Time.deltaTime;
            yield return null;
        }

        Destroy(GetComponent<GrowthEffect>(), 0.1f);
        StopCoroutine("Growth");
    }


    private void Awake()
    {
        StartCoroutine("Growth", this);
    }

    float easeInOutCubic(float x)
    {
        return 1 - Mathf.Cos((x * Mathf.PI) / 2);
    }
}
