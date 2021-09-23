using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LevelManager : MonoBehaviour
{
    private ScriptedLevel level;
    private int levelIndex = 0;

    private AsyncOperationHandle levelOpHandle;
    private Dictionary<AssetReference, GameObject> loadedPrefabs = new Dictionary<AssetReference, GameObject>();
    private Dictionary<AssetReference, AsyncOperationHandle<GameObject>> handles = new Dictionary<AssetReference, AsyncOperationHandle<GameObject>>();


    public void LoadLevel(int _levelIndex)
    {
        levelIndex = _levelIndex;

        //Release the old handles/prefabs
        print("1");
        CleanUpAddressables();
        print("2");
        if (levelOpHandle.IsValid())
        {
            print("3");
            Addressables.Release(levelOpHandle);
            print("4");
        }

        //Load new level
        print("Assets/Levels/Level_" + levelIndex + ".asset");
        levelOpHandle = Addressables.LoadAssetAsync<ScriptedLevel>("Assets/Levels/Level_" + _levelIndex + ".asset");
        levelOpHandle.Completed += handle =>
        {
            level = (ScriptedLevel)handle.Result;
            //Load new props
            for (int i = 0; i < level.propData.Length; i++)
            {
                LoadAddressable(level.propData[i].propAssetRef);
            }

            StartCoroutine(PybUtilityScene.LoadScene("GameScene"));
        };


    }

    private void LoadAddressable(AssetReference _assetRef)
    {
        var op = Addressables.LoadAssetAsync<GameObject>(_assetRef);
        handles.Add(_assetRef, op);
        op.Completed += handle =>
        {
            var prefab = handle.Result;
            loadedPrefabs.Add(_assetRef, prefab);
        };
    }

    private void CleanUpAddressables()
    {
        if (loadedPrefabs != null)
        {
            foreach (KeyValuePair<AssetReference, GameObject> entry in loadedPrefabs)
            {
                // do something with entry.Value or entry.Key
                Addressables.ReleaseInstance(entry.Value.gameObject);
            }
        }
        if (handles != null)
        {
            foreach (KeyValuePair<AssetReference, AsyncOperationHandle<GameObject>> entry in handles)
            {
                // do something with entry.Value or entry.Key
                if (entry.Key.IsValid())
                {
                    Addressables.Release(entry.Value);
                }
            }
        }
        
    }

    public GameObject GetLoadedPrefab(AssetReference _assetRef)
    {
        return GameObject.Instantiate(loadedPrefabs[_assetRef]);
    }


    public void LoadNextLevel()
    {
        levelIndex += 1;
        LoadLevel(levelIndex);
    }

    public ScriptedLevel GetLevel()
    {
        return this.level;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

}
