using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCandy : MonoBehaviour {
    [SerializeField] public Sprite[] sprites;
    public string type;
    public Gem GetGem { get; private set; }
    public bool isBonus;
    public int BonusMatchType;
    public bool isSwirl=false;
    public int seconds = 0;
    [SerializeField]
    private TextMesh textMesh;
    [SerializeField] 
    private int intType = 0;
    public int ToIntType()
    {
        return intType;
    }
    public void setBlockAnimation()
    {
        GetComponent<Animator>().SetBool("block", false);
    }
    public void isGem(Gem g)
    {
        GetGem = g;
    }
    public bool isequal(HitCandy hitCandy)
    {
        return hitCandy != null && hitCandy.type == type && hitCandy.type != "ingredient" + 0 && hitCandy.type != "ingredient" + 1;
    }
    private void Update()
    {
        if (isSwirl == false)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[GetGem.level];
        }
    }
}
