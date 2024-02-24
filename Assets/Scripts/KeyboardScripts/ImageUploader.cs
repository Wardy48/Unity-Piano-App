/* using UnityEngine;
using System.IO;
using UnityEngine.UI;
using SFB; // Namespace for StandaloneFileBrowser

public class ImageLoader : MonoBehaviour
{
    [SerializeField] Image displayImage;

    public void LoadImage()
    {
        // Open file
        var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false);
        if (paths.Length > 0)
        {
            // Read the bytes from the file
            byte[] imageData = File.ReadAllBytes(paths[0]);

            // Create a texture
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imageData);

            // Create a new sprite
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            // Assign the sprite to the Image component
            displayImage.sprite = sprite;
        }
    }
} */