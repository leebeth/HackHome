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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.DetailLaboratory);

            InitComponents(Intent.GetIntExtra(ID_DETAIL_LABORATORY, 0), Intent.GetStringExtra(TOKEN));
        }

        private async void InitComponents(int idEvidenceDetail, string token)
        {
            ServiceClient service = new ServiceClient();
            EvidenceDetail detail = await service.GetEvidenceByIDAsync(token, idEvidenceDetail);
            

            #region Tittle

            #endregion

            #region Status

            #endregion

            #region Description
            var DescriptionWebView = FindViewById<WebView>(Resource.Id.Descripcion);
            string descriptionContent = "";
            DescriptionWebView.LoadDataWithBaseURL(null, descriptionContent, "text/html", "uft-8", null);
            #endregion

            #region Image            
            var ImageEvidence = FindViewById<ImageView>(Resource.Id.ImageEvidence);
            string urlImage = "";
            Koush.UrlImageViewHelper.SetUrlDrawable(ImageEvidence, urlImage);
            #endregion
        }
    }
}