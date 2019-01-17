using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorUIManager : MonoBehaviour
{
    //===== SINGLETON =====
    private static EditorUIManager _instance = null;

    public static EditorUIManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance == null) _instance = this;
    }

    //===== GENERAL =====
    [SerializeField] private GameObject _buttonPrefab;
    private List<GameObject> _buttons = new List<GameObject>();
    private float _buttonOffset = 64.0f;

    public void AddButton(int index)
    {
        var inst = Instantiate(_buttonPrefab,transform);
        inst.GetComponent<ButtonPlaceableObject>().Index = index;
        inst.SetActive(true);
        inst.transform.position -= new Vector3(0,index * _buttonOffset, 0);
    }
}
