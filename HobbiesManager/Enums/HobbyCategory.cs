using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbiesManager.Enums
{
    public enum HobbyCategory
    {
        [Description("Idrott")]
        Sport,

        [Description("Konst och Hantverk")]
        Arts_and_craft,

        [Description("Friluftsliv och Äventyr")]
        Outdoor_and_Adventure,

        [Description("Musik och Dans")] 
        Music_and_Dance,

        [Description("Samlingar och Antikviteter")] 
        Collecting_and_Antiques,

        [Description("Spel och Pussel")] 
        Games_and_Puzzles,

        [Description("Teknologi och Programmering")] 
        Technology_and_Programming,

        [Description("Litteratur och Skrivande")] 
        Literature_and_Writing,

        [Description("Allmän")]
        General
    }
}
