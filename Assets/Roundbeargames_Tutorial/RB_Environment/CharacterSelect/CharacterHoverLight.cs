using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public class CharacterHoverLight : MonoBehaviour
    {
        public Vector3 offset = new Vector3();

        CharacterControl hoverSelectedCharacter;
        MouseControl mouseHoverSelect;
        Vector3 targetPos = new Vector3();
        new Light light;

        private void Start()
        {
            mouseHoverSelect = Object.FindObjectOfType<MouseControl>();
            light = GetComponent<Light>();
        }

        private void Update()
        {
            if (mouseHoverSelect.selectedCharacterType == PlayableCharacterType.NONE)
            {
                hoverSelectedCharacter = null;
                light.enabled = false;
            }
            else
            {
                light.enabled = true;
                LightUpSelectedCharacter();
            }
        }

        private void LightUpSelectedCharacter()
        {
            if (hoverSelectedCharacter == null)
            {
                hoverSelectedCharacter = CharacterManager.instance.GetCharacter(mouseHoverSelect.selectedCharacterType);
                this.transform.position = hoverSelectedCharacter.transform.position + hoverSelectedCharacter.transform.TransformDirection(offset);
            }
        }
    }
}
