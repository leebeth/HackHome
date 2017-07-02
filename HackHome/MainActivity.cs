using Android.App;
using Android.Widget;
using Android.OS;
using HackAtHome.SAL;
using HackAtHome.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Android.Content;

namespace HackHome
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/MyIcon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            var ButtonValidate = FindViewById<Button>(Resource.Id.buttonValidate);
            ButtonValidate.Click += (sender, e) =>
            {
                var studentEmail = FindViewById<EditText>(Resource.Id.editTextEmail).Text;
                var Passwd = FindViewById<EditText>(Resource.Id.editTextPassword).Text;
                Validate(studentEmail, Passwd);

            };
        }
        private async void Validate(string studentEmail, string password)
        {
            var ServiceCliente = new ServiceClient();
            ResultInfo Result = await ServiceCliente.AutenticateAsync(studentEmail, password);
            var Token = Result.Token;
            var FullName = Result.FullName;
            Intent Intent = new Intent(this, typeof(EvidenceListActivity));
            Intent.PutExtra("token", Token);
            Intent.PutExtra("fullName", FullName);
            StartActivity(Intent);
        }


    }
}

