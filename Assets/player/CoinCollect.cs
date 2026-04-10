using UnityEngine;
using TMPro;

public class CoinCollect : MonoBehaviour
{
    private int coinCountValue = 0;
    private TMP_Text cointText;
    private GameObject audioManager;
    private AudioSource audioSource;

    void Start()
    {
        cointText = GameObject.Find("CoinText").GetComponent<TMP_Text>();
        cointText.text = "Coins: " + coinCountValue;
        //audioSource = transform.Find("CoinSound").GetComponent<AudioSource>();
        audioSource = GameObject.Find("CoinManager").transform.Find("CoinSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider obj)
    {
        GameObject objParent = obj.transform.parent.gameObject;
        //AudioSource audio = audioManager.GetComponentInChildren<AudioSource>();

        
        if (objParent.tag == "Coin")
        {
            TakeCoin(objParent);
        }
        
    }

    void TakeCoin(GameObject obj)
    {
        Debug.Log("Coin!");
        coinCountValue++;
        audioSource.Play();
        Destroy(obj);

        cointText.text = "Coins: " + coinCountValue;

    }

    public int CoinCount { get { return coinCountValue;} set {coinCountValue = value;}}
}
