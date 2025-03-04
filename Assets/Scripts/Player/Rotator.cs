using UnityEngine;

[RequireComponent(typeof(InputPlayerReader))]
public class Rotator : MonoBehaviour
{
    private readonly Quaternion _lookRight = Quaternion.identity;
    private readonly Quaternion _lookLeft = Quaternion.Euler(0, 180, 0);

    private InputPlayerReader _inputReader;

    private void Awake()
    {
        _inputReader = GetComponent<InputPlayerReader>();
    }

    private void Update()
    {
        RotateLogic();
    }

    private void RotateLogic()
    {
        if (_inputReader.HorizontalInput < 0)
            gameObject.transform.rotation = _lookLeft;
        else if (_inputReader.HorizontalInput > 0)
            gameObject.transform.rotation = _lookRight;
    }
}
