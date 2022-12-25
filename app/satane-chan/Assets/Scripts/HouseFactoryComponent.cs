using Assets.Scripts.Managers.Parameters;
using UnityEngine;

public class HouseFactoryComponent : MonoBehaviour
{
    /// <summary>
    /// �p�����[�^�Ǘ�
    /// </summary>
    public ParameterManagerComponent parameterManager;
    /// <summary>
    /// �Ƃ̈ꗗ�i�[�p�I�u�W�F�N�g
    /// </summary>
    public GameObject[] HousesGameObject;

    /// <summary>
    /// �Ƃ̃v���n�u
    /// </summary>
    public GameObject HousePrefab;

    private int count = 0;

    // Update is called once per frame
    void Update()
    {
        if( count % parameterManager.house.occurInterval == 0)
        {
            float randomValue = Random.Range(0f, 1f);
            if (randomValue < parameterManager.house.frequencyOfAppearance[0])
            {
                CreateHouse(0);
            }
            else if (randomValue < parameterManager.house.frequencyOfAppearance[1])
            {
                CreateHouse(1);
            }
            else if (randomValue < parameterManager.house.frequencyOfAppearance[2])
            {
                CreateHouse(2);
            }
            else
            {
                CreateHouse(3);
            }
            count = 0;
        }
        count++;
    }

    private void CreateHouse(int pos)
    {
        GameObject obj = Instantiate(HousePrefab, Vector3.zero, Quaternion.identity);
        obj.transform.parent = HousesGameObject[pos].transform;
        HouseComponent house = obj.GetComponent<HouseComponent>();
        house.z = 9f - pos * 0.1f;
        house.parameterManager = parameterManager;
    }
}
