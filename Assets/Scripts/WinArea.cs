using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinArea : MonoBehaviour
{
    Collider2D winTrigger;
    bool win = false;
    GameObject winningObject;
    // Start is called before the first frame update
    void Start()
    {
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
        foreach (var foe in GameObject.FindGameObjectsWithTag("Opponent"))
            Destroy(foe);

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

        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("VictoryScene");
    }
}
