using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CloudManager
{
    private const string PACKAGE_NAME = "player";
    private const string URL = "https://caf3f374-8590-4518-851a-a4472a007def.client-api.unity3dusercontent.com/client_api/v1/environments/server/buckets/0ecf1d6b-b540-404f-9607-d731abb983ce/release_by_badge/latest/entry_by_path/content/?path=";

    public async Task<GameObject> GetPrefab(string prefabName)
    {
        GameObject prefab = null;
        if (prefab == null)
        {
            prefab = CheckPrefabToDowload(prefabName);
        }
        if (prefab == null)
        {
            prefab = await DowloadPrefab(prefabName, PACKAGE_NAME);
        }
        if (prefab == null)
        {
            Debug.Log("Prefab not found");
        }
        return prefab;
    }

    public async Task<Texture> GetTexture(string textureName)
    {
        Texture texture = null;
        if (texture == null)
        {
            texture = CheckTextureToDowload(textureName);
        }
        if (texture == null)
        {
            texture = await DowloadTexture(textureName);
        }
        if (texture == null)
        {
            Debug.Log("Texture not found");
        }
        return texture;
    }

    public void DeletePrefab(string prefabName)
    {
        string path = Application.persistentDataPath + "/" + prefabName;
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public void DeleteTexture(string textureName)
    {
        string path = Application.persistentDataPath + "/" + textureName + ".png";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    private GameObject CheckPrefabToDowload(string prefabName)
    {
        string path = Application.persistentDataPath + "/" + prefabName;
        if (File.Exists(path))
        {
            AssetBundle bundle = AssetBundle.LoadFromFile(path);
            GameObject prefab = bundle.LoadAsset<GameObject>(prefabName);
            bundle.Unload(false);
            return prefab;
        }
        return null;
    }

    private async Task<GameObject> DowloadPrefab(string prefabName, string packageName)
    {
        string url = URL + packageName;

        using (UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            request.SendWebRequest();

            while (!request.isDone)
            {
                await Task.Yield();
            }

            if (request.result == UnityWebRequest.Result.Success)
            {
                UnityWebRequest www = UnityWebRequest.Get(url);

                www.SendWebRequest();

                while (!www.isDone)
                {
                    await Task.Yield();
                }

                AssetBundle assetBundle = DownloadHandlerAssetBundle.GetContent(request);
                GameObject[] prefabs = assetBundle.LoadAssetWithSubAssets<GameObject>(prefabName);

                if (prefabs.Length > 0)
                {
                    GameObject prefab = prefabs[0];

                    string path = Application.persistentDataPath + "/" + packageName;
                    File.WriteAllBytes(path, www.downloadHandler.data);

                    assetBundle.Unload(false);
                    return prefab;
                }
                return null;
            }
            else
            {
                Debug.Log("Failed to download AssetBundle: " + request.error);
                return null;
            }
        }
    }

    private Texture2D CheckTextureToDowload(string textureName)
    {
        string path = Application.persistentDataPath + "/" + textureName + ".png";
        if (File.Exists(path))
        {
            byte[] bytes = File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(bytes);
            return texture;
        }
        return null;
    }

    private async Task<Texture2D> DowloadTexture(string textureName)
    {
        AsyncOperationHandle<Texture2D> handle = Addressables.LoadAssetAsync<Texture2D>(textureName);
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            string path = Application.persistentDataPath + "/" + textureName + ".png";

            Texture2D texture = handle.Result;
            byte[] bytes = texture.EncodeToPNG();
            File.WriteAllBytes(path, bytes);

            Addressables.Release(handle);
            return texture;
        }
        else
        {
            Debug.Log("Failed to download texture: " + handle.OperationException);
            Addressables.Release(handle);
            return null;
        }
    }
}
