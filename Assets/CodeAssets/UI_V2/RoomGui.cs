using Assets.CodeAssets.UI.Subscreens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeAssets.UI_V2
{

    /// <summary>
    /// This class displays a set of icons, to be filled in with the currently-selected characters for the run.
    /// It also includes buttons that represent the options of abandoning the run (and thus all rewards), vs continuing on.
    /// Finally, each Room contains two icons representing stuff that can be purchased from that room-- these are mostly Abnormality cards
    /// that are mostly negative cursed objects but provide money at the end of the run, generating an implicit push-your-luck mechanic.
    /// </summary>
    public class RoomGui : MonoBehaviour
    {
        public List<ShortCharacterShowoffPanel> CharacterRosterForThisRun;

        public Button StartNextBattleButton;
        public Button AbandonRunPanel;
    }
}
