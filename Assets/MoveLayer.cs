using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveLayer : MonoBehaviour {
    //public static MoveLayer THIS;
    public string Name;
    public string urlOnTournament;
    [HideInInspector] public int movecount;
    [HideInInspector] public int limitMove;
    public UnityEngine.UI.Text GetTextMove;
    public int state = 0;
    Vector2[] visibleSize;
    private int SizeX;
    private int SizeY;
    public GameObject[] bug;
    [SerializeField] List<Vector2> ylist2 = new List<Vector2>();
    [SerializeField] Arrays GetArrays;
    [SerializeField] HitCandy[] GetCandyPrefab;
    [SerializeField] HitCandy SwirlCandy;
    [SerializeField] HitCandy[] GetCandieSecons;
    [SerializeField] OpenAppLevel levelsApps;
    [SerializeField] OpenAppLevel AppLevel;
    HitCandy GetHitGem;
    [SerializeField] Sprite sprite12;
    [SerializeField] HitCandy[] GetBonusPrefab;
    [SerializeField] HitCandy swirlPrefab;
    [SerializeField] Sprite[] GetIngredientSprite;
    [SerializeField]GameObject plus5Seconds;
    bool load;
    int Ending=0;
    public int End() {return Ending; }
    public bool loaldo() { return load; }
    public void loadMove(bool load1)
    {
        load = load1;
    }
    private List<Vector3> positions = new List<Vector3>();
    private List<Block> GetBlocks = new List<Block>();
    // Use this for initialization
    void Start()
    {
        
    }
    /// <summary>
    /// restart
    /// </summary>
    public void restarting()
    {
        SizeX = AppLevel.MaxX;
        SizeY = AppLevel.MaxY;
        Gems();
    }
    public void moves()
    {
        switch (state)
        {
            case 0:
                //if (levelsApps.modeLvl == 2) StartCoroutine(IngredientScale());
                if (Input.GetMouseButtonDown(0))
                {
                    var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero);
                    if (hit.collider != null)
                    {
                        GetHitGem = hit.collider.GetComponent<HitCandy>();
                        state = 1;
                        bug[0].transform.position = GetHitGem.transform.position;
                        bug[0].SetActive(true);
                        bug[0].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        if (GetHitGem.GetGem.level == 5)
                        {
                            levelsApps.printScores += 50;
                            GetBlocks.Add(levelsApps.blocksp[GetHitGem.GetGem.y * levelsApps.MaxX + GetHitGem.GetGem.x]);
                            Destroy(GetHitGem.gameObject);
                            state = 0;
                        }
                        if (GetHitGem.isSwirl&&GetBlocks.Count>0) 
                        { 
                            state = 0;
                            var pblck = GetBlocks[Random.Range(0, GetBlocks.Count)];
                            CreateGem(pblck.row, pblck.col, GetCandyPrefab[Random.Range(0, GetCandyPrefab.Length-1)]);
                            GetBlocks.Remove(pblck);
                        }
                        if ((uint)levelsApps.ltype == 1)
                        {
                            //levelsApps.GetPause();
                        }
                    }
                }
                break;
            case 1:
                if (Input.GetMouseButtonUp(0))
                {
                    var hits = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero);
                    if (hits.collider != null)//&& GetHitGem != hits.collider.gameObject)
                    {
                        HitCandy GetHitGem2 = hits.collider.gameObject.GetComponent<HitCandy>();
                        bug[0].SetActive(false);
                        state = 2;

                        if (
                            levelsApps.blocksp[GetHitGem.GetGem.y * levelsApps.MaxX + GetHitGem.GetGem.x].modelvlsquare == 3
                            )
                        {
                            state = 0;
                        }
                        else
                        {
                            print(state);
                            //if (movecount == 1) { Ending = 1; }
                            //movecount -= 1;
                            if (GetHitGem2 != null) { StartCoroutine(Match(hits));state = 0;  }
                            else { state = 0; }
                        }
                    }
                }
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        GetTextMove.text = string.Format("{0}", movecount);
        if (movecount != limitMove)
        {
            GetTextMove.gameObject.SetActive(true);
            moves();
        }
    }
    [SerializeField] List<List<Vector2>> xlist1 = new List<List<Vector2>>();
    public List<string> vslist;
    public Gem IngredientPosition(int ingr1,int ingr2)
    {
        int[] IngredientXPosition = { SizeX / 2, (SizeX / 2) + 1, SizeX / 2 - 1 };
        Gem[] genIngr0 = new Gem[ingr1];
        Gem[] gemIngr1 = new Gem[ingr2];
        int randpos;
        for (int i = 0; i < ingr1; i++)
        {
            randpos = IngredientXPosition[Random.Range(0, IngredientXPosition.Length)];
            if (IsNulls((SizeY - 1) - i, randpos) == true)
            {
                randpos = IngredientXPosition[Random.Range(0, IngredientXPosition.Length)];
            }
            genIngr0[i] = GetArrays.gems[(SizeY - 1) - i, randpos];
            genIngr0[i].hitGem.type = "ingredient" + 0;
            genIngr0[i].hitGem.GetComponent<SpriteRenderer>().sprite = GetIngredientSprite[0];
        }
        for (int i = 0; i < ingr2; i++)
        {
            randpos = IngredientXPosition[Random.Range(0, IngredientXPosition.Length)];
            if (GetArrays.gems[(SizeY / 2) - i, randpos].hitGem.type == "ingredient" + 0) randpos = IngredientXPosition[Random.Range(0, IngredientXPosition.Length)];
            if (IsNulls((SizeY / 2) - i, randpos) == true)
            {
                randpos = IngredientXPosition[Random.Range(0, IngredientXPosition.Length)];
                if (GetArrays.gems[(SizeY / 2) - i, randpos].hitGem.type == "ingredient" + 0) randpos = IngredientXPosition[Random.Range(0, IngredientXPosition.Length)];
            }
            gemIngr1[i] = GetArrays.gems[(SizeY / 2) - i, IngredientXPosition[Random.Range(0, IngredientXPosition.Length)]];
            gemIngr1[i].hitGem.type = "ingredient" + 1;
            gemIngr1[i].hitGem.GetComponent<SpriteRenderer>().sprite = GetIngredientSprite[1];
        }
        GetArrays.ingredientsGems = new HitCandy[genIngr0.Length];
        GetArrays.ingredientsGems2 = new HitCandy[gemIngr1.Length];//
        for (int k = 0; k < genIngr0.Length; k++) { GetArrays.ingredientsGems[k] = genIngr0[k].hitGem; }
        for (int k = 0; k < gemIngr1.Length; k++) { GetArrays.ingredientsGems2[k] = gemIngr1[k].hitGem; }
        //GetArrays.ingredientsGems2 = gemIngr1[0].hitGem;//
        return gemIngr1[0];
    }
    public void GetDestroyAlls()
    {
        GetBlocks.Clear();state = 0;
        for (int row = 0; row < GetArrays.SizeY; row++)
        {
            for (int col = 0; col < GetArrays.SizeX; col++)
            {
                if (GetArrays[row, col].INull == false)
                {
                    Destroy(obj: GetArrays[row, col].hitGem.gameObject);
                }
            }
        }
    }
    /// <summary>
    /// начало
    /// </summary>
    private void Gems()
    {
        float xc = levelsApps.blckWH();//
        float yr = levelsApps.blckWH();//
        for (int row = 0; row < GetArrays.SizeY; row++)
        {
            for (int col = 0; col < GetArrays.SizeX; col++)
            {
                if (GetArrays[row, col].INull == false)
                {
                    Destroy(obj: GetArrays[row, col].hitGem.gameObject);
                }
            }
        }
        for (int row = 0; row < SizeY; row++)
        {
            for (int col = 0; col < SizeX; col++)
            {
                GetArrays.gems[row, col].Nil();
            }
        }
        int g = SizeX;
        visibleSize = new Vector2[g];
        int iteration = 0;
        int candylimit=0;
        bool iswirl = false;
        int randcol = Random.Range(2, SizeX - 2);
        for (int row = 0; row < SizeY; row++)
        {
            for (int col = 0; col < SizeX; col++)
            {
                candylimit = GetCandyPrefab.Length-1;
                HitCandy hitCandy = GetCandyPrefab[Random.Range(0, candylimit)];

                if (iswirl == false&&col==randcol) { 
                    hitCandy = SwirlCandy; iswirl = true;
                    bug[0].transform.SetParent(GetArrays.transform);
                    bug[0].transform.position = hitCandy.transform.position;
                }
                CreateGem(row, col, hitCandy);
            }
        }

        for (int i = 0; i < SizeX; i++)
        {
            visibleSize[i] = levelsApps.vector2position + new Vector2(i * xc, SizeY * yr); //new Vector2(-2.37f, -4.27f) + new Vector2(i * 0.7f, SizeY * 0.7f);
        }
        //if(levelsApps.modeLvl!=2)LoadHelp();
    }
    GameObject cursormouse;
    public bool IsNulls(int row, int col)
    {
        if (levelsApps.blocksp[row* levelsApps.MaxX+col].types==0)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="row"></param>
    /// <param name="c"></param>
    /// <param name="hgem"></param>
    public void CreateGem(int row, int c, HitCandy hgem)
    {
        int[] randomplus5sec = {0,1,2,3,4,5,6,7,8};
        int plus5sec = randomplus5sec[Random.Range(0, randomplus5sec.Length)];
        if ((int)levelsApps.ltype==1&&plus5sec == c) { hgem = GetCandieSecons[Random.Range(0, GetCandieSecons.Length)]; }
        float xc = levelsApps.blckWH();
        float yr = levelsApps.blckWH();
        Vector2 vectorgem = levelsApps.vector2position + (new Vector2(c * xc, row * yr));
        HitCandy gemit = ((GameObject)Instantiate(hgem.gameObject, new Vector3(vectorgem.x, vectorgem.y, -0.1f), Quaternion.identity)).GetComponent<HitCandy>();
        gemit.transform.SetParent(GetArrays.transform);
        GetArrays[row, c].OnInit(gemit);
        if (IsNulls(row, c) == true&&!gemit.isSwirl) { Destroy(gemit.gameObject); }
    }
    /// <summary>
    /// Начинаем собирать совпадения
    /// </summary>
    /// <param name="raycast2"></param>
    /// <returns></returns>
    IEnumerator Match(RaycastHit2D raycast2)
    {
        HitCandy GetHitGem2 = raycast2.collider.gameObject.GetComponent<HitCandy>();
        var GetHitGemNeigbours = GetArrays.GetNewProp(GetHitGem.GetGem, GetHitGem2.GetGem);
        var matchs = GetHitGemNeigbours.gemms.Distinct();
        if (matchs.Count() >= 2 && GetHitGem.GetGem.level == GetHitGem2.GetGem.level)
        {
            CreateNewBonus(GetHitGem.GetGem, GetHitGem2.GetGem, GetHitGem.type); yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.3f);
        GetHitGem = null; GetHitGem2 = null;
        state = 0;
    }
    public void ender()
    {
        Ending = 0;
    }
    public void CreateNewBonus(Gem bgem,Gem bgem2,string type)
    {
        float xc = levelsApps.blckWH();
        float yr = levelsApps.blckWH();
        string[] CollectItemsfromcolor = { "", "red", "purple", "blue1", "blue2" };
        for (int i = 0; i < CollectItemsfromcolor.Count(); i++)
        {
            if (bgem2.hitGem.type == CollectItemsfromcolor[i])
            {
                //print(bgem2.hitGem.type);
                if (i == (int)levelsApps.collectItems[0]) { if (levelsApps.ingCtar[0] > 0) levelsApps.ingCtar[0] -= 3; }
                if (i == (int)levelsApps.collectItems[1]) { if (levelsApps.ingCtar[1] > 0) levelsApps.ingCtar[1] -= 3; }
            }
        }
        if (levelsApps.blocksp[bgem2.y * levelsApps.MaxX + bgem2.x].modelvlsquare == 3)
        {
            levelsApps.NotColorSquare(bgem2.x, bgem2.y, sprite12);
            levelsApps.blockscount -= 1;
        }
        levelsApps.printScores += 10;
        if (bgem2.hitGem.sprites.Length >= bgem2.level) { bgem2.level += 1; }
        Vector2 vectorgem = levelsApps.vector2position + new Vector2(bgem.x * xc, bgem.y * yr);
        bgem2.hitGem.GetComponent<SpriteRenderer>().sprite = bgem2.hitGem.sprites[bgem2.level];
        bgem2.hitGem.GetComponent<Animator>().SetBool("block", true);
        positions.Add(vectorgem);
        GetBlocks.Add(levelsApps.blocksp[bgem.y * levelsApps.MaxX + bgem.x]);
        if (bgem2.x > 0 && bgem2.x < levelsApps.MaxX)
        {
            if (levelsApps.Blocksf[bgem2.y * levelsApps.MaxX + (bgem2.x - 1)].block() == 0)
            {
                levelsApps.Blocksf[bgem2.y * levelsApps.MaxX + (bgem2.x - 1)].Changeblck(1);
                levelsApps.blocksp[bgem2.y * levelsApps.MaxX + (bgem2.x - 1)].GetComponent<SpriteRenderer>().enabled = true;
                levelsApps.blocksp[bgem2.y * levelsApps.MaxX + (bgem2.x - 1)].types = 1;
                GetBlocks.Add(levelsApps.blocksp[bgem2.y * levelsApps.MaxX + (bgem2.x - 1)]);
            }
        }
        if (bgem2.x >= 0 && bgem2.x < levelsApps.MaxX - 1)
        {
            if (levelsApps.Blocksf[bgem2.y * levelsApps.MaxX + (bgem2.x + 1)].block() == 0)
            {
                levelsApps.Blocksf[bgem2.y * levelsApps.MaxX + (bgem2.x + 1)].Changeblck(1);
                levelsApps.blocksp[bgem2.y * levelsApps.MaxX + (bgem2.x + 1)].GetComponent<SpriteRenderer>().enabled = true;
                levelsApps.blocksp[bgem2.y * levelsApps.MaxX + (bgem2.x + 1)].types = 1;
                GetBlocks.Add(levelsApps.blocksp[bgem2.y * levelsApps.MaxX + (bgem2.x + 1)]);
            }
        }
        Destroy(bgem.hitGem.gameObject);
    }
}
