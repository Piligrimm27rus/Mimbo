using System.Collections;
using UnityEngine;

public class MushroomGrowthEffect : MonoBehaviour
{
    Vector3 newScale;
    Vector3 oldScale;

    float timeEffect;
    public bool isGrowth;
    float playerScale;
    float time = 0;

    IEnumerator Effect()
    {
        time = 0;
        while (timeEffect >= Time.time)
        {
            
            transform.localScale = Vector3.Lerp(transform.localScale, newScale, easeInOutCubic(time / 4));
            time += Time.deltaTime;
            yield return null;
        }

        time = 0;
        while (transform.localScale != oldScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, oldScale, easeInOutCubic(time / 4));
            time += Time.deltaTime;
            yield return null;
        }

        Destroy(GetComponent<MushroomGrowthEffect>(), 0.1f);
        StopCoroutine("Effect");
    }


    private void Awake()
    {
        newScale = transform.localScale * 2.5f;
        oldScale = transform.localScale;


        timeEffect = Time.time + 10f;

        StartCoroutine("Effect", this);
    }

    float easeInOutCubic(float x)
    {
        return 1 - Mathf.Cos((x * Mathf.PI) / 2);
    }
}