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
using Android.Webkit;
using HackAtHome.SAL;
using HackAtHome.Entities;

namespace HackHome
{
    [Activity(Label = "@string/ApplicationName", Icon = "@drawable/MyIcon")]
    public class DetailLaboratoryActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DetailLaboratory);

            var IdEvidenceDetail = Intent.Extras.GetInt("IdDetail");
            var Token = Intent.Extras.GetString("token");
            var Tittle = Intent.Extras.GetString("tittle");
            var Status = Intent.Extras.GetString("status");
            var Username = Intent.Extras.GetString("username");

            GetEvidenceDetail(IdEvidenceDetail,Token,Title,Status,Username);
        }

        private async void GetEvidenceDetail(int idEvidenceDetail, string token, string tittle, string status, string username)
        {
            ServiceClient service = new ServiceClient();
            EvidenceDetail detail = await service.GetEvidenceByIDAsync(token, idEvidenceDetail);

            var usernameView = FindViewById<TextView>(Resource.Id.UserName);
            usernameView.Text = username;

            var tittleView = FindViewById<TextView>(Resource.Id.ActivityName);
            tittleView.Text = tittle;

            var statusView = FindViewById<TextView>(Resource.Id.ActivityStatus);
            statusView.Text = status;
            
            var DescriptionWebView = FindViewById<WebView>(Resource.Id.Descripcion);
            DescriptionWebView.LoadDataWithBaseURL(null, detail.Description, "text/html", "uft-8", null);
           
            var ImageEvidence = FindViewById<ImageView>(Resource.Id.ImageEvidence);
            Koush.UrlImageViewHelper.SetUrlDrawable(ImageEvidence, detail.Url);            
        }
    }
}