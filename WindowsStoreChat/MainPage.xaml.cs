﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNet.SignalR.Client.Hubs;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsStoreChat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Start();
        }

        private async void Start()
        {
            var hubConnection = new HubConnection("http://localhost:56031/");
            var chatHubProxy = hubConnection.CreateHubProxy("chatHub");

            chatHubProxy.On<string, string>("sendMessage",
                                            (userName, message) => Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Messages.Text += Environment.NewLine + userName + " sier: " + message));

            await hubConnection.Start();

            BtnSend.Click += (sender, args) =>
                {
                    chatHubProxy.Invoke("broadcastMessage", "StoreApp", Message.Text);
                    Message.Text = string.Empty;
                };
        }

        private void SendMessage()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}
