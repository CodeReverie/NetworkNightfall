using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Auth; 
using System;
using System.Threading.Tasks;
using Firebase.Extensions;


public class FirebaseController : MonoBehaviour
{

    public GameObject loginPanel, signupPanel, profilePanel, forgetPasswordPanel, notificationPanel;
    public TextMeshProUGUI loginEmail, loginPassword, signupEmail, signupPassword, signupCPassword, signupUserName, forgetPassEmail, notifTitle, notifMessage, profileUserName, profileUserEmail;
    public TMP_InputField loginEmailf, loginPasswordf, signupEmailf, signupPasswordf, signupCPasswordf, signupUserNamef, forgetPassEmailf;
    public Toggle rememberMe;

    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

    bool isSignIn = false;
   
    void Start(){
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
        var dependencyStatus = task.Result;
        if (dependencyStatus == Firebase.DependencyStatus.Available) {
    // Create and hold a reference to your FirebaseApp,
    // where app is a Firebase.FirebaseApp property of your application class.
      InitializeFirebase();

    // Set a flag here to indicate whether Firebase is ready to use by your app.
  } else {
    UnityEngine.Debug.LogError(System.String.Format(
      "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
    // Firebase Unity SDK is not safe to use here.
  }
});
    }



     
    public void setName(){
    loginEmail.text = loginEmailf.text;
    loginPassword.text = loginPasswordf.text;
    signupEmail.text = signupEmailf.text;
    signupPassword.text = signupPasswordf.text;
    signupCPassword.text = signupCPasswordf.text;
    signupUserName.text = signupUserNamef.text;
    forgetPassEmail.text = forgetPassEmailf.text;
    }

    public void OpenLoginPanel()
    {
        loginPanel.SetActive(true);
        signupPanel.SetActive(false);
        profilePanel.SetActive(false);
        forgetPasswordPanel.SetActive(false);
        notificationPanel.SetActive(false);

    }
     public void OpenSignUpPanel()
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(true);
        profilePanel.SetActive(false);
        forgetPasswordPanel.SetActive(false);
        notificationPanel.SetActive(false);
    }
     public void OpenProfilePanel()
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(false);
        profilePanel.SetActive(true);
        forgetPasswordPanel.SetActive(false);
        notificationPanel.SetActive(false);
    }
     public void OpenForgetPassPanel()
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(false);
        profilePanel.SetActive(false);
        forgetPasswordPanel.SetActive(true);
        notificationPanel.SetActive(false);
    }
     






    public void LoginUser(){
        setName();
        if(string.IsNullOrEmpty(loginEmailf.text) && string.IsNullOrEmpty(loginPasswordf.text)){
            showNotificationMessage("Error", "forget Email Empty");
            return;
        }
        //Do Login
        SignInUser(loginEmail.text, loginPassword.text);
    }

    public void SignupUser(){

        setName();
        if (string.IsNullOrEmpty(signupEmailf.text) && string.IsNullOrEmpty(signupPasswordf.text) && string.IsNullOrEmpty(signupCPasswordf.text) && string.IsNullOrEmpty(signupUserNamef.text)){
            showNotificationMessage("Error", "forget Email Empty");
            return;
        }
        //Do Signup

        CreateUser(signupEmail.text, signupPassword.text, signupUserName.text);
    }


    public void forgetPass(){
        if (string.IsNullOrEmpty(forgetPassEmailf.text) ){
        showNotificationMessage("Error", "Fields empty. Input your details");
            return;
        }
        forgetPasswordSubmit(forgetPassEmail.text);
    }


     public void showNotificationMessage(string title, string message)
    {
        notifTitle.text = "" + title; 
        notifMessage.text = "" + message;
        
        notificationPanel.SetActive(true);
    }


    public void CloseNotif_Panel(){
          notifTitle.text = ""  ; 
        notifMessage.text = ""  ;
        notificationPanel.SetActive(false);
    }

    public void LogOut(){
        auth.SignOut();
        profileUserEmail.text = "";
        profileUserName.text ="";
        OpenLoginPanel();
    }

    public void CreateUser(string email, string password, string Username)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled){
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted){
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error"+ task.Exception);

                foreach (Exception exception in task.Exception.Flatten().InnerExceptions){

                Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;

                    if (firebaseEx != null)
                    {
                        var errorCode = (AuthError)firebaseEx.ErrorCode;
                        showNotificationMessage("Error", GetErrorMessage(errorCode) ) ;
                    }
                }               
                return;
            }

            //firebase user has been created
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})", 
                newUser.DisplayName, newUser.UserId);

            UpdateUserProfile(Username);
        });
    }


    public void SignInUser(string email, string password){

           auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled){
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled");
                return;
            }
            if (task.IsFaulted){
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error"+ task.Exception);

                foreach (Exception exception in task.Exception.Flatten().InnerExceptions){
                Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
                if (firebaseEx != null)
                {
                    var errorCode = (AuthError)firebaseEx.ErrorCode;
                    showNotificationMessage("Error", GetErrorMessage(errorCode) ) ;
                }
                }

                
                return;
            }

            //firebase user has been created
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Signed In successfully: {0} ({1})", 
                newUser.DisplayName, newUser.UserId);
            profileUserName.text = "" + newUser.DisplayName;
            profileUserEmail.text = "" + newUser.Email;





            OpenProfilePanel();
            
        });
    }


    void InitializeFirebase(){
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged; 
        AuthStateChanged(this, null);
    }



    void AuthStateChanged(object sender, System.EventArgs eventArgs){
        if (auth.CurrentUser != user) {
            
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            
            if (!signedIn && user != null){
                Debug.Log("Signed out" + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn){
                Debug.Log("signed in"+ user.UserId);
                isSignIn = true;
            }
        }
    }


    void Destroy (){
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }


    void UpdateUserProfile(string UserName) {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user!= null){
            Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile{
                DisplayName = UserName, 
                PhotoUrl = new System.Uri("https://via.placeholder.com/150"),
            };
            user.UpdateUserProfileAsync(profile).ContinueWith(task => {
                if (task.IsCanceled){
                     Debug.LogError("UpdateUserProfileAsync was canceled.");
                     return;
                }
                if (task.IsFaulted){
                    Debug.LogError("UpdateUserProfileAsync encountered an error:"+ task.Exception);
                    return;
                }
                Debug.Log("user profile updated successfully");
                showNotificationMessage("Alert", "Account Successfully Created");
            });
        }
    }
    
    bool isSigned = false;

    void Update(){

        if (isSignIn){
            if (!isSigned){
                isSigned = true; 
                profileUserName.text = "" + user.DisplayName;
                profileUserEmail.text = "" + user.Email;
                OpenProfilePanel();
            }

        }
    }

    private static string GetErrorMessage(AuthError errorCode)
{
    var message = "";
    switch (errorCode)
    {
        case AuthError.AccountExistsWithDifferentCredentials:
            message = "Account does not exist";
            break;
        case AuthError.MissingPassword:
            message = "Missing Password";
            break;
        case AuthError.WeakPassword:
            message = "Is your password goku? Cause it's weak";
            break;
        case AuthError.WrongPassword:
            message = "Wrong Password, fool";
            break;
        case AuthError.EmailAlreadyInUse:
            message = "Email is already taken";
            break;
        case AuthError.InvalidEmail:
            message = "Invalid Email";
            break;
        case AuthError.MissingEmail:
            message = "Where's your email?";
            break;
        default:
            message = "Invalid Error";
            break;
    }
    return message;
}

   void forgetPasswordSubmit(string forgetPassEmail)
{
    auth.SendPasswordResetEmailAsync(forgetPassEmail).ContinueWithOnMainThread(task =>
    {
        if (task.IsCanceled)
        {
            Debug.LogError("SendPasswordResetEmailAsync was canceled");
        }
        if (task.IsFaulted)
        {
            foreach (Exception exception in task.Exception.Flatten().InnerExceptions)
            {
                Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
                if (firebaseEx != null)
                {
                    var errorCode = (AuthError)firebaseEx.ErrorCode;
                    showNotificationMessage("Error", GetErrorMessage(errorCode));
                }
            }
        }
            showNotificationMessage("Alert", "Successfully sent email for reset password!");

    });
}





 
}
