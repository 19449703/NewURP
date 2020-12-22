using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public class CharacterManager : Singleton<CharacterManager>
    {
        public List<CharacterControl> characters = new List<CharacterControl>();

        public CharacterControl GetCharacter(PlayableCharacterType type)
        {
            foreach(var ch in characters)
            {
                if (ch.playableCharacterType == type)
                    return ch;
            }

            return null;
        }

        public CharacterControl GetCharacter(Animator animator)
        {
            foreach (var ch in characters)
            {
                if (ch.skinedMeshAnimator == animator)
                    return ch;
            }

            return null;
        }
    }
}
