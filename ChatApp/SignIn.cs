
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Firebase.Auth;

namespace ChatApp
{
    [Activity(Label = "SignIn", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class SignIn : AppCompatActivity, IOnCompleteListener
    {
        private FirebaseAuth auth;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SignIn);

            auth = FirebaseAuth.Instance;

            var edtEmail = FindViewById<EditText>(Resource.Id.email);
            var edtPass = FindViewById<EditText>(Resource.Id.password);
            var signInBtn = FindViewById<Button>(Resource.Id.signInBtn);

            signInBtn.Click += delegate {
                auth.CreateUserWithEmailAndPassword(edtEmail.Text, edtPass.Text)
                    .AddOnCompleteListener(this);
            };
        }

		public void OnComplete(Task task)
		{
			if (task.IsSuccessful)
			{
				Toast.MakeText(this, "Sign In Successful", ToastLength.Short).Show();
				Finish();
			}
			else
			{
				Toast.MakeText(this, "Sign In Failed", ToastLength.Short).Show();
			}
		}
    }
}
