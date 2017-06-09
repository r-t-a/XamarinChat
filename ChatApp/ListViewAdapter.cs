using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace ChatApp
{
    internal class ListViewAdapter : BaseAdapter
    {
        private MainActivity mainActivity;
        private List<MessageContent> messages;

        public ListViewAdapter(MainActivity mainActivity, List<MessageContent> messages)
        {
            this.mainActivity = mainActivity;
            this.messages = messages;
        }

        public override int Count => messages.Count;

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var inflater = (LayoutInflater)mainActivity.BaseContext.GetSystemService(Context.LayoutInflaterService);
            var itemView = inflater.Inflate(Resource.Layout.List_Item, null);

            TextView message_user, message_time, message_text;
            message_user = itemView.FindViewById<TextView>(Resource.Id.message_user);
            message_time = itemView.FindViewById<TextView>(Resource.Id.message_time);
            message_text = itemView.FindViewById<TextView>(Resource.Id.message_text);

            message_user.Text = messages[position].Email;
            message_time.Text = messages[position].Time;
            message_text.Text = messages[position].Message;

            return itemView;
        }
    }
}