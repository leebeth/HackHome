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
        public const string ID_DETAIL_LABORATORY = "IdDetail";
        public const string TOKEN = "token";
        public const string TITTLE = "tittle";
        public const string STATUS = "status";
        public const string USERNAME = "username";

        Complex Data;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EvidenceList);
            var Token = Intent.Extras.GetString("token");
            var FullName = Intent.Extras.GetString("fullName");
            var TextViewEmail = FindViewById<TextView>(Resource.Id.textViewFullName);
            TextViewEmail.Text = FullName;

            Data = (Complex)this.FragmentManager.FindFragmentByTag("FragmentData");
            if (Data == null)
            {
                Data = new Complex();
                var FragmentTransaction = this.FragmentManager.BeginTransaction();
                FragmentTransaction.Add(Data, "FragmentData");
                FragmentTransaction.Commit();
            }
            if (Data.Evidences == null || Data.Evidences.Count == 0)
            {
                Android.Util.Log.Debug("lab11log", "Activity A - GetEvidences");
                GetEvidences(Token, FullName);
            }
            else
            {
                Android.Util.Log.Debug("lab11log", "Activity A - buildEvidence");
                buildEvidence(Data.Evidences, Token, FullName);
            }
        }

        private async void GetEvidences(string token, string fullName)
        {
            var ServiceCliente = new HackAtHome.SAL.ServiceClient();
            List<Evidence> Result = await ServiceCliente.GetEvidencesAsync(token);
            Data.Evidences = Result;
            buildEvidence(Result,token,fullName);            
        }

        private void buildEvidence(List<Evidence> result, string token, string fullName)
        {
            var ListViewEvidences = FindViewById<ListView>(Resource.Id.listViewEvidences);
            ListViewEvidences.Adapter = new EvidencesAdapter(this, result, Resource.Layout.EvidenceItem, Resource.Id.textViewName, Resource.Id.textViewStatus);
            ListViewEvidences.ItemClick += (sender, e) =>
            {
                var Index = e.Position;
                Evidence evidence = result[Index];
                Intent Intent = new Intent(this, typeof(DetailLaboratoryActivity));
                Intent.PutExtra(ID_DETAIL_LABORATORY, evidence.EvidenceID);
                Intent.PutExtra(TITTLE, evidence.Title);
                Intent.PutExtra(STATUS, evidence.Status);
                Intent.PutExtra(TOKEN, token);
                Intent.PutExtra(USERNAME, fullName);
                StartActivity(Intent);
            };
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            Android.Util.Log.Debug("lab11log", "Activity A - OnSaveInstanceState");
            base.OnSaveInstanceState(outState);
        }
    }
}