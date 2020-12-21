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
        GameObject whiteSelection;
        Animator characterSelectCamAnimator;

        private void Awake()
        {
            characterSelect.selectedCharacterType = PlayableCharacterType.NONE;
            characterSelectLight = Object.FindObjectOfType<CharacterSelectLight>();
            characterHoverLight = Object.FindObjectOfType<CharacterHoverLight>();

            whiteSelection = GameObject.Find("WhiteSelection");
            whiteSelection.SetActive(false);

            characterSelectCamAnimator = GameObject.Find("CharacterSelectCameraController").GetComponent<Animator>();
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
                    
                    var control = CharacterManager.instance.GetCharacter(selectedCharacterType);
                    control.skinedMeshAnimator.SetTrigger(TransitionParameter.ClickAnimation.ToString());

                    whiteSelection.SetActive(true);
                    whiteSelection.transform.parent = control.skinedMeshAnimator.transform;
                    whiteSelection.transform.localPosition = new Vector3(0f, -0.045f, 0f);
                }
                else
                {
                    characterSelectLight.light.enabled = false;
                    whiteSelection.SetActive(false);
                }

                characterSelectCamAnimator.SetBool(selectedCharacterType.ToString(), true);
            }
        }
    }
}