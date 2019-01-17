using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private GameObject currentSelected = null;

    private PlacementMode _placementMode = PlacementMode.Add;

    private float _placementCooldown = 0.01f;
    private bool _canPlaceBlocks = true;

    private bool _IsHoveringOverButton = false;

    private void Start()
    {
        var ui = EditorUIManager.Instance;

        for (int i = 0; i < _placeableObjects.Count; ++i)
        {
            ui.AddButton(i);
        }
    }


    private void Update()
    {
        //controls that lets user select which block to place or to delete blocks -> replace later with UI
        HandleBlockSelection();

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
            && currentSelected
            && !_IsHoveringOverButton
            && Input.GetMouseButton(0))
        {
            //start placement cooldown
            _canPlaceBlocks = false;
            StartCoroutine(PlacementCooldown());

            //remove overlapping block
            DeleteBlockAtMousePos();

            //create the new block
            var obj = Instantiate(currentSelected, _editorMouse.transform.position, Quaternion.identity);
            obj.GetComponent<Collider2D>().enabled = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
            DetachCurrentSelectedBlock();
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
            DeleteBlockAtMousePos();
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

    //replace soon
    void HandleBlockSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _placementMode = PlacementMode.Add;

            DetachCurrentSelectedBlock();

            currentSelected = Instantiate(_placeableObjects[0], Vector3.zero, Quaternion.identity);
            currentSelected.transform.parent = _editorMouse;
            currentSelected.transform.localPosition = Vector3.zero;
            currentSelected.GetComponent<Collider2D>().enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Destroy(currentSelected);
            _placementMode = PlacementMode.Delete;
        }
    }

    private void DetachCurrentSelectedBlock()
    {
        //remove current block to be placed
        Destroy(currentSelected);
    }

    //placement cooldown after X amount of seconds
    private IEnumerator PlacementCooldown()
    {
        yield return new WaitForSeconds(_placementCooldown);
        _canPlaceBlocks = true;
    }

    /// <summary>
    /// called by buttons when they're clicked
    /// </summary>
    /// <param name="id"></param>
    public void SetSelectedBlockID(int id)
    {
        //remove current selected block
        DetachCurrentSelectedBlock();
        //set placement mode to Add
        _placementMode = PlacementMode.Add;
        //spawn new block with current id
        currentSelected = Instantiate(_placeableObjects[id], _editorMouse.transform.position, Quaternion.identity);
        currentSelected.transform.parent = _editorMouse;
        currentSelected.transform.localPosition = Vector3.zero;
        currentSelected.GetComponent<Collider2D>().enabled = false;
    }


    public void SetIsHoveringOverButton(bool b)
    {
        _IsHoveringOverButton = b;
    }

    public Sprite GetSpriteFromIndex(int index)
    {
        return _placeableObjects[index].GetComponent<SpriteRenderer>().sprite;
    }
}

