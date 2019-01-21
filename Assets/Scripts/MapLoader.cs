using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapLoader : MonoBehaviour
{

    [SerializeField] private GameObject _block;
    [SerializeField] private GameObject _beginFlag;
    [SerializeField] private GameObject _finishFlag;

    private GameObject _levelParent;

    private void Start()
    {
        _levelParent = new GameObject("Level");

        //open file
        BinaryReader reader = new BinaryReader(File.OpenRead("Levels/levelVideo.bin"));

        //get amount of entities in level
        int amountOfEntities = reader.ReadInt32();

        //loop through every entity
        for (int i = 0; i < amountOfEntities; ++i)
        {
            //get the entity id
            int id = reader.ReadInt32();
            //get the entity position
            float x = reader.ReadSingle();
            float y = reader.ReadSingle();
            float z = reader.ReadSingle();
            Vector3 pos = new Vector3(x, y, z);

            //cast entity id to entityID
            EntityID eid = (EntityID)id;
            //spawn right block depending on entity ID
            switch (eid)
            {
                case EntityID.Block:
                    var block = Instantiate(_block, pos, Quaternion.identity);
                    block.transform.parent = _levelParent.transform;
                    break;
                case EntityID.Beginning:
                    var begin = Instantiate(_beginFlag, pos, Quaternion.identity);
                    begin.transform.parent = _levelParent.transform;
                    break;
                case EntityID.Finish:
                    var finish = Instantiate(_finishFlag, pos, Quaternion.identity);
                    finish.transform.parent = _levelParent.transform;
                    break;
            }



        }

        //for testing
        string[] files = Directory.GetFiles("Levels");
        for (int i = 0; i < files.Length; ++i)
        {
            Debug.Log(files[i]);
        }

    }
}
