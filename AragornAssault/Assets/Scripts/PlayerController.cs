using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] float controlSpeed;
    [SerializeField] float xRange = 10f;
    [SerializeField] float yRange = 7f;
    // Start is called before the first frame update


    private void OnEnable()
    {
        movement.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalThrow = movement.ReadValue<Vector2>().x;
        float verticalThrow = movement.ReadValue<Vector2>().y;

        float xOffset = horizontalThrow * Time.deltaTime * controlSpeed;
        float rawXpos = transform.localPosition.x + xOffset;
        float clampedXpos = Mathf.Clamp(rawXpos,-xRange,xRange);

        float yOffset = verticalThrow * Time.deltaTime * controlSpeed;
        float rawYpos = transform.localPosition.y + yOffset;
        float clampedYpos = Mathf.Clamp(rawYpos,-yRange,yRange);

        transform.localPosition = new Vector3 (clampedXpos,clampedYpos,transform.localPosition.z);
    }
}
