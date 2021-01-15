using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnityMovement : MonoBehaviour
{
    [SerializeField] float speed;
    private Rigidbody rigidbody;
    private float changeAngleTime = 0;

    private Transform pursuitTarget;
    [SerializeField] float pursuitTime = 2f;
    private float _pursuitTime;
    private bool isPursuit;
    [SerializeField]
    float timeToChangeAngle = 2;

    [SerializeField] int rayCount = 10; //количество лучей
    [SerializeField] float angleBetweenRays = 20f; //Угол между лучами
    [SerializeField] float rayDistance; //Дистанция луча
    [SerializeField] LayerMask rayTarget; //Цель луча
    private Vector3 rayDir; //в какую сторону падает луч
    private float j; //

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Time.time > changeAngleTime && !isPursuit) //меняем направление по времени
        {
            changeAngleTime = Time.time + Random.Range(0, timeToChangeAngle);
            ChangeEntityAngle();
        }

        Ray();
    }

    private void Ray()
    {
        j = -angleBetweenRays * Mathf.Deg2Rad / rayCount * (rayCount / 2);
        for (int i = 0; i < rayCount; i++) //указываем направление луча
        {
            float x = Mathf.Sin(j);
            float y = Mathf.Cos(j);

            j += angleBetweenRays * Mathf.Deg2Rad / rayCount;

            rayDir = transform.TransformDirection(new Vector3(x, 0, y));

            ThrowRay(rayDir);
        }
    }

    private void FixedUpdate()
    {
        if (isPursuit)
        {
            if (_pursuitTime >= Time.time)
            {
                if (pursuitTarget != null)
                {
                    Vector3 lookAtTarget = (pursuitTarget.position - transform.position).normalized;

                    float angle; 
                    angle = Mathf.Atan2(lookAtTarget.x, lookAtTarget.z) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));

                    rigidbody.MovePosition(rigidbody.position + lookAtTarget * speed * Time.deltaTime);
                }
            }
            else
            {
                pursuitTarget = null;
                isPursuit = false;
            }
        }
        else
            rigidbody.MovePosition(rigidbody.position + transform.forward * speed * Time.deltaTime);
    }

    private void ThrowRay(Vector3 direction) //Выпуск луча
    {
        Vector3 startRayVector = new Vector3(transform.position.x, 1.5f, transform.position.z);

        Debug.DrawRay(startRayVector, direction * rayDistance, Color.red);

        if (Physics.Raycast(startRayVector, direction, out RaycastHit hit, rayDistance, rayTarget))
        {

            if (hit.transform.CompareTag("Player"))
            {
                if (transform.localScale.y > hit.transform.localScale.y) //начать преследование
                {
                    pursuitTarget = hit.transform;
                    _pursuitTime = Time.time + pursuitTime;
                    isPursuit = true;
                }
                else if (transform.localScale.y <= hit.transform.localScale.y)
                {
                    ChangeEntityAngle();
                }
            }
        }
    }

    private void ChangeEntityAngle()
    {
        transform.rotation = Quaternion.Euler(0, randomAngle(), 0);
    }

    private float randomAngle()
    {
        return Random.Range(0, 180);
    }
}
