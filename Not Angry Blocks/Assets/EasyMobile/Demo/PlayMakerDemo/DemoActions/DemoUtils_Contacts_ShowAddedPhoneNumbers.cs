#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Actions.Internal;
using EasyMobile.Demo;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    [Tooltip("Show All Phone Numbers.")]
    public class DemoUtils_Contacts_ShowAddedPhoneNumbers : FsmStateAction
    {
        [Tooltip("The Game Object contain collection View.")]
        public FsmGameObject collectionViewGameObject;

        [Tooltip("The Object that save all the phone numbers.")]
        public FsmObject phoneNumbersObject;

        public override void Reset()
        {
            collectionViewGameObject = null;
        }

        public override void OnEnter()
        {
            StringStringCollectionView collectionView = collectionViewGameObject.Value.GetComponent<StringStringCollectionView>();
            AddedPhoneNumbersObject tempPhoneNumbers = (AddedPhoneNumbersObject)phoneNumbersObject.Value;
                 
            if ((collectionView != null)&&(tempPhoneNumbers.AddedPhoneNumbers!=null))
            {
                collectionView.Show(tempPhoneNumbers.AddedPhoneNumbers, "Phone Numbers");
                Finish();
            }
            Finish();

        }
    }
}
#endif