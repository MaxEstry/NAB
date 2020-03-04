using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using EasyMobile.Demo;
using UnityEngine.SceneManagement;

namespace EasyMobile.PlayerMaker.Demo
{
    public class DemoUtils_TurnBasedManager : MonoBehaviour
    {
        #region Inner classes               

        private Action<bool> GetAlertCallbackAction(string successMessage, string failedMessage)
        {
            return success =>
            {
                var title = success ? "Success" : "Fail";
                NativeUI.Alert(title, success ? successMessage : failedMessage);
            };
        }



        public Participant SelectedParticipant
        {
            get
            {
                if (CurrentOpponents == null)
                    return null;

                var index = participantsDropdown.value;
                if (index < 0 || index >= CurrentOpponents.Length)
                    return null;

                return CurrentOpponents[index];
            }
        }

        public bool SelectedParticipantLeftMatch
        {
            get
            {
                var participant = SelectedParticipant;
                if (participant == null)
                    return false;

                return participant.Status == Participant.ParticipantStatus.Left ||
                       participant.Status == Participant.ParticipantStatus.Done;
            }
        }

        public bool CanRematch
        {
            get
            {
                return CurrentMatch != null && CurrentMatch.Status == TurnBasedMatch.MatchStatus.Ended;
            }
        }


        public bool IsMyTurn
        {
            get
            {
                if (CurrentMatch == null)
                    return false;

                return CurrentMatch.IsMyTurn && canTakeTurn;
            }
        }


        #endregion

        #region Fields & Properties
        private const string MatchFinishedMessage = "Click the \"Acknowledge Finished Match\" button to call \"AcknowledgeFinished\" method " +
                                               "to acknowledge that the user has seen the results of the finished match. " +
                                               "This will remove the match from the user's inbox.";

        private const string NullMatchMessage = "Please create a match first.";

        private const string ExpiritedMatchMessage = "[Google Play Games only]\n" +
            "The match expired. A match expires when a user does not respond to an invitation or turn notification for two weeks. " +
            "A match also expires in one day if there is an empty auto-match slot available " +
            "but Google Play games services cannot find a user to auto-match.";

        private const string ExpiredMatchMessage = "[Google Play Games only]\n" +
                                                 "The match expired. A match expires when a user does not respond to an invitation or turn notification for two weeks. " +
                                                 "A match also expires in one day if there is an empty auto-match slot available " +
                                                 "but Google Play games services cannot find a user to auto-match.";

        private const string DeletedMatchMessage = " [Google Play Games only]\nThe match has been deleted.";

        private const string CancelledMatchMessage = "[Google Play Games only]\n" +
            "The match was cancelled by one of the participants. " +
            "This might occur, for example, " +
            "if a user who was invited to the match declined the invitation or if a participant explicitly cancels the match. " +
            "Google Play games services allows participants to cancel the match at any point after joining a match " +
            "(if you game interface supports this action).";

        [SerializeField]
        private LoadingPanelController loadingPanel = null;

        [SerializeField]
        private DemoUtils demoUtils = null;

        [SerializeField]
        private Dropdown participantsDropdown = null;


        [SerializeField]
        private GameObject isMyTurnUI = null;

        public TurnBasedMatch CurrentMatch { get; set; }
        public GameServicesDemo_Multiplayer_TurnbasedKitchenSink.MatchData CurrentMatchData { get; set; }
        public Participant[] CurrentOpponents { get; set; }

        public bool canTakeTurn = false;

        #endregion

        #region Methods

        protected virtual IEnumerator Start()
        {
            while (!GameServices.IsInitialized() || Application.isEditor)
            {
                if (!loadingPanel.IsShowing)
                {
                    loadingPanel.SetMessageText(Application.isEditor ? "Please test on a real mobile device." : "Wait until authenticated or exit.");
                    loadingPanel.SetButtonLabel("Exit");
                    loadingPanel.RegisterButtonCallback(() => { SceneManager.LoadScene("GameServicesDemo_Multiplayer_Playmaker"); });
                    loadingPanel.Show();
                }
                yield return null;
            }

            loadingPanel.Hide();
            RefreshParticipantsDropDown();
        }

        void FixedUpdate()
        {
            demoUtils.DisplayBool(isMyTurnUI, IsMyTurn, "Your turn");
        }

        public string GetParticipantDisplayString(Participant participant)
        {
            if (participant == null)
                return "null\n";

            string result = "[Participant]\n";
            result += "DisplayName: " + participant.DisplayName + "\n";
            result += "IsConnectedToRoom: " + participant.IsConnectedToRoom + "\n";
            result += "ParticipantId: " + participant.ParticipantId + "\n";
            result += "Status: " + participant.Status + "\n";

            if (participant.Player == null)
            {
                result += "Player: null\n";
            }
            else
            {
                result += "Player.id: " + participant.Player.id + "\n";
                result += "Player.isFriend: " + participant.Player.isFriend + "\n";
                result += "Player.state: " + participant.Player.state + "\n";
                result += "Player.userName: " + participant.Player.userName + "\n";
            }

            return result;
        }


        public string GetMatchDisplayString(TurnBasedMatch match)
        {
            if (match == null)
                return "null\n";

            string result = "[TurnBasedMatch]\n";
            result += "MatchId: " + match.MatchId + "\n";
            result += "Status: " + match.Status + "\n";
            result += "CurrentParticipantId: " + match.CurrentParticipantId + "\n";
            result += "SelfParticipantId: " + match.SelfParticipantId + "\n";
            result += "PlayerCount: " + match.PlayerCount + "\n";
            result += "IsMyTurn: " + match.IsMyTurn + "\n";
            result += "Has Data: " + (match.Data != null) + "\n";
            result += "Data size: " + (match.Data != null ? match.Data.Length : 0) + "\n";

            return result;
        }

        public void RefreshParticipantsDropDown()
        {
            participantsDropdown.ClearOptions();

            if (CurrentOpponents == null)
                return;

            var options = new List<Dropdown.OptionData>();
            foreach (var participant in CurrentOpponents)
            {
                if (participant.ParticipantId != CurrentMatch.SelfParticipantId)
                {
                    var displayName = string.Format("<b><i>[{0}]</i></b>{1}", participant.Status, participant.DisplayName);
                    options.Add(new Dropdown.OptionData(displayName));
                }
            }

            var openSlots = CurrentMatch.PlayerCount - CurrentOpponents.Length - 1;
            for (int i = 0; i < openSlots; i++)
                options.Add(new Dropdown.OptionData("Auto-match slot"));

            participantsDropdown.AddOptions(options);
        }

        public void OnMatchReceived(TurnBasedMatch match, bool autoLaunch, bool playerWantsToQuit)
        {
            if (match == null)
            {
                NativeUI.Alert("Error", "Received a null match.");
                return;
            }

            // This only happens on Game Center platform when the local player
            // removes the match in the matches UI while being the turn holder.
            // We'll end the local player's turn and pass the match to a next
            // participant. If there's no active participant left we'll end the match.
            if (playerWantsToQuit)
            {
                if (match.HasVacantSlot)
                {
                    GameServices.TurnBased.LeaveMatchInTurn(match, string.Empty, null);
                    return;
                }

                var nextParticipant = match.Participants.FirstOrDefault(
                                          p => p.ParticipantId != match.SelfParticipantId &&
                                          (p.Status == Participant.ParticipantStatus.Joined ||
                                          p.Status == Participant.ParticipantStatus.Invited ||
                                          p.Status == Participant.ParticipantStatus.Matching));

                if (nextParticipant != default(Participant))
                {
                    GameServices.TurnBased.LeaveMatchInTurn(
                        match,
                        nextParticipant.ParticipantId,
                        null
                    );
                }
                else
                {
                    // No valid next participant, match ends here.
                    // In this case we'll set the outcome for all players as Tied for demo purpose.
                    // In a real game you may determine the outcome based on the game data and your game logic.
                    MatchOutcome outcome = new MatchOutcome();
                    foreach (var id in match.Participants.Select(p => p.ParticipantId))
                    {
                        var result = MatchOutcome.ParticipantResult.Tied;
                        outcome.SetParticipantResult(id, result);
                    }
                    GameServices.TurnBased.Finish(match, match.Data, outcome, null);
                }

                return;
            }

            if (CurrentMatch != null && CurrentMatch.MatchId != match.MatchId)
            {
                var alert = NativeUI.ShowTwoButtonAlert("Received Different Match",
                                "A different match has been arrived, do you want to replace it with the current one?", "Yes", "No");

                if (alert != null)
                {
                    alert.OnComplete += button =>
                    {
                        if (button == 0)
                            CheckAndPlayMatch(match, playerWantsToQuit);
                    };
                    return;
                }

                CheckAndPlayMatch(match, playerWantsToQuit);
                return;
            }

            CheckAndPlayMatch(match, playerWantsToQuit);
        }

        private void CheckAndPlayMatch(TurnBasedMatch match, bool playerWantsToQuit)
        {
            if (match.Data != null && match.Data.Length > 0 && GameServicesDemo_Multiplayer_TurnbasedKitchenSink.MatchData.FromByteArray(match.Data) == null)
            {
                NativeUI.Alert("Error", "The arrived match can't be opened in this scene. You might want to open it in the TicTacToe demo instead.");
                return;
            }

            CurrentMatch = match;
            CurrentOpponents = CurrentMatch.Participants.Where(p => p.ParticipantId != CurrentMatch.SelfParticipantId).ToArray();
            RefreshParticipantsDropDown();
            canTakeTurn = true;

            if (CurrentMatch.Data == null || CurrentMatch.Data.Length < 1) /// New game detected...
                CurrentMatchData = new GameServicesDemo_Multiplayer_TurnbasedKitchenSink.MatchData() { TurnCount = 0 };
            else
                CurrentMatchData = GameServicesDemo_Multiplayer_TurnbasedKitchenSink.MatchData.FromByteArray(CurrentMatch.Data);

            if (CurrentMatch.Status == TurnBasedMatch.MatchStatus.Ended)
            {
                canTakeTurn = false;
                var result = string.Format("Winner: {0}\nTurnCount: {1}\n\n", CurrentMatchData.WinnerName ?? "null", CurrentMatchData.TurnCount);
                NativeUI.Alert("Finished Match Arrived", result + MatchFinishedMessage + "\n\nMatch info:\n" + GetMatchDisplayString(CurrentMatch));
                return;
            }
            else if (CurrentMatch.Status == TurnBasedMatch.MatchStatus.Cancelled)
            {
                NativeUI.Alert("Cancelled Match Arrived", CancelledMatchMessage);
                return;
            }
            else if (CurrentMatch.Status == TurnBasedMatch.MatchStatus.Deleted)
            {
                NativeUI.Alert("Deleted Match Arrived", DeletedMatchMessage);
                return;
            }
            else if (CurrentMatch.Status == TurnBasedMatch.MatchStatus.Expired)
            {
                NativeUI.Alert("Expired Match Arrived", ExpiredMatchMessage);
                return;
            }

            var opponent = CurrentMatch.Participants.Where(p => p.ParticipantId != CurrentMatch.SelfParticipantId).FirstOrDefault();
            if (opponent != default(Participant) &&
                (opponent.Status == Participant.ParticipantStatus.Done || opponent.Status == Participant.ParticipantStatus.Left))
            {
                NativeUI.Alert("Game Over", "You won. Your opponent has left the match.");
                return;
            }

            CurrentMatchData.TurnCount++;
            NativeUI.Alert("Match Arrived", "New match data has been arrived:\n" + GetMatchDisplayString(match));
        }

        #endregion
    }
}