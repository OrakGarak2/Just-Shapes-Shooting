using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cooking : MonoBehaviour
{
    [SerializeField] Transform foods;
    [SerializeField] List<Transform> foodList = new List<Transform>();

    [SerializeField] int selectedFoodsIndex = 0;

    private void Start()
    {
        int childCount;
        if (foods.childCount != 0)
        {
            childCount = foods.childCount;

            for (int i = 0; i < childCount; i++)
            {
                foodList.Add(foods.GetChild(i));
            }
        }
    }

    void FoodSpawn()
    {
        List<Transform> selectedFoods = foodList.ToList();

        for (int i = 0; i < 2; i++)
        {
            selectedFoods.RemoveAt(selectedFoodsIndex + i);
        }

        selectedFoodsIndex = selectedFoodsIndex + 2 >= selectedFoods.Count ? 0 : selectedFoodsIndex + 1;

        Vector2 vec2 = GameManager.Instance.trPlayer.position;
        vec2.y = -6f;
        vec2.x -= 1f;

        for (int i = 0; i < selectedFoods.Count; i++)
        {
            vec2.x += i;
            selectedFoods[i].GetComponent<Food>().endPoint = vec2;
            selectedFoods[i].gameObject.SetActive(true);
        }
    }
}
