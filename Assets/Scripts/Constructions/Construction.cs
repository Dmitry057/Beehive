using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(OnDragChunkObject))]
public abstract class Construction : MonoBehaviour
{
    public string ConstrName { get; protected set; }
    public SpriteRenderer ConstrSprite { get; protected set; }
    public Image ProgressBar { get; protected set; }
    public List<CostRes> ConstrPrice { get; protected set; }
    public List<ConstrCell> ConstrCells { get; protected set; }

    public bool IsPlanted;

    private OnDragChunkObject _constrMovement;
    private UserResources _resources;
    private float _liveTime;
    private void Awake()
    {
        FindMovement();

        ChangeActiveConstrCells(false);

        _resources = FindObjectOfType<UserResources>();
    }
    private void Update()
    {
        GenerateResource(10f);
    }
    private void FindMovement()
    {
        _constrMovement = GetComponent<OnDragChunkObject>();
        _constrMovement.enabled = false;
    }
    public virtual void AddMovement()
    {
        print("AddMovement " + gameObject.name);
        _constrMovement.enabled = true;
    }
    public virtual void ChangeActiveConstrCells(bool setActive)
    {
        foreach(ConstrCell cell in ConstrCells)
        {
            cell.gameObject.SetActive(setActive);
        }
    }
    public virtual void RemoveMovement()
    {
        Destroy(_constrMovement);
    }
    public virtual void Test()
    {
        print(ConstrName);
    }
    public virtual bool HaveAllFreeCells()
    {
        bool a = true;
        foreach(ConstrCell cell in ConstrCells)
        {
            a &= cell.FindFreeCells();
        }
        return a;
    }
    public virtual void Save()
    {
        ConstructionData data = new ConstructionData()
        {
            Name = this.name,
            Id = 1,
            PlantVector = new Vector2(transform.position.x, transform.position.y),
            Level = 1,
            Exp = 0
        };
    }

    public virtual void GenerateResource(float timeDelta)
    {
        if (!IsPlanted) return;

        if (_liveTime >= 0)
        {
            _liveTime -= Time.deltaTime;
            ProgressBar.fillAmount =1 -  _liveTime / timeDelta;
        }
        else
        {
            _liveTime = timeDelta;
            _resources.Honey.SetValue(100);
        }
        
    }

}
public struct ConstructionData
{
    public string Name;
    public int Id;
    public Vector2 PlantVector;
    public int Level;
    public float Exp;
}

