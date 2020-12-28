using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] RectTransform canvas;
    [SerializeField] float speed;
    [SerializeField] float rotationOffset;

    CharacterController player;
    Vector2 inputPointA;
    Vector2 inputPointB;
    bool isTouched;

    private void Start()
    {
        player = GetComponent<CharacterController>();
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
            player.Move(new Vector3(direction.x, 0, direction.y) * Time.deltaTime * speed);

            //rotation
            float angle;
            angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + rotationOffset;
            player.transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
        }
    }
}
