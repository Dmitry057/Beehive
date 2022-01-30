using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDragChunkObject : MonoBehaviour
{
    #region Options
    private bool _isDragged;
    private bool _doOneTime;
    private bool _isStarted = false;

    private float _zCoord;
    private Vector3 newPos;
    private LayerMask _mask;
    #endregion

    #region Components
    private Camera _camera;
    private CanvasAtributes _canvasAtributes;

    #endregion
    public void SetUIAttributes(CanvasAtributes atributes)
    {
        _canvasAtributes = atributes;
        _canvasAtributes.ShowConfirm();
        AddListeners();
    }
    private void AddListeners()
    {
        _canvasAtributes.CancelButton.onClick.AddListener(_canvasAtributes.HideConfirm);
        _canvasAtributes.CancelButton.onClick.AddListener(DestroyConstruction);
        _canvasAtributes.AcceptButton.onClick.AddListener(Accept);
    }
    private void Start() // dont forget about remove this component!
    {
        _camera = Camera.main;

        _mask = LayerMask.GetMask("Chunk");

        GetComponent<Construction>().ChangeActiveConstrCells(true);
    }

    private void OnMouseDown()
    {
        _zCoord = _camera.WorldToScreenPoint(gameObject.transform.position).z;
        _isDragged = true;
        _camera.GetComponent<UserMovement>().CanMovement = false;
    }
    private void OnMouseUp()
    {
        _isDragged = false;
        _camera.GetComponent<UserMovement>().CanMovement = true;
    }
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = _zCoord;

        return _camera.ScreenToWorldPoint(mousePoint);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 18);
        if (Vector3.Distance(transform.position, newPos) < 1f && _doOneTime)
        {
            _doOneTime = false;
           _canvasAtributes.AcceptButton.interactable = GetComponent<Construction>().HaveAllFreeCells();
        }
        if (!_isDragged && _isStarted) return;

        RaycastHit2D hit = Physics2D.Raycast(GetMouseWorldPos(), Vector3.forward, 1, _mask);
        if (hit.collider != null)
        {

            if (hit.collider.gameObject.GetComponent<Chunk>())
            {
                if (newPos != hit.collider.gameObject.transform.position)
                {
                    _doOneTime = true;
                }
                newPos = hit.collider.gameObject.transform.position;
                _isStarted = true;

            }
        }
    }
    private void Accept()
    {
        _canvasAtributes.RemoveListeners(_canvasAtributes.CancelButton, _canvasAtributes.AcceptButton);
        _canvasAtributes.HideConfirm();
        GetComponent<Construction>().ChangeActiveConstrCells(false);
        GetComponent<Construction>().IsPlanted = true;
        GetComponent<Construction>().ProgressBar.gameObject.SetActive(true);
        this.enabled = false;
    }
    private void DestroyConstruction()
    {
        Destroy(this.gameObject);
        _canvasAtributes.RemoveListeners(_canvasAtributes.CancelButton, _canvasAtributes.AcceptButton);
    }
}
