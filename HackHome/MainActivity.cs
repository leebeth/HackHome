using Android.App;
using Android.Widget;
using Android.OS;

namespace HackHome
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/MyIcon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);

        }
    }
}

