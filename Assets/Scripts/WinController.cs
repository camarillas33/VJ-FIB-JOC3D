using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class WinController : MonoBehaviour
{
    public TMP_Text winner01;
    public TMP_Text loser01;
    public TMP_Text winner02;
    public TMP_Text loser02;

    private TextMeshProUGUI textWinner01;
    private TextMeshProUGUI textLoser01;
    private TextMeshProUGUI textWinner02;
    private TextMeshProUGUI textLoser02;
    // Start is called before the first frame update
    void Start()
    {
        textWinner01 = winner01.GetComponent<TextMeshProUGUI>();
        textLoser01 = loser01.GetComponent<TextMeshProUGUI>();
        textWinner02 = winner02.GetComponent<TextMeshProUGUI>();
        textLoser02 = loser02.GetComponent<TextMeshProUGUI>();

        textWinner01.gameObject.SetActive(false);
        textLoser01.gameObject.SetActive(false);
        textWinner02.gameObject.SetActive(false);
        textLoser02.gameObject.SetActive(false);
    }

    public void winPlayer01() {
        textWinner01.gameObject.SetActive(true);
        textLoser02.gameObject.SetActive(true);
    }

    public void winPlayer02() {
        textWinner02.gameObject.SetActive(true);
        textLoser01.gameObject.SetActive(true);
    }
}
