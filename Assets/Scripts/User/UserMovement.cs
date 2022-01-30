using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GD.MinMaxSlider;
[RequireComponent(typeof(Camera))]
public class UserMovement : MonoBehaviour
{
    [MinMaxSlider(0f, 100f)]
    [SerializeField] private Vector2 size;

    [MinMaxSlider(-100f, 100f)]
    [SerializeField] private Vector2 height;

    [MinMaxSlider(-100f, 100f)]
    [SerializeField] private Vector2 width;

    [Range(0.001f, 1f)]
    [SerializeField] private float scaleSpeed;

    [Range(0.001f, 1f)]
    [SerializeField] private float moveSpeed;

    [HideInInspector]
    public bool CanMovement;

    private Camera _camera;


    #region Pose props
    private Vector3 _startMousePos;
    private float _groundZ = 0;
    private bool isTouched;
    #endregion
    private void Start()
    {
        _camera = GetComponent<Camera>();
    }
    private void FixedUpdate()
    {

        if (Input.touchCount == 2)
        {

            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroLastPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOneLastPos = touchOne.position - touchOne.deltaPosition;

            float distTouch = (touchZeroLastPos - touchOneLastPos).magnitude;
            float currentDistTouch = (touchZero.position - touchOne.position).magnitude;

            float difference = currentDistTouch - distTouch;

            SetScale(difference * scaleSpeed);
        }
        else if (CanMovement)
        {
            SetPosition();
        }
        
        SetScale(Input.GetAxis("Mouse ScrollWheel") * 100);
    }
    private void SetScale(float increment)
    {
       float z = Mathf.Clamp(-_camera.transform.position.z - increment, size.x, size.y);
        _camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y, -z);
    }

    private void SetPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startMousePos = GetWorldPosition(_groundZ);
            isTouched = true;
        }

        if (Input.GetMouseButton(0) && isTouched)
        {
            Vector3 direction = _startMousePos - GetWorldPosition(_groundZ);
            Vector3 moveTo = _camera.transform.position += direction;
            moveTo.x = Mathf.Clamp(moveTo.x, width.x, width.y);
            moveTo.y = Mathf.Clamp(moveTo.y, height.x, height.y);
            _camera.transform.position = moveTo;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isTouched = false;
        }
    }
    private Vector3 GetWorldPosition(float z)
    {
        Ray mousePos = _camera.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        ground.Raycast(mousePos, out distance);
        return mousePos.GetPoint(distance);
    }

}

