using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private SpriteRenderer _sprite;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Move(_speed, false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Move(-_speed, true);
        }       
    }

    private void Move(float speed, bool _isFlip)
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        _sprite.flipX = _isFlip;
    }    
}
