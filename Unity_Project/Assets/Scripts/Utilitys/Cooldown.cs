using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utility{
    public class Cooldown
    {
        private float length;
        private float currentTime;
        private bool onCooldown;

        public Cooldown(float length=1, bool startOnCooldown = false)
        {
            this.length = length;
            currentTime = 0;
            onCooldown = startOnCooldown;
        }

        public bool OnCooldown { get => onCooldown;}

        public void CooldownUpdate()
        {
            if (onCooldown){
                currentTime += Time.deltaTime;
                if(currentTime >= length){
                    currentTime = 0;
                    onCooldown = false;
                }
            }
        }
        public void StartCooldown(){
            onCooldown = true;
            currentTime = 0;
        }

    }
}
