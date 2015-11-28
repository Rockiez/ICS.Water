using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICS.Acquisition;
using ICS.Models;
using ICS.Common;

namespace ICS.Water
{
    class Global
    {
        public static KL_M4514 KL_M4514Provider
        {
            get { return (KL_M4514) ClassFactory.GetProvider(equipmentCategory.KL_M4514, ComSetting); }
        }

        public static ADAM4150 ADAM4150Provider
        {
            get { return (ADAM4150) ClassFactory.GetProvider(equipmentCategory.ADAM4150, ComSetting); }
        }

        public static Models.Com.ComSettingModel _ComSetting;

        public static Models.Com.ComSettingModel ComSetting
        {
            get
            {
                if (_ComSetting == null)
                {
                    _ComSetting = new SettingHelper<Models.Com.ComSettingModel>().GetSetting();
                    _ComSetting.DigitalQuantityCom = "Com3";
                    _ComSetting.AnalogQuantityCom = "Com3";
                }
                return _ComSetting;
            }
        }
    }
}
