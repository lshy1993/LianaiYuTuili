using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.UIScript
{
    public class DialogFade : DetectFade
    {
        private List<GameObject> dialogButtons;
        private int[] destinations;
        private bool open = false;

        public void setDialogBtns(List<GameObject> dialogButtons)
        {
            this.dialogButtons = dialogButtons;
        }


        public override void Open(float fadein)
        {
            InitPosition();
            open = true;
        }

        private void InitPosition()
        {
            foreach (GameObject btn in dialogButtons)
            {
                btn.transform.position = new Vector3(0, 410);
            }

            int n = dialogButtons.Count;
            int d = (670 - 50 * n) / (n + 1);
            destinations = new int[dialogButtons.Count];
            for (int i = 0; i < destinations.Length; i++)
            {
                destinations[i] = 360 - ((i + 1) * d + i * 50);
            }
        }

        new void FixedUpdate()
        {
            base.FixedUpdate();
            if (open)
            {
                for (int i = 0; i < dialogButtons.Count; i++)
                {
                    float y = Mathf.MoveTowards(dialogButtons[i].transform.position.y, destinations[i], (360 - destinations[i]) * 0.3f * Time.fixedDeltaTime);
                    dialogButtons[i].transform.position = new Vector3(0, y);
                }
            }

        }
    }
}
