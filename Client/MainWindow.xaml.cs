using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Client.ServiceChat;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IChatServiceCallback
    {
        bool isConnected = false;
        ChatServiceClient client;
        int id;
        public MainWindow()
        {
            InitializeComponent();
        }

        void ConnectUser()
        {
            if (!isConnected)
            {
                client = new ChatServiceClient(new System.ServiceModel.InstanceContext(this));
                id = client.Connect(tbUserName.Text);
                tbUserName.IsEnabled = false;
                btnConDis.Content = "Disconnect";
                isConnected = true;
            }
        }

        void DisconnectUser()
        {
            if (isConnected)
            {
                client.Disconnect(id);
                client = null;
                tbUserName.IsEnabled = true;
                btnConDis.Content = "Connect";
                isConnected = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(isConnected)
            {
                DisconnectUser();
            }
            else
            {
                ConnectUser();
            }
        }

        public void MessageCallback(string message)
        {
            lbChat.Items.Add(message);
            lbChat.ScrollIntoView(lbChat.Items[lbChat.Items.Count-1]);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            client = new ChatServiceClient(new System.ServiceModel.InstanceContext(this));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        private void TbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if (client != null)
                {
                    client.SendMessage(tbMessage.Text, id);
                    tbMessage.Text = string.Empty;
                }
            }
        }
    }
}
