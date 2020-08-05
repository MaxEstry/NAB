#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Advertising")]
    [Tooltip("Grants the provider-level data privacy consent for the specified ad network.")]
    public class Advertising_GrantAdNetworkDataPrivacyConsent : FsmStateAction
    {
        [Tooltip("The ad network to grant the consent.")]
        [ObjectType(typeof(AdNetwork))]
        public FsmEnum adNetwork;

        public override void OnEnter()
        {
            Advertising.GrantDataPrivacyConsent((AdNetwork)adNetwork.Value);
            Finish();
        }
    }
}
#endif

