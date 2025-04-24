using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataSceneManager : MonoBehaviour
{

    // ��� �����Ͱ� �ε�� ���� �� �ε� �ؾߵ�x
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
