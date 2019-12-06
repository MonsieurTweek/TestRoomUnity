using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationGridController : MonoBehaviour
{
    private const int CUSTOMIZATION_LAYER = 8;
    private static CustomizationGridController _instance = null;
    public static CustomizationGridController instance { get { return _instance; } set { _instance = value; } }

    public CustomizationGridController()
    {
        if (CustomizationGridController.instance == null)
        {
            CustomizationGridController.instance = this;
        }
        else
        {
            throw new System.Exception("One instance of CustomizationGridController already exists on game object " + this.gameObject);
        }
    }

    public event Action<uint> onTileSelected;

    private ScrollRect _scrollRect = null;

    [SerializeField]
    private CustomizationTileController _tilePrefab = null;
    [SerializeField]
    private Camera _customizationCamera = null;

    private List<CustomizationTileController> _tilePool = new List<CustomizationTileController>();

    private RenderTexture _renderTexture;

    private void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();
        _customizationCamera.enabled = false;
    }

    public static void RefreshGrid(GameObject[] parts, Vector3 cameraOffset)
    {
        instance._customizationCamera.transform.position = CustomizationPartController.currentBone.transform.position + cameraOffset;

        int count = 0;

        for (int i = 0; i < instance._tilePool.Count; ++i)
        {
            bool isActive = i < parts.Length;

            instance._tilePool[i].gameObject.SetActive(isActive);

            if (isActive == true)
            {
                instance.RefreshTile(instance._tilePool[i], parts[i]);
                count++;
            }
        }

        for (int j = 0; j < parts.Length - count; ++j)
        {
            CustomizationTileController tile = Instantiate<CustomizationTileController>(instance._tilePrefab, instance._scrollRect.content);

            tile.button.onClick.AddListener(delegate { instance.SelectTile(tile.index); });

            instance._tilePool.Add(tile);

            instance.RefreshTile(tile, parts[j]);
        }

        instance._scrollRect.Rebuild(CanvasUpdate.Layout);
    }

    private void RefreshTile(CustomizationTileController tile, GameObject part)
    {
        bool wasActive = part.activeSelf; 

        part.SetActive(true);
        _customizationCamera.enabled = true;

        part.layer = CUSTOMIZATION_LAYER;

        _customizationCamera.targetTexture = RenderTexture.GetTemporary(128, 128, 16);

        _customizationCamera.Render();

        RenderTexture saveActive = RenderTexture.active;
        RenderTexture.active = _customizationCamera.targetTexture;
        int width = _customizationCamera.targetTexture.width;
        int height = _customizationCamera.targetTexture.height;

        Texture2D texture = new Texture2D(width, height);
        texture.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);

        texture.Apply();

        RenderTexture.active = saveActive;
        RenderTexture.ReleaseTemporary(_customizationCamera.targetTexture);

        tile.icon.texture = texture;
        tile.background.color = Color.white;

        part.layer = 0;

        _customizationCamera.enabled = false;
        part.SetActive(wasActive);
    }

    private void SelectTile(uint index)
    {
        onTileSelected(index);

        CustomizationPartController.currentBone.SetPart(index);
    }
}