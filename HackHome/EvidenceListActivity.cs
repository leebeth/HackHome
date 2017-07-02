using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HackAtHome.Entities;
using HackAtHome.CustomAdapters;

namespace HackHome
{
    [Activity(Label = "@string/ApplicationName", Icon = "@drawable/MyIcon")]
    public class EvidenceListActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EvidenceList);
            var Token = Intent.Extras.GetString("token");
            var FullName = Intent.Extras.GetString("fullName");
            var TextViewEmail = FindViewById<TextView>(Resource.Id.textViewFullName);
            TextViewEmail.Text = FullName;
            GetEvidences(Token);
        }

        private async void GetEvidences(string token)
        {
            var ServiceCliente = new HackAtHome.SAL.ServiceClient();
            List<Evidence> Result = await ServiceCliente.GetEvidencesAsync(token);

            var ListViewEvidences = FindViewById<ListView>(Resource.Id.listViewEvidences);
            ListViewEvidences.Adapter = new EvidencesAdapter(this, Result, Resource.Layout.EvidenceItem, Resource.Id.textViewName, Resource.Id.textViewStatus);
        }
    }
}