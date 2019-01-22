using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    [SerializeField] private GameObject _block;
    [SerializeField] private GameObject _beginFlag;
    [SerializeField] private GameObject _finishFlag;


    public void LoadLevel(string filename, Transform parentObj)
    {
        //open file
        BinaryReader reader = new BinaryReader(File.OpenRead(filename));

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
                    block.transform.parent = parentObj;
                    break;
                case EntityID.Beginning:
                    var begin = Instantiate(_beginFlag, pos, Quaternion.identity);
                    begin.transform.parent = parentObj;
                    break;
                case EntityID.Finish:
                    var finish = Instantiate(_finishFlag, pos, Quaternion.identity);
                    finish.transform.parent = parentObj;
                    break;
            }

        }

        reader.Close();
    }

}
