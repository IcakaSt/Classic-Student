using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public int size = 20;
    public GameObject skins;
    Skin[] skinsClassList;

    // Start is called before the first frame update
    public void SpawnPlayer()
    {
        skinsClassList = skins.GetComponent<Skins>().skins;

        GameObject skin;
        skin = Instantiate(skinsClassList[PlayerPrefs.GetInt("characterId")].module, this.transform.position, this.gameObject.transform.rotation);
        skin.transform.localScale = new Vector3(size, size, size);
        Animator anim = skin.AddComponent(typeof(Animator)) as Animator;
        anim.runtimeAnimatorController = skinsClassList[PlayerPrefs.GetInt("characterId")].anim;
        skin.name ="Skin";
        skin.transform.parent = this.gameObject.gameObject.transform;
    }
}
