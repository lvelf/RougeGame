using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MpBar : MonoBehaviour
{
    public Text MpText;
    public static int Mp_Current;
    public static int MpMax = 1000;

    private Image Mpbar;

    // Start is called before the first frame update
    void Start()
    {
        Mpbar = GetComponent<Image>();
        Mp_Current = MpMax;
    }

    // Update is called once per frame
    void Update()
    {
        Mpbar.fillAmount = (float)Mp_Current / (float)MpMax;
        MpText.text = Mp_Current.ToString() + "/" + MpMax.ToString();
    }
}
