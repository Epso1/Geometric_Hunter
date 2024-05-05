using System.Collections;
using UnityEngine;
using TMPro;
using Firebase.Extensions;
using Firebase.Auth;
using Firebase;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class LogInManager : MonoBehaviour
{
    #region variables
    [Header("Login")]
    public TMP_InputField loginEmail;
    public TMP_InputField loginPassword;

    [Header("Sign up")]
    public TMP_InputField signUpEmail;
    public TMP_InputField signUpPassword;
    public TMP_InputField signUpPasswordConfirm;

    [Header("Extra")]
    public GameObject loadingScreen;
    public TextMeshProUGUI logTxt, successTxt;

    [Header("Reset pass")]
    public TMP_InputField resetEmail;
    #endregion

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
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }
            // Firebase user has been created.

            loadingScreen.SetActive(false);
            AuthResult result = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);


            if (result.User.IsEmailVerified)
            {
                UnityEngine.Debug.Log("Email is verified.");
                showLogMsg("Sign up Successful");
            }
            else
            {
                UnityEngine.Debug.Log("Email is not verified.");
                showLogMsg("Please verify your email!");
                StartCoroutine(SendEmailForVerificationAsync());
            }

        });
    }


    public void SendEmailVerification()
    {
        UnityEngine.Debug.Log("Starting coroutine.");
        StartCoroutine(SendEmailForVerificationAsync());
    }

    public IEnumerator SendEmailForVerificationAsync()
    {
        UnityEngine.Debug.Log("Launching coroutine.");
        FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
        if (user != null)
        {
            var sendEmailTask = user.SendEmailVerificationAsync();
            yield return new WaitUntil(() => sendEmailTask.IsCompleted);

            if (sendEmailTask.Exception != null)
            {
                print("Email send error");
               
            }
            else
            {
                print("Email successfully send");
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
                logTxt.text = "Invalid email or password, please try again.";
                loadingScreen.SetActive(false);
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInAndRetrieveDataWithCredentialAsync encountered an error: " + task.Exception);
                logTxt.text = "Invalid email or password, please try again.";
                loadingScreen.SetActive(false);
                return;
            }
            loadingScreen.SetActive(false);
            AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", result.User.DisplayName, result.User.UserId);

            if (result.User.IsEmailVerified)
            {
                showLogMsg("Log in Successful");

                successTxt.enabled = true;
                successTxt.text = "SUCCESS!\nId: " + result.User.UserId;
            }
            else
            {
                showLogMsg("Please verify email!!");
            }

        });

    }
    public void SendPasswordResetEmail()
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
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SendPasswordResetEmailAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("Password reset email sent successfully.");
            });
        }
    }
    #endregion

    #region extra
    void showLogMsg(string msg)
    {
        logTxt.text = msg;
        logTxt.GetComponent<Animation>().Play("textFadeout");
    }
    #endregion

}
