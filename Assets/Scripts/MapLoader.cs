﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    [SerializeField] private GameObject _block;
    [SerializeField] private GameObject _beginFlag;
    [SerializeField] private GameObject _finishFlag;
    [SerializeField] private GameObject _grassBlock;
    [SerializeField] private GameObject _sawtrap;
    [SerializeField] private GameObject _torch;
    [SerializeField] private GameObject _platform;
    [SerializeField] private GameObject _spikes;
    [SerializeField] private GameObject _sawshooter;

   
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

            float angle = reader.ReadSingle();

            //cast entity id to entityID
            EntityID eid = (EntityID)id;

            //spawn right block depending on entity ID
            switch (eid)
            {
                case EntityID.Block:
                    var block = Instantiate(_block, pos, Quaternion.Euler(new Vector3(0,0,angle)));
                    block.transform.parent = parentObj;

                    int blockID = reader.ReadInt32();

                    //get tile comp and set id
                    var scr = block.GetComponent<WorldTileScript>();
                    if(scr)
                    {
                        scr.ID = blockID;
                    }

                    break;
                case EntityID.Beginning:
                    var begin = Instantiate(_beginFlag, pos, Quaternion.Euler(new Vector3(0, 0, angle)));
                    begin.transform.parent = parentObj;
                    break;
                case EntityID.Finish:
                    var finish = Instantiate(_finishFlag, pos, Quaternion.Euler(new Vector3(0, 0, angle)));
                    finish.transform.parent = parentObj;
                    break;
                case EntityID.Grass:
                    var grass = Instantiate(_grassBlock, pos, Quaternion.Euler(new Vector3(0, 0, angle)));
                    grass.transform.parent = parentObj;
                    break;
                case EntityID.Sawtrap:
                    var sawtrap = Instantiate(_sawtrap, pos, Quaternion.Euler(new Vector3(0, 0, angle)));
                    sawtrap.transform.parent = parentObj;
                    break;
                case EntityID.Torch:
                    var torch = Instantiate(_torch, pos, Quaternion.Euler(new Vector3(0, 0, angle)));
                    torch.transform.parent = parentObj;
                    break;
                case EntityID.Platform:
                    var platform = Instantiate(_platform, pos, Quaternion.Euler(new Vector3(0, 0, angle)));
                    platform.transform.parent = parentObj;
                    break;
                case EntityID.Spikes:
                    var spikes = Instantiate(_spikes, pos, Quaternion.Euler(new Vector3(0, 0, angle)));
                    spikes.transform.parent = parentObj;
                    break;
                case EntityID.SawShooter:
                    var shooter = Instantiate(_sawshooter, pos, Quaternion.Euler(new Vector3(0, 0, angle)));
                    shooter.transform.parent = parentObj;
                    break;
            }

        }

        reader.Close();
    }

}
