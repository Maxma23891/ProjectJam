using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBranchScene", menuName = "Data/New BranchScene")]
[System.Serializable]

public class BranchScene : StoryScene
{
    public List<Branch> scenes;
    public StoryScene normalScene;
    [System.Serializable]
    public struct Branch
    {
        public StoryScene targetScene;
        public List<string> keyConditions;
    }

}
