using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        Managers.Game.MonsterAndPlayerClear();
        Managers.Lock.ReleaseLocks();
        Managers.Game.SaveData.entireEnemy = 0;
        Managers.Game.SaveData.escapedChicken = 0;
        Managers.Game.SaveData.stage = 0;
        Managers.Game.SaveData.remainChicken = 0;

        SceneType = Define.Scene.Title;

        Japanese(); 
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Managers.Scene.LoadScene(Define.Scene.Game); 
        }
    }

    public override void Clear()
    {
        Debug.Log("LoginScene Clear!");
    }

    public void ClickStart()
    {
        Debug.Log("로딩");

        if (Managers.Game.lang == Define.Language.Jpanese)
        {
            Managers.UI.ShowPopupUI<UI_IntroPopup_JP>().SetInfo((int)UI_IntroPopup_JP.GameObjects.Intro1, (int)UI_IntroPopup_JP.GameObjects.Intro4, () =>
            {
                Managers.Scene.LoadScene(Define.Scene.Game);
            });
        }
        else if (Managers.Game.lang == Define.Language.Korean)
        {
            Managers.UI.ShowPopupUI<UI_IntroPopup_KR>().SetInfo((int)UI_IntroPopup_KR.GameObjects.Intro1, (int)UI_IntroPopup_KR.GameObjects.Intro4, () =>
            {
                Managers.Scene.LoadScene(Define.Scene.Game);
            });
        }

    }

    public void ClickLoad()
    {
        Debug.Log("로드");
    }

    public void ClickExit()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Debug.Log("게임 종료");
        Application.Quit();
#endif
    }

    public void Japanese()
    {
        Managers.Game.lang = Define.Language.Jpanese; 

//        TMP_FontAsset jpnFont = Resources.Load<TMP_FontAsset>("Fonts & Materials/NotoSansJP-Black SDF");
//        GameObject prefab = Resources.Load<GameObject>("Prefabs/UI/Popup/UI_IntroPopup");
//        GameObject instance; 
//        instance = GameObject.Instantiate(prefab);

//        Transform IntroTextTransform = instance.transform.Find("IntroText");
//        if (IntroTextTransform != null)
//        {
//            TextMeshProUGUI textMeshPro = IntroTextTransform.GetComponent<TextMeshProUGUI>();
//            if (textMeshPro != null)
//            {
//                textMeshPro.font = jpnFont;
//            }
//        }
////        PrefabUtility.SaveAsPrefabAsset(instance, "Assets/Resources/Prefabs/UI/Popup/UI_IntroPopup.prefab"); 
//        DestroyImmediate(instance);


//        prefab = Resources.Load<GameObject>("Prefabs/UI/Popup/UI_PlayPopup");
//        GameObject instance2;
//        instance2 = Instantiate(prefab);
//        Transform playTextTransform = instance2.transform.Find("PlayText");
//        if (playTextTransform != null)
//        {
//            TextMeshProUGUI textMeshPro = playTextTransform.GetComponent<TextMeshProUGUI>();
//            if (textMeshPro != null)
//            {
//                textMeshPro.font = jpnFont;
//            }
//        }
//        Destroy(instance2);
    }

    public void Korean()
    {
        Managers.Game.lang = Define.Language.Korean;

        //TMP_FontAsset korFont = Resources.Load<TMP_FontAsset>("Fonts & Materials/NotoSansKR-Black SDF");

        //GameObject prefab = Resources.Load<GameObject>("Prefabs/UI/Popup/UI_IntroPopup");
        //GameObject instance;
        //instance = Instantiate(prefab);
        //Transform IntroTextTransform = instance.transform.Find("IntroText");
        //if (IntroTextTransform != null)
        //{
        //    TextMeshProUGUI textMeshPro = IntroTextTransform.GetComponent<TextMeshProUGUI>();
        //    if (textMeshPro != null)
        //    {
        //        textMeshPro.font = korFont;
        //    }
        //}
        //DestroyImmediate(instance);

        //prefab = Resources.Load<GameObject>("Prefabs/UI/Popup/UI_PlayPopup");
        //GameObject instance2;
        //instance2 = Instantiate(prefab);
        //Transform playTextTransform = instance2.transform.Find("PlayText");
        //if (playTextTransform != null)
        //{
        //    TextMeshProUGUI textMeshPro = playTextTransform.GetComponent<TextMeshProUGUI>();
        //    if (textMeshPro != null)
        //    {
        //        textMeshPro.font = korFont;
        //    }
        //}
        //DestroyImmediate(instance2);


        //Managers.Game.lang = Define.Language.Korean;

        //TMP_FontAsset korFont = (TMP_FontAsset)AssetDatabase.LoadAssetAtPath("Assets/TextMesh Pro/Resources/Fonts & Materials/NotoSansKR-Black SDF.asset", typeof(TMP_FontAsset));

        //GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Prefabs/UI/Popup/UI_IntroPopup.prefab", typeof(GameObject));
        //GameObject instance;
        //instance= (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        //Transform IntroTextTransform = instance.transform.Find("IntroText");
        //if (IntroTextTransform != null)
        //{
        //    TextMeshProUGUI textMeshPro = IntroTextTransform.GetComponent<TextMeshProUGUI>();
        //    if (textMeshPro != null)
        //    {
        //        textMeshPro.font = korFont;
        //    }
        //}
        //PrefabUtility.SaveAsPrefabAsset(instance, "Assets/Resources/Prefabs/UI/Popup/UI_IntroPopup.prefab");
        //DestroyImmediate(instance);

        //prefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Prefabs/UI/Popup/UI_PlayPopup.prefab", typeof(GameObject));
        //GameObject instance2; 
        //instance2 = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        //Transform playTextTransform = instance2.transform.Find("PlayText");
        //if (playTextTransform != null)
        //{
        //    TextMeshProUGUI textMeshPro = playTextTransform.GetComponent<TextMeshProUGUI>();
        //    if (textMeshPro != null)
        //    {
        //        textMeshPro.font = korFont;
        //    }
        //}
        //PrefabUtility.SaveAsPrefabAsset(instance2, "Assets/Resources/Prefabs/UI/Popup/UI_PlayPopup.prefab");
        //DestroyImmediate(instance2);

    }
}
