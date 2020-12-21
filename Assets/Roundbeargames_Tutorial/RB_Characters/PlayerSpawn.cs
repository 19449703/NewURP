using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public class PlayerSpawn : MonoBehaviour
    {
        public CharacterSelect characterSelect;
        private string objName;

        private void Start()
        {
            switch(characterSelect.selectedCharacterType)
            {
                case PlayableCharacterType.YELLOW:
                    {
                        objName = "yBot - yellow";
                        break;
                    }
                case PlayableCharacterType.RED:
                    {
                        objName = "yBot - red";
                        break;
                    }
                case PlayableCharacterType.GREEN:
                    {
                        objName = "yBot - green";
                        break;
                    }
            }

            GameObject obj = Instantiate(Resources.Load(objName, typeof(GameObject))) as GameObject;
            obj.transform.position = this.transform.position;
            obj.gameObject.GetComponent<ManualInput>().enabled = true;
            GetComponent<MeshRenderer>().enabled = false;

            Cinemachine.CinemachineVirtualCamera[] cams = GameObject.FindObjectsOfType<Cinemachine.CinemachineVirtualCamera>();
            foreach(var v in cams)
            {
                CharacterControl control = CharacterManager.instance.GetCharacter(characterSelect.selectedCharacterType);
                Collider target = control.GetBodyPart("Spine1");
                v.LookAt = target.transform;
                v.Follow = target.transform;
            }
        }
    }
}