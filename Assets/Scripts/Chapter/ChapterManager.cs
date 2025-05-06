using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterManager : MonoBehaviour
{
    private static ChapterManager instance;
    public static ChapterManager Instance { get => instance ??= FindAnyObjectByType<ChapterManager>(); }

    public ChapterViewModel ChapterViewModel { get; private set; }

    private void Awake()
    {
        ChapterViewModel = new ChapterViewModel();
    }
}
