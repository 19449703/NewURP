using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public class MouseControl : MonoBehaviour
    {
        Ray ray;
        RaycastHit hit;

        public PlayableCharacterType selectedCharacterType;
        public CharacterSelect characterSelect;

        CharacterSelectLight characterSelectLight;
        CharacterHoverLight characterHoverLight;

        private void Start()
        {
            characterSelect.selectedCharacterType = PlayableCharacterType.NONE;
            characterSelectLight = Object.FindObjectOfType<CharacterSelectLight>();
            characterHoverLight = Object.FindObjectOfType<CharacterHoverLight>();
        }

        public void Update()
        {
            ray = CameraManager.instance.mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                CharacterControl control = hit.collider.gameObject.GetComponent<CharacterControl>();
                if (control != null)
                {
                    selectedCharacterType = control.playableCharacterType;
                }
                else
                {
                    selectedCharacterType = PlayableCharacterType.NONE;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                characterSelect.selectedCharacterType = selectedCharacterType;
                
                if (selectedCharacterType != PlayableCharacterType.NONE)
                {
                    characterSelectLight.light.enabled = true;
                    characterSelectLight.transform.position = characterHoverLight.transform.position;
                }
                else
                {
                    characterSelectLight.light.enabled = false;
                }
            }
        }
    }
}