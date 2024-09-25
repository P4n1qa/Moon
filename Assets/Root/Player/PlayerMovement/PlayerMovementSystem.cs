using UnityEngine;

namespace Root.Player.PlayerMovement
{
    public class PlayerMovementSystem : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float rotationSpeed;
    
        private Vector3 _movement;
        private Rigidbody _rb;
        private FixedJoystick _fixedJoystick;

        private bool _isSystemReady;

        public void Initialize(FixedJoystick fixedJoystick)
        {
            _rb = GetComponent<Rigidbody>();
            _fixedJoystick = fixedJoystick;
            _isSystemReady = true;
        }
        
        private void Update()
        {
            if (!_isSystemReady)return;
            Move();
            Rotate();
        }

        private void Move()
        {
            float moveX = _fixedJoystick.Horizontal;
            float moveY = _fixedJoystick.Vertical;
        
            Vector3 movement = new Vector3(moveX, 0f, moveY);
            
            if (movement.magnitude > 1)
            {
                movement = movement.normalized;
            }

            _movement = movement * speed;
        
            _rb.velocity = _movement;
        }

        private void Rotate()
        {
            if (_movement == Vector3.zero) return;
            Quaternion targetRotation = Quaternion.LookRotation(_movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 
                rotationSpeed * Time.deltaTime);
        }
    }
}
