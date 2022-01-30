using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(BoxCollider2D))]
public class SpriteButton : MonoBehaviour
{
    public UnityEvent OnClick;
    private void OnMouseDown()
    {
        OnClick?.Invoke();
    }
}
