using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MagicShapeSpawn : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;

    [SerializeField] List<GameObject> magicShapeList = new List<GameObject>();

    public GameObject curMagicShape;

    void Start()
    {
        string str_curMagicShape = GameManager.Instance.curMagicShape.ToString();
        GameObject foundMagicShape = magicShapeList.Where(obj => obj.name.Contains(str_curMagicShape)).First();
        curMagicShape = Instantiate(foundMagicShape, spawnPoint.position, spawnPoint.rotation);
    }
}
