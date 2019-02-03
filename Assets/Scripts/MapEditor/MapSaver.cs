using System.Collections;
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
    Platform = 7,
    Spikes = 8,
    SawShooter = 9,
    Slime = 10,
    DarkBackground = 11,
    WeakBlock = 12
}

public class MapSaver : MonoBehaviour
{
    //1. get all blocks
    //2. get begin and finish
    //3. later -> get traps and enemies

    public void SaveLevel(string path)
    {
        //get all objects with block tag
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

        //get grass blocks
        GameObject[] grassBlocks = GameObject.FindGameObjectsWithTag("Grass");

        //get saw traps
        GameObject[] sawtraps = GameObject.FindGameObjectsWithTag("Sawblade");

        //get spikes
        GameObject[] spikes = GameObject.FindGameObjectsWithTag("Spikes");

        //get saw shooters
        GameObject[] sawshooters = GameObject.FindGameObjectsWithTag("SawShooter");

        //get torhces
        GameObject[] torches = GameObject.FindGameObjectsWithTag("Torch");

        //get platforms
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");

        //get beginning
        GameObject[] beginnings = GameObject.FindGameObjectsWithTag("LevelBegin");

        //get finish
        GameObject[] finished = GameObject.FindGameObjectsWithTag("Finish");

        //get slimes
        GameObject[] slimes = GameObject.FindGameObjectsWithTag("Slime");

        //dark background
        GameObject[] backgrounds = GameObject.FindGameObjectsWithTag("DarkBackground");

        //weak blocks
        GameObject[] weakBlocks = GameObject.FindGameObjectsWithTag("WeakBlock");

        //open binary writer
        BinaryWriter writer = new BinaryWriter(File.Create(path));

        //writer amount of entities to writer
        int totAmount = blocks.Length + beginnings.Length + finished.Length + grassBlocks.Length + sawtraps.Length 
            + torches.Length + platforms.Length + spikes.Length + sawshooters.Length + slimes.Length + backgrounds.Length
            + weakBlocks.Length;

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

            float rotZ = block.transform.eulerAngles.z;
            writer.Write(rotZ);

            var tile = block.GetComponent<AutoTilingScript>();
            int id = (int)tile.ID;
            writer.Write(id);
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

            float rotZ = beginning.transform.eulerAngles.z;
            writer.Write(rotZ);
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

            float rotZ = finish.transform.eulerAngles.z;
            writer.Write(rotZ);
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

            float rotZ = grass.transform.eulerAngles.z;
            writer.Write(rotZ);
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

            float rotZ = saw.transform.eulerAngles.z;
            writer.Write(rotZ);
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

            float rotZ = torch.transform.eulerAngles.z;
            writer.Write(rotZ);
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

            float rotZ = platform.transform.eulerAngles.z;
            writer.Write(rotZ);
        }

        //write all spikes to file
        for(int i = 0; i < spikes.Length; ++i)
        {
            GameObject spike = spikes[i];
            Vector3 pos = spike.transform.position;
            writer.Write((int)EntityID.Spikes);
            writer.Write(pos.x);
            writer.Write(pos.y);
            writer.Write(pos.z);

            float rotZ = spike.transform.eulerAngles.z;
            writer.Write(rotZ);
        }

        //write all sawshooters to file
        for (int i = 0; i < sawshooters.Length; ++i)
        {
            GameObject sawshooter = sawshooters[i];
            Vector3 pos = sawshooter.transform.position;
            writer.Write((int)EntityID.SawShooter);
            writer.Write(pos.x);
            writer.Write(pos.y);
            writer.Write(pos.z);

            float rotZ = sawshooter.transform.eulerAngles.z;
            writer.Write(rotZ);
        }

        //add slimes
        for (int i = 0; i < slimes.Length; ++i)
        {
            GameObject slime = slimes[i];
            Vector3 pos = slime.transform.position;
            writer.Write((int)EntityID.Slime);
            writer.Write(pos.x);
            writer.Write(pos.y);
            writer.Write(pos.z);

            float rotZ = slime.transform.eulerAngles.z;
            writer.Write(rotZ);
        }
        //add dark backgrounds
        for (int i = 0; i < backgrounds.Length; ++i)
        {
            GameObject background = backgrounds[i];
            Vector3 pos = background.transform.position;
            writer.Write((int)EntityID.DarkBackground);
            writer.Write(pos.x);
            writer.Write(pos.y);
            writer.Write(pos.z);

            float rotZ = background.transform.eulerAngles.z;
            writer.Write(rotZ);
        }

        //add weak blocks
        for (int i = 0; i < weakBlocks.Length; ++i)
        {
            GameObject weakBlock = weakBlocks[i];
            Vector3 pos = weakBlock.transform.position;
            writer.Write((int)EntityID.WeakBlock);
            writer.Write(pos.x);
            writer.Write(pos.y);
            writer.Write(pos.z);

            float rotZ = weakBlock.transform.eulerAngles.z;
            writer.Write(rotZ);
        }
        writer.Close();
    }
}
