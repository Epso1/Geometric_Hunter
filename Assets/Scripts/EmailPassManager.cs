using System.Collections;
using UnityEngine;
using TMPro;
using System;
using Firebase.Extensions;
using Firebase.Auth;
using Firebase;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.SceneManagement;

public class EmailPassManager : MonoBehaviour
{
    #region variables
    [Header("Login")]
    public TMP_InputField loginEmail;
    public TMP_InputField loginPassword;

    [Header("Sign up")]
    public TMP_InputField signUpEmail;
    public TMP_InputField signUpPassword;

    [Header("Extra")]
    public GameObject loadingScreen;
    public TextMeshProUGUI logTxt;
    public GameObject loginUi, signupUi, inputNameUI, checkEmailUI, resetPasswordUI;
    [SerializeField] private GameObject imageFadeOut;
    [SerializeField] private GameObject imageFadeIn;
    [SerializeField] private float fadeDuration = 1.0f;

    [Header("Reset pass")]
    public TMP_InputField resetEmail;

    [Header("Input name")]
    public TMP_InputField inputName;
    public string homeSceneName;

    private string tempUserID;
    private string tempUserEmail;
    #endregion

    void Start()
    {
        imageFadeIn.SetActive(true);
    }

    #region signup 
    public void SignUp()
    {
        loadingScreen.SetActive(true);

        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        string email = signUpEmail.text;
        string password = signUpPassword.text;
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                loadingScreen.SetActive(false);
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                showLogMsg("User creation was cancelled.");
                return;
            }
            if (task.IsFaulted)
            {
                loadingScreen.SetActive(false);
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);

                foreach (Exception e in task.Exception.InnerExceptions)
                {
                    Firebase.FirebaseException firebaseEx = e as Firebase.FirebaseException;
                    if (firebaseEx != null)
                    {
                        switch ((AuthError)firebaseEx.ErrorCode)
                        {
                            case AuthError.EmailAlreadyInUse:
                                showLogMsg("User already exists.");
                                break;
                            case AuthError.WeakPassword:
                                showLogMsg("Weak password. Please choose a stronger password.");
                                break;
                            case AuthError.NetworkRequestFailed:
                                showLogMsg("Connection lost. Please check your internet connection.");
                                break;
                            default:
                                showLogMsg("An error occurred: " + firebaseEx.Message);
                                break;
                        }
                    }
                }
                return;
            }

 
            loadingScreen.SetActive(false);
            AuthResult result = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);

            signUpEmail.text = "";
            signUpPassword.text = "";

            if (result.User.IsEmailVerified)
            {
                showLogMsg("Sign up Successful");
            }
            else
            {
                SendEmailVerification();
            }

        });
    }

    
    public void SendEmailVerification()
    {
        StartCoroutine(SendEmailForVerificationAsync());
    }

    IEnumerator SendEmailForVerificationAsync()
    {
        FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
        if (user != null)
        {
            var sendEmailTask = user.SendEmailVerificationAsync();
            yield return new WaitUntil(() => sendEmailTask.IsCompleted);

            if (sendEmailTask.Exception != null)
            {
                print("Email send error");
                showLogMsg("Email send error!");
            }
            else
            {
                print("Email successfully send");
                showLogMsg("Email successfully send!");
                signupUi.SetActive(false);
                checkEmailUI.SetActive(true);
            }
        }
    }
    public void SendPasswordResetEmail()
    {
        StartCoroutine(SendPasswordResetEmailAsync());
    }

    IEnumerator SendPasswordResetEmailAsync()
    {
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
        string emailAddress = resetEmail.text;
        if (user != null)
        {
            var sendEmailTask = auth.SendPasswordResetEmailAsync(emailAddress);
            yield return new WaitUntil(() => sendEmailTask.IsCompleted);

            if (sendEmailTask.Exception != null)
            {
                string errorMessage = "Email send error: " + sendEmailTask.Exception.ToString();
                print(errorMessage);
                if (errorMessage.Contains("badly formatted"))
                {
                    showLogMsg("Email badly formatted!");
                }
                else
                {
                    showLogMsg("Email send error!");
                }
              
            }
            else
            {
                print("Email successfully send");
                showLogMsg("Password reset email sent successfully.");
                checkEmailUI.SetActive(true);
                resetPasswordUI.SetActive(false);
            }
        }
    }

    #endregion

    #region Login
    public void Login()
    {
        loadingScreen.SetActive(true);

        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        string email = loginEmail.text;
        string password = loginPassword.text;

        Credential credential =
        EmailAuthProvider.GetCredential(email, password);
        auth.SignInAndRetrieveDataWithCredentialAsync(credential).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInAndRetrieveDataWithCredentialAsync was canceled.");
                showLogMsg("Invalid email or password, please try again.");
                loadingScreen.SetActive(false);
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInAndRetrieveDataWithCredentialAsync encountered an error: " + task.Exception);
                showLogMsg("Invalid email or password, please try again.");
                loadingScreen.SetActive(false);
                return;
            }
            loadingScreen.SetActive(false);
            AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);

            if (result.User.IsEmailVerified)
            {
                tempUserID = result.User.UserId;
                tempUserEmail = result.User.Email; 
                loginUi.SetActive(false);
                               
                showLogMsg("Log in successful!");
                StartCoroutine(CheckPlayerDataRoutine(tempUserID));    

            }
            else
            {
                showLogMsg("Please verify email!!");
            }

        });

    }

   

    public void SendPasswordResetEmail2()
    {
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
        string emailAddress = resetEmail.text;
        if (user != null)
        {
            auth.SendPasswordResetEmailAsync(emailAddress).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("SendPasswordResetEmailAsync was canceled.");
                    showLogMsg("Send password reset email was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SendPasswordResetEmailAsync encountered an error: " + task.Exception);
                    showLogMsg("Send password reset email encountered an error.");
                    return;
                }
                
                Debug.Log("Password reset email sent successfully.");
                checkEmailUI.SetActive(true);
                resetPasswordUI.SetActive(false);
                showLogMsg("Password reset email sent successfully.");
               
                
                
            });
        }
    }
    #endregion

    #region extra
    void showLogMsg(string msg)
    {
        logTxt.text = msg;
    }

    public void ValidateInputName()
    {
        DataManager.Instance.CreatePlayerData(tempUserID, inputName.text);
        StartCoroutine(LoadHomeScene());
    }

    private IEnumerator CheckPlayerDataRoutine(string tempUserID)
    {
        yield return StartCoroutine(DataManager.Instance.CheckPlayerDataEnum(tempUserID));

        // Después de que la corrutina haya terminado, procede con la comprobación
        if (DataManager.Instance.dataFound == true)
        {
            DataManager.Instance.LoadPlayerData(tempUserID);
            StartCoroutine(LoadHomeScene());
        }
        else
        {
            inputNameUI.SetActive(true);
        }
    }

    private IEnumerator LoadHomeScene()
    {
        imageFadeOut.SetActive(true);
        yield return new WaitForSeconds(fadeDuration);
        SceneManager.LoadScene(homeSceneName);
    }
    public void EraseLogTxt()
    {
        logTxt.text = "";
    }

    private IEnumerator EraseLogText(float wait)
    {
        yield return new WaitForSeconds(wait);
        logTxt.text = "";

    }
    public void EraseLogInInputs()
    {
        loginEmail.text = "";
        loginPassword.text = "";
    }

    public void ExitGame()
    {
        StartCoroutine(ExitGameEnum());
    }

    private IEnumerator ExitGameEnum()
    {
        yield return new WaitForSecondsRealtime(1f);
        Application.Quit();
    }
    #endregion

}
