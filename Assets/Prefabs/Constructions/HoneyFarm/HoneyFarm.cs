using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HoneyFarm : Construction
{
    public string HoneyName;
    public SpriteRenderer HoneySprite;

    public List<CostRes> HoneyPrice;
    public List<ConstrCell> HoneyCells;
    public Image HoneyProgressBar;
    public override void Test()
    {
        ConstrName = HoneyName;
        base.Test();
    }
    public override bool HaveAllFreeCells()
    {
        ConstrCells = HoneyCells;
        return base.HaveAllFreeCells();
    }
    public override void ChangeActiveConstrCells(bool setActive)
    {
        ConstrCells = HoneyCells;
        base.ChangeActiveConstrCells(setActive);
    }
    public override void GenerateResource(float timeDelta)
    {
        ProgressBar = HoneyProgressBar;
        base.GenerateResource(timeDelta);
    }
}
