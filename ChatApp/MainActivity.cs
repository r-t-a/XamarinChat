using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Firebase.Xamarin.Database;
using System.Collections.Generic;
using com.refractored.fab;
using Firebase.Database;
using System;
using Firebase.Auth;
using Firebase;

namespace ChatApp
{
    [Activity(Label = "ChatApp", MainLauncher = true, Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class MainActivity : AppCompatActivity, IValueEventListener
    {
        private FirebaseClient firebase;
        private List<MessageContent> messages = new List<MessageContent>();
        private ListView listChat;
        private EditText edtChat;
        private FloatingActionButton fab;

        public int ResultCode = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            //FirebaseApp.InitializeApp(this);

			firebase = new FirebaseClient(GetString(Resource.String.firebase_url));
            FirebaseDatabase.Instance.GetReference("chat").AddValueEventListener(this);

            fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            edtChat = FindViewById<EditText>(Resource.Id.input);
            listChat = FindViewById<ListView>(Resource.Id.messages);

            fab.Click += delegate {
                SendMessage();
            };

            if(FirebaseAuth.Instance.CurrentUser == null) {
                StartActivityForResult(new Android.Content.Intent(this, typeof(SignIn)), ResultCode);
            } else {
                Toast.MakeText(this, $"Welcome {FirebaseAuth.Instance.CurrentUser.Email}", ToastLength.Short).Show();
                DisplayMessage();
            }
        }

        private async void SendMessage()
        {
            var items = await firebase.Child("chat").PostAsync(new MessageContent(FirebaseAuth.Instance.CurrentUser.Email, edtChat.Text));
            edtChat.Text = "";
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }

		public void OnCancelled(DatabaseError error)
		{
		}

		public void OnDataChange(DataSnapshot snapshot)
		{
            DisplayMessage();
		}

        private async void DisplayMessage()
        {
            messages.Clear();
            var items = await firebase.Child("chat").OnceAsync<MessageContent>();
            foreach(var item in items) {
                messages.Add(item.Object);
            }
            var adapter = new ListViewAdapter(this, messages);
            listChat.Adapter = adapter;
        }
    }
}

