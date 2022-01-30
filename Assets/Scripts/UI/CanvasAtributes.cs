using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasAtributes : MonoBehaviour
{
    [SerializeField] private Text TotalCostText;

    public Button AcceptButton;

    public Button CancelButton;

    public Button ShovelButton;

    [SerializeField] private UserResources res;

    [SerializeField] private Animator MenuAnimator;

    private void Start()
    {
        
    }
    public void ShowConfirm()
    {
        MenuAnimator.SetBool("ChunkConfirmation", true);
    }
    public void HideConfirm()
    {
        MenuAnimator.SetBool("ChunkConfirmation", false);
    }
    public void ShowShovel()
    {
        AcceptButton.onClick.AddListener(SellProduct);
        ShovelButton.interactable = false;
        CancelButton.onClick.AddListener(CloseShovel);
        ShowConfirm();
    }
    public void CloseShovel()
    {
        ShovelButton.interactable = true;
        AcceptButton.interactable = true;

        res.buyer.Return();
       
        HideConfirm();

        
    }
    public void RemoveListeners(Button button1, Button button2)
    {
         button1.onClick.RemoveAllListeners();
         button2.onClick.RemoveAllListeners();
    }
    public void RemoveListeners(Button button1, Button button2, Button button3)
    {
        RemoveListeners(button1, button2);
        button3.onClick.RemoveAllListeners();
    }
    public void RemoveListeners(Button button1, Button button2, Button button3, Button button4)
    {
        RemoveListeners(button1, button2, button3);
        button4.onClick.RemoveAllListeners();
    }

    public void ReloadGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Loading");
    }
    public void BoughtUnsuccesfull()
    {
        AcceptButton.interactable = false;
    }
    public void BoughtSuccesfull()
    {
        AcceptButton.interactable = true;
    }
    public void SellProduct()
    {
        ShovelButton.interactable = true;
        res.buyer.SellOut();
        HideConfirm();
    }
    public void OpenCloseConstructionTable()
    {
        MenuAnimator.SetTrigger("ConstructionTable");
    }
    public string totalCost(List<CostRes> costReses)
    {
        string s = "";
        foreach (CostRes res in costReses)
        {
            s += res.name + " - " + res.value + "\n";
        }
        return s;
    }
}
