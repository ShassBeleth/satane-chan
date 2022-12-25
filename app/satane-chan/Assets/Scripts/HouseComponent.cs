using Assets.Scripts.Managers.Parameters;
using UnityEngine;

public class HouseComponent : MonoBehaviour
{
    /// <summary>
    /// ƒpƒ‰ƒ[ƒ^ŠÇ—
    /// </summary>
    public ParameterManagerComponent parameterManager;
    public float z;
    void Start()
    {
        this.transform.localPosition = new Vector3(parameterManager.house.startPositionX, 0f, -z );
        this.transform.localScale = Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.position;
        pos.x -= parameterManager.house.speed * z;
        this.transform.position = pos;

        if( pos.x < parameterManager.house.leftInvisibleWall)
        {
            Destroy(this.gameObject);
        }
    }
}
