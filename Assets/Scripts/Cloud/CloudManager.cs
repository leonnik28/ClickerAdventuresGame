using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CloudManager
{
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
            Debug.Log("Failed to download image: " + handle.OperationException);
            Addressables.Release(handle);
            return null;
        }
    }
}
