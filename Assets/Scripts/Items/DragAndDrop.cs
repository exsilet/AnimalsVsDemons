using UnityEngine;

namespace Items
{
    class DragAndDrop : MonoBehaviour
    {
        private Vector3 _startPosition;
        private Vector3 _offset;
        private Camera _camera;
        private Vector3 _mousePosition;

        public Vector3 StartPosition => _startPosition;

        private void Awake() 
            => _camera = Camera.main;

        private void Start()
        {
            _startPosition = transform.position;
        }        

        public void SetStartPosition()
            => transform.position = _startPosition;

        private void OnMouseDown()
        {
            _offset = transform.position - GetMousePosition();

            var rayOrigin = _camera.transform.position;
            var rayDirection = GetMousePosition() - rayOrigin;

            RaycastHit2D hitInfo = Physics2D.Raycast(rayOrigin, rayDirection);
            transform.GetComponent<Collider2D>().enabled = false;
        }
           
        private void OnMouseDrag() 
            => transform.position = GetMousePosition() + _offset;

        private void OnMouseUp()
        {
            Vector2 clickPosition = GetMousePosition();
            RaycastHit2D[] hitInfo = Physics2D.RaycastAll(clickPosition, Vector2.zero);
            transform.GetComponent<Collider2D>().enabled = true;
            Invoke(nameof(SetStartPosition), 0.2f);
        }        

        private Vector3 GetMousePosition()
        {
            if (Input.GetMouseButton(0))
            {
                _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                _mousePosition.z = 0;                
            }            
            return _mousePosition;
        }        
    }
}
