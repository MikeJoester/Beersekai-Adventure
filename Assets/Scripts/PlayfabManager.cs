using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayfabManager : MonoBehaviour
{
    bool Available = false;

    [Header("UI")]
    public Text messageText;
    public InputField emailInput;
    public InputField passwordInput;
    string PlName = null;

    [Header("Windows")]
    public GameObject nameWindow;
    public GameObject LeaderboardWindow;

    [Header("DisplayNameWindow")]
    public GameObject nameError;
    public InputField nameInput;

    [Header("Leaderboard")]
    public GameObject leaderboardPanel;
    public GameObject listingPrefab;
    public Transform listingContainer;
    static string LeaderboardName;
    bool isEnabled;

    public void RegisterButton()
    {
        if (passwordInput.text.Length < 8)
        {
            messageText.text = "Password too short! Must be 8 characters or above.";
            return;
        }
        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        nameWindow.SetActive(true);
        messageText.text = "Registered and logged in!";
    }

    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    public void GuestLogin()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginGuestSuccess, OnError);
    }

    void OnLoginGuestSuccess(LoginResult result)
    {
        messageText.text = "Login successful!";
        Debug.Log("Successful login as Guest!");
        PlName = "Guest";
        SceneManager.LoadScene("GameMode");
        PlayerPrefs.SetString("name", PlName);
    }

    public void ResetPasswordButton()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailInput.text,
            TitleId = "70D4C"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, ResetPasswordError);
    }

    void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        messageText.text = "Password reset mail sent!";
    }

    void ResetPasswordError(PlayFabError error)
    {
        messageText.text = "Email not found!";
    }

    void OnLoginSuccess(LoginResult result)
    {
        messageText.text = "Login successful!";
        Debug.Log("Successful login!");
        
        if (result.InfoResultPayload.PlayerProfile != null)
        {
            PlName = result.InfoResultPayload.PlayerProfile.DisplayName;
        }

        if (PlName == null)
        {
            nameWindow.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("GameMode");
            PlayerPrefs.SetString("name", PlName);
        }
    }

    public void SubmitNameButton()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = nameInput.text
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        PlayerPrefs.SetString("name", PlName);
        SceneManager.LoadScene("GameMode");
    }

    static void OnError(PlayFabError error)
    {
        var textres = new PlayfabManager();
        textres.messageText = GameObject.Find("Message").GetComponent<Text>();
        textres.messageText.text = "Cant login lmao";
        Debug.Log(error.GenerateErrorReport());
    }

    public static void SendLeaderBoard(int score)
    {
        if (SceneManager.GetActiveScene().name == "TimeAttack")
        {
            LeaderboardName = "TimeAttackHScore";
            print("Sent Time Attack");
        }
        if (SceneManager.GetActiveScene().name == "Multiplayer")
        {
            LeaderboardName = "MultiplayerHScore";
            print("Sent Multiplayer");
        }
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = LeaderboardName,
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    static void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successful leaderboard sent!");
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = LeaderboardName,
            StartPosition = 0,
            MaxResultsCount = 10
        };

        if (!Available)
        {
            PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
        }
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (var item in result.Leaderboard)
        {
            GameObject newBoard = Instantiate(listingPrefab, listingContainer);
            LeaderboardListing listing = newBoard.GetComponent<LeaderboardListing>();
            listing.PlayerNum.text = (item.Position + 1).ToString();
            listing.PlayerName.text = item.DisplayName;
            listing.PlayerScore.text = item.StatValue.ToString();
            //Debug.Log(string.Format("PLACE: {0} | NAME: {1} | VALUE:{2}", item.Position, item.DisplayName, item.StatValue));
        }
        Animator animator = leaderboardPanel.GetComponent<Animator>();
        //isEnabled = animator.GetBool("openState");
        animator.SetBool("ToggleState", true);
        Available = true;
    }

    public void CloseLBPanel()
    {
        Animator anim = leaderboardPanel.GetComponent<Animator>();
        anim.SetBool("ToggleState", false);

        for (int i = listingContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(listingContainer.GetChild(i).gameObject);
        }
        Available = false;
    }
}
