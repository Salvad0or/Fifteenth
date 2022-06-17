using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Newtonsoft.Json;
using FifteenthLibrary;
using System.Runtime.CompilerServices;
using MainWindowLibrary.Interface.Base_command;
using MainWindowLibrary.Interface;
using MainWindowLibrary.ViewModel.ViewModels;
using MainWindowLibrary.Services;
using MainWindowLibrary.Models;

namespace MainWindowLibrary
{

    public class View : ViewModels
    {
        #region Поля и свойства


        public static ObservableCollection<Repository> ClientsDataBase { get; }
        public int DataGridIndex { get; set; }

        public Repository SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                if (Equals(_selectedGroup, value)) return;
                _selectedGroup = value;
                OnPropertyChanged();
            }
        }


        #region private Поля
        readonly static string path = @"DataBase.json";
        private string _name;
        private string _surname;
        private int _phone;
        private int _balance;
        private string _passport;
        private string _comment;
        private bool _checkbox;
        private int _senderphonenumber;
        private int _recipeienphonenumber;
        private int _transferamount;
        private BindingList<ActionWindowData> _actionwindow;
        private Repository _selectedGroup;
        private ObservableCollection<Clients> _sender;
        private ObservableCollection<Clients> _recipeien;

        #endregion

        #region Поля и свойства окна добавления клиентов

        public string Name
        {
            get { return _name; }
            set
            {
                if (Equals(_name, value))
                    return;
                _name = value;
                OnPropertyChanged();


            }
        }

        public string SurName
        {
            get { return _surname; }
            set
            {
                if (Equals(_surname, value))
                    return;
                _surname = value;
                OnPropertyChanged();
            }
        }

        public int Phone
        {
            get { return _phone; }
            set
            {
                if (Equals(_phone, value))
                    return;
                _phone = value;
                OnPropertyChanged();
            }
        }

        public int Balance
        {
            get { return _balance; }
            set
            {
                if (Equals(_balance, value))
                    return;
                _balance = value;
                OnPropertyChanged();
            }
        }

        public string Passport
        {
            get { return _passport; }
            set
            {
                if (Equals(_passport, value))
                    return;
                _passport = value;
                OnPropertyChanged();
            }
        }

        public string Comment
        {
            get { return _comment; }
            set
            {
                if (Equals(_comment, value))
                    return;
                _comment = value;
                OnPropertyChanged();
            }
        }

        public bool CheckBox
        {
            get { return _checkbox; }
            set
            {
                if (Equals(_checkbox, value))
                    return;
                _checkbox = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Поля и свойства окна перевода средств

        public int SenderPhoneNumber
        {

            get => _senderphonenumber;
            set
            {
                if (Equals(_senderphonenumber, value)) return;

                _senderphonenumber = value;
                OnPropertyChanged();


            }
        }

        public int RecipeienPhoneNumber
        {
            get => _recipeienphonenumber;
            set
            {
                if (Equals(_recipeienphonenumber, value)) return;

                _recipeienphonenumber = value;
                OnPropertyChanged();


            }
        }

        public int TransferAmount
        {
            get => _transferamount;
            set
            {
                if (_transferamount < 0) _transferamount = 0;
                else _transferamount = value;

                OnPropertyChanged();
            }
        }

        public ObservableCollection<Clients> Sender

        {
            get => _sender;
            set
            {
                if (Equals(_sender, value)) return;
                _sender = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Clients> Recipeien
        {
            get => _recipeien;
            set
            {
                if (Equals(_recipeien, value)) return;
                _recipeien = value;
                OnPropertyChanged();
            }
        }



        #endregion

        #region Поля и свойства окна журнала действий

        public BindingList<ActionWindowData> ActionWindow
        {
            get => _actionwindow;
            set
            {
                if (Equals(_actionwindow, value)) return;

                _actionwindow = value;

                OnPropertyChanged();
            }
        }

        Action<string> ActionWindowDelegate;

        #endregion

        #endregion

        #region Команды

        #region Команда Удаления клиента

        public ICommand DeleteClient { get; }

        public bool CanDeleteCommandExecute(object p) => true;

        private void OnDeleteGroupCommandExecuted(object p)
        {

            if (!(p is Repository rep)) return;
            ActionWindowDelegate?.Invoke($"Счет клиента {rep.DataBase[DataGridIndex].Name} был закрыт");
            rep.DataBase.Remove(rep.DataBase[DataGridIndex]);
        }



        #endregion

        #region Добавление нового клиента
        public ICommand AddsNewClientCommand { get; }

        public bool CanAddsNewClientExecute(object p)
        {
            if (Name == null || SurName == null || Phone < 10000 || Balance < 10000 || Passport == null)
                return false;
            return true;

        }

        public void OnAddsNewClientExecuted(object p)
        {

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < ClientsDataBase[i].DataBase.Count; j++)
                {
                    if (Equals(ClientsDataBase[i].DataBase[j].Phone, Phone))
                    {
                        MessageBox.Show("Такой номер в базе уже присутствует");
                        return;
                    }
                }
            }


            if (CheckBox)
                ClientsDataBase[1].DataBase.Add(new Clients(Name, SurName, Phone, Passport, Balance, Comment));
            else
                ClientsDataBase[0].DataBase.Add(new Clients(Name, SurName, Phone, Passport, Balance, Comment));

            Clear();

        }

        #endregion

        #region Команда поиска отправителя

        public ICommand GetSenderCommand { get; }

        public bool CanGetSenderDataExecuted(object p) => true;


        public void OnGetSenderDataExecute(object p)
        {
            Sender = new ObservableCollection<Clients>();     

            try
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < ClientsDataBase[i].DataBase.Count; j++)
                    {
                        if (SenderPhoneNumber == ClientsDataBase[i].DataBase[j].Phone)
                        {
                            Sender.Add(ClientsDataBase[i].DataBase[j]);
                            return;
                        }
                    }
                }

                throw new CustomException("Клиент не был найден", 1);
            }
            catch (CustomException e)
            {

                MessageBox.Show($"{e.Message}. Код ошибки: {e.ErrorCode}.");
            }

            
        }

        #endregion

        #region Команда поиска получателя

        public ICommand GetRecipientCommand { get; }

        public bool CanGetRecipientDataExecute(object p) => true;


        public void OnGetRecipeientDataExecuted(object p)
        {


            Recipeien = new ObservableCollection<Clients>();


            try
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < ClientsDataBase[i].DataBase.Count; j++)
                    {
                        if (RecipeienPhoneNumber == ClientsDataBase[i].DataBase[j].Phone)
                        {
                            Recipeien.Add(ClientsDataBase[i].DataBase[j]);
                            return;
                        }
                    }
                }

                throw new CustomException("Клиент не найден",1);
            }
            catch (CustomException e) when (e.ErrorCode == 1)
            {

                MessageBox.Show($"{e.Message}. Код ошибки: {e.ErrorCode}.");
            }
           
        }

        #endregion

        #region Команда перевода средств

        public ICommand TransferMoneyCommand { get; }

        public bool CanTranferMoneyExecuted(object p)
        {

            string sender = string.Empty;
            string recipeien = string.Empty;

            for (int i = 0; i < Sender.Count; i++)
            {
                sender = Sender[i].Name;
            }
            for (int i = 0; i < Recipeien.Count; i++)
            {
                recipeien = Recipeien[i].Name;
            }

            if (Equals(sender, recipeien) || Sender.Count == 0 || Recipeien.Count == 0)
            {
                return false;
            }

            return true;
        }

        public void OnTransferMoneyExecuted(object p)
        {

            if (Sender[0].Balance - TransferAmount < 0)
            {
                MessageBox.Show($"У отправителя {Sender[0].Name} недостаточно средств");
                return;
            }

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < ClientsDataBase[i].DataBase.Count; j++)
                {
                    if (ClientsDataBase[i].DataBase[j].Phone == Sender[0].Phone)
                    {
                        ClientsDataBase[i].DataBase[j].Balance -= TransferAmount;
                        DataWorker.SaveData(ClientsDataBase);
                    }

                    if (ClientsDataBase[i].DataBase[j].Phone == Recipeien[0].Phone)
                    {
                        ClientsDataBase[i].DataBase[j].Balance += TransferAmount;
                        DataWorker.SaveData(ClientsDataBase);
                    }
                }
            }

            ActionWindowDelegate?.Invoke(
                $"Был соверешен перевод средств от клиента {Sender[0].Name} клиенту {Recipeien[0].Name} " +
                $"в размере {TransferAmount}\n" +
                $"Баланс отправителя {Sender[0].Balance}\n" +
                $"Баланс получателя {Recipeien[0].Balance}");

            Sender = new ObservableCollection<Clients>();
            Recipeien = new ObservableCollection<Clients>();
            SenderPhoneNumber = default;
            RecipeienPhoneNumber = default;
            TransferAmount = default;
        }

        #endregion

        #region Команда меню. О программе. Здесь используется библиотека

        public ICommand AboutProgramCommand { get; }

        public bool CanTellAboutProgramExecuted(object p) => true;

        public void OnTellAboutProgramExecute(object p) => AboutProgram.TellAboutProgram();


        #endregion

        #endregion

        #region События удаления, добавления клиентов

        /// <summary>
        /// Standat's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void DataBase_CollectionChanged1(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            int index = (sender as ObservableCollection<Clients>).Count - 1;

            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    DataWorker.SaveData(ClientsDataBase);
                    ActionWindowDelegate?.Invoke($"Был добавлен клиент{(sender as ObservableCollection<Clients>)[index].Name}");
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    DataWorker.SaveData(ClientsDataBase);
                    break;
            }
        }
        /// <summary>
        /// VIP's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataBase_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            int index = (sender as ObservableCollection<Clients>).Count - 1;

            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    DataWorker.SaveData(ClientsDataBase);
                    ActionWindowDelegate?.Invoke($"Был добавлен клиент{(sender as ObservableCollection<Clients>)[index].Name}");
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    DataWorker.SaveData(ClientsDataBase);
                    break;
            }
        }

        #endregion

        #region Конструкторы
        public View()
        {
            Sender = new ObservableCollection<Clients>();
            Recipeien = new ObservableCollection<Clients>();
            ActionWindow = new BindingList<ActionWindowData>();
            ActionWindowDelegate = HeWriteToJournal;

            DeleteClient = new LamdaCommand(OnDeleteGroupCommandExecuted, CanDeleteCommandExecute);
            AddsNewClientCommand = new LamdaCommand(OnAddsNewClientExecuted, CanAddsNewClientExecute);
            GetSenderCommand = new LamdaCommand(OnGetSenderDataExecute, CanGetSenderDataExecuted);
            GetRecipientCommand = new LamdaCommand(OnGetRecipeientDataExecuted, CanGetRecipientDataExecute);
            TransferMoneyCommand = new LamdaCommand(OnTransferMoneyExecuted, CanTranferMoneyExecuted);
            AboutProgramCommand = new LamdaCommand(OnTellAboutProgramExecute, CanTellAboutProgramExecuted);

            ClientsDataBase[0].DataBase.CollectionChanged += DataBase_CollectionChanged;
            ClientsDataBase[1].DataBase.CollectionChanged += DataBase_CollectionChanged1;


        }
        static View()
        {
            ClientsDataBase = JsonConvert.DeserializeObject<ObservableCollection<Repository>>(File.ReadAllText(path));
        }
        #endregion

        #region Вспомогательные методы

        #region Метод очистки панелей
        public void Clear()
        {
            Name = default;
            SurName = default;
            Phone = default;
            Balance = default;
            CheckBox = default;
            Comment = default;

        }


        #endregion

        #region Метод добавления действий в журнал
        public void HeWriteToJournal(string message)
        {
            MessageBox.Show(message);

            ActionWindow.Add(new ActionWindowData
            {
                Text = message,
                Date = DateTime.Now.ToShortDateString()
            });
        }
        #endregion


        #endregion


    }
     
}
