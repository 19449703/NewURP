using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public enum InputKeyType
    {
        KEY_MOVE_UP,
        KEY_MOVE_DOWN,
        KEY_MOVE_RIGHT,
        KEY_MOVE_LEFT,

        KEY_JUMP,
        KEY_ATTACK,
        KEY_TURBO,

        //KEY_BLOCK,
    }

    public class VirtualInputManager : Singleton<VirtualInputManager>
    {
        public PlayerInput playerInput;

        public bool turbo = false;
        public bool moveUp = false;
        public bool moveDown = false;
        public bool moveLeft = false;
        public bool moveRight = false;
        public bool jump = false;
        public bool attack = false;

        [Header("Custom Key Binding")]
        public bool UseCustomKeys;

        [Space(5)]
        public bool Bind_MoveUp;
        public bool Bind_MoveDown;
        public bool Bind_MoveRight;
        public bool Bind_MoveLeft;
        public bool Bind_Jump;
        public bool Bind_Attack;
        public bool Bind_Turbo;
        public bool Bind_Block;

        [Space(10)]
        public Dictionary<InputKeyType, KeyCode> dicKeys = new Dictionary<InputKeyType, KeyCode>();
        public KeyCode[] PossibleKeys;

        private void Awake()
        {
            PossibleKeys = System.Enum.GetValues(typeof(KeyCode)) as KeyCode[];

            GameObject obj = Instantiate(Resources.Load("PlayerInput", typeof(GameObject))) as GameObject;
            playerInput = obj.GetComponent<PlayerInput>();
        }

        public void LoadKeys()
        {
            if (playerInput.savedKeys.keyCodesList.Count > 0)
            {
                foreach (KeyCode k in playerInput.savedKeys.keyCodesList)
                {
                    if (k == KeyCode.None)
                    {
                        SetDefaultKeys();
                        break;
                    }
                }
            }
            else
            {
                SetDefaultKeys();
            }

            for (int i = 0; i < playerInput.savedKeys.keyCodesList.Count; i++)
            {
                dicKeys[(InputKeyType)i] = playerInput.savedKeys.keyCodesList[i];
            }
        }

        public void SaveKeys()
        {
            playerInput.savedKeys.keyCodesList.Clear();

            int count = System.Enum.GetValues(typeof(InputKeyType)).Length;

            for (int i = 0; i < count; i++)
            {
                playerInput.savedKeys.keyCodesList.Add(dicKeys[(InputKeyType)i]);
            }
        }

        public void SetDefaultKeys()
        {
            dicKeys.Clear();

            dicKeys.Add(InputKeyType.KEY_MOVE_UP, KeyCode.W);
            dicKeys.Add(InputKeyType.KEY_MOVE_DOWN, KeyCode.S);
            dicKeys.Add(InputKeyType.KEY_MOVE_LEFT, KeyCode.A);
            dicKeys.Add(InputKeyType.KEY_MOVE_RIGHT, KeyCode.D);

            dicKeys.Add(InputKeyType.KEY_JUMP, KeyCode.Space);
            dicKeys.Add(InputKeyType.KEY_ATTACK, KeyCode.Return);
            dicKeys.Add(InputKeyType.KEY_TURBO, KeyCode.RightShift);

            //dicKeys.Add(InputKeyType.KEY_BLOCK, KeyCode.Mouse1);

            SaveKeys();
        }

        private void Update()
        {
            //if (!UseCustomKeys)
            //{
            //    return;
            //}

            if (UseCustomKeys)
            {
                if (Bind_MoveUp)
                {
                    if (KeyIsChanged(InputKeyType.KEY_MOVE_UP))
                    {
                        Bind_MoveUp = false;
                    }
                }

                if (Bind_MoveDown)
                {
                    if (KeyIsChanged(InputKeyType.KEY_MOVE_DOWN))
                    {
                        Bind_MoveDown = false;
                    }
                }

                if (Bind_MoveRight)
                {
                    if (KeyIsChanged(InputKeyType.KEY_MOVE_RIGHT))
                    {
                        Bind_MoveRight = false;
                    }
                }

                if (Bind_MoveLeft)
                {
                    if (KeyIsChanged(InputKeyType.KEY_MOVE_LEFT))
                    {
                        Bind_MoveLeft = false;
                    }
                }

                if (Bind_Jump)
                {
                    if (KeyIsChanged(InputKeyType.KEY_JUMP))
                    {
                        Bind_Jump = false;
                    }
                }

                if (Bind_Attack)
                {
                    if (KeyIsChanged(InputKeyType.KEY_ATTACK))
                    {
                        Bind_Attack = false;
                    }
                }

                if (Bind_Turbo)
                {
                    if (KeyIsChanged(InputKeyType.KEY_TURBO))
                    {
                        Bind_Turbo = false;
                    }
                }

                //if (Bind_Block)
                //{
                //    if (KeyIsChanged(InputKeyType.KEY_BLOCK))
                //    {
                //        Bind_Block = false;
                //    }
                //}
            }
        }

        private void SetCustomKey(InputKeyType inputKey, KeyCode key)
        {
            Debug.Log("key changed: " + inputKey.ToString() + " -> " + key.ToString());

            if (!dicKeys.ContainsKey(inputKey))
            {
                dicKeys.Add(inputKey, key);
            }
            else
            {
                dicKeys[inputKey] = key;
            }

            SaveKeys();
        }
        
        bool KeyIsChanged(InputKeyType inputKey)
        {
            if (Input.anyKey)
            {
                foreach (KeyCode k in PossibleKeys)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        continue;
                    }

                    if (Input.GetKeyDown(k))
                    {
                        SetCustomKey(inputKey, k);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
