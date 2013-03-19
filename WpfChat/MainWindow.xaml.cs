using System;
using System.Windows.Threading;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace WpfChat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Start();
        }

        private async void Start()
        {
            var hubConnection = new HubConnection("http://localhost:56031/");
            var chatHubProxy = hubConnection.CreateHubProxy("chatHub");

            chatHubProxy.On<string, string>("sendMessage",
                                            (userName, message) =>
                                            Dispatcher.Invoke(DispatcherPriority.Normal,
                                                              new Action<string, string>(SendMessage), userName, message));

            await hubConnection.Start();

            BtnSend.Click += (sender, args) =>
                {
                    chatHubProxy.Invoke("broadcastMessage", "WPF", Message.Text);
                    Message.Text = string.Empty;
                };
        }

        private void SendMessage(string userName, string message)
        {
            Messages.Text += Environment.NewLine + userName + " sier: " + message;
        }
    }
}
