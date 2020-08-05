#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Advertising")]
    [Tooltip("Revokes the provider-level data privacy consent of the specified ad network.")]
    public class Advertising_RevokeAdNetworkDataPrivacyConsent : FsmStateAction
    {
        [Tooltip("The ad network to revoke the consent.")]
        [ObjectType(typeof(AdNetwork))]
        public FsmEnum adNetwork;

        public override void OnEnter()
        {
            Advertising.RevokeDataPrivacyConsent((AdNetwork)adNetwork.Value);
            Finish();
        }
    }
}
#endif

