using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceChanger : MonoBehaviour
{
    public Renderer characterRenderer;

    private void Awake()
    {
        if (characterRenderer == null)
            characterRenderer = GetComponentInChildren<Renderer>();
    }

    /// <summary>
    /// 캐릭터의 얼굴을 변경합니다.
    /// </summary>
    /// <param name="name">Hera, Zeus</param>
    /// <param name="state">Normal, Cry, Angry, Run, Victory</param>
    public void ChangeFace(string name, string state)
    {
        Material[] materials = characterRenderer.materials;
        int index = 1;

        if (name.Equals("hera"))
            index = 0;

        materials[index] = Resources.Load<Material>(new StringBuilder().Append("Character/Face/")
        .Append(name)
        .Append("_Face_")
        .Append(state).ToString());

        characterRenderer.materials = materials;
    }

}
