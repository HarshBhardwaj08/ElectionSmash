using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitVfxEffects : MonoBehaviour
{
    
    
    [SerializeField] GameObject headhittransform, chestHitTransform, leghitTransform;
    public void HeadHitEffect(GameObject hitobject)
    {
        Instantiate(hitobject, headhittransform.transform.position, Quaternion.identity);
    }
    public void ChestHitEffect(GameObject hitobject)
    {
        Instantiate(hitobject, chestHitTransform.transform.position, Quaternion.identity);
    }
    public void LegHitEffect(GameObject hitobject)
    {
        Instantiate(hitobject, leghitTransform.transform.position, Quaternion.identity);
    }
}
