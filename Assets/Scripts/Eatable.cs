using UnityEngine;
using UnityEngine.SceneManagement;

public class Eatable : MonoBehaviour
{
    //transform - тот кто ест
    //collision - кого едят

    private void OnTriggerEnter(Collider other)
    {
        if (transform.GetComponent<SpawnEntity>() == null)
        {
            if (transform.CompareTag("Player") && other.gameObject.CompareTag("Entity")) //если игрок
            {
                if (transform.localScale.y >= other.transform.localScale.y) //если размер игрока такой же или больше
                {
                    GameManager.instance.score++;
                    EatEntity(other);
                }
            }
            else if (transform.CompareTag("Player") && other.gameObject.CompareTag("Coin"))
            {
                Debug.Log("+1 монетка");
                EatEntity(other);
            }
            else if (transform.CompareTag("Player") && other.gameObject.CompareTag("Mushroom"))
            {
                AddMushroomEffect(other.GetComponent<MushroomEffect>().GetNameEffect());
                EatEntity(other);
            }
            else if (transform.CompareTag("Player") && other.gameObject.CompareTag("Portal"))
            {
                SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings); //следующая сцена
            }

            if (transform.localScale.y > other.transform.localScale.y) //едят игрока
            {
                if (transform.CompareTag("Entity") && other.gameObject.CompareTag("Player"))
                {
                    EatEntity(other);
                }
            }
        }
    }

    private void AddMushroomEffect(string nameEffect)
    {
        switch (nameEffect)
        {
            case "Speed":
                gameObject.AddComponent<MushroomSpeedEffect>();
                break;
            case "Growth":
                gameObject.AddComponent<MushroomGrowthEffect>();
                break;
            case "Freeze":
                gameObject.AddComponent<MushroomFreezeEffect>();
                break;
            default:
                break;
        }
    }

    private void EatEntity(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().enabled = false;
        }
        else
        {
            other.gameObject.GetComponent<EnityMovement>().enabled = false;
        }

        other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        other.gameObject.GetComponent<CapsuleCollider>().enabled = false;

        other.transform.SetParent(transform);
        other.transform.localPosition = new Vector3(0, transform.position.y + other.transform.localScale.y, 0);
        other.transform.rotation = Quaternion.Euler(180, 0, 0);
        EnityManager.instance.RemoveAndDestroyObjectInEnityList(other.gameObject, 0.5f);
    }
}
