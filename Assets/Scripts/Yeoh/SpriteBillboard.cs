using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]

public class SpriteBillboard : MonoBehaviour
{
    public bool enableBillboard=true;

#if UNITY_EDITOR
    void OnEnable()
    {
        EditorApplication.update += EditorUpdate;
    }

    void OnDisable()
    {
        EditorApplication.update -= EditorUpdate;
    }

    void EditorUpdate()
    {
        if(!Application.isPlaying) Billboard();
    }
#endif

    void Update()
    {
        if(!Application.isPlaying) return;

        if(!fixedUpdate) Billboard();
    }

    public bool fixedUpdate=true;
    
    void FixedUpdate()
    {
        if(!Application.isPlaying) return;
        
        if(fixedUpdate) Billboard();
    }

    public bool onlyY;

    void Billboard()
    {
        if(!enableBillboard) return;

        if(onlyY) transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
        else transform.rotation = Camera.main.transform.rotation;
    }
}
