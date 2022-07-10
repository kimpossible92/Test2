using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
public class MoveLayer : MonoBehaviour {
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
    [SerializeField] UnityEngine.UI.Text l1;
    bool check0 = false; 
    bool load;
    int Ending=0;
    //
    [HideInInspector]
    public float p1 = 2.4f, p2 = 0.9f, p3 = 0.6f;
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
        check0 = false;
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
    int row1 = 0;
    bool isStop = false;
    void SetNextRow()
    {
        if(isStop)RotateRow(0);
    }
    void setNextRow1()
    {
        if (isStop) RotateRow(1);
    }
    void setNextRow2()
    {
        if (isStop) RotateRow(2);
    }
    public void RotateRow(int col)
    {
        if (row1 >= SizeY) { row1 = 0; }
        for (int r = 0; r < SizeY; r++)
        {
            int newRow = r - 1;
            if (newRow <= -1) { newRow = SizeY - 1; }
            var newhit = GetArrays[r, col].hitGem; 
            var oldhit = GetArrays[r, col].hitGem;
            oldhit.transform.position = levelsApps.blocksp[newRow * levelsApps.MaxX + col].transform.position;
            GetArrays[newRow, col].OnInit2(newhit);
        }
        row1++; 
    }
    public void setStop()
    {
        checkStop();
        isStop = false;
    }
    public void setUpdates()
    {
        if (!isStop) { l1.text = "Stop"; isStop = true; }
        else { 
            l1.text = "Start";
            checkStop();
            isStop = false; 
        }
        
        if (l1.text == "Start")
        {

        }
    }
    protected int scorePlayer;
    [SerializeField] UnityEngine.UI.Text _text;
    public void checkStop()
    {
        for (int r1 = 0; r1 < SizeY - 1; r1++)
        {
            GetHitGem = GetArrays[r1, 0].hitGem;
            var GetHitGemNeigbours = GetArrays.GetProp(GetArrays[r1, 0].hitGem.GetGem);
            var GetHitGemNeigbour2 = GetArrays.GetProp(GetArrays[r1, SizeX - 1].hitGem.GetGem);
            var matchs = GetHitGemNeigbours.gemms.Distinct();
            var matchs2 = GetHitGemNeigbour2.gemms.Distinct();
            if (matchs.Count() >= 2)
            {
                scorePlayer += matchs.Count() * 10; 
            }
            else if (matchs2.Count() >= 2)
            {
                scorePlayer += matchs2.Count() * 10;
            }
            else
            {
                return;
            }
            //print("checkwin:" + scorePlayer);
        }
    }
    void Update()
    {
        _text.text = scorePlayer.ToString();
    }
    [SerializeField] List<List<Vector2>> xlist1 = new List<List<Vector2>>();
    public List<string> vslist;
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
                GetArrays[row, col].setlvlApps(levelsApps);
            }
        }
        int g = SizeX;
        visibleSize = new Vector2[g];
        int candylimit = 0;
        int randcol = Random.Range(0, 2);
        List<int> listCand= new List<int>();
        int change = 0;
        for(int ci = 0; ci < GetCandyPrefab.Length; ci++) { listCand.Add(ci); }
        for (int row = 0; row < SizeY-1; row++)
        {
            for (int col = 0; col < SizeX; col++)
            {
                candylimit = GetCandyPrefab.Length-1;
                HitCandy hitCandy = GetCandyPrefab[Random.Range(0, candylimit)];

                if (randcol==1)
                {
                    hitCandy = GetCandyPrefab[change];
                }
                else
                {
                    hitCandy = GetCandyPrefab[change];
                }
                CreateGem(row, col, hitCandy);
            }
            change++;
            if (change > listCand.Count-1) { change = 0; }
        }

        for (int i = 0; i < SizeX; i++)
        {
            visibleSize[i] = levelsApps.vector2position + new Vector2(i * xc, SizeY * yr); //new Vector2(-2.37f, -4.27f) + new Vector2(i * 0.7f, SizeY * 0.7f);
        }
        if (!check0)
        {
            //InvokeRepeating("SetNextRow", 2.4f, 2.4f);
            //InvokeRepeating("setNextRow1", 0.9f, 0.9f);
            //InvokeRepeating("setNextRow2", 0.6f, 0.6f);
            InvokeRepeating("SetNextRow", p1, p1);
            InvokeRepeating("setNextRow1", p2, p2);
            InvokeRepeating("setNextRow2", p3, p3);
            check0 = true;
        }
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
    public void ender()
    {
        Ending = 0;
    }
}
