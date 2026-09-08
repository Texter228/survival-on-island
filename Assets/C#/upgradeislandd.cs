using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeislandd : MonoBehaviour // Переименован класс для соответствия стандартам  
{
    private static upgradeislandd instance; // Изменено на instance  

    public GenerateIsland island;
    [SerializeField] private GameObject camera;
    public GameObject[] offObjects;
    public GameObject[] OnObjects;
    public int offsetx;
    public int offsety;
    public CameraController cam;

    public GameObject[] destroy;

    public static bool startupgrade1; // Статическая переменная для доступа в других классах  


    private void Start()
    {
        instance = this; // Создание ссылки на текущий экземпляр  
    }

    private void Update()
    {
        // Проверка на статическую переменную startupgrade  
        if (startupgrade1)
        {
            ActivateObjects();
            MoveCameraToIsland();
            startupgrade1 = false;
        }

        if (island.created)
        {
            cam.stop = false;
            for (int i = 0; i < destroy.Length; i++)
            {
                Destroy(destroy[i]);
            }
            for (int i = 0; i < OnObjects.Length; i++)
            {
                OnObjects[i].SetActive(true);
            }
        }

    }

    private void ActivateObjects()
    {
        if (island != null) // Проверка, чтобы избежать NullReferenceException  
        {
            island.isCreate = true;
        }
    }

    private void MoveCameraToIsland()
    {
        if (camera != null)
        {
            foreach (var obj in offObjects)
            {
                obj.SetActive(false);
            }
            cam.stop = true;
            // Плавное движение камеры к острову  
            StartCoroutine(MoveCameraCoroutine(cam));
        }
    }

    private IEnumerator MoveCameraCoroutine(CameraController cam)
    {
        Vector3 targetPosition = new Vector3(island.transform.position.x + offsetx, island.transform.position.y + offsety, cam.transform.position.z);
        float duration = 1.0f; // Время перемещения  
        float elapsed = 0f;

        Vector3 startingPos = cam.transform.position;

        while (elapsed < duration)
        {
            cam.transform.position = Vector3.Lerp(startingPos, targetPosition, (elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null; // Ожидание следующего кадра  
        }

        // Убедиться, что камера точно на целевой позиции  
        cam.transform.position = targetPosition;
    }
}