using UnityEngine;

public class FieldTrigger : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && (EnityManager.instance.GetEntityCount() < EnityManager.instance.MaxEntity))
        {
            float scaleTriggerX = transform.lossyScale.x / 2;
            float scaleTriggerZ = transform.lossyScale.z / 2;

            EnityManager.instance.SpawnEntity(scaleTriggerX, scaleTriggerZ, transform.position) ;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnityManager.instance.DestroyVisibleEntity();
        }
    }
}
