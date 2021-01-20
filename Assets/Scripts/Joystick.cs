using UnityEngine.UI;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    [SerializeField] Image circle;
    [SerializeField] Image outerCircle;
    [SerializeField] RectTransform canvas;

    Vector2 inputPointA;
    Vector2 inputPointB;
    bool isTouched;

    private void Update()
    {
        if (OnLoadSettings.gameEvent == GameEvent.InGame)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, Input.mousePosition, Camera.main, out inputPointA);

                circle.rectTransform.anchoredPosition = inputPointA;
                outerCircle.rectTransform.anchoredPosition = inputPointA;

                circle.enabled = true;
                outerCircle.enabled = true;
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
    }

    private void FixedUpdate()
    {
        if (isTouched)
        {
            Vector2 offset = inputPointB - inputPointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, circle.rectTransform.rect.width) * -1;

            circle.rectTransform.anchoredPosition = new Vector2(inputPointA.x + direction.x, inputPointA.y + direction.y);
        }
        else
        {
            circle.enabled = false;
            outerCircle.enabled = false;
        }
    }
}
