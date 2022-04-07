﻿using System;
using Ladeskab.Interfaces;

namespace Ladeskab
{
    public class ChargeControl : IChargeControl
    {
        private IUsbCharger _usbCharger;
        private IDisplay _display;

        public bool Connected()
        {
            return _usbCharger.Connected;
            
        }

        public ChargeControl(IUsbCharger usbCharger, IDisplay display) 
        {
            _usbCharger = usbCharger;
            _display = display;
            usbCharger.CurrentValueEvent += HandleNewCurrent;


        }

        private void HandleNewCurrent(object? sender, CurrentEventArgs e)
        {
            NewCurrentDetected(e.Current);
        }

        private void NewCurrentDetected(double current)
        {
            if(current <= 0)
            {
                return;
            }
            else if(0< current && current <=5)
            {
                _display.PrintFullyCharged();
            }
            else if(5 < current && current <=500)
            {
                _display.PrintCurrentlyCharging();
            }
            else if(current > 500)
            {
                StopCharge();
                _display.PrintOverchargeError();
            }
        }

        public void StartCharge()
        {
            _usbCharger.StartCharge();
        }

        public void StopCharge()
        {
            _usbCharger.StopCharge();
        }

    }
}

