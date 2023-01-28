using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ChooseSkin : MonoBehaviour
{
    public GameObject skins;
    public GameObject charactersList;
    [SerializeField] int choosenId;

    string changeDirection;
    [SerializeField] List<GameObject> allCharacters = new List<GameObject>();
    [SerializeField] Skin[] skinsClassList;

    public GameObject rightButton, leftButton;

    // Start is called before the first frame update
    void Start()
    {
        skinsClassList = skins.GetComponent<Skins>().skins;
        choosenId = PlayerPrefs.GetInt("characterId");

        for (int i = 0; i < skinsClassList.Length; i++)
        {
                GameObject skin;
                skin = Instantiate(skinsClassList[i].module, charactersList.transform.position, charactersList.transform.rotation);
                skin.transform.localScale = new Vector3(45, 45, 45);
                Animator anim= skin.AddComponent(typeof(Animator)) as Animator;
                anim.runtimeAnimatorController = skinsClassList[i].anim;
                skin.name = i + "Skin";
                skin.transform.parent = charactersList.gameObject.transform;
                allCharacters.Add(skin);
                if (i != choosenId)
                { allCharacters[i].SetActive(false); }
            
        }
        ChangeCharacter(choosenId);
    }

    private void ChangeCharacter(int id)
    {
        allCharacters[PlayerPrefs.GetInt("characterId")].SetActive(false);
        allCharacters[id].SetActive(true);
        PlayerPrefs.SetInt("characterId", id);

        if (id == skinsClassList.Length - 1){ rightButton.SetActive(false); leftButton.SetActive(true); }
        else if (id == 0){ leftButton.SetActive(false); rightButton.SetActive(true); }
        else { leftButton.SetActive(false); rightButton.SetActive(true); }
        
    }

    public void ChangeId(string target)
    {
            changeDirection = target;    
    }

    private void Update()
    {

            if (changeDirection == "Right")
            {
                changeDirection = "";
                choosenId++;
                ChangeCharacter(choosenId);
            }
            if (changeDirection == "Left")
            {
                changeDirection = "";
                choosenId--;
                ChangeCharacter(choosenId);
            }
       charactersList.transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
    }
}
