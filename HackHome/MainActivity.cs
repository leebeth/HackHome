using Android.App;
using Android.Widget;
using Android.OS;
using HackAtHome.SAL;
using HackAtHome.Entities;

namespace HackHome
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/MyIcon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);
            var ButtonValidate = FindViewById<Button>(Resource.Id.buttonValidate);
            ButtonValidate.Click += (sender, e) =>
            {
                var studentEmail = FindViewById<EditText>(Resource.Id.editTextEmail).Text;
                var Passwd = FindViewById<EditText>(Resource.Id.editTextPassword).Text;
                string Token = Validate(studentEmail, Passwd);

            };
        }
        private async string Validate(string studentEmail, string password)
        {
            var ServiceCliente = new ServiceClient();            
            ResultInfo Result = await ServiceCliente.AutenticateAsync(studentEmail,password);
            return Result.Token;
        }
    }
}

