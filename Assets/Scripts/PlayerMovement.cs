using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] RectTransform canvas;
    [SerializeField] float rotationOffset;
    public float speed;

    Rigidbody player_rb;
    Vector2 inputPointA;
    Vector2 inputPointB;
    bool isTouched;

    private void Start()
    {
        player_rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, Input.mousePosition, Camera.main, out inputPointA);

        }
        if (Input.GetMouseButton(0))
        {
            isTouched = true;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, Input.mousePosition, Camera.main, out inputPointB);
        }
        else
        {
            isTouched = false;
        }
    }
    private void FixedUpdate()
    {
        if (isTouched)
        {
            Vector2 offset = inputPointB - inputPointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1f);

            //move
            player_rb.MovePosition(player_rb.position + new Vector3(direction.x, 0, direction.y) * Time.deltaTime * speed);

            //rotation
            float angle;
            angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + rotationOffset;
            player_rb.transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
        }
    }
}
