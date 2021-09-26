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
    private Dictionary<string, AssetReference> propAssetReferences = new Dictionary<string, AssetReference>();

    public void LoadLevel(int _levelIndex)
    {
        levelIndex = _levelIndex;

        //Release the old handles/prefabs
        CleanUpAddressables();
        propAssetReferences.Clear();
        if (levelOpHandle.IsValid())
        {
            Addressables.Release(levelOpHandle);
        }

        //Load new level
        levelOpHandle = Addressables.LoadAssetAsync<ScriptedLevel>("Assets/Levels/Level_" + _levelIndex + ".asset");
        levelOpHandle.Completed += handle =>
        {
            level = (ScriptedLevel)handle.Result;
            //Load new props
            for (int i = 0; i < level.propData.Length; i++)
            {
                LoadAddressable(level.propData[i].assetPath);
            }

            StartCoroutine(PybUtilityScene.LoadScene("GameScene", 3f));
        };


    }

    private void LoadAddressable(string _assetPath)
    {
        AssetReference currentRef = new AssetReference(_assetPath);
        propAssetReferences.Add(_assetPath, currentRef);
        var op = Addressables.LoadAssetAsync<GameObject>(currentRef);
        handles.Add(currentRef, op);
        op.Completed += handle =>
        {
            var prefab = handle.Result;
            loadedPrefabs.Add(currentRef, prefab);
        };
    }

    private void CleanUpAddressables()
    {
        if (loadedPrefabs != null)
        {
            foreach (KeyValuePair<AssetReference, GameObject> entry in loadedPrefabs)
            {
                Addressables.ReleaseInstance(entry.Value.gameObject);
            }
        }
        if (handles != null)
        {
            foreach (KeyValuePair<AssetReference, AsyncOperationHandle<GameObject>> entry in handles)
            {
                if (entry.Key.IsValid())
                {
                    Addressables.Release(entry.Value);
                }
            }
        }
        
    }

    public GameObject GetLoadedPrefab(string _assetPath)
    {

        AssetReference assetReference = propAssetReferences[_assetPath];
        return GameObject.Instantiate(loadedPrefabs[assetReference]);
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
