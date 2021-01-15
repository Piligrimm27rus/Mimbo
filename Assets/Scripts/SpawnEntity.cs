using UnityEngine;

[RequireComponent(typeof(EnityMovement))]
public class SpawnEntity : MonoBehaviour
{
    [SerializeField]
    Vector3 spawnOffset;

    [SerializeField]
    int spawnTime = 2;

    EnityMovement enityMovement;
    Rigidbody rigidbody;

    Vector3 endPos;
    Vector3 startPos;

    float time = 0;

    private void Start()
    {
        enityMovement = GetComponent<EnityMovement>();
        rigidbody = GetComponent<Rigidbody>();

        enityMovement.enabled = false;
        rigidbody.isKinematic = true;

        endPos = transform.position;
        startPos = endPos - spawnOffset * transform.localScale.y;
        transform.position = startPos;
    }


    private void Update()
    {
        transform.position = Vector3.Lerp(startPos, endPos, easeInOutCubic(time / spawnTime));
        time += Time.deltaTime;

        if (transform.position == endPos)
        {
            enityMovement.enabled = true;
            rigidbody.isKinematic = false;
            Destroy(GetComponent<SpawnEntity>());
        }
    }

    float easeInOutCubic(float x)
    {
        return 1 - Mathf.Cos((x * Mathf.PI) / 2);
    }
}
