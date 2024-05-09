using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newfloorstaircase : MonoBehaviour
{
    bool zoomcor;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Player") || zoomcor) { return; }

        zoomcor = true;
        index.idx.floormanager.resetrooms();
        index.idx.floormanager.newfloor(1, new Vector3(0,0,0));
        StartCoroutine(zoom());
    }

    private IEnumerator zoom()
    {
        float orthsize = index.idx.virtualCamera.m_Lens.OrthographicSize;
        // index.idx.virtualCamera.m_Lens.OrthographicSize = orthsize / 2;
        float zoomanimlength = 0.2f;
        float orthshrinkfactor = 1000;

        float timer = 0;
        while (timer < zoomanimlength)
        {

            timer += Time.fixedDeltaTime;
            index.idx.virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(orthsize, orthsize / orthshrinkfactor, timer/zoomanimlength);
            yield return new WaitForFixedUpdate();

        }

        index.idx.Player.transform.position = new Vector3(-33, 0, 0);

        while (timer > 0)
        {

            timer -= Time.fixedDeltaTime;
            index.idx.virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(orthsize, orthsize / orthshrinkfactor , timer / zoomanimlength);
            yield return new WaitForFixedUpdate();

        }

        //yield return new WaitForSeconds(1);
        index.idx.virtualCamera.m_Lens.OrthographicSize = orthsize;

        zoomcor = false;
    }
}
