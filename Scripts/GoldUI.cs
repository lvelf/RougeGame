using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldUI : MonoBehaviour
{
    public int startGoldQuantity;

    public Text coinQuantity;

    public static int CurrentGoldQuantity;
    // Start is called before the first frame update
    void Start()
    {
        CurrentGoldQuantity = startGoldQuantity;
    }

    // Update is called once per frame
    void Update()
    {
        coinQuantity.text = CurrentGoldQuantity.ToString();
    }
}
