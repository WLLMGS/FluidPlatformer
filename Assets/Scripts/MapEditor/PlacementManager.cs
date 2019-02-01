using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlacementMode
{
    Add,
    Delete
}


public class PlacementManager : MonoBehaviour
{
    //===== SINGLETON =====
    private static PlacementManager _instance = null;

    public static PlacementManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance == null) _instance = this;
    }

    //===== GENERAL =====
    [SerializeField] private Transform _editorMouse;
    [SerializeField] private List<GameObject> _placeableObjects = new List<GameObject>();
    [SerializeField] private GameObject _deletionBlock = null;
    [SerializeField] private Text _filenameSave;
    [SerializeField] private Dropdown _filenameDropdown;

    private List<GameObject> currentSelected = new List<GameObject>();
    private GameObject _levelParent;
    private MapSaver _mapSaver;
    private MapLoader _mapLoader;

    private PlacementMode _placementMode = PlacementMode.Add;

    private float _placementCooldown = 0.01f;
    private bool _canPlaceBlocks = true;

    private bool _IsHoveringOverButton = false;
    private int _BrushSize = 3;
    private int _selectedID = 0;



    private void Start()
    {
        _levelParent = new GameObject("Level");
        var ui = EditorUIManager.Instance;
        _mapSaver = GetComponent<MapSaver>();
        _mapLoader = GetComponent<MapLoader>();

        for (int i = 0; i < _placeableObjects.Count; ++i)
        {
            ui.AddButton(i);
        }
    }


    private void Update()
    {
        //controls that lets user select which block to place or to delete blocks -> replace later with UI
        HandleControls();

        //depending on the placement mode -> add blocks or delete blocks
        switch (_placementMode)
        {
            case PlacementMode.Add:
                HandleBlockPlacement();
                break;
            case PlacementMode.Delete:
                HandleBlockDeletion();
                break;
        }
    }

    void HandleBlockPlacement()
    {
        //if can place blocks && there is a block selected && the mouse is clicked
        if (_canPlaceBlocks
            && currentSelected.Count > 0
            && !_IsHoveringOverButton
            && Input.GetMouseButton(0))
        {
            //start placement cooldown
            _canPlaceBlocks = false;
            StartCoroutine(PlacementCooldown());

            //remove overlapping block
            RemoveUnderlyingBlocks();

            //create the new block
            var blockdata = currentSelected[0].GetComponent<EditorBlockData>();

            foreach (GameObject block in currentSelected)
            {
                var obj = Instantiate(block, block.transform.position + new Vector3(0,0, blockdata.ZValue)
                    , block.transform.rotation);

                obj.GetComponent<Collider2D>().enabled = true;
                obj.transform.parent = _levelParent.transform;
            }

        }

        if (Input.GetMouseButtonDown(1))
        {
            DetachCurrentSelectedBlocks();
        }
    }

    void HandleBlockDeletion()
    {
        //if block placement cooldown is up & mouse is clicked
        if (_canPlaceBlocks
            && !_IsHoveringOverButton
            && Input.GetMouseButton(0))
        {
            //start block placement cooldown
            _canPlaceBlocks = false;
            StartCoroutine(PlacementCooldown());

            //remove the block the user is hovering over
            DeleteUnderlyingBlocks();
        }

        if (Input.GetMouseButtonDown(1))
        {
            DetachCurrentSelectedBlocks();
        }
    }

    void DeleteBlockAtMousePos()
    {
        //raycast at mouse pos in direction zero
        RaycastHit2D hit = Physics2D.Raycast(_editorMouse.position, Vector2.zero); ;
        //if there is a hit remove that object
        if (hit)
        {
           Destroy(hit.collider.gameObject);
        }

    }

    void RemoveUnderlyingBlocks()
    {
        var currentData = currentSelected[0].GetComponent<EditorBlockData>();

        //check if they're on the same layer
        foreach (GameObject block in currentSelected)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(block.transform.position, Vector2.zero);

            foreach(var hit in hits)
            {
                if (hit)
                {
                    var data = hit.collider.GetComponent<EditorBlockData>();
                    if (currentData.ZValue == data.ZValue) Destroy(hit.collider.gameObject);
                }
            }
            
        }
    }

    void DeleteUnderlyingBlocks()
    {
        foreach (GameObject block in currentSelected)
        {
            RaycastHit2D hit = Physics2D.Raycast(block.transform.position, Vector2.zero);
            
            if (hit)
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }

    //replace soon
    void HandleControls()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            IncreaseBrushSize();
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            DecreaseBrushSize();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DetachCurrentSelectedBlocks();

            _placementMode = PlacementMode.Delete;

            AttachDeletionBlocks();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //rotate blocks + 90.0f
            RotateSelectedBlocks();
        }
    }

    private void DetachCurrentSelectedBlocks()
    {
        //remove current block to be placed
        //Destroy(currentSelected);

        foreach (GameObject block in currentSelected)
        {
            Destroy(block);
        }
        currentSelected.Clear();
    }

    //placement cooldown after X amount of seconds
    private IEnumerator PlacementCooldown()
    {
        yield return new WaitForSeconds(_placementCooldown);
        _canPlaceBlocks = true;
    }


    public void SetSelectedBlockID(int id)
    {
        //set selected id to new id
        _selectedID = id;
        //remove current selected block
        DetachCurrentSelectedBlocks();
        //set placement mode to Add
        _placementMode = PlacementMode.Add;

        //add blocks based on brushsize
        for (int x = 0; x < _BrushSize; ++x)
        {
            for (int y = 0; y < _BrushSize; ++y)
            {
                var obj = Instantiate(_placeableObjects[id], _editorMouse.transform.position, Quaternion.identity);
                obj.transform.parent = _editorMouse;
                obj.transform.localPosition = new Vector3(0 - (_BrushSize / 2) + x, 0 - (_BrushSize / 2) + y, 0);
                obj.GetComponent<Collider2D>().enabled = false;
                currentSelected.Add(obj);
            }
        }
    }

    private void IncreaseBrushSize()
    {
        _BrushSize += 2;

        if (_placementMode == PlacementMode.Add)
        {
            //delete currently selected blocks
            DetachCurrentSelectedBlocks();

            //add new selected blocks
            SetSelectedBlockID(_selectedID);
        }
        else if(_placementMode == PlacementMode.Delete)
        {
            DetachCurrentSelectedBlocks();
            AttachDeletionBlocks();
        }

    }
    private void DecreaseBrushSize()
    {
        //delete currently selected blocks
        

        if (_placementMode == PlacementMode.Add)
        {
            if (_BrushSize >= 3)
            {
                //decrease brush size
                _BrushSize -= 2;
                //remove currently attached blocks
                DetachCurrentSelectedBlocks();
                //add new selected blocks
                SetSelectedBlockID(_selectedID);
            }
        }
        else if(_placementMode == PlacementMode.Delete)
        {
            if (_BrushSize >= 3)
            {
                //decrease brush size
                _BrushSize -= 2;
                //remove currently attached blocks
                DetachCurrentSelectedBlocks();
                //add new deletion blocks
                AttachDeletionBlocks();
            }  
        }
    }

    private void AttachDeletionBlocks()
    {
        for (int x = 0; x < _BrushSize; ++x)
        {
            for (int y = 0; y < _BrushSize; ++y)
            {
                var obj = Instantiate(_deletionBlock, _editorMouse.transform.position, Quaternion.identity);
                obj.transform.parent = _editorMouse;
                obj.transform.localPosition = new Vector3(0 - (_BrushSize / 2) + x, 0 - (_BrushSize / 2) + y, 0);
                currentSelected.Add(obj);
            }
        }
    }


    private void RotateSelectedBlocks()
    {
        foreach(var block in currentSelected)
        {
            block.transform.Rotate(new Vector3(0, 0, 90.0f));
        }
    }
    public void SetIsHoveringOverButton(bool b)
    {
        _IsHoveringOverButton = b;
    }

    public Sprite GetSpriteFromIndex(int index)
    {
        SpriteRenderer renderer;
        Sprite sprite;

        renderer = (_placeableObjects[index].GetComponent<SpriteRenderer>());

        if (renderer != null) sprite = renderer.sprite;
        else
        {
            sprite = _placeableObjects[index].GetComponentInChildren<SpriteRenderer>().sprite;
        }

        return sprite;
       
    }

    public void ClearLevel()
    {
        foreach(Transform child in _levelParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void SaveFile()
    {
        string path;

        if (_filenameSave.text == "" || _filenameSave.text == null)
        {
            return;
        }
        else
        {
            path = "Levels/" + _filenameSave.text + ".bin";
        }

        EditorUIManager.Instance.LastEditedFile = _filenameSave.text;

        _mapSaver.SaveLevel(path);
    }

    public void SaveCurrentOpenedFile()
    {
        string filename = EditorUIManager.Instance.LastEditedFile;
        string path;
        if (filename == "" || filename == null) return;
        else
        {
            path = "Levels/" + filename + ".bin";
        }

        _mapSaver.SaveLevel(path);
    }

    public void LoadFile()
    {
        //first clear all current blocks
        ClearLevel();

        //get filename
        string path;

        int index = _filenameDropdown.value;
        string name = _filenameDropdown.options[index].text;

        path = "Levels/" + name + ".bin";

        EditorUIManager.Instance.LastEditedFile = name;

        _mapLoader.LoadLevel(path, _levelParent.transform);
    }

    
}

