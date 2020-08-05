using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

namespace EasyMobile.PlayerMaker.Demo
{
    public class DemoUtils_RealTime_Manager : DemoUtils_Multiplayer_BasedControl
    {
        #region Inner classes

        [Serializable]
        public class SampleData
        {
            public string Text { get; set; }

            public float Value { get; set; }

            public DateTime TimeStamp { get; set; }

            private byte[] dummyDatas = new byte[0];

            public int GetSize()
            {
                var bytes = ToByteArray();
                return bytes != null ? bytes.Length : 0;
            }

            public int GetDummySize()
            {
                return dummyDatas != null ? dummyDatas.Length : 0;
            }

            public int GetBaseSize()
            {
                return GetSize() - GetDummySize();
            }

            public void UpdateDummySize(int size)
            {
                if (size < 0)
                    return;

                dummyDatas = new byte[size];
            }

            public byte[] ToByteArray()
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(memoryStream, this);
                    return memoryStream.ToArray();
                }
            }

            public static SampleData FromByteArray(byte[] bytes)
            {
                if (bytes == null)
                    throw new ArgumentNullException();

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    memoryStream.Write(bytes, 0, bytes.Length);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    SampleData obj = (SampleData)binaryFormatter.Deserialize(memoryStream);
                    return obj;
                }
            }

            public override string ToString()
            {
                string result = "[SampleData]\n";
                result += "Text(string): " + Text + "\n";
                result += "Value(float): " + Value + "\n";
                result += "Timestamp(DateTime): " + TimeStamp + "\n";

                byte[] bytes = ToByteArray();
                result += "Size: " + (bytes != null ? bytes.Length.ToString() : "0") + " byte(s)";

                return result;
            }
        }

        #endregion


        private const string VariantHint = "The match variant. The meaning of this parameter is defined by the game. " +
                                           "It usually indicates a particular game type or mode(for example \"capture the flag\", \"first to 10 points\", etc). " +
                                           "Setting this value to 0 (default) allows the player to be matched into any waiting match. " +
                                           "Setting it to a nonzero number to match the player only with players whose match request shares the same variant number. " +
                                           "This value must be between 1 and 1023 (inclusive).";

        private const string ExclusiveBitmaskHint = "If your game has multiple player roles (such as farmer, archer, and wizard) " +
                                                    "and you want to restrict auto-matched games to one player of each role, " +
                                                    "add an exclusive bitmask to your match request.When auto-matching with this option, " +
                                                    "players will only be considered for a match when the logical AND of their exclusive bitmasks is equal to 0. " +
                                                    "In other words, this value represents the exclusive role the player making this request wants to play in the created match. " +
                                                    "If this value is 0 (default) it will be ignored. " +
                                                    "If you're creating a match with the standard matchmaker UI, this value will also be ignored.";

        private const string MinPlayersHint = "The minimum number of players that may join the match, " +
                                              "including the player who is making the match request. Must be at least 2 (default).";

        private const string MaxPlayersHint = "The maximum number of players that may join the match, " +
                                              "including the player who is making the match request. " +
                                              "Must be equal or greater than \"minPlayers\" and may be no more than the maximum number of players " +
                                              "allowed for the turnbased type. Default value is 2.";

        private const string CreateQuickMatchHint = "Start a game with randomly selected opponent(s).";

        private const string CreateWithMatchmakerUIHint = "Start a game with built-in invitation screen.";

        private const string AcceptFromInboxHint = "Creates a real-time game starting with the inbox screen.";

        private const string SendMessageHint = "Send a message to a particular participant, " +
                                               "which can be selected via the \"Target Participant\" dropdown list above.";

        private const string SendMessageToAllHint = "Sends a message to all other participants.";

        private const string LeaveRoomHint = "Leaves the room.";

        private const string UseReliableMessageHint = "Determine if \"SendMessage\" and \"SendMessageToAll\" " +
                                                      "should send reliable or unreliable message. " +
                                                      "Unreliable messages are faster, but are not guaranteed to arrive and may arrive out of order.";

        private const string ShouldReinviteDisconnectedPlayerHint = "[Game Center only] " +
                                                                    "Called when a player in a two-player match was disconnected. " +
                                                                    "Your game should return \"true\" if it wants Game Kit to attempt to reconnect the player, " +
                                                                    "\"false\" if it wants to terminate the match.";

        private const string SampleDataHint = "These value will be sent to other participant(s) " +
                                              "when calling \"SendMessage\" or \"SendMessageToAll\". ";

        private const string ReceivedMessagesHint = "All the messages (or errors) received in " +
                                                    "\"OnRealTimeMessageReceived\" will be displayed here.";
        [SerializeField]
        private Button clearReceivedMessagesButton = null;
        [SerializeField]
        private Button variantHintButton = null,
          bitmaskHintButton = null,
          minPlayersHintButton = null,
          maxPlayersHintButton = null,
          createQuickMatchHintButton = null,
          createWithMatchmakerUIHintButton = null,
          acceptFromInboxHintButton = null,
          sendMessageHintButton = null,
          sendMessageToAllHintButton = null,
          leaveRoomHintButton = null,
          sampleDataHintButton = null,
          useReliableMessageHintButton = null,
          reiniviteDisconnectedPlayerHintButton = null,
          receivedMessagesHintButton = null;

        public bool isShouldInvite = false;

        private List<Participant> Opponents = new List<Participant>();

        [SerializeField]
        private Dropdown targetParticipantDropdown = null;

        [SerializeField]
        private GameObject matchRequestRoot = null,
                           matchCreationRoot = null,
                           ingameRoot = null;

        [SerializeField]
        private GameObject receivedMessagesRoot = null;

        [SerializeField]
        private Text receivedMessagesTextPrefab = null;


        [SerializeField]
        private Text finalSizeText = null;

        [SerializeField]
        private InputField sampleDataTextInputField = null,
            sampleDataValueInputField = null,
            dummySizeInputField = null;

        [SerializeField]
        private Scrollbar receivedMessagesVerticalScrollbar = null;

        [SerializeField]
        private Image createQuickMatchSpinningCircle = null;

        private List<Text> ReceivedMessages = new List<Text>();

        private bool isCreateQuickMatchSpinningUIShowing = false;
        private Coroutine createQuickMatchSpinningCoroutine = null;
        private float createQuickMatchSpinningAngle = 10, createQuickMatchSpinningFillSpeed = 0.002f;
        private int createQuickMatchRequest = 0;

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
            if (GameServices.RealTime.IsRoomConnected() == true)
            {
                ShowInGameUI();
            }
            else
            {
                ShowSetupUI();
            }

            InitTextField();
            InitBtn();
            RefreshFinalSizeText();
        }

        protected virtual void OnDestroy()
        {
            if (GameServices.IsInitialized())
                GameServices.RealTime.LeaveRoom();
        }


        private void ShowSetupUI()
        {
            matchRequestRoot.SetActive(true);
            matchCreationRoot.SetActive(true);
            ingameRoot.SetActive(false);
        }

        private void ShowInGameUI()
        {
            matchRequestRoot.SetActive(false);
            matchCreationRoot.SetActive(false);
            ingameRoot.SetActive(true);
            ClearReceivedMessagesText();
        }

        public void UpdateTargetParticipantDrowdown()
        {
            Debug.Log("Update Drop Down");

            ClearTargetParticipantDropdown();

            var connectedParticipant = GameServices.RealTime.GetConnectedParticipants();
            if (connectedParticipant == null || connectedParticipant.Count < 1)
                return;

            var self = GameServices.RealTime.GetSelf();
            if (self == null)
                return;

            var options = new List<Dropdown.OptionData>();
            foreach (var participant in connectedParticipant)
            {
                if (self.Equals(participant))
                    continue;

                options.Add(new Dropdown.OptionData(participant.DisplayName));
                Opponents.Add(participant);
            }
            targetParticipantDropdown.AddOptions(options);
            targetParticipantDropdown.value = 0;
        }

        public void ClearTargetParticipantDropdown()
        {
            targetParticipantDropdown.ClearOptions();
            Opponents.Clear();
            Debug.Log("Clear DropDown");
        }

        public void OnCreateQuickMatchBtnClick()
        {
            createQuickMatchRequest++;
            StartCreateQuickMatchSpinningUI();
        }

        public void StopSpinning()
        {
            createQuickMatchRequest--;

            if (createQuickMatchRequest <= 0)
                StopCreateQuickMatchSpinningUI();
        }


        private void InitBtn()
        {
            variantHintButton.onClick.AddListener(ShowVariantHint);
            bitmaskHintButton.onClick.AddListener(ShowExclusiveBitmaskHint);
            minPlayersHintButton.onClick.AddListener(ShowMinPlayersHint);
            maxPlayersHintButton.onClick.AddListener(ShowMaxPlayersHint);
            createQuickMatchHintButton.onClick.AddListener(ShowCreateQuickMatchHint);
            createWithMatchmakerUIHintButton.onClick.AddListener(ShowCreateWithmatchmakerHint);
            acceptFromInboxHintButton.onClick.AddListener(ShowAcceptFromInboxHint);
            sendMessageHintButton.onClick.AddListener(ShowSendMessageHint);
            sendMessageToAllHintButton.onClick.AddListener(ShowSendMessageToAllHint);
            leaveRoomHintButton.onClick.AddListener(ShowLeaveRoomHint);
            sampleDataHintButton.onClick.AddListener(ShowSampleDataHint);
            useReliableMessageHintButton.onClick.AddListener(ShowUseReliableMessageHint);
            reiniviteDisconnectedPlayerHintButton.onClick.AddListener(ShowShouldReinviteDisconnectedPlayerHint);
            receivedMessagesHintButton.onClick.AddListener(ShowReceivedMessagesHint);

            clearReceivedMessagesButton.onClick.AddListener(ClearReceivedMessagesText);
        }



        private void InitTextField()
        {
            sampleDataTextInputField.onEndEdit.AddListener(_ => RefreshFinalSizeText());
            sampleDataValueInputField.onEndEdit.AddListener(_ => RefreshFinalSizeText());
            dummySizeInputField.onEndEdit.AddListener(_ => RefreshFinalSizeText());
        }

        private void RefreshFinalSizeText()
        {
            var data = GetSentData();
            var size = data != null ? data.Length : 0;
            finalSizeText.text = "FINAL SIZE: " + size + "byte(s)";
        }

        private byte[] GetSentData()
        {
            float value = 0;
            if (!float.TryParse(sampleDataValueInputField.text, out value))
                return null;

            var sampleData = new SampleData()
            {
                TimeStamp = DateTime.UtcNow,
                Text = sampleDataTextInputField.text,
                Value = value,
            };

            int dummySize = 0;
            if (int.TryParse(dummySizeInputField.text, out dummySize))
                sampleData.UpdateDummySize(dummySize);

            return sampleData.ToByteArray();
        }

        public void AddReceivedMessage(string message)
        {
            var newMessage = Instantiate(receivedMessagesTextPrefab, receivedMessagesRoot.transform);
            newMessage.gameObject.SetActive(true);
            newMessage.text = message;
            ReceivedMessages.Add(newMessage);
            receivedMessagesVerticalScrollbar.value = 0f;
        }

        public void ClearReceivedMessagesText()
        {
            ReceivedMessages.ForEach(rm => Destroy(rm.gameObject));
            ReceivedMessages.Clear();
        }

        #region Show hint methods

        public void ShowVariantHint()
        {
            NativeUI.Alert("Variant", VariantHint);
        }

        public void ShowExclusiveBitmaskHint()
        {
            NativeUI.Alert("Exclusive Bitmask", ExclusiveBitmaskHint);
        }

        public void ShowMinPlayersHint()
        {
            NativeUI.Alert("Min players", MinPlayersHint);
        }

        public void ShowMaxPlayersHint()
        {
            NativeUI.Alert("Max players", MaxPlayersHint);
        }

        public void ShowCreateQuickMatchHint()
        {
            NativeUI.Alert("Create Quick Match", CreateQuickMatchHint);
        }

        public void ShowCreateWithmatchmakerHint()
        {
            NativeUI.Alert("Create With Matchmaker UI", CreateWithMatchmakerUIHint);
        }

        public void ShowAcceptFromInboxHint()
        {
            NativeUI.Alert("Accept From Inbox", AcceptFromInboxHint);
        }

        public void ShowSendMessageHint()
        {
            NativeUI.Alert("Send Message", SendMessageHint);
        }

        public void ShowSendMessageToAllHint()
        {
            NativeUI.Alert("Send Message To All", SendMessageToAllHint);
        }

        public void ShowLeaveRoomHint()
        {
            NativeUI.Alert("Leave Room", LeaveRoomHint);
        }

        public void ShowSampleDataHint()
        {
            NativeUI.Alert("Sample Data", SampleDataHint);
        }

        public void ShowUseReliableMessageHint()
        {
            NativeUI.Alert("Use Reliable Message", UseReliableMessageHint);
        }

        public void ShowShouldReinviteDisconnectedPlayerHint()
        {
            NativeUI.Alert("Should Re-invite Disconnected Player", ShouldReinviteDisconnectedPlayerHint);
        }

        public void ShowReceivedMessagesHint()
        {
            NativeUI.Alert("Received Message", ReceivedMessagesHint);
        }

        #endregion

        private void StartCreateQuickMatchSpinningUI()
        {
            if (isCreateQuickMatchSpinningUIShowing)
                return;

            createQuickMatchSpinningCircle.gameObject.SetActive(true);
            createQuickMatchSpinningCoroutine = StartCoroutine(CreateQuickMatchSpinningCoroutine());
            createQuickMatchHintButton.gameObject.SetActive(false);
            isCreateQuickMatchSpinningUIShowing = true;
        }

        private void StopCreateQuickMatchSpinningUI()
        {
            if (!isCreateQuickMatchSpinningUIShowing || createQuickMatchSpinningCoroutine == null)
                return;

            StopCoroutine(createQuickMatchSpinningCoroutine);
            createQuickMatchHintButton.gameObject.SetActive(true);
            isCreateQuickMatchSpinningUIShowing = false;
            createQuickMatchSpinningCircle.gameObject.SetActive(false);
        }

        private IEnumerator CreateQuickMatchSpinningCoroutine()
        {
            float fillAmount = 0f;
            while (true)
            {
                fillAmount += createQuickMatchSpinningFillSpeed;
                createQuickMatchSpinningCircle.fillAmount = Mathf.Repeat(fillAmount, 1);
                createQuickMatchSpinningCircle.rectTransform.Rotate(0, 0, createQuickMatchSpinningAngle);
                yield return null;
            }
        }

    }
}