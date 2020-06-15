using UnityEngine;

public class HomeCanvasController : MonoBehaviour
{
    public float animationTime = 0.5f;
    public float animationDelay = 0.5f;

    private TileController[] _tiles = null;

    private void Awake()
    {
        _tiles = GetComponentsInChildren<TileController>();
    }

    private void Start()
    {
        gameObject.SetActive(false);

        for (int i = 0; i < _tiles.Length; i++)
        {
            _tiles[i].transform.localScale = Vector3.zero;

            LeanTween.scale(_tiles[i].gameObject, Vector3.one, Random.Range(0f, animationTime)).setDelay(Random.Range(0f, animationDelay));
        }

        gameObject.SetActive(true);
    }
}
