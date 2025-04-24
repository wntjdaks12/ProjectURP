using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataSceneManager : MonoBehaviour
{

    // 모든 데이터가 로드될 때ㅐ 신 로드 해야됨x
    public void Start()
    {
        StartCoroutine(TestLoadAsync());
    }

    private IEnumerator TestLoadAsync()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("SampleScene");
    }
}
