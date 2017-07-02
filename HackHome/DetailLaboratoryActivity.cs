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
    [Activity(Label = "DetailLaboratoryActivity")]
    public class DetailLaboratoryActivity : Activity
    {
        public const string ID_DETAIL_LABORATORY = "IdDetail";
        public const string TOKEN = "token";
        public const string TITTLE = "tittle";
        public const string STATUS = "status";
        public const string USERNAME = "username";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.DetailLaboratory);

            InitComponents(Intent.GetIntExtra(ID_DETAIL_LABORATORY, 0), Intent.GetStringExtra(TOKEN),
                Intent.GetStringExtra(TITTLE), Intent.GetStringExtra(STATUS), Intent.GetStringExtra(USERNAME));
        }

        private async void InitComponents(int idEvidenceDetail, string token, string tittle, string status, string username)
        {
            ServiceClient service = new ServiceClient();
            EvidenceDetail detail = await service.GetEvidenceByIDAsync(token, idEvidenceDetail);

            #region Username
            var usernameView = FindViewById<TextView>(Resource.Id.UserName);
            usernameView.Text = username;
            #endregion

            #region Tittle
            var tittleView = FindViewById<TextView>(Resource.Id.ActivityName);
            tittleView.Text = tittle;
            #endregion

            #region Status
            var statusView = FindViewById<TextView>(Resource.Id.ActivityStatus);
            statusView.Text = status;
            #endregion

            #region Description
            var DescriptionWebView = FindViewById<WebView>(Resource.Id.Descripcion);
            DescriptionWebView.LoadDataWithBaseURL(null, detail.Description, "text/html", "uft-8", null);
            #endregion

            #region Image            
            var ImageEvidence = FindViewById<ImageView>(Resource.Id.ImageEvidence);
            Koush.UrlImageViewHelper.SetUrlDrawable(ImageEvidence, detail.Url);
            #endregion
        }
    }
}