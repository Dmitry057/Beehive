using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantConstructions : MonoBehaviour
{
    [SerializeField] private CanvasAtributes _canvasAttributes;
    private Construction _construction;
    private Camera _camera;
    private bool IsSelected;
    private GameObject constrObj;
    private void Start()
    {
        _camera = Camera.main;
    }

    public void TryToPlant(Construction construction)
    {
        constrObj = Instantiate(construction.gameObject,new Vector3(_camera.transform.position.x, _camera.transform.position.y, 0), Quaternion.identity);
        _construction = constrObj.GetComponent<Construction>();
        _construction.AddMovement();
        _construction.GetComponent<OnDragChunkObject>().SetUIAttributes(_canvasAttributes);
        IsSelected = true;
    }
    
    public void PlantConstruction()
    {
        
        
    }
    private void Update()
    {
        if (!IsSelected) return;
        

        if (Input.GetMouseButton(0)) 
        {
            PlantConstruction();
        }
    }
}
