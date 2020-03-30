using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationGridController : MonoBehaviour
{
    private const int CUSTOMIZATION_LAYER = 9;
    
    private static CustomizationGridController _instance = null;
    public static CustomizationGridController instance { get { return _instance; } set { _instance = value; } }

    public event Action<uint> onTileSelected;
    public event Action onGridRefreshing;
    public event Action onGridRefreshed;

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

        if (CustomizationGridController.instance == null)
        {
            CustomizationGridController.instance = this;
        }
        else
        {
            throw new System.Exception("One instance of CustomizationGridController already exists on game object " + this.gameObject);
        }
    }

    public static void RefreshGrid(GameObject[] skins, Vector3 cameraOffset)
    {
        instance.onGridRefreshing();

        instance._customizationCamera.transform.position = CustomizationPartController.currentPart.bone.transform.position + cameraOffset;

        int count = 0;

        for (int i = 0; i < instance._tilePool.Count; ++i)
        {
            bool isActive = i < skins.Length;

            instance._tilePool[i].gameObject.SetActive(isActive);

            if (isActive == true)
            {
                instance.RefreshTile(instance._tilePool[i], skins[i]);
                count++;
            }
        }

        for (int i = 0; i < skins.Length - count; ++i)
        {
            CustomizationTileController tile = Instantiate<CustomizationTileController>(instance._tilePrefab, instance._scrollRect.content);

            tile.button.onClick.AddListener(delegate { instance.SelectTile(tile.index); });

            instance._tilePool.Add(tile);

            instance.RefreshTile(tile, skins[i]);
        }

        for (int i = 0; i < skins.Length; ++i)
        {
            if (skins[i].activeSelf == true)
            {
                instance.SelectTile((uint) i);
                break;
            }
        }

        instance._scrollRect.Rebuild(CanvasUpdate.Layout);

        instance.onGridRefreshed();
    }

    private void RefreshTile(CustomizationTileController tile, GameObject part)
    {
        bool wasActive = part.activeSelf;
        int currentLayer = part.layer;

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

        part.layer = currentLayer;

        _customizationCamera.enabled = false;
        part.SetActive(wasActive);
    }

    private void SelectTile(uint index)
    {
        onTileSelected(index);

        CustomizationPartController.currentPart.SetPart(index);
    }
}