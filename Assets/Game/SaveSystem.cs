using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    //private PlayerPrefs playerPrefs;
    private GameObject player;
    private CoinCollect coinCollect;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        coinCollect = player.GetComponent<CoinCollect>();
        
        int coinCount = PlayerPrefs.GetInt("CoinCount", 0);
        coinCollect.CoinCount = coinCount;
    }

    // Update is called once per frame
    public void SaveCoins()
    {
        int coinCount = coinCollect.CoinCount;
        PlayerPrefs.SetInt("CoinCount", coinCount);
    }
}
