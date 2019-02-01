using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

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

    //===== BLOCK PLACING BUTTONS =====
    [SerializeField] private GameObject _buttonPrefab;
    private List<GameObject> _buttons = new List<GameObject>();
    private float _buttonOffset = 64.0f;

    public void AddButton(int index)
    {
        var inst = Instantiate(_buttonPrefab, transform);
        inst.GetComponent<ButtonPlaceableObject>().Index = index;
        inst.SetActive(true);
        inst.transform.position -= new Vector3(0, index * _buttonOffset, 0);
    }

    //===== SAVE POP UP =====
    [SerializeField] private GameObject _SavePopUp;

    public void ShowSavePopUp()
    {
        _SavePopUp.SetActive(true);
    }

    public void HideSavePopUp()
    {
        _SavePopUp.SetActive(false);
    }

    //===== LOAD POP UP =====
    [SerializeField] private GameObject _LoadPopUp;

    public void ShowLoadPopUp()
    {
        _dropdown.ClearOptions();

        _LoadPopUp.SetActive(true);

        string[] results = Directory.GetFiles("Levels/");

        List<string> options = new List<string>();

        foreach(string path in results)
        {
            int indBegin = path.IndexOf('/', 0, path.Length);
            int indEnd = path.IndexOf('.', 0, path.Length);
            int length = indEnd - (indBegin + 1);

            string file = path.Substring(indBegin + 1, length);
            Debug.Log(file);
            options.Add(file);
        }

        _dropdown.AddOptions(options);
    }

    public void HideLoadPopUp()
    {
        _LoadPopUp.SetActive(false);
    }

    //===== DROPDOWN =====
    [SerializeField] private Dropdown _dropdown;

    //===== LAST SAVED / LOADED FILE =====
    private string _LastEditedFile = "";

    public string LastEditedFile
    {
        get { return _LastEditedFile; }
        set { _LastEditedFile = value; }
    }

    

    //===== START =====
    private void Start()
    {
        _SavePopUp.SetActive(false);
        _LoadPopUp.SetActive(false);
    }
}
