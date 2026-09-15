using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

public class MeteorController : MonoBehaviour
{
    //Task A
    public Camera mainCamera;
    public GameObject[] meteorPrefabs;
    public List<GameObject> meteorList = new List<GameObject>();

    //Task B
    public float progress;
    public float duration;
    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        //Task A
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            SpawnMeteorOnScreen();
        }

        //Task B
        progress += Time.deltaTime;
        if (progress > duration)
        {
            progress = 0f;

            if (meteorList.Count > 0)
            {
                DestroyMeteorOnScreen();
            }
        }
    }
    public void SpawnMeteorOnScreen()
    {
        int randomNumber = Random.Range(0, meteorPrefabs.Length);

        Vector2 randomPointInViewport = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
        Vector2 randomPosition = mainCamera.ViewportToWorldPoint(randomPointInViewport);
        
        GameObject spawnedObj = Instantiate(meteorPrefabs[randomNumber], randomPosition, Quaternion.identity);
        meteorList.Add(spawnedObj);
    }
    public void DestroyMeteorOnScreen()
    {
        int randomNumber = Random.Range(0, meteorList.Count);

        Destroy(meteorList[randomNumber]);
        meteorList.RemoveAt(randomNumber);
    }
}
