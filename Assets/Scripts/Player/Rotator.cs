using UnityEngine;

public class Rotator : MonoBehaviour
{
    private readonly Quaternion _lookRight = Quaternion.identity;
    private readonly Quaternion _lookLeft = Quaternion.Euler(0, 180, 0);

    public void RotateLogic(float inputAxis)
    {
        if (inputAxis < 0)
            gameObject.transform.rotation = _lookLeft;
        else if (inputAxis > 0)
            gameObject.transform.rotation = _lookRight;
    }

    public void Flip(Vector2 position) =>
        transform.rotation = position.x - transform.position.x > 0 ? _lookRight : _lookLeft;
}