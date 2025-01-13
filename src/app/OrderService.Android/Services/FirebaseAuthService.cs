using Android.Gms.Extensions;
using Firebase.Auth;
using OrderService.Interfaces;
using System.Threading.Tasks;

namespace OrderService.Droid.Services
{
    public class FirebaseAuthService : IFirebaseAuthService
    {
        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                var tokenResult = await user.User.GetIdToken(true).AsAsync<GetTokenResult>();
                return tokenResult.Token;
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return "";
            }
        }

        public async Task<string> LoginWithGoogle(string idToken)
        {
            try
            {
                var credential = GoogleAuthProvider.GetCredential(idToken, null);
                var user = await FirebaseAuth.Instance.SignInWithCredential(credential);
                var tokenResult = await FirebaseAuth.Instance.CurrentUser.GetIdToken(true).AsAsync<GetTokenResult>();
                return tokenResult.Token;
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return "";
            }
        }
    }
}