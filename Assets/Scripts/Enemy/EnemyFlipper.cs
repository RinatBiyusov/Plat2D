using UnityEngine;


public class EnemyFlipper : MonoBehaviour
{
    private readonly Quaternion _lookRight = Quaternion.identity;
    private readonly Quaternion _lookLeft = Quaternion.Euler(0, 180, 0);
    
    public void Flip(Vector2 position) => transform.rotation = position.x - transform.position.x > 0 ? _lookRight : _lookLeft;
}