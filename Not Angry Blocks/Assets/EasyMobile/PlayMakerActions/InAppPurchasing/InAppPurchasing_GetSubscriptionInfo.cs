#if PLAYMAKER
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

#if EM_UIAP
using UnityEngine.Purchasing;
#endif

namespace EasyMobile.PlayerMaker.Actions
{
    [ActionCategory("Easy Mobile - In-App Purchasing")]
    [Tooltip("Gets information of a subscription product such as expire date using Unity IAP's SubscriptionManager class. " +
        "If the product with the given name cannot be found or the product is not subscription-based, the action will fail.")]
    public class InAppPurchasing_GetSubscriptionInfo : FsmStateAction
    {
        [Tooltip("The name of the product.")]
        [RequiredField]
        public FsmString productName;

        [ActionSection("Result")]

        [Tooltip("Event sent if no error occurs.")]
        public FsmEvent isSuccessEvent;

        [Tooltip("Event sent if an error occurs.")]
        public FsmEvent isNotSuccessEvent;

        [Tooltip("Where to sent event.")]
        public FsmEventTarget eventTarget;

        [Tooltip("True if no error occurs.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isSuccess;

        [Tooltip("The error description if any.")]
        [UIHint(UIHint.Variable)]
        public FsmString errorDescription;

        [ActionSection("Retrieved Subscription Info")]

        [Tooltip("The product ID.")]
        [UIHint(UIHint.Variable)]
        public FsmString productId;

        [Tooltip("Whether this subscription is subscribed.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isSubscribed;

        [Tooltip("Whether this subscription is auto renewing.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isAutoRenewing;

        [Tooltip("Whether this subscription is cancelled.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isCancelled;

        [Tooltip("Whether this subscription is expired.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isExpired;

        [Tooltip("Whether this subscription is in free trial.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isFreeTrial;

        [Tooltip("Whether this subscription is in introductory price period.")]
        [UIHint(UIHint.Variable)]
        public FsmBool isIntroductoryPricePeriod;

        [Tooltip("The purchase date represented as a string in MM/dd/yyyy HH:mm format.")]
        [UIHint(UIHint.Variable)]
        public FsmString purchaseDate;

        [Tooltip("The cancel date represented as a string in MM/dd/yyyy HH:mm format.")]
        [UIHint(UIHint.Variable)]
        public FsmString cancelDate;

        [Tooltip("The expire date represented as a string in MM/dd/yyyy HH:mm format.")]
        [UIHint(UIHint.Variable)]
        public FsmString expireDate;

        [Tooltip("The free trial period represented as total seconds.")]
        [UIHint(UIHint.Variable)]
        public FsmFloat freeTrialPeriod;

        [Tooltip("The introductory price string.")]
        [UIHint(UIHint.Variable)]
        public FsmString introductoryPrice;

        [Tooltip("The introductory price period represented as total seconds.")]
        [UIHint(UIHint.Variable)]
        public FsmFloat introductoryPricePeriod;

        [Tooltip("The introductory price period cycles.")]
        [UIHint(UIHint.Variable)]
        public FsmInt introductoryPricePeriodCyles;

        [Tooltip("The remaining time represented as total seconds.")]
        [UIHint(UIHint.Variable)]
        public FsmFloat remainingTime;

        [Tooltip("The SKU details.")]
        [UIHint(UIHint.Variable)]
        public FsmString skuDetails;

        [Tooltip("The subscription period represented as total seconds.")]
        [UIHint(UIHint.Variable)]
        public FsmFloat subscriptionPeriod;

        [Tooltip("The subscription info JSON string.")]
        [UIHint(UIHint.Variable)]
        public FsmString subscriptionInfoJsonString;

        public const string DateTimeStringFormat = "MM/dd/yyyy HH:mm";

        public override void Reset()
        {
            productName = null;
            isSuccessEvent = null;
            isNotSuccessEvent = null;
            eventTarget = null;
            isSuccess = null;
            errorDescription = null;

            productId = null;
            cancelDate = null;
            expireDate = null;
            freeTrialPeriod = null;
            introductoryPrice = null;
            introductoryPricePeriod = null;
            introductoryPricePeriodCyles = null;    
            purchaseDate = null;
            remainingTime = null;
            skuDetails = null;
            subscriptionPeriod = null;
            isAutoRenewing = null;
            isSubscribed = null;
            isCancelled = null;
            isExpired = null;
            isFreeTrial = null;
            isIntroductoryPricePeriod = null;   
        }

        public override void OnEnter()
        {
            #if EM_UIAP

            var p = InAppPurchasing.GetIAPProductByName(productName.Value);

            if (p == null)
            {
                isSuccess.Value = false;
                errorDescription.Value = "Product not found.";
                Fsm.Event(eventTarget, isNotSuccessEvent);
                Finish();
            }

            if (p.Type != IAPProductType.Subscription)
            {
                isSuccess.Value = false;
                errorDescription.Value = "Product is not subscription-based.";
                Fsm.Event(eventTarget, isNotSuccessEvent);
                Finish();
            }

            var info = InAppPurchasing.GetSubscriptionInfo(productName.Value);

            if (info != null)
            {
                productId.Value = info.getProductId();
                cancelDate.Value = info.getCancelDate().ToString(DateTimeStringFormat);
                expireDate.Value = info.getExpireDate().ToString(DateTimeStringFormat);
                freeTrialPeriod.Value = (float)info.getFreeTrialPeriod().TotalSeconds;
                introductoryPrice.Value = info.getIntroductoryPrice();
                introductoryPricePeriod.Value = (float)info.getIntroductoryPricePeriod().TotalSeconds;
                introductoryPricePeriodCyles.Value = (int)info.getIntroductoryPricePeriodCycles();    
                purchaseDate.Value = info.getPurchaseDate().ToString(DateTimeStringFormat);
                remainingTime.Value = (float)info.getRemainingTime().TotalSeconds;
                skuDetails.Value = info.getSkuDetails();
                subscriptionPeriod.Value = (float)info.getSubscriptionPeriod().TotalSeconds;
                isAutoRenewing.Value = info.isAutoRenewing() == Result.True;
                isSubscribed.Value = info.isSubscribed() == Result.True;
                isCancelled.Value = info.isCancelled() == Result.True;
                isExpired.Value = info.isExpired() == Result.True;
                isFreeTrial.Value = info.isFreeTrial() == Result.True;
                isIntroductoryPricePeriod.Value = info.isIntroductoryPricePeriod() == Result.True;   

                isSuccess.Value = true;
                Fsm.Event(eventTarget, isSuccessEvent);
                Finish();
            }
            else
            {
                isSuccess.Value = false;
                errorDescription.Value = "Couldn't get subscription info: unknown error.";
                Fsm.Event(eventTarget, isNotSuccessEvent);
                Finish();
            }
            #else
            isSuccess.Value = false;
            errorDescription.Value = "In-App Purchasing module is not enabled.";
            Fsm.Event(eventTarget, isNotSuccessEvent);
            Finish();    
            #endif
        }
    }
}
#endif

