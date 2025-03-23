using UnityEngine;
using System.Collections.Generic;

namespace Runtime
{
    public class KeyboardInputHandler : MonoBehaviour
    {
        // 按键配置结构体
        [System.Serializable]
        public struct KeyConfig
        {
            public KeyCode keyCode;
            public string displayName;
        }

        // 需要监听的按键列表（可在Inspector配置）
        public KeyConfig[] monitoredKeys = new KeyConfig[]
        {
            new KeyConfig{keyCode = KeyCode.W, displayName = "前进"},
            new KeyConfig{keyCode = KeyCode.S, displayName = "后退"},
            new KeyConfig{keyCode = KeyCode.A, displayName = "左移"},
            new KeyConfig{keyCode = KeyCode.D, displayName = "右移"}
        };

        // 按键状态存储类
        private class KeyState
        {
            public bool isPressed;      // 当前是否按下
            public float pressTime;    // 按下时刻（秒）
            public float holdDuration; // 持续时长（秒）
            public int pressCount;     // 累计按压次数
        }

        private Dictionary<KeyCode, KeyState> keyStates = new Dictionary<KeyCode, KeyState>();

        void Start()
        {
            // 初始化按键状态
            foreach (var keyConfig in monitoredKeys)
            {
                keyStates[keyConfig.keyCode] = new KeyState();
            }
        }

        void Update()
        {
            UpdateKeyStates();
        }

        void UpdateKeyStates()
        {
            foreach (var keyConfig in monitoredKeys)
            {
                var keyCode = keyConfig.keyCode;
                var state = keyStates[keyCode];

                if (Input.GetKeyDown(keyCode))
                {
                    // 记录按下时刻
                    state.isPressed = true;
                    state.pressTime = Time.time;
                    state.pressCount++;
                }

                if (Input.GetKeyUp(keyCode))
                {
                    // 计算总持续时间
                    state.isPressed = false;
                    //state.holdDuration += Time.time - state.pressTime;
                    state.holdDuration = 0f;
                }

                if (state.isPressed)
                {
                    // 实时更新持续时长
                    state.holdDuration = Time.time - state.pressTime;
                }
            }
        }

        // 获取指定键的实时数据
        public float GetKeyHoldDuration(KeyCode key)
        {
            if (keyStates.TryGetValue(key, out KeyState state))
            {
                return state.holdDuration;
            }
            return 0f;
        }

        // 获取所有键的当前状态
        void OnGUI()
        {
            GUILayout.BeginVertical(GUI.skin.box);
            GUILayout.Label("键盘输入监控");
            
            foreach (var keyConfig in monitoredKeys)
            {
                var state = keyStates[keyConfig.keyCode];
                var status = state.isPressed ? $"按住中（{state.holdDuration:F2}s）" : $"已释放（累计：{state.holdDuration:F2}s）";
                GUILayout.Label($"{keyConfig.displayName} ({keyConfig.keyCode}): {status}");
            }
            
            GUILayout.EndVertical();
        }
    }
}


