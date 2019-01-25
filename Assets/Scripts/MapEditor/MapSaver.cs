﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public enum EntityID
{
    Block = 1,
    Beginning = 2,
    Finish = 3,
    Grass = 4,
    Sawtrap = 5,
    Torch = 6,
    Platform = 7
}

public class MapSaver : MonoBehaviour
{
    //1. get all blocks
    //2. get begin and finish
    //3. later -> get traps and enemies

    public void SaveLevel(string path)
    {
        Debug.Log(path);

        //get all objects with block tag
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        Debug.Log(blocks.Length);

        //get grass blocks
        GameObject[] grassBlocks = GameObject.FindGameObjectsWithTag("Grass");

        //get saw traps
        GameObject[] sawtraps = GameObject.FindGameObjectsWithTag("Sawblade");

        //get torhces
        GameObject[] torches = GameObject.FindGameObjectsWithTag("Torch");

        //get platforms
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");

        //get beginning
        GameObject[] beginnings = GameObject.FindGameObjectsWithTag("LevelBegin");

        //get finish
        GameObject[] finished = GameObject.FindGameObjectsWithTag("Finish");

        //open binary writer
        BinaryWriter writer = new BinaryWriter(File.Create(path));

        //writer amount of entities to writer
        int totAmount = blocks.Length + beginnings.Length + finished.Length + grassBlocks.Length + sawtraps.Length + torches.Length + platforms.Length;
        writer.Write(totAmount);

        //write all blocks to file
        for(int i = 0; i < blocks.Length; ++i)
        {
            GameObject block = blocks[i];
            Vector3 pos = block.transform.position;
            writer.Write((int) EntityID.Block);
            writer.Write(pos.x);
            writer.Write(pos.y);
            writer.Write(pos.z);
        }

        //write all beginnings to file
        for(int i = 0; i < beginnings.Length; ++i)
        {
            GameObject beginning = beginnings[i];
            Vector3 pos = beginning.transform.position;
            writer.Write((int)EntityID.Beginning);
            writer.Write(pos.x);
            writer.Write(pos.y);
            writer.Write(pos.z);
        }

        //write all finishes to file
        for(int i = 0; i < finished.Length; ++i)
        {
            GameObject finish = finished[i];
            Vector3 pos = finish.transform.position;
            writer.Write((int)EntityID.Finish);
            writer.Write(pos.x);
            writer.Write(pos.y);
            writer.Write(pos.z);
        }
        
        //write all grass blocks to file
        for(int i = 0; i < grassBlocks.Length; ++i)
        {
            GameObject grass = grassBlocks[i];
            Vector3 pos = grass.transform.position;
            writer.Write((int)EntityID.Grass);
            writer.Write(pos.x);
            writer.Write(pos.y);
            writer.Write(pos.z);
        }

        //write all sawtraps to file
        for (int i = 0; i < sawtraps.Length; ++i)
        {
            GameObject saw = sawtraps[i];
            Vector3 pos = saw.transform.position;
            writer.Write((int)EntityID.Sawtrap);
            writer.Write(pos.x);
            writer.Write(pos.y);
            writer.Write(pos.z);
        }

        //write all torches to file
        for (int i = 0; i < torches.Length; ++i)
        {
            GameObject torch = torches[i];
            Vector3 pos = torch.transform.position;
            writer.Write((int)EntityID.Torch);
            writer.Write(pos.x);
            writer.Write(pos.y);
            writer.Write(pos.z);
        }
       
        //write all platforms to file
        for (int i = 0; i < platforms.Length; ++i)
        {
            GameObject platform = platforms[i];
            Vector3 pos = platform.transform.position;
            writer.Write((int)EntityID.Platform);
            writer.Write(pos.x);
            writer.Write(pos.y);
            writer.Write(pos.z);
        }

        writer.Close();
    }
}
