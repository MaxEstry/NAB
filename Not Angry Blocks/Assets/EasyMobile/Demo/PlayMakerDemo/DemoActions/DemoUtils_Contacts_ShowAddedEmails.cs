#if PLAYMAKER
using UnityEngine;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;
using EasyMobile.PlayerMaker.Actions.Internal;
using EasyMobile.Demo;

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - Demo Actions")]
    [Tooltip("Show All Emails.")]
    public class DemoUtils_Contacts_ShowAddedEmails : FsmStateAction
    {
        [Tooltip("The Game Object contain collection View.")]
        public FsmGameObject collectionViewGameObject;

        [Tooltip("The Object that save all the emails.")]
        public FsmObject emailsObject;

        public override void Reset()
        {
            collectionViewGameObject = null;
        }

        public override void OnEnter()
        {
            StringStringCollectionView collectionView = collectionViewGameObject.Value.GetComponent<StringStringCollectionView>();
            AddedEmailsObject tempEmail = (AddedEmailsObject)emailsObject.Value;       
          
            if ((collectionView != null)&&(tempEmail.AddedEmails != null))
            {               
                collectionView.Show(tempEmail.AddedEmails, "Emails");
                Finish();
            }
            Finish();

        }
    }
}
#endif