using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPlaceableObject : MonoBehaviour
{
    private PlacementManager _placementManager = null;
    private Button _button = null;
    [SerializeField] private int _index = 0;
    [SerializeField] private GameObject _child;

    public int Index
    {
        get { return _index; }
        set { _index = value; }
    }

    private void Start()
    {
        _placementManager = PlacementManager.Instance;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ChangeBlockListener);

        _child.GetComponent<Image>().sprite = _placementManager.GetSpriteFromIndex(_index);
    }

    private void ChangeBlockListener()
    {
        _placementManager.SetSelectedBlockID(_index);
    }

    private void OnMouseEnter()
    {
        Debug.Log("HOVER");
    }

}
