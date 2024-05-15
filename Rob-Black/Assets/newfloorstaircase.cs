using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newfloorstaircase : MonoBehaviour
{
    public bool zoomcor = false;

    private void Start()
    {
        transform.parent = transform.parent.parent;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(zoomcor);

        if (!collision.collider.CompareTag("Player") || zoomcor) { return; }

        zoomcor = true;
        StartCoroutine(zoom());
    }

    private IEnumerator zoom()
    {
        print(zoomcor);

        float oldplayerspeed = (float) index.idx.Player.GetComponent<PlayerMove>().playerSpeed;

        float orthsize = index.idx.virtualCamera.m_Lens.OrthographicSize;
        //index.idx.virtualCamera.m_Lens.OrthographicSize = orthsize / 2;
        float zoomanimlength = 1f;
        float orthshrinkfactor = 1000;

        index.idx.Player.GetComponent<PlayerMove>().playerSpeed = 0;
        //index.idx.Player.transform.position = new Vector3(-33, 0, 0);
        float timer = 0;
        while (timer < zoomanimlength)
        {
            yield return new WaitForFixedUpdate();
            timer += Time.fixedDeltaTime;
            index.idx.virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(orthsize, orthsize / orthshrinkfactor, timer/zoomanimlength);


        }

        Debug.Log(timer);
        index.idx.Player.transform.position = new Vector3(-33, 0, 0);

        index.idx.screenblack(true);

        index.idx.floormanager.resetrooms();
        index.idx.floormanager.newfloor(1, new Vector3(0, 0, 0));

        Debug.LogError("point 1");
        while (timer > 0)
        {
            yield return new WaitForFixedUpdate();
            timer -= Time.fixedDeltaTime;
            index.idx.virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(orthsize, orthsize / orthshrinkfactor , timer / zoomanimlength);
           

        }
        Debug.Log(timer);
        //.LogError("point 2");
        // index.idx.Player.transform.position = new Vector3(-33, 0, 0);

        //yield return new WaitForSeconds(1);
        index.idx.virtualCamera.m_Lens.OrthographicSize = orthsize;

        index.idx.Player.GetComponent<PlayerMove>().playerSpeed = oldplayerspeed;

        index.idx.screenblack(false);

        Destroy(gameObject);

        zoomcor = false;
        print("emndmsakj");
       // Debug.LogError("point3");

    }
}
