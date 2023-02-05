using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinArea : MonoBehaviour
{
    public static bool winning = true;
    Collider2D winTrigger;
    bool win = false;
    GameObject winningObject;
    public Cinemachine.CinemachineVirtualCamera winCam;
    // Start is called before the first frame update
    void Start()
    {
        winning = false;
        winTrigger = GetComponent<Collider2D>();
    }

    private void FixedUpdate() {
        if (win)
            return;
        ContactFilter2D cf = new ContactFilter2D();
        List<Collider2D> col = new List<Collider2D>();
        Physics2D.OverlapCollider(winTrigger, cf, col);
        foreach(var x in col) {
            if (x.gameObject.GetComponent<RootNode>()) {
                win = true;
                winningObject = x.gameObject;
                StartCoroutine(onWin());
                Debug.Log("Sieg!");
                return;
            }
        }
    }


    IEnumerator onWin() {
        winning = true;
        foreach (var foe in GameObject.FindGameObjectsWithTag("Opponent"))
            Destroy(foe);

        winCam.enabled = true;
        GameObject.Find("CM vcam1").GetComponent<Cinemachine.CinemachineVirtualCamera>().enabled = false;

        yield return new WaitForSeconds(1);

        var node = winningObject.GetComponent<RootNode>();
        var tip = winningObject.GetComponent<IsRootTip>();
        node.IsBossRoot = true;
        node.IsDead = false;
        tip.IsTip = true;
        winningObject.GetComponent<RootInfluence>().enabled = false;
        winningObject.GetComponent<RandomWalk>().gravity = new Vector2(0, 0.1f);
        winningObject.GetComponent<RandomSplit>().chancheBranch = 0.03f;
        winningObject.GetComponent<RandomSplit>().TimeBeforeNewSplit = 0.3f;
        node.Flower.SetActive(true);

        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("VictoryScene");
    }
}
